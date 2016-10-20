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

namespace AIMPPL_Copy
{
    public partial class BulkPlaylistFixerForm : Form
    {
        private LanguageManager LM;
        private List<Playlist> playlists;
        new private MainForm Parent;

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
        
        private bool LoadPlaylist(Playlist Playlist)
        {
            var missing = new List<Song>();
            var formatChanged = new List<FormatChange>();

            Util.FindMissing(Playlist, out missing, out formatChanged);

            if (missing.Count + formatChanged.Count > 0)
            {
                ptcTree.AddPlaylist(Playlist, missing, formatChanged);
                return true;
            }

            return false;
        }

        private void BulkPlaylistFixerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Parent.ChildClosed(this);
        }
    }
}
