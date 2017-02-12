using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Apex.Translation;
using Aga.Controls.Tree;
using System.IO;
using AIMPPL_Copy.PlaylistTree;

namespace AIMPPL_Copy
{
    public partial class BulkPlaylistFixerForm : Form
    {
        private LanguageManager LM;
        private List<Playlist> playlists;
        new private MainForm Parent;

        public BulkPlaylistFixerForm(LanguageManager LanguageManager, Playlist Playlist, MainForm Parent)
        : this(LanguageManager, new List<Playlist>(new Playlist[] { Playlist }), Parent)
        { }

        public BulkPlaylistFixerForm(LanguageManager LanguageManager, List<Playlist> Playlists, MainForm Parent)
        {
            InitializeComponent();

            LM = LanguageManager;
            LM.AddControls(this.Controls);

            playlists = Playlists;

            this.Parent = Parent;
        }

        private void BulkPlaylistFixerForm_Load(object sender, EventArgs e)
        {
            foreach (var playlist in playlists)
            {
                LoadPlaylist(playlist);
            }
        }
        
        private void LoadPlaylist(Playlist Playlist)
        {
            var missing = new List<Song>();
            var formatChanged = new List<FormatChange>();

            Util.FindMissing(Playlist, out missing, out formatChanged);

            if (missing.Count + formatChanged.Count > 0)
            {
                ptcTree.AddPlaylist(Playlist, missing, formatChanged);
            }
        }

        public void LoadSinglePlaylist(Playlist Playlist)
        {
            ptcTree.Clear();
            playlists.Clear();
            playlists.Add(Playlist);

            LoadPlaylist(Playlist);
        }

        public void LoadPlaylists(List<Playlist> Playlists)
        {
            ptcTree.Clear();
            playlists = Playlists;

            foreach (var playlist in Playlists)
            {
                LoadPlaylist(playlist);
            }
        }

        private void BulkPlaylistFixerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Parent.ChildClosed(this);
        }

        private void BulkPlaylistFixerForm_SizeChanged(object sender, EventArgs e)
        {
            pnlBottomLeft.Width = pnlBottom.Width / 3;
            pnlBottomRightLeft.Width = pnlBottomRight.Width / 2;
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
                for (int i = 0; i < playlistNode.Nodes.Count; i++)
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
                            {
                                song.Path = newPath;
                            }
                        }
                        playlistNode.Nodes.RemoveAt(i);
                        i--;
                    }
                }
                if (playlistNode.Nodes.Count == 0)
                {
                    ptcTree.RemovePlaylist(playlistNode);
                }

                playlist.Save();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (DirectoryDialogue.ShowDialog() == DialogResult.OK)
            {
                foreach (var playlistNode in ptcTree.GetPlaylistNodes())
                {
                    var songs = playlistNode.Nodes.Where((x) => !x.IsChecked).Cast<SongNode>().Select((x) => x.Song).ToList();
                    if (songs.Count == 0)
                    {
                        continue;
                    }
                    var foundSongs = Util.SearchSongs(songs, DirectoryDialogue.SelectedPath, chkScanTags.Checked, chkScanCue.Checked);

                    foreach(var song in foundSongs)
                    {
                        var node = playlistNode.Nodes.Cast<SongNode>().First((x) => x.Song == song.Item1) as SongNode;
                        node.DestinationFilename = song.Item2;
                        node.IsChecked = true;
                    }
                }
            }
        }

        private void chkScanTags_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(LM.GetString("FIX_PLAYLIST.MESSAGE.SCAN_TAGS"), LM.GetString("FIX_PLAYLIST.LABEL.SCAN_TAGS"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                (sender as CheckBox).Checked = true;
            }
            else
            {
                (sender as CheckBox).Checked = false;
            }
        }
    }
}
