using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AIMPPL_Copy.PlaylistTree;
using Apex.Translation;

namespace AIMPPL_Copy
{
    public partial class PlaylistFixerForm : Form
    {
        private readonly LanguageManager _lm;
        private readonly MainForm _parent;
        private List<Playlist> _playlists;

        public PlaylistFixerForm(LanguageManager languageManager, Playlist playlist, MainForm parent)
            : this(languageManager, new List<Playlist>(new[] {playlist}), parent)
        {
        }

        public PlaylistFixerForm(LanguageManager languageManager, List<Playlist> playlists, MainForm parent)
        {
            InitializeComponent();

            _lm = languageManager;
            _lm.AddAllControls(this);

            _playlists = playlists;

            _parent = parent;
        }

        private void BulkPlaylistFixerForm_Load(object sender, EventArgs e)
        {
            foreach (var playlist in _playlists)
                LoadPlaylist(playlist);
        }

        private void LoadPlaylist(Playlist playlist)
        {
            Util.FindMissing(playlist, out var missing, out var formatChanged);

            if (missing.Count + formatChanged.Count > 0)
                ptcTree.AddPlaylist(playlist, missing, formatChanged);
        }

        public void LoadSinglePlaylist(Playlist playlist)
        {
            ptcTree.Clear();
            _playlists.Clear();
            _playlists.Add(playlist);

            LoadPlaylist(playlist);
        }

        public void LoadPlaylists(List<Playlist> playlists)
        {
            ptcTree.Clear();
            _playlists = playlists;

            foreach (var playlist in playlists)
                LoadPlaylist(playlist);
        }

        private void BulkPlaylistFixerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _parent.ChildClosed(this);
        }

        private void BulkPlaylistFixerForm_SizeChanged(object sender, EventArgs e)
        {
            // I'm not proud of this one.
            pnlBottomLeft.Width = pnlBottom.Width / 4;
            pnlBottomRightLeft.Width = pnlBottomRight.Width / 3;
            pnlBottomRightRightLeft.Width = pnlBottomRightRight.Width / 2;
        }

        private void ptcTree_DestinationClicked(object sender, PlaylistTreeControl.DestinationClickedEventArgs e)
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
                e.Node.DestinationFilename = FileDialogue.FileName;
                e.Node.IsChecked = true;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            foreach (var playlistNode in ptcTree.GetPlaylistNodes())
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
                    ptcTree.RemovePlaylist(playlistNode);

                playlist.Save();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (DirectoryDialogue.ShowDialog() != DialogResult.OK)
                return;

            foreach (var playlistNode in ptcTree.GetPlaylistNodes())
            {
                var songs = playlistNode.Nodes.Where(x => !x.IsChecked)
                    .Cast<SongNode>()
                    .Select(x => x.Song)
                    .ToList();
                if (songs.Count == 0)
                    continue;
                var foundSongs = Util.SearchSongs(songs, DirectoryDialogue.SelectedPath, chkScanTags.Checked,
                    chkScanCue.Checked);

                foreach (var song in foundSongs)
                {
                    var node = playlistNode.Nodes.Cast<SongNode>().First(x => x.Song == song.Item1);
                    node.DestinationFilename = song.Item2;
                    node.IsChecked = true;
                }
            }
        }

        private void chkScanTags_Click(object sender, EventArgs e)
        {
            (sender as CheckBox).Checked = MessageBox.Show(_lm.GetString("FIX_PLAYLIST.MESSAGE.SCAN_TAGS"),
                                               _lm.GetString("FIX_PLAYLIST.LABEL.SCAN_TAGS"), MessageBoxButtons.YesNo) == DialogResult.Yes;
        }
    }
}
