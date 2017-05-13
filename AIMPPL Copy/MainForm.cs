using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using AIMPPL_Copy.AIMP3;
using AIMPPL_Copy.AIMP4;
using AIMPPL_Copy.Properties;
using Apex;
using Apex.Prompt;
using Apex.Translation;

namespace AIMPPL_Copy
{
    public partial class MainForm : Form
    {
        private PlaylistFixerForm _bulkPlaylistFixerForm;
        private readonly Dictionary<string, int> _coverMap;
        private readonly LanguageManager _lm;
        private StatisticsForm _statisticsForm;

        public MainForm()
        {
            InitializeComponent();

            _lm = new LanguageManager("lang");
            _lm.UseOSLanguage("en-AU");
            _lm.AddAllControls(this);

            const BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public;
            var searchQueue = new Queue<Component>();
            foreach (Component control in Controls)
                searchQueue.Enqueue(control);
            searchQueue.Enqueue(MainTitle);

            while (searchQueue.Count > 0)
            {
                var control = searchQueue.Dequeue();
                var props = control.GetType().GetProperties(bindFlags);
                foreach (var prop in props)
                    if (prop.Name == "LanguageManager")
                    {
                        prop.SetValue(control, _lm);
                    }
                    else if (prop.Name == "Controls")
                    {
                        var children = (Control.ControlCollection)prop.GetValue(control);
                        foreach (Control child in children)
                            searchQueue.Enqueue(child);
                    }
            }

            TooltipTranslator.UpdateControl(btnCopy, "MAIN.TOOLTIP.COPY");
            TooltipTranslator.UpdateControl(btnFixPlaylist, "MAIN.TOOLTIP.FIX_PLAYLIST");
            TooltipTranslator.LanguageManager = _lm;

            _coverMap = new Dictionary<string, int>();
            // Load thumbnails to speed it up zoom zoom.
            if (File.Exists("thumbs.dat"))
                using (var fs = File.Open("thumbs.dat", FileMode.Open))
                {
                    using (var r = new BinaryReader(fs))
                    {
                        var count = r.ReadInt32();
                        for (var i = 0; i < count; i++)
                        {
                            var path = r.ReadString();
                            var length = r.ReadInt32();
                            using (var ms = new MemoryStream(r.ReadBytes(length)))
                            {
                                var img = Image.FromStream(ms);
                                AddCover(img, path);
                            }
                        }
                    }
                }

            if (Settings.Default.PlaylistPath == "<default>")
            {
                var prompt = new FolderPicker("AIMP Playlist Folder", "Select the AIMP playlist folder.",
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AIMP\\PLS"));
                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default.PlaylistPath = prompt.ChosenFolder;
                }
                else
                {
                    MessageBox.Show("I'll try again later.");
                    Close();
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadPlaylists();
        }

        private void LoadPlaylists()
        {
            lstPlaylists.Items.Clear();
            // AIMP3 playlists.
            foreach (var file in Directory.GetFiles(Settings.Default.PlaylistPath, "*.aimppl"))
            {
                var playlist = new Aimp3Playlist(file);
                lstPlaylists.Items.Add(playlist);
            }
            // AIMP4 playlists.
            foreach (var file in Directory.GetFiles(Settings.Default.PlaylistPath, "*.aimppl4"))
            {
                var playlist = new Aimp4Playlist(file);
                lstPlaylists.Items.Add(playlist);
            }
            if (lstPlaylists.Items.Count > 0)
            {
                lstPlaylists.Sorted = true;
                lstPlaylists.SelectedIndex = 0;
            }
        }

        private void lstPlaylists_SelectedIndexChanged(object sender, EventArgs e)
        {
            var playlist = lstPlaylists.SelectedItem as Playlist;
            if (playlist == null)
                return;
            lblGroups.Parameters = new object[] {playlist.Groups.Count};
            lblSongCount.Parameters = new object[] {playlist.Songs.Count};
            lblSize.Parameters = new object[] {Formatting.FormatBytes(playlist.Size)};
            lblDuration.Parameters = new object[]
                {TimeSpan.FromMilliseconds(playlist.Duration).ToString("dd\\:hh\\:mm\\:ss")};
            lblCoverSize.Parameters = new object[] {Formatting.FormatBytes(playlist.CoverSize)};
            lblScanSize.Parameters = new object[] {Formatting.FormatBytes(playlist.ScanSize)};

            treSongs.SuspendLayout();
            treSongs.Nodes.Clear();
            foreach (var group in playlist.Groups)
            {
                var groupNode = new TreeNode(group.ToString())
                {
                    Tag = group
                };
                foreach (var song in group.Songs)
                {
                    var songNode = new TreeNode(song.ToString())
                    {
                        Tag = song,
                        ImageIndex = -1
                    };
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

        private int GetCover(Group group)
        {
            var path = group.Cover.Path;
            if (string.IsNullOrWhiteSpace(path))
                return 1;

            if (_coverMap.ContainsKey(path))
                return _coverMap[path];
            var img = Image.FromFile(path);
            var small = Utilities.ResizeImage(img, 16, 16);
            return AddCover(small, path);
        }

        private int AddCover(Image cover, string path)
        {
            _coverMap.Add(path, imlCovers.Images.Count);
            imlCovers.Images.Add(cover);
            cover.Dispose();
            return imlCovers.Images.Count - 1;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
            using (var fs = File.Open("thumbs.dat", FileMode.Create))
            {
                using (var w = new BinaryWriter(fs))
                {
                    // Save cover count, then filename and BMP pairs.
                    w.Write(_coverMap.Count);
                    foreach (var image in _coverMap)
                    {
                        w.Write($"{image.Key}");
                        using (var ms = new MemoryStream())
                        {
                            imlCovers.Images[image.Value].Save(ms, ImageFormat.Png);
                            var bytes = ms.ToArray();
                            w.Write(bytes.Length);
                            w.Write(bytes);
                        }
                    }
                }
            }
        }

        private async void btnCopy_Click(object sender, EventArgs e)
        {
            if (Settings.Default.DestinationPath == "<default>")
                Settings.Default.DestinationPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

            var prompt = new FolderPicker("Select Destination", "Select where to copy the songs to.",
                Settings.Default.DestinationPath);
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                var playlist = lstPlaylists.SelectedItem as Playlist;
                Settings.Default.DestinationPath = prompt.ChosenFolder;
                if (rdbSongs.Checked)
                {
                    await CopySongs(Settings.Default.DestinationPath, playlist);
                }
                else if (rdbAlbums.Checked)
                {
                    await CopyAlbums(Settings.Default.DestinationPath, playlist);
                }
                else if (rdbScans.Checked)
                {
                    await CopyAlbums(Settings.Default.DestinationPath, playlist);
                    await CopyScans(Settings.Default.DestinationPath, playlist);
                }
            }
            prompt.Dispose();
        }

        /// <summary>
        ///     Copies songs to specified destination.
        /// </summary>
        /// <param name="path">Path to copy songs to.</param>
        /// <param name="playlist">Playlist to source songs from.</param>
        private async Task CopySongs(string path, Playlist playlist)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var size = playlist.Size;
            var copied = 0L;
            // Copy each song individually.
            foreach (var song in playlist.Songs)
            {
                await Task.Run(() =>
                {
                    File.Copy(song.Path, Path.Combine(path, Path.GetFileName(song.Path)), true);
                });
                copied += song.Size;
                pgbProgress.Value = (int)(copied * 100 / size);
                pgbProgress.CustomText = $"Songs {Formatting.FormatBytes(copied)}/{Formatting.FormatBytes(size)}";
            }

            pgbProgress.Value = 0;
            pgbProgress.CustomText = "Done";
        }

        /// <summary>
        ///     Copies albums to specified destination.
        /// </summary>
        /// <param name="path">Path to copy albums to.</param>
        /// <param name="playlist">Playlist to source albums from.</param>
        private async Task CopyAlbums(string path, Playlist playlist)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var size = playlist.Size;
            var copied = 0L;
            // Need to create a folder per group.
            foreach (var group in playlist.Groups)
            {
                var groupDir = Path.Combine(path, group.Name);
                if (!Directory.Exists(groupDir))
                    Directory.CreateDirectory(groupDir);

                // Copy songs.
                foreach (var song in group.Songs)
                {
                    await Task.Run(() =>
                    {
                        File.Copy(song.Path,
                            Path.Combine(groupDir, Path.GetFileName(song.Path)), true);
                    });
                    copied += song.Size;
                    pgbProgress.Value = (int)(copied * 100 / size);
                    pgbProgress.CustomText = $"Songs {Formatting.FormatBytes(copied)}/{Formatting.FormatBytes(size)}";
                }
            }

            size = playlist.CoverSize;
            copied = 0L;
            foreach (var group in playlist.Groups)
            {
                var groupDir = Path.Combine(path, group.Name);
                if (!Directory.Exists(groupDir))
                    Directory.CreateDirectory(groupDir);

                // Copy cover art.
                if (group.Cover.Size > 0)
                {
                    await Task.Run(() =>
                    {
                        File.Copy(group.Cover.Path,
                            Path.Combine(groupDir, Path.GetFileName(group.Cover.Path)), true);
                    });
                    copied += group.Cover.Size;
                    pgbProgress.Value = (int)(copied * 100 / size);
                    pgbProgress.CustomText = $"Covers {Formatting.FormatBytes(copied)}/{Formatting.FormatBytes(size)}";
                }
            }

            pgbProgress.Value = 0;
            pgbProgress.CustomText = "Done";
        }

        /// <summary>
        ///     Copies scans to the specified destination.
        /// </summary>
        /// <param name="path">Path to copy scans to.</param>
        /// <param name="playlist">Playlist to source scans from.</param>
        private async Task CopyScans(string path, Playlist playlist)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var size = playlist.ScanSize;
            var copied = 0L;
            foreach (var group in playlist.Groups)
            {
                if (group.Scans.Count == 0)
                    continue;
                var groupDir = Path.Combine(path, group.Name);
                var scanDir = Path.Combine(groupDir, group.Scans[0].Directory);

                if (!Directory.Exists(scanDir))
                    Directory.CreateDirectory(scanDir);

                // Copy each scan.
                foreach (var scan in group.Scans)
                {
                    await Task.Run(() =>
                    {
                        File.Copy(scan.Path, Path.Combine(scanDir, Path.GetFileName(scan.Path)),
                            true);
                    });
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

            lblSongAlbum.Parameters = new object[] {song.Album};
            lblSongArtist.Parameters = new object[] {song.Artist};
            lblSongSize.Parameters = new object[] {Formatting.FormatBytes(song.Size)};
            lblSongDuration.Parameters = new object[]
                {TimeSpan.FromMilliseconds(song.Duration).ToString("dd\\:hh\\:mm\\:ss")};
            lblSongTitle.Parameters = new object[] {song.Title};
            lblSongTrackNo.Parameters = new object[] {song.TrackNo};

            lblGroupCoverSize.Parameters = new object[] {Formatting.FormatBytes(group.Cover.Size)};
            lblGroupDuration.Parameters = new object[]
                {TimeSpan.FromMilliseconds(group.Duration).ToString("dd\\:hh\\:mm\\:ss")};
            lblGroupScanSize.Parameters = new object[] {Formatting.FormatBytes(group.ScanSize)};
            lblGroupSongCount.Parameters = new object[] {group.Songs.Count};
            lblGroupSize.Parameters = new object[] {Formatting.FormatBytes(group.Size)};
        }

        private void btnFixPlaylist_Click(object sender, EventArgs e)
        {
            if (chkBulkMode.Checked)
            {
                if (_bulkPlaylistFixerForm == null)
                {
                    var playlists = lstPlaylists.Items.Cast<Playlist>().ToList();
                    _bulkPlaylistFixerForm = new PlaylistFixerForm(_lm, playlists, this);
                    _bulkPlaylistFixerForm.Show();
                }
                else
                {
                    var playlists = lstPlaylists.Items.Cast<Playlist>().ToList();
                    _bulkPlaylistFixerForm.LoadPlaylists(playlists);
                    _bulkPlaylistFixerForm.BringToFront();
                }
            }
            else
            {
                if (lstPlaylists.SelectedItem != null && lstPlaylists.SelectedItem is Playlist)
                    if (_bulkPlaylistFixerForm == null)
                    {
                        _bulkPlaylistFixerForm = new PlaylistFixerForm(_lm, lstPlaylists.SelectedItem as Playlist, this);
                        _bulkPlaylistFixerForm.Show();
                    }
                    else
                    {
                        _bulkPlaylistFixerForm.LoadSinglePlaylist(lstPlaylists.SelectedItem as Playlist);
                        _bulkPlaylistFixerForm.BringToFront();
                    }
            }
        }

        public void ChildClosed(Form child)
        {
            if (child is PlaylistFixerForm)
                _bulkPlaylistFixerForm = null;
            else if (child is StatisticsForm)
                _statisticsForm = null;
        }

        private void upgradePlaylistsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstPlaylists.SelectedItem is Playlist)
            {
                var newPl = Aimp4Playlist.UpgradePlaylist(lstPlaylists.SelectedItem as Aimp3Playlist);
                newPl.Save();
                File.Delete((lstPlaylists.SelectedItem as Playlist).Path);
                LoadPlaylists();
            }
        }

        private void upgradeAllPlaylistsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var playlists = lstPlaylists.Items.Cast<Playlist>()
                .Where(x => x is Aimp3Playlist)
                .Cast<Aimp3Playlist>()
                .ToList();
            foreach (var playlist in playlists)
            {
                var newPl = Aimp4Playlist.UpgradePlaylist(playlist);
                newPl.Save();
                File.Delete(playlist.Path);
            }

            LoadPlaylists();
        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_statisticsForm == null)
            {
                _statisticsForm = new StatisticsForm(_lm, lstPlaylists.Items.Cast<Playlist>().ToList(), this);
                _statisticsForm.Show();
            }
            else
            {
                _statisticsForm.BringToFront();
            }
        }
    }
}
