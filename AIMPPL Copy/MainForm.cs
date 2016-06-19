using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Apex;
using Apex.Prompt;

namespace AIMPPL_Copy
{
    public partial class MainForm : Form
    {
        private Dictionary<string, int> CoverMap;

        public MainForm()
        {
            InitializeComponent();

            CoverMap = new Dictionary<string, int>();

            if (Properties.Settings.Default.PlaylistPath == "<default>")
            {
                var prompt = new FolderPicker("AIMP Playlist Folder", "Select the AIMP playlist folder.", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AIMP3\\PLS"));
                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.PlaylistPath = prompt.ChosenFolder;
                }
                else
                {
                    MessageBox.Show("I'll try again later.");
                    this.Close();
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (var file in Directory.GetFiles(Properties.Settings.Default.PlaylistPath, "*.aimppl"))
            {
                var playlist = new Playlist(file);
                lstPlaylists.Items.Add(playlist);
            }
        }

        private void lstPlaylists_SelectedIndexChanged(object sender, EventArgs e)
        {
            var playlist = lstPlaylists.SelectedItem as Playlist;
            lblGroups.Parameters = new object[] { playlist.Groups.Count };
            lblGroups.Text = $"Groups: {playlist.Groups.Count}";
            lblSongCount.Parameters = new object[] { playlist.SongCount };
            lblSongCount.Text = $"Songs: {playlist.SongCount}";
            lblSize.Text = $"Size: {Formatting.FormatBytes(playlist.Size)}";
            lblDuration.Text = $"Duration: {TimeSpan.FromMilliseconds(playlist.Duration).ToString("dd\\:hh\\:mm\\:ss")}";
            lblCoverSize.Text = $"Cover Size: {Formatting.FormatBytes(playlist.CoverSize)}";
            lblScanSize.Text = $"Scan Size: {Formatting.FormatBytes(playlist.ScanSize)}";

            treSongs.SuspendLayout();
            treSongs.Nodes.Clear();
            foreach (var group in playlist.Groups)
            {
                var groupNode = new TreeNode(group.ToString());
                groupNode.Tag = group;
                foreach (var song in group.Songs)
                {
                    var songNode = new TreeNode(song.ToString());
                    songNode.Tag = song;
                    songNode.ImageIndex = -1;
                    groupNode.Nodes.Add(songNode);
                }
                groupNode.ImageIndex = GetCover(group);
                treSongs.Nodes.Add(groupNode);
            }
            treSongs.ResumeLayout();
        }

        private int GetCover(Group Group)
        {
            var path = Group.Cover.Path;
            if (string.IsNullOrWhiteSpace(path))
            {
                // Default ? image.
                return 1;
            }

            if (CoverMap.ContainsKey(path))
            {
                return CoverMap[path];
            }
            else
            {
                var img = Image.FromFile(path);
                var small = Utilities.ResizeImage(img, 16, 16);
                CoverMap.Add(path, imlCovers.Images.Count);
                imlCovers.Images.Add(small);
                img.Dispose();
                return imlCovers.Images.Count - 1;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void treSongs_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node;

            Group group;
            Song song;
            if (node.Tag is Group)
            {
                group = node.Tag as Group;
                song = node.Nodes[0].Tag as Song;
            }
            else if (node.Tag is Song)
            {
                song = node.Tag as Song;
                group = node.Parent.Tag as Group;
            }
            else
            {
                return;
            }

            lblSongAlbum.Text = $"Album: {song.Album}";
            lblSongArtist.Text = $"Artist: {song.Artist}";
            lblSongSize.Text = $"Size: {Formatting.FormatBytes(song.Size)}";
            lblSongDuration.Text = $"Duration: {TimeSpan.FromMilliseconds(song.Duration).ToString("dd\\:hh\\:mm\\:ss")}";
            lblSongTitle.Text = $"Title: {song.Title}";
            lblSongTrackNo.Text = $"Track: {song.TrackNo}";

            lblGroupCoverSize.Text = $"Cover Size: {Formatting.FormatBytes(group.Cover.Size)}";
            lblGroupScanSize.Text = $"Scan Size: {Formatting.FormatBytes(group.ScanSize)}";
            lblGroupSize.Text = $"Size: {Formatting.FormatBytes(group.Size)}";
            lblGroupSongCount.Text = $"Song Count: {group.Songs.Count}";
            lblGroupDuration.Text = $"Duration: {TimeSpan.FromMilliseconds(group.Duration).ToString("dd\\:hh\\:mm\\:ss")}";
        }
    }
}
