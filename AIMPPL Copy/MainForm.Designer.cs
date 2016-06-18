namespace AIMPPL_Copy
{
    partial class MainForm
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
            this.lstPlaylists = new System.Windows.Forms.ListBox();
            this.lblSize = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblDuration = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblSongs = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblGroups = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.translatableLabel1 = new Apex.Translation.Controls.TranslatableLabel();
            this.lblCoverSize = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.SuspendLayout();
            // 
            // lstPlaylists
            // 
            this.lstPlaylists.FormattingEnabled = true;
            this.lstPlaylists.ItemHeight = 12;
            this.lstPlaylists.Location = new System.Drawing.Point(14, 24);
            this.lstPlaylists.Name = "lstPlaylists";
            this.lstPlaylists.Size = new System.Drawing.Size(193, 436);
            this.lstPlaylists.TabIndex = 0;
            this.lstPlaylists.SelectedIndexChanged += new System.EventHandler(this.lstPlaylists_SelectedIndexChanged);
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.DefaultString = "Songs: {0}";
            this.lblSize.Location = new System.Drawing.Point(213, 60);
            this.lblSize.Name = "lblSize";
            this.lblSize.Parameters = new object[0];
            this.lblSize.Size = new System.Drawing.Size(96, 12);
            this.lblSize.TabIndex = 5;
            this.lblSize.Text = "MAIN.LABEL.SIZE";
            this.lblSize.TranslationString = "MAIN.LABEL.SIZE";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.DefaultString = "Songs: {0}";
            this.lblDuration.Location = new System.Drawing.Point(213, 48);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Parameters = new object[0];
            this.lblDuration.Size = new System.Drawing.Size(130, 12);
            this.lblDuration.TabIndex = 4;
            this.lblDuration.Text = "MAIN.LABEL.DURATION";
            this.lblDuration.TranslationString = "MAIN.LABEL.DURATION";
            // 
            // lblSongs
            // 
            this.lblSongs.AutoSize = true;
            this.lblSongs.DefaultString = "Songs: {0}";
            this.lblSongs.Location = new System.Drawing.Point(213, 36);
            this.lblSongs.Name = "lblSongs";
            this.lblSongs.Parameters = new object[0];
            this.lblSongs.Size = new System.Drawing.Size(146, 12);
            this.lblSongs.TabIndex = 3;
            this.lblSongs.Text = "MAIN.LABEL.SONG_COUNT";
            this.lblSongs.TranslationString = "MAIN.LABEL.SONG_COUNT";
            // 
            // lblGroups
            // 
            this.lblGroups.AutoSize = true;
            this.lblGroups.DefaultString = "Groups: {0}";
            this.lblGroups.Location = new System.Drawing.Point(213, 24);
            this.lblGroups.Name = "lblGroups";
            this.lblGroups.Parameters = new object[0];
            this.lblGroups.Size = new System.Drawing.Size(154, 12);
            this.lblGroups.TabIndex = 2;
            this.lblGroups.Text = "MAIN.LABEL.GROUP_COUNT";
            this.lblGroups.TranslationString = "MAIN.LABEL.GROUP_COUNT";
            // 
            // translatableLabel1
            // 
            this.translatableLabel1.AutoSize = true;
            this.translatableLabel1.DefaultString = null;
            this.translatableLabel1.Location = new System.Drawing.Point(12, 9);
            this.translatableLabel1.Name = "translatableLabel1";
            this.translatableLabel1.Size = new System.Drawing.Size(0, 12);
            this.translatableLabel1.TabIndex = 1;
            this.translatableLabel1.TranslationString = null;
            // 
            // lblCoverSize
            // 
            this.lblCoverSize.AutoSize = true;
            this.lblCoverSize.DefaultString = "Songs: {0}";
            this.lblCoverSize.Location = new System.Drawing.Point(213, 72);
            this.lblCoverSize.Name = "lblCoverSize";
            this.lblCoverSize.Parameters = new object[0];
            this.lblCoverSize.Size = new System.Drawing.Size(139, 12);
            this.lblCoverSize.TabIndex = 6;
            this.lblCoverSize.Text = "MAIN.LABEL.COVER_SIZE";
            this.lblCoverSize.TranslationString = "MAIN.LABEL.COVER_SIZE";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 477);
            this.Controls.Add(this.lblCoverSize);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblSongs);
            this.Controls.Add(this.lblGroups);
            this.Controls.Add(this.translatableLabel1);
            this.Controls.Add(this.lstPlaylists);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstPlaylists;
        private Apex.Translation.Controls.TranslatableLabel translatableLabel1;
        private Apex.Translation.Controls.TranslatableLabelFormat lblGroups;
        private Apex.Translation.Controls.TranslatableLabelFormat lblSongs;
        private Apex.Translation.Controls.TranslatableLabelFormat lblDuration;
        private Apex.Translation.Controls.TranslatableLabelFormat lblSize;
        private Apex.Translation.Controls.TranslatableLabelFormat lblCoverSize;
    }
}

