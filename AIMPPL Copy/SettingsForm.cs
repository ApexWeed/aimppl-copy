using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Apex.Extensions;
using Apex.Translation;

namespace AIMPPL_Copy
{
    public partial class SettingsForm : Form
    {
        private readonly LanguageManager _lm;
        private readonly MainForm _parent;

        public SettingsForm(LanguageManager languageManager, MainForm parent)
        {
            InitializeComponent();

            _lm = languageManager;
            _lm.AddAllControls(this);
            _parent = parent;

            uiPlaylistPath.Text = Properties.Settings.Default.PlaylistPath;
            uiDestinationPath.Text = Properties.Settings.Default.DestinationPath;
            uiScanFilenames.Checked = Properties.Settings.Default.ScanFilenames;
            uiScanCues.Checked = Properties.Settings.Default.ScanCues;
            uiScanTags.Checked = Properties.Settings.Default.ScanTags;
            foreach (var ext in Properties.Settings.Default.MusicExtensions)
                uiMusicExtensions.Items.Add(ext);
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.PlaylistPath = uiPlaylistPath.Text;
            Properties.Settings.Default.DestinationPath = uiDestinationPath.Text;
            Properties.Settings.Default.ScanFilenames = uiScanFilenames.Checked;
            Properties.Settings.Default.ScanCues = uiScanCues.Checked;
            Properties.Settings.Default.ScanTags = uiScanTags.Checked;
            Properties.Settings.Default.MusicExtensions.Clear();
            foreach (string item in uiMusicExtensions.Items)
                Properties.Settings.Default.MusicExtensions.Add(item);

            _parent.ChildClosed(this);
        }

        private void uiAddExtension_Click(object sender, EventArgs e)
        {
            if (uiMusicExtension.Text.Length == 0)
                return;

            uiMusicExtensions.Items.Add(uiMusicExtension.Text);
            uiMusicExtension.Text = string.Empty;
        }

        private void uiDeleteExtension_Click(object sender, EventArgs e)
        {
            uiMusicExtensions.RemoveSelection();
        }

        private void uiClearExtensions_Click(object sender, EventArgs e)
        {
            uiMusicExtensions.Items.Clear();
        }

        private void uiDefaultExtensions_Click(object sender, EventArgs e)
        {
            uiMusicExtensions.Items.Clear();
            uiMusicExtensions.Items.AddRange(Util.MusicExtensions);
        }

        private void uiMoveUp_Click(object sender, EventArgs e)
        {
            uiMusicExtensions.MoveSelection(ListBoxExtensions.MoveDirection.Up);
        }

        private void uiMoveDown_Click(object sender, EventArgs e)
        {
            uiMusicExtensions.MoveSelection(ListBoxExtensions.MoveDirection.Down);
        }

        private void uiMusicExtension_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                uiAddExtension.PerformClick();
        }
    }
}
