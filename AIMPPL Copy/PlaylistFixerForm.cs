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

            LoadPlaylist(SelectedPlaylist);
        }

        public void LoadPlaylist(Playlist SelectedPlaylist)
        {
            playlist = SelectedPlaylist;
            var missing = new List<Song>();
            var formatChanged = new List<FormatChange>();

            Util.FindMissing(SelectedPlaylist, out missing, out formatChanged);

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
            // the checkbox, button, and row selector. If there is a scrollbar, it uses a further 18.
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
                var songs = dgvMissing.Rows.Cast<DataGridViewRow>().Where((x) => !(bool)(x.Cells[clmChange.Index].Value)).Select((x) => x.Cells[clmSongBind.Index].Value as Song).ToList();
                var foundSongs = Util.SearchSongs(songs, DirectoryDialogue.SelectedPath);

                var rows = dgvMissing.Rows.Cast<DataGridViewRow>();
                foreach (var song in foundSongs)
                {
                    var row = rows.First((x) => x.Cells[clmSongBind.Index].Value == song.Item1);
                    row.Cells[clmDestination.Index].Value = song.Item2;
                    row.Cells[clmChange.Index].Value = true;
                }
            }
        }
    }
}
