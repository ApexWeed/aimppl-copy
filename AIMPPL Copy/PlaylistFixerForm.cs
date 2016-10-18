using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Apex.Translation;

namespace AIMPPL_Copy
{
    public partial class PlaylistFixerForm : Form
    {
        private LanguageManager LM;
        private Playlist playlist;
        new private MainForm Parent;

        private Dictionary<string, bool> FileExistanceCache;
        private HashSet<string> FolderScanCache;
        private string[] extensions = new string[]
        {
            "flac",
            "mp3",
            "alac",
            "tak",
            "ape",
            "wav",
            "ogg",
            "m4a",
        };

        public PlaylistFixerForm(LanguageManager LanguageManager, Playlist SelectedPlaylist, MainForm Parent)
        {
            InitializeComponent();

            LM = LanguageManager;

            var bindFlags = BindingFlags.Instance | BindingFlags.Public;
            var searchQueue = new Queue<Component>();
            foreach (Component control in Controls)
            {
                searchQueue.Enqueue(control);
            }
            searchQueue.Enqueue(PlaylistFixerTitle);
            searchQueue.Enqueue(ColumnHeaders);
            ColumnHeaders.UpdateColumnHeader(clmSource, "FIX_PLAYLIST.LABEL.COLUMN.SOURCE");
            ColumnHeaders.UpdateColumnHeader(clmDestination, "FIX_PLAYLIST.LABEL.COLUMN.DESTINATION");

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

            this.Parent = Parent;
            FileExistanceCache = new Dictionary<string, bool>();
            FolderScanCache = new HashSet<string>();

            LoadPlaylist(SelectedPlaylist);
        }

        public void LoadPlaylist(Playlist SelectedPlaylist)
        {
            playlist = SelectedPlaylist;
            var songs = playlist.Songs;
            var missing = new List<Song>();
            var formatChanged = new List<FormatChange>();

            foreach (var song in songs)
            {
                if (!FileExists(song.Path))
                {
                    var changed = false;
                    foreach (var extension in extensions)
                    {
                        var newPath = Path.Combine(Path.GetDirectoryName(song.Path), Path.ChangeExtension(song.Path, extension));
                        if (FileExists(newPath))
                        {
                            formatChanged.Add(new FormatChange(song, extension));
                            changed = true;
                            break;
                        }
                    }
                    if (!changed)
                    {
                        missing.Add(song);
                    }
                }
            }

            //lstMissing.Items.Clear();
            //lstMissing.Items.AddRange(missing.ToArray());

            //lstChangedFiletype.Items.Clear();
            //lstChangedFiletype.Items.AddRange(formatChanged.ToArray());

            dgvMissing.Rows.Clear();
            foreach (var song in missing)
            {
                dgvMissing.Rows.Add(false, song.Path, "", LM.GetString("FIX_PLAYLIST.BUTTON.BROWSE"), song);
            }
            foreach (var song in formatChanged)
            {
                dgvMissing.Rows.Add(true, song.Song.Path, Path.ChangeExtension(song.Song.Path, song.NewExtension), LM.GetString("FIX_PLAYLIST.BUTTON.BROWSE"), song.Song);
            }

            ResizeColumns();
        }

        /// <summary>
        /// Returns whether a file exists on disk or not, uses a cache and scans the whole folder if
        /// the file is not in the cache already.
        /// </summary>
        /// <param name="Path">Path to check for existance.</param>
        /// <returns>Whether the file exists.</returns>
        private bool FileExists(string Path)
        {
            if (!FileExistanceCache.ContainsKey(Path))
            {
                var dirPath = System.IO.Path.GetDirectoryName(Path);

                // Account for non existant folders.
                if (!Directory.Exists(dirPath))
                {
                    FileExistanceCache.Add(Path, false);
                    return false;
                }

                if (FolderScanCache.Contains(dirPath))
                {
                    FileExistanceCache.Add(Path, false);
                }
                else
                {
                    FolderScanCache.Add(dirPath);
                    var dirFiles = Directory.GetFiles(dirPath);
                    foreach (var file in dirFiles)
                    {
                        FileExistanceCache.Add(file, true);
                    }
                    if (!dirFiles.Contains(Path))
                    {
                        FileExistanceCache.Add(Path, false);
                    }
                }
            }

            return FileExistanceCache[Path];
        }

