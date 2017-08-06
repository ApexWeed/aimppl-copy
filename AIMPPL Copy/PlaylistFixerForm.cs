using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AIMPPL_Copy.DB;
using AIMPPL_Copy.PlaylistTree;
using Apex.Extensions;
using Apex.Translation;

namespace AIMPPL_Copy
{
    public partial class PlaylistFixerForm : Form
    {
        public enum FixType
        {
            Playlist,
            Database
        }

        private LanguageManager _lm;
        private MainForm _parent;
        private List<Playlist> _playlists = new List<Playlist>();
        private Dictionary<Song, List<PlaylistNode>> _songMap;

        private string _connectionString;
        private FilesTable _filesTable;
        private HashTable _artistsTable;
        private HashTable _albumsTable;
        private HashTable _formatsTable;
        private Dictionary<string, string> _artists;
        private Dictionary<string, string> _albums;
        private Dictionary<int, string> _formats;
        private List<Song> _songs;

        public FixType CurrentType { get; private set; }

        public PlaylistFixerForm(LanguageManager languageManager, Playlist playlist, MainForm parent)
            : this(languageManager, new List<Playlist>(new[] { playlist }), parent)
        {
        }

        public PlaylistFixerForm(LanguageManager languageManager, List<Playlist> playlists, MainForm parent)
            : this(languageManager, parent, FixType.Playlist)
        {
            uiTitle.TranslationString = "TITLE.FIX_PLAYLIST";

            _playlists = playlists;

            _songMap = new Dictionary<Song, List<PlaylistNode>>();
        }

        public PlaylistFixerForm(LanguageManager languageManger, MainForm parent)
            : this(languageManger, parent, FixType.Database)
        {
            uiTitle.TranslationString = "TITLE.FIX_DB";
            LoadDB();
        }

        private PlaylistFixerForm(LanguageManager languageManager, MainForm parent, FixType type)
        {
            InitializeComponent();

            uiBottom.SplitWidth();
            uiScanFilenames.Checked = Properties.Settings.Default.ScanFilenames;
            uiScanCues.Checked = Properties.Settings.Default.ScanCues;
            uiScanTags.Checked = Properties.Settings.Default.ScanTags;

            _lm = languageManager;
            _lm.AddAllControls(this);

            _parent = parent;

            CurrentType = type;
        }

        private void PlaylistFixerForm_Load(object sender, EventArgs e)
        {
            foreach (var playlist in _playlists)
                LoadPlaylist(playlist);
        }

        private void LoadPlaylist(Playlist playlist)
        {
            Util.FindMissing(playlist, out var missingSongs, out var formatChanges);

            PlaylistNode node = null;

            if (missingSongs.Count + formatChanges.Count > 0)
                node = uiTree.AddPlaylist(playlist, missingSongs, formatChanges);

            if (node == null)
                return;

            foreach (var song in missingSongs)
                if (_songMap.ContainsKey(song))
                    _songMap[song].AddDistinct(node);
                else
                    _songMap[song] = new List<PlaylistNode> { node };

            foreach (var formatChange in formatChanges)
                if (_songMap.ContainsKey(formatChange.Song))
                    _songMap[formatChange.Song].AddDistinct(node);
                else
                    _songMap[formatChange.Song] = new List<PlaylistNode> { node };

            UpdateStatus();
        }

        private string GetArtist(string ids)
        {
            if (_artistsTable is null || ids == string.Empty)
                return string.Empty;

            return string.Join(";", ids.Split(';').Select(x => _artists[x]));
        }

        private string GetAlbum(string ids)
        {
            if (_albumsTable is null || ids == string.Empty)
                return string.Empty;

            return string.Join(";", ids.Split(';').Select(x => _albums[x]));
        }

        private bool FormatChanged(string format, string path)
        {
            var parts = path.Split(':');
            if (parts.Length > 1 && parts[parts.Length - 2].EndsWith(".cue"))
                return false;
            return !format.Equals(Path.GetExtension(path).Substring(1), StringComparison.OrdinalIgnoreCase);
        }

        private void UpdateStatus()
        {
            var checkedCount = uiTree.GetPlaylistNodes().SelectMany(n => n.Nodes).Count(n => n.IsChecked);
            var count = uiTree.GetPlaylistNodes().Select(n => n.Nodes.Count).Sum();
            uiStatus.Text = $"Checked: {checkedCount}/{count}";
        }

