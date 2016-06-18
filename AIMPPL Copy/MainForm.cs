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
        public MainForm()
        {
            InitializeComponent();
            
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
            lblSongs.Parameters = new object[] { playlist.SongCount };
            lblSongs.Text = $"Songs: {playlist.SongCount}";
            lblSize.Text = $"Size: {Formatting.FormatBytes(playlist.Size)}";
            lblDuration.Text = $"Duration: {TimeSpan.FromMilliseconds(playlist.Duration).ToString("dd\\:hh\\:mm\\:ss")}";
            lblCoverSize.Text = $"Cover Size: {Formatting.FormatBytes(playlist.CoverSize)}";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