        private void PlaylistFixerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Parent.ChildClosed(this);
        }

        private void dgvMissing_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Only trigger off the button.
            if (e.ColumnIndex == clmBrowse.Index)
            {
                SelectReplacement(e.RowIndex);
            }
        }

        private void dgvMissing_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Only trigger double clicks on the destination.
            if (e.ColumnIndex == clmDestination.Index)
            {
                SelectReplacement(e.RowIndex);
            }
        }

        private void SelectReplacement(int Index)
        {
            var path = dgvMissing.Rows[Index].Cells[clmDestination.Index].Value.ToString();
            if (string.IsNullOrWhiteSpace(path))
            {
                FileDialogue.FileName = "";
                FileDialogue.InitialDirectory = Path.GetDirectoryName(dgvMissing.Rows[Index].Cells[clmSource.Index].Value.ToString());
            }
            else
            {
                FileDialogue.FileName = path;
                FileDialogue.InitialDirectory = Path.GetDirectoryName(path);
            }

            if (FileDialogue.ShowDialog() == DialogResult.OK)
            {
                dgvMissing.Rows[Index].Cells[clmDestination.Index].Value = FileDialogue.FileName;
                dgvMissing.Rows[Index].Cells[clmChange.Index].Value = true;
            }
        }

        private void dgvMissing_Resize(object sender, EventArgs e)
        {
            ResizeColumns();
        }

        private void ResizeColumns()
        {
            // Final usable size for source / destination is width of the data grid view - 165 pixels to account for 
            // the checkbox, button, and row selector. If there is a scrollbar, it uses a further 16.
            var availableWidth = dgvMissing.Width - 169;
            var vScrollbar = dgvMissing.Controls.OfType<VScrollBar>().First();
            if (vScrollbar.Visible)
            {
                availableWidth -= 18;
            }
            clmSource.Width = availableWidth / 2;
            clmDestination.Width = availableWidth / 2 + ((availableWidth / 2) % 2);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow row in dgvMissing.Rows)
            for (int i = 0; i < dgvMissing.Rows.Count; i++)
            {
                var row = dgvMissing.Rows[i];
                var replace = (bool)row.Cells[clmChange.Index].Value;

                if (replace)
                {
                    var song = (Song)row.Cells[clmSongBind.Index].Value;
                    var newPath = (string)row.Cells[clmDestination.Index].Value;
                    if (File.Exists(newPath))
                    {
                        song.Path = newPath;
                    }
                    dgvMissing.Rows.RemoveAt(i);
                    i--;
                }
            }

            playlist.Save();
        }

        private void PlaylistFixerForm_Resize(object sender, EventArgs e)
        {
            pnlBottomLeft.Width = pnlBottom.Width / 2;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (DirectoryDialogue.ShowDialog() == DialogResult.OK)
            {
                var files = Apex.FileUtil.GetFiles(DirectoryDialogue.SelectedPath);
                var songs = dgvMissing.Rows.Cast<DataGridViewRow>().Where((x) => !(bool)(x.Cells[clmChange.Index].Value)).Select((x) => new Tuple<Song, int>(x.Cells[clmSongBind.Index].Value as Song, x.Index)).ToList();

                // Try to find the song in the search directory.
                foreach (var song in songs)
                {
                    var found = false;
                    // In theory, FLAC and MP3 files are much more likely to be found so prioritise searching by
                    // extension rather than trying each extension on each file one after the other.
                    foreach (var extension in extensions)
                    {
                        if (found)
                        {
                            continue;
                        }

                        foreach (var file in files)
                        {
                            if (found)
                            {
                                continue;
                            }

                            // Update the data grid with the new filename if we found it.
                            if (Path.GetFileName(Path.ChangeExtension(song.Item1.Path, extension)) == Path.GetFileName(file))
                            {
                                dgvMissing.Rows[song.Item2].Cells[clmDestination.Index].Value = file;
                                dgvMissing.Rows[song.Item2].Cells[clmChange.Index].Value = true;
                                found = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