        public void LoadSinglePlaylist(Playlist playlist)
        {
            uiTree.Clear();
            _playlists.Clear();
            _playlists.Add(playlist);

            LoadPlaylist(playlist);
        }

        public void LoadPlaylists(List<Playlist> playlists)
        {
            uiTree.Clear();
            _playlists = playlists;

            foreach (var playlist in playlists)
                LoadPlaylist(playlist);
        }

        public void LoadDB()
        {
            uiTree.Clear();

            _connectionString = $"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"AIMP\AudioLibrary\Local.db")}";
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM TableArtists;";
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        _artistsTable = new HashTable("TableArtists");
                        _artistsTable.Load(reader);
                        _artists = new Dictionary<string, string>();
                        foreach (var artist in _artistsTable)
                            _artists.Add(artist.Id.ToString(), artist.Value);
                    }
                    command.CommandText = "SELECT * FROM TableAlbums;";
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        _albumsTable = new HashTable("TableAlbums");
                        _albumsTable.Load(reader);
                        _albums = new Dictionary<string, string>();
                        foreach (var album in _albumsTable)
                            _albums.Add(album.Id.ToString(), album.Value);
                    }
                    command.CommandText = "SELECT * FROM TableFormats;";
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        _formatsTable = new HashTable("TableFormats");
                        _formatsTable.Load(reader);
                        _formats = new Dictionary<int, string>();
                        foreach (var format in _formatsTable)
                            _formats.Add(format.Id, format.Value);
                    }
                    command.CommandText = "SELECT * FROM TableFiles;";
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        _filesTable = new FilesTable();
                        _filesTable.Load(reader);
                    }
                }
            }

            _songs = new List<Song>();
            _songs.AddRange(
                from file in _filesTable
                select new DummySong(file.FilePath, file.Title, GetArtist(file.ArtistId), GetAlbum(file.AlbumId), file));

            Util.FindMissing(_songs, out var missingSongs, out var formatChanges);

            if (missingSongs.Any() || formatChanges.Any())
                uiTree.AddPlaylist(new DummyPlaylist("Database"), missingSongs, formatChanges);
        }

        private void PlaylistFixerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _parent.ChildClosed(this);
        }

        private void PlaylistFixerForm_SizeChanged(object sender, EventArgs e)
        {
            uiBottom.SplitWidth();
        }

        private void uiTree_DestinationClicked(object sender, PlaylistTreeControl.DestinationClickedEventArgs e)
        {
            var path = e.Node.DestinationFilename;
            if (string.IsNullOrWhiteSpace(path))
            {
                FileDialogue.FileName = "";
                FileDialogue.InitialDirectory = Path.GetDirectoryName(e.Node.SourceFilename);
            }
            else
            {
                FileDialogue.FileName = path;
                FileDialogue.InitialDirectory = Path.GetDirectoryName(path);
            }

            if (FileDialogue.ShowDialog() == DialogResult.OK)
            {
                foreach (var playlistNode in _songMap[e.Node.Song])
                {
                    var node = playlistNode.Nodes.Cast<SongNode>().FirstOrDefault(n => n.Song == e.Node.Song);
                    if (node == null)
                        continue;
                    node.CheckState = CheckState.Checked;
                    node.DestinationFilename = FileDialogue.FileName;
                }
            }
        }

        private void uiApply_Click(object sender, EventArgs e)
        {
            switch (CurrentType)
            {
                case FixType.Playlist:
                {
                    ApplyPlaylist();
                    break;
                }
                case FixType.Database:
                {
                    ApplyDatabase();
                    break;
                }
            }
            UpdateStatus();
        }

        private void ApplyPlaylist()
        {
            foreach (var playlistNode in uiTree.GetPlaylistNodes())
            {
                var playlist = playlistNode.Playlist;
                for (var i = 0; i < playlistNode.Nodes.Count; i++)
                {
                    var child = playlistNode.Nodes[i];

                    if (child.IsChecked)
                    {
                        var song = (child as SongNode).Song;
                        var newPath = (child as SongNode).DestinationFilename;
                        if (File.Exists(newPath))
                        {
                            song.Path = newPath;
                        }
                        else
                        {
                            if (File.Exists(newPath.Substring(0, newPath.LastIndexOf(':'))))
                                song.Path = newPath;
                        }
                        playlistNode.Nodes.RemoveAt(i);
                        i--;
                    }
                }
                if (playlistNode.Nodes.Count == 0)
                    uiTree.RemovePlaylist(playlistNode);

                playlist.Save();
            }
        }

        private void ApplyDatabase()
        {
            var dbNode = uiTree.GetPlaylistNodes()[0];
            var toRemove = new List<SongNode>();
            foreach (var songNode in dbNode.Nodes.Where(n => n.IsChecked).Cast<SongNode>())
            {
                var song = songNode.Song as DummySong;
                song.Row.FilePath = songNode.DestinationFilename;
                toRemove.Add(songNode);
                if (FormatChanged(_formats[song.Row.FileFormat], song.Row.FilePath))
                    song.Row.FileFormat = _formats.First(n => n.Value.Equals(Path.GetExtension(song.Row.FileName).Substring(1),
                        StringComparison.OrdinalIgnoreCase)).Key;
            }

            foreach (var node in toRemove)
                dbNode.Nodes.Remove(node);

            uiStatus.Text = "Writing changes";

            File.Copy($"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"AIMP\AudioLibrary\Local.db")}", $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $@"AIMP\AudioLibrary\Local_{DateTime.Now:yyyy-MM-dd HH-mm-ss}.db")}");
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                _filesTable.Commit(conn);
            }
        }

        private void SearchProgressCallback(Util.SearchProgress progress)
        {
            uiStatus.Text = $"Search {progress.Status}: {progress.FoundSongs}/{progress.TotalSongs}";
        }

        private async void uiSearch_Click(object sender, EventArgs e)
        {
            if (DirectoryDialogue.ShowDialog() != DialogResult.OK)
                return;

            uiBottom.Enabled = false;

            switch (CurrentType)
            {
                case FixType.Playlist:
                {
                    await PlaylistSearch();
                    break;
                }
                case FixType.Database:
                {
                    await DatabaseSearch();
                    break;
                }
            }

            uiBottom.Enabled = true;

            UpdateStatus();
        }

        private async Task PlaylistSearch()
        {
            var foundSongs = new List<(Song song, string file)>();
            var playlistNodes = uiTree.GetPlaylistNodes();
            var songs = playlistNodes.SelectMany(n => n.Nodes).Where(n => !n.IsChecked).Cast<SongNode>().Select(n => n.Song).Distinct().ToList();
            await Task.Run(() => foundSongs = Util.SearchSongs(songs, DirectoryDialogue.SelectedPath,
                uiScanFilenames.Checked, uiScanTags.Checked, uiScanCues.Checked, SearchProgressCallback));

            foreach (var find in foundSongs)
            {
                foreach (var playlistNode in _songMap[find.song])
                {
                    var node = playlistNode.Nodes.Cast<SongNode>().FirstOrDefault(n => n.Song == find.song);
                    if (node == null)
                        continue;
                    node.CheckState = CheckState.Checked;
                    node.DestinationFilename = find.file;
                }
            }
        }

        private async Task DatabaseSearch()
        {
            var foundSongs = new List<(Song song, string file)>();
            var dbNode = uiTree.GetPlaylistNodes()[0];
            var songs = dbNode.Nodes.Where(n => !n.IsChecked).Cast<SongNode>().Select(n => n.Song).Distinct().ToList();
            await Task.Run(() => foundSongs = Util.SearchSongs(songs, DirectoryDialogue.SelectedPath,
                uiScanFilenames.Checked, uiScanTags.Checked, uiScanCues.Checked, SearchProgressCallback));

            foreach (var find in foundSongs)
            {
                var nodes = dbNode.Nodes.Cast<SongNode>().Where(n => n.Song == find.song);
                foreach (var node in nodes)
                {
                    node.CheckState = CheckState.Checked;
                    node.DestinationFilename = find.file;
                }
            }
        }

        private void uiScanTags_Click(object sender, EventArgs e)
        {
            uiScanTags.Checked = MessageBox.Show(_lm.GetString("FIX_PLAYLIST.MESSAGE.SCAN_TAGS"),
                                               _lm.GetString("FIX_PLAYLIST.LABEL.SCAN_TAGS"), MessageBoxButtons.YesNo) == DialogResult.Yes;
        }
    }
}
