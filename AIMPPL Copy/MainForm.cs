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
using System.Reflection;
using Apex;
using Apex.Prompt;
using Apex.Translation;
using System.Globalization;

namespace AIMPPL_Copy
{
    public partial class MainForm : Form
    {
        private Dictionary<string, int> CoverMap;
        private LanguageManager LM;

        public MainForm()
        {
            InitializeComponent();

            LM = new LanguageManager("lang");
            var languages = LM.GetLanguages();
            if (languages.Where((x) => x.Code == CultureInfo.InstalledUICulture.Name).Count() == 1)
            {
                LM.MainLanguage = CultureInfo.InstalledUICulture.Name;
                if (languages.Count > 1)
                {
                    LM.FallbackLanguage = languages[0].Code;
                    if (LM.FallbackLanguage == LM.MainLanguage && languages.Count > 1)
                    {
                        LM.FallbackLanguage = languages[1].Code;
                    }
                }
            }
            else if (languages.Where((x) => x.Code.StartsWith(CultureInfo.InstalledUICulture.Name.Split('-')[0])).Count() >= 1)
            {
                LM.MainLanguage = languages.Where((x) => x.Code.StartsWith(CultureInfo.InstalledUICulture.Name.Split('-')[0])).First().Code;
                if (languages.Count > 1)
                {
                    LM.FallbackLanguage = languages[0].Code;
                    if (LM.FallbackLanguage == LM.MainLanguage && languages.Count > 1)
                    {
                        LM.FallbackLanguage = languages[1].Code;
                    }
                }
            }
            else
            {
                LM.MainLanguage = languages[0].Code;
                LM.FallbackLanguage = languages[1].Code;
            }

            var bindFlags = BindingFlags.Instance | BindingFlags.Public;
            var searchQueue = new Queue<Component>();
            foreach (Component control in Controls)
            {
                searchQueue.Enqueue(control);
            }
            searchQueue.Enqueue(MainTitle);

            while (searchQueue.Count > 0)
            {
                var control = searchQueue.Dequeue();
                var props = control.GetType().GetProperties(bindFlags);
                foreach (var prop in props)
                {
                    if (prop.Name == "LanguageManager")
                    {
                        prop.SetValue(control, LM);
                    }
                    else if (prop.Name == "Controls")
                    {
                        var children = (Control.ControlCollection)prop.GetValue(control);
                        foreach (Control child in children)
                        {
                            searchQueue.Enqueue(child);
                        }
                    }
                }
            }

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
            if (lstPlaylists.Items.Count > 0)
            {
                lstPlaylists.SelectedIndex = 0;
            }
        }

        private void lstPlaylists_SelectedIndexChanged(object sender, EventArgs e)
        {
            var playlist = lstPlaylists.SelectedItem as Playlist;
            lblGroups.Parameters = new object[] { playlist.Groups.Count };
            lblSongCount.Parameters = new object[] { playlist.Songs.Count };
            lblSize.Parameters = new object[] { Formatting.FormatBytes(playlist.Size) };
            lblDuration.Parameters = new object[] { TimeSpan.FromMilliseconds(playlist.Duration).ToString("dd\\:hh\\:mm\\:ss") };
            lblCoverSize.Parameters = new object[] { Formatting.FormatBytes(playlist.CoverSize) };
            lblScanSize.Parameters = new object[] { Formatting.FormatBytes(playlist.ScanSize) };

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
                groupNode.SelectedImageIndex = groupNode.ImageIndex;
                treSongs.Nodes.Add(groupNode);
            }
            treSongs.ResumeLayout();
            if (treSongs.Nodes.Count > 0)
            {
                treSongs.SelectedNode = treSongs.Nodes[0];
                //treSongs.Nodes[0].Expand();
                treSongs.Select();
            }
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

        private async void btnCopy_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DestinationPath == "<default>")
            {
                Properties.Settings.Default.DestinationPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            }

            var prompt = new FolderPicker("Select Destination", "Select where to copy the songs to.", Properties.Settings.Default.DestinationPath);
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                var playlist = lstPlaylists.SelectedItem as Playlist;
                Properties.Settings.Default.DestinationPath = prompt.ChosenFolder;
                if (rdbSongs.Checked)
                {
                    await CopySongs(Properties.Settings.Default.DestinationPath, playlist);
                }
                else if (rdbAlbums.Checked)
                {
                    await CopyAlbums(Properties.Settings.Default.DestinationPath, playlist);
                }
                else if (rdbScans.Checked)
                {
                    await CopyAlbums(Properties.Settings.Default.DestinationPath, playlist);
                    await CopyScans(Properties.Settings.Default.DestinationPath, playlist);
                }
            }
        }

        /// <summary>
        /// Copies songs to specified destination.
        /// </summary>
        /// <param name="Path">Path to copy songs to.</param>
        /// <param name="Playlist">Playlist to source songs from.</param>
        private async Task CopySongs(string Path, Playlist Playlist)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            var size = Playlist.Size;
            var copied = 0L;
            // Copy each song individually.
            foreach (var song in Playlist.Songs)
            {
                await Task.Run(() => { File.Copy(song.Path, System.IO.Path.Combine(Path, System.IO.Path.GetFileName(song.Path)), true); });
                copied += song.Size;
                pgbProgress.Value = (int)(copied * 100 / size);
                pgbProgress.CustomText = $"Songs {Formatting.FormatBytes(copied)}/{Formatting.FormatBytes(size)}";
            }

            pgbProgress.Value = 0;
            pgbProgress.CustomText = "Done";
        }

        /// <summary>
        /// Copies albums to specified destination.
        /// </summary>
        /// <param name="Path">Path to copy albums to.</param>
        /// <param name="Playlist">Playlist to source albums from.</param>
        private async Task CopyAlbums(string Path, Playlist Playlist)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            var size = Playlist.Size;
            var copied = 0L;
            // Need to create a folder per group.
            foreach (var group in Playlist.Groups)
            {
                var groupDir = System.IO.Path.Combine(Path, group.Name);
                if (!Directory.Exists(groupDir))
                {
                    Directory.CreateDirectory(groupDir);
                }

                // Copy songs.
                foreach (var song in group.Songs)
                {
                    await Task.Run(() => { File.Copy(song.Path, System.IO.Path.Combine(groupDir, System.IO.Path.GetFileName(song.Path)), true); });
                    copied += song.Size;
                    pgbProgress.Value = (int)(copied * 100 / size);
                    pgbProgress.CustomText = $"Songs {Formatting.FormatBytes(copied)}/{Formatting.FormatBytes(size)}";
                }
            }

            size = Playlist.CoverSize;
            copied = 0L;
            foreach (var group in Playlist.Groups)
            {
                var groupDir = System.IO.Path.Combine(Path, group.Name);
                if (!Directory.Exists(groupDir))
                {
                    Directory.CreateDirectory(groupDir);
                }

                // Copy cover art.
                if (group.Cover.Size > 0)
                {
                    await Task.Run(() => { File.Copy(group.Cover.Path, System.IO.Path.Combine(groupDir, System.IO.Path.GetFileName(group.Cover.Path)), true); });
                    copied += group.Cover.Size;
                    pgbProgress.Value = (int)(copied * 100 / size);
                    pgbProgress.CustomText = $"Covers {Formatting.FormatBytes(copied)}/{Formatting.FormatBytes(size)}";
                }
            }

            pgbProgress.Value = 0;
            pgbProgress.CustomText = "Done";
        }

        /// <summary>
        /// Copies scans to the specified destination.
        /// </summary>
        /// <param name="Path">Path to copy scans to.</param>
        /// <param name="Playlist">Playlist to source scans from.</param>
        private async Task CopyScans(string Path, Playlist Playlist)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            var size = Playlist.ScanSize;
            var copied = 0L;
            foreach (var group in Playlist.Groups)
            {
                if (group.Scans.Count == 0)
                {
                    continue;
                }
                var groupDir = System.IO.Path.Combine(Path, group.Name);
                var scanDir = System.IO.Path.Combine(groupDir, group.Scans[0].Directory);

                if (!Directory.Exists(scanDir))
                {
                    Directory.CreateDirectory(scanDir);
                }

                // Copy each scan.
                foreach (var scan in group.Scans)
                {
                    await Task.Run(() => { File.Copy(scan.Path, System.IO.Path.Combine(scanDir, System.IO.Path.GetFileName(scan.Path)), true); });
                    copied += scan.Size;
                    pgbProgress.Value = (int)(copied * 100 / size);
                    pgbProgress.CustomText = $"Scans {Formatting.FormatBytes(copied)}/{Formatting.FormatBytes(size)}";
                }
            }

            pgbProgress.Value = 0;
            pgbProgress.CustomText = "Done";
        }

        private void treSongs_AfterSelect(object sender, TreeViewEventArgs e)
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

            lblSongAlbum.Parameters = new object[] { song.Album };
            lblSongArtist.Parameters = new object[] { song.Artist };
            lblSongSize.Parameters = new object[] { Formatting.FormatBytes(song.Size) };
            lblSongDuration.Parameters = new object[] { TimeSpan.FromMilliseconds(song.Duration).ToString("dd\\:hh\\:mm\\:ss") };
            lblSongTitle.Parameters = new object[] { song.Title };
            lblSongTrackNo.Parameters = new object[] { song.TrackNo };

            lblGroupCoverSize.Parameters = new object[] { Formatting.FormatBytes(group.Cover.Size) };
            lblGroupDuration.Parameters = new object[] { TimeSpan.FromMilliseconds(group.Duration).ToString("dd\\:hh\\:mm\\:ss") };
            lblGroupScanSize.Parameters = new object[] { Formatting.FormatBytes(group.ScanSize) };
            lblGroupSongCount.Parameters = new object[] { group.Songs.Count };
            lblGroupSize.Parameters = new object[] { Formatting.FormatBytes(group.Size) };
        }
    }
}
