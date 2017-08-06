namespace AIMPPL_Copy
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.translatableLabel1 = new Apex.Translation.Controls.TranslatableLabel();
            this.uiPlaylistPath = new System.Windows.Forms.TextBox();
            this.uiPlaylistPathBrowse = new Apex.Translation.Controls.TranslatableButton();
            this.uiDestinationPathBrowse = new Apex.Translation.Controls.TranslatableButton();
            this.uiDestinationPath = new System.Windows.Forms.TextBox();
            this.translatableLabel2 = new Apex.Translation.Controls.TranslatableLabel();
            this.uiMusicExtensions = new System.Windows.Forms.ListBox();
            this.translatableLabel3 = new Apex.Translation.Controls.TranslatableLabel();
            this.uiAddExtension = new Apex.Translation.Controls.TranslatableButton();
            this.uiDeleteExtension = new Apex.Translation.Controls.TranslatableButton();
            this.uiClearExtensions = new Apex.Translation.Controls.TranslatableButton();
            this.uiDefaultExtensions = new Apex.Translation.Controls.TranslatableButton();
            this.uiMusicExtension = new System.Windows.Forms.TextBox();
            this.uiMoveDown = new Apex.Translation.Controls.TranslatableButton();
            this.uiMoveUp = new Apex.Translation.Controls.TranslatableButton();
            this.uiTitle = new Apex.Translation.Controls.TranslatableTitle();
            this.uiScanCues = new Apex.Translation.Controls.TranslatableCheckBox();
            this.uiScanTags = new Apex.Translation.Controls.TranslatableCheckBox();
            this.uiScanFilenames = new Apex.Translation.Controls.TranslatableCheckBox();
            this.SuspendLayout();
            // 
            // translatableLabel1
            // 
            this.translatableLabel1.AutoSize = true;
            this.translatableLabel1.DefaultString = null;
            this.translatableLabel1.Location = new System.Drawing.Point(10, 9);
            this.translatableLabel1.Name = "translatableLabel1";
            this.translatableLabel1.Size = new System.Drawing.Size(183, 12);
            this.translatableLabel1.TabIndex = 1000;
            this.translatableLabel1.Text = "SETTINGS.LABEL.PLAYLIST_PATH";
            this.translatableLabel1.TranslationString = "SETTINGS.LABEL.PLAYLIST_PATH";
            // 
            // uiPlaylistPath
            // 
            this.uiPlaylistPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiPlaylistPath.Location = new System.Drawing.Point(12, 24);
            this.uiPlaylistPath.Name = "uiPlaylistPath";
            this.uiPlaylistPath.Size = new System.Drawing.Size(379, 19);
            this.uiPlaylistPath.TabIndex = 0;
            // 
            // uiPlaylistPathBrowse
            // 
            this.uiPlaylistPathBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiPlaylistPathBrowse.DefaultString = null;
            this.uiPlaylistPathBrowse.Location = new System.Drawing.Point(397, 22);
            this.uiPlaylistPathBrowse.Name = "uiPlaylistPathBrowse";
            this.uiPlaylistPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.uiPlaylistPathBrowse.TabIndex = 1;
            this.uiPlaylistPathBrowse.Text = "SETTINGS.BUTTON.BROWSE";
            this.uiPlaylistPathBrowse.TranslationString = "SETTINGS.BUTTON.BROWSE";
            this.uiPlaylistPathBrowse.UseVisualStyleBackColor = true;
            // 
            // uiDestinationPathBrowse
            // 
            this.uiDestinationPathBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiDestinationPathBrowse.DefaultString = null;
            this.uiDestinationPathBrowse.Location = new System.Drawing.Point(397, 59);
            this.uiDestinationPathBrowse.Name = "uiDestinationPathBrowse";
            this.uiDestinationPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.uiDestinationPathBrowse.TabIndex = 3;
            this.uiDestinationPathBrowse.Text = "SETTINGS.BUTTON.BROWSE";
            this.uiDestinationPathBrowse.TranslationString = "SETTINGS.BUTTON.BROWSE";
            this.uiDestinationPathBrowse.UseVisualStyleBackColor = true;
            // 
            // uiDestinationPath
            // 
            this.uiDestinationPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiDestinationPath.Location = new System.Drawing.Point(12, 61);
            this.uiDestinationPath.Name = "uiDestinationPath";
            this.uiDestinationPath.Size = new System.Drawing.Size(379, 19);
            this.uiDestinationPath.TabIndex = 2;
            // 
            // translatableLabel2
            // 
            this.translatableLabel2.AutoSize = true;
            this.translatableLabel2.DefaultString = null;
            this.translatableLabel2.Location = new System.Drawing.Point(10, 46);
            this.translatableLabel2.Name = "translatableLabel2";
            this.translatableLabel2.Size = new System.Drawing.Size(206, 12);
            this.translatableLabel2.TabIndex = 1000;
            this.translatableLabel2.Text = "SETTINGS.LABEL.DESTINATION_PATH";
            this.translatableLabel2.TranslationString = "SETTINGS.LABEL.DESTINATION_PATH";
            // 
            // uiMusicExtensions
            // 
            this.uiMusicExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiMusicExtensions.FormattingEnabled = true;
            this.uiMusicExtensions.ItemHeight = 12;
            this.uiMusicExtensions.Location = new System.Drawing.Point(12, 98);
            this.uiMusicExtensions.Name = "uiMusicExtensions";
            this.uiMusicExtensions.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.uiMusicExtensions.Size = new System.Drawing.Size(298, 244);
            this.uiMusicExtensions.TabIndex = 100;
            // 
            // translatableLabel3
            // 
            this.translatableLabel3.AutoSize = true;
            this.translatableLabel3.DefaultString = null;
            this.translatableLabel3.Location = new System.Drawing.Point(10, 83);
            this.translatableLabel3.Name = "translatableLabel3";
            this.translatableLabel3.Size = new System.Drawing.Size(206, 12);
            this.translatableLabel3.TabIndex = 1000;
            this.translatableLabel3.Text = "SETTINGS.LABEL.MUSIC_EXTENSIONS";
            this.translatableLabel3.TranslationString = "SETTINGS.LABEL.MUSIC_EXTENSIONS";
            // 
            // uiAddExtension
            // 
            this.uiAddExtension.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiAddExtension.DefaultString = null;
            this.uiAddExtension.Location = new System.Drawing.Point(316, 123);
            this.uiAddExtension.Name = "uiAddExtension";
            this.uiAddExtension.Size = new System.Drawing.Size(75, 23);
            this.uiAddExtension.TabIndex = 6;
            this.uiAddExtension.Text = "SETTINGS.BUTTON.ADD";
            this.uiAddExtension.TranslationString = "SETTINGS.BUTTON.ADD";
            this.uiAddExtension.UseVisualStyleBackColor = true;
            this.uiAddExtension.Click += new System.EventHandler(this.uiAddExtension_Click);
            // 
            // uiDeleteExtension
            // 
            this.uiDeleteExtension.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiDeleteExtension.DefaultString = null;
            this.uiDeleteExtension.Location = new System.Drawing.Point(397, 123);
            this.uiDeleteExtension.Name = "uiDeleteExtension";
            this.uiDeleteExtension.Size = new System.Drawing.Size(75, 23);
            this.uiDeleteExtension.TabIndex = 7;
            this.uiDeleteExtension.Text = "SETTINGS.BUTTON.DELETE";
            this.uiDeleteExtension.TranslationString = "SETTINGS.BUTTON.DELETE";
            this.uiDeleteExtension.UseVisualStyleBackColor = true;
            this.uiDeleteExtension.Click += new System.EventHandler(this.uiDeleteExtension_Click);
            // 
            // uiClearExtensions
            // 
            this.uiClearExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiClearExtensions.DefaultString = null;
            this.uiClearExtensions.Location = new System.Drawing.Point(316, 152);
            this.uiClearExtensions.Name = "uiClearExtensions";
            this.uiClearExtensions.Size = new System.Drawing.Size(75, 23);
            this.uiClearExtensions.TabIndex = 8;
            this.uiClearExtensions.Text = "SETTINGS.BUTTON.CLEAR";
            this.uiClearExtensions.TranslationString = "SETTINGS.BUTTON.CLEAR";
            this.uiClearExtensions.UseVisualStyleBackColor = true;
            this.uiClearExtensions.Click += new System.EventHandler(this.uiClearExtensions_Click);
            // 
            // uiDefaultExtensions
            // 
            this.uiDefaultExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiDefaultExtensions.DefaultString = null;
            this.uiDefaultExtensions.Location = new System.Drawing.Point(397, 152);
            this.uiDefaultExtensions.Name = "uiDefaultExtensions";
            this.uiDefaultExtensions.Size = new System.Drawing.Size(75, 23);
            this.uiDefaultExtensions.TabIndex = 9;
            this.uiDefaultExtensions.Text = "SETTINGS.BUTTON.DEFAULT";
            this.uiDefaultExtensions.TranslationString = "SETTINGS.BUTTON.DEFAULT";
            this.uiDefaultExtensions.UseVisualStyleBackColor = true;
            this.uiDefaultExtensions.Click += new System.EventHandler(this.uiDefaultExtensions_Click);
            // 
            // uiMusicExtension
            // 
            this.uiMusicExtension.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiMusicExtension.Location = new System.Drawing.Point(316, 98);
            this.uiMusicExtension.Name = "uiMusicExtension";
            this.uiMusicExtension.Size = new System.Drawing.Size(156, 19);
            this.uiMusicExtension.TabIndex = 5;
            this.uiMusicExtension.KeyUp += new System.Windows.Forms.KeyEventHandler(this.uiMusicExtension_KeyUp);
            // 
            // uiMoveDown
            // 
            this.uiMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiMoveDown.DefaultString = null;
            this.uiMoveDown.Location = new System.Drawing.Point(397, 181);
            this.uiMoveDown.Name = "uiMoveDown";
            this.uiMoveDown.Size = new System.Drawing.Size(75, 23);
            this.uiMoveDown.TabIndex = 11;
            this.uiMoveDown.Text = "SETTINGS.BUTTON.MOVE_DOWN";
            this.uiMoveDown.TranslationString = "SETTINGS.BUTTON.MOVE_DOWN";
            this.uiMoveDown.UseVisualStyleBackColor = true;
            this.uiMoveDown.Click += new System.EventHandler(this.uiMoveDown_Click);
            // 
            // uiMoveUp
            // 
            this.uiMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiMoveUp.DefaultString = null;
            this.uiMoveUp.Location = new System.Drawing.Point(316, 181);
            this.uiMoveUp.Name = "uiMoveUp";
            this.uiMoveUp.Size = new System.Drawing.Size(75, 23);
            this.uiMoveUp.TabIndex = 10;
            this.uiMoveUp.Text = "SETTINGS.BUTTON.MOVE_UP";
            this.uiMoveUp.TranslationString = "SETTINGS.BUTTON.MOVE_UP";
            this.uiMoveUp.UseVisualStyleBackColor = true;
            this.uiMoveUp.Click += new System.EventHandler(this.uiMoveUp_Click);
            // 
            // uiTitle
            // 
            this.uiTitle.DefaultString = null;
            this.uiTitle.ParentForm = this;
            this.uiTitle.TranslationString = "TITLE.SETTINGS";
            // 
            // uiScanCues
            // 
            this.uiScanCues.AutoSize = true;
            this.uiScanCues.DefaultString = null;
            this.uiScanCues.Location = new System.Drawing.Point(316, 254);
            this.uiScanCues.Name = "uiScanCues";
            this.uiScanCues.Size = new System.Drawing.Size(175, 16);
            this.uiScanCues.TabIndex = 1001;
            this.uiScanCues.Text = "SETTINGS.LABEL.SCAN_CUE";
            this.uiScanCues.TranslationString = "SETTINGS.LABEL.SCAN_CUE";
            this.uiScanCues.UseVisualStyleBackColor = true;
            // 
            // uiScanTags
            // 
            this.uiScanTags.AutoSize = true;
            this.uiScanTags.DefaultString = null;
            this.uiScanTags.Location = new System.Drawing.Point(316, 232);
            this.uiScanTags.Name = "uiScanTags";
            this.uiScanTags.Size = new System.Drawing.Size(182, 16);
            this.uiScanTags.TabIndex = 1002;
            this.uiScanTags.Text = "SETTINGS.LABEL.SCAN_TAGS";
            this.uiScanTags.TranslationString = "SETTINGS.LABEL.SCAN_TAGS";
            this.uiScanTags.UseVisualStyleBackColor = true;
            // 
            // uiScanFilenames
            // 
            this.uiScanFilenames.AutoSize = true;
            this.uiScanFilenames.DefaultString = null;
            this.uiScanFilenames.Location = new System.Drawing.Point(316, 210);
            this.uiScanFilenames.Name = "uiScanFilenames";
            this.uiScanFilenames.Size = new System.Drawing.Size(214, 16);
            this.uiScanFilenames.TabIndex = 1003;
            this.uiScanFilenames.Text = "SETTINGS.LABEL.SCAN_FILENAMES";
            this.uiScanFilenames.TranslationString = "SETTINGS.LABEL.SCAN_FILENAMES";
            this.uiScanFilenames.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.uiScanCues);
            this.Controls.Add(this.uiScanTags);
            this.Controls.Add(this.uiScanFilenames);
            this.Controls.Add(this.uiMoveDown);
            this.Controls.Add(this.uiMoveUp);
            this.Controls.Add(this.uiMusicExtension);
            this.Controls.Add(this.uiDefaultExtensions);
            this.Controls.Add(this.uiClearExtensions);
            this.Controls.Add(this.uiDeleteExtension);
            this.Controls.Add(this.uiAddExtension);
            this.Controls.Add(this.translatableLabel3);
            this.Controls.Add(this.uiMusicExtensions);
            this.Controls.Add(this.uiDestinationPathBrowse);
            this.Controls.Add(this.uiDestinationPath);
            this.Controls.Add(this.translatableLabel2);
            this.Controls.Add(this.uiPlaylistPathBrowse);
            this.Controls.Add(this.uiPlaylistPath);
            this.Controls.Add(this.translatableLabel1);
            this.MinimumSize = new System.Drawing.Size(350, 250);
            this.Name = "SettingsForm";
            this.Text = "TITLE.SETTINGS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Apex.Translation.Controls.TranslatableLabel translatableLabel1;
        private System.Windows.Forms.TextBox uiPlaylistPath;
        private Apex.Translation.Controls.TranslatableButton uiPlaylistPathBrowse;
        private Apex.Translation.Controls.TranslatableButton uiDestinationPathBrowse;
        private System.Windows.Forms.TextBox uiDestinationPath;
        private Apex.Translation.Controls.TranslatableLabel translatableLabel2;
        private System.Windows.Forms.ListBox uiMusicExtensions;
        private Apex.Translation.Controls.TranslatableLabel translatableLabel3;
        private Apex.Translation.Controls.TranslatableButton uiAddExtension;
        private Apex.Translation.Controls.TranslatableButton uiDeleteExtension;
        private Apex.Translation.Controls.TranslatableButton uiClearExtensions;
        private Apex.Translation.Controls.TranslatableButton uiDefaultExtensions;
        private System.Windows.Forms.TextBox uiMusicExtension;
        private Apex.Translation.Controls.TranslatableButton uiMoveDown;
        private Apex.Translation.Controls.TranslatableButton uiMoveUp;
        private Apex.Translation.Controls.TranslatableTitle uiTitle;
        private Apex.Translation.Controls.TranslatableCheckBox uiScanCues;
        private Apex.Translation.Controls.TranslatableCheckBox uiScanTags;
        private Apex.Translation.Controls.TranslatableCheckBox uiScanFilenames;
    }
}