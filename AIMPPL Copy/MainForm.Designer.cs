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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lstPlaylists = new System.Windows.Forms.ListBox();
            this.treSongs = new System.Windows.Forms.TreeView();
            this.imlCovers = new System.Windows.Forms.ImageList(this.components);
            this.pgbProgress = new Apex.Controls.InfoProgressBar();
            this.btnCopy = new Apex.Translation.Controls.TranslatableButton();
            this.rdbScans = new Apex.Translation.Controls.TranslatableRadioButton();
            this.rdbAlbums = new Apex.Translation.Controls.TranslatableRadioButton();
            this.rdbSongs = new Apex.Translation.Controls.TranslatableRadioButton();
            this.grpGroup = new Apex.Translation.Controls.TranslatableGroupBox();
            this.lblGroupScanSize = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblGroupCoverSize = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblGroupDuration = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblGroupSongCount = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblGroupSize = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.grpSong = new Apex.Translation.Controls.TranslatableGroupBox();
            this.lblSongDuration = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblSongTrackNo = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblSongTitle = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblSongAlbum = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblSongArtist = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblSongSize = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.grpPlaylist = new Apex.Translation.Controls.TranslatableGroupBox();
            this.lblGroups = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblScanSize = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblSongCount = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblDuration = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblSize = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblCoverSize = new Apex.Translation.Controls.TranslatableLabelFormat();
            this.lblSongs = new Apex.Translation.Controls.TranslatableLabel();
            this.lblPlaylists = new Apex.Translation.Controls.TranslatableLabel();
            this.MainTitle = new Apex.Translation.Controls.TranslatableTitle();
            this.btnFixPlaylist = new Apex.Translation.Controls.TranslatableButton();
            this.TooltipTranslator = new Apex.Translation.Controls.TranslatableTooltips();
            this.Tooltips = new System.Windows.Forms.ToolTip(this.components);
            this.chkBulkMode = new Apex.Translation.Controls.TranslatableCheckBox();
            this.grpGroup.SuspendLayout();
            this.grpSong.SuspendLayout();
            this.grpPlaylist.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstPlaylists
            // 
            this.lstPlaylists.FormattingEnabled = true;
            this.lstPlaylists.ItemHeight = 12;
            this.lstPlaylists.Location = new System.Drawing.Point(14, 24);
            this.lstPlaylists.Name = "lstPlaylists";
            this.lstPlaylists.Size = new System.Drawing.Size(193, 460);
            this.lstPlaylists.TabIndex = 0;
            this.lstPlaylists.SelectedIndexChanged += new System.EventHandler(this.lstPlaylists_SelectedIndexChanged);
            // 
            // treSongs
            // 
            this.treSongs.ImageIndex = 0;
            this.treSongs.ImageList = this.imlCovers;
            this.treSongs.Location = new System.Drawing.Point(213, 24);
            this.treSongs.Name = "treSongs";
            this.treSongs.SelectedImageIndex = 0;
            this.treSongs.Size = new System.Drawing.Size(316, 460);
            this.treSongs.TabIndex = 7;
            this.treSongs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treSongs_AfterSelect);
            // 
            // imlCovers
            // 
            this.imlCovers.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlCovers.ImageStream")));
            this.imlCovers.TransparentColor = System.Drawing.Color.Transparent;
            this.imlCovers.Images.SetKeyName(0, "16x16透明.png");
            this.imlCovers.Images.SetKeyName(1, "question.png");
            // 
            // pgbProgress
            // 
            this.pgbProgress.CustomText = null;
            this.pgbProgress.DisplayStyle = Apex.Controls.ProgressBarDisplayText.CustomText;
            this.pgbProgress.Location = new System.Drawing.Point(616, 461);
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.Size = new System.Drawing.Size(156, 23);
            this.pgbProgress.TabIndex = 17;
            // 
            // btnCopy
            // 
            this.btnCopy.DefaultString = null;
            this.btnCopy.Location = new System.Drawing.Point(535, 461);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 16;
            this.btnCopy.Text = "MAIN.BUTTON.COPY";
            this.btnCopy.TranslationString = "MAIN.BUTTON.COPY";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // rdbScans
            // 
            this.rdbScans.AutoSize = true;
            this.rdbScans.DefaultString = null;
            this.rdbScans.Location = new System.Drawing.Point(535, 386);
            this.rdbScans.Name = "rdbScans";
            this.rdbScans.Size = new System.Drawing.Size(162, 16);
            this.rdbScans.TabIndex = 15;
            this.rdbScans.Text = "MAIN.LABEL.COPY_SCANS";
            this.rdbScans.TranslationString = "MAIN.LABEL.COPY_SCANS";
            this.rdbScans.UseVisualStyleBackColor = true;
            // 
            // rdbAlbums
            // 
            this.rdbAlbums.AutoSize = true;
            this.rdbAlbums.Checked = true;
            this.rdbAlbums.DefaultString = null;
            this.rdbAlbums.Location = new System.Drawing.Point(535, 364);
            this.rdbAlbums.Name = "rdbAlbums";
            this.rdbAlbums.Size = new System.Drawing.Size(170, 16);
            this.rdbAlbums.TabIndex = 14;
            this.rdbAlbums.TabStop = true;
            this.rdbAlbums.Text = "MAIN.LABEL.COPY_ALBUMS";
            this.rdbAlbums.TranslationString = "MAIN.LABEL.COPY_ALBUMS";
            this.rdbAlbums.UseVisualStyleBackColor = true;
            // 
            // rdbSongs
            // 
            this.rdbSongs.AutoSize = true;
            this.rdbSongs.DefaultString = null;
            this.rdbSongs.Location = new System.Drawing.Point(535, 342);
            this.rdbSongs.Name = "rdbSongs";
            this.rdbSongs.Size = new System.Drawing.Size(162, 16);
            this.rdbSongs.TabIndex = 13;
            this.rdbSongs.Text = "MAIN.LABEL.COPY_SONGS";
            this.rdbSongs.TranslationString = "MAIN.LABEL.COPY_SONGS";
            this.rdbSongs.UseVisualStyleBackColor = true;
            // 
            // grpGroup
            // 
            this.grpGroup.Controls.Add(this.lblGroupScanSize);
            this.grpGroup.Controls.Add(this.lblGroupCoverSize);
            this.grpGroup.Controls.Add(this.lblGroupDuration);
            this.grpGroup.Controls.Add(this.lblGroupSongCount);
            this.grpGroup.Controls.Add(this.lblGroupSize);
            this.grpGroup.DefaultString = null;
            this.grpGroup.Location = new System.Drawing.Point(535, 130);
            this.grpGroup.Name = "grpGroup";
            this.grpGroup.Size = new System.Drawing.Size(237, 100);
            this.grpGroup.TabIndex = 12;
            this.grpGroup.TabStop = false;
            this.grpGroup.Text = "MAIN.LABEL.GROUP_GROUP";
            this.grpGroup.TranslationString = "MAIN.LABEL.GROUP_GROUP";
            // 
            // lblGroupScanSize
            // 
            this.lblGroupScanSize.AutoSize = true;
            this.lblGroupScanSize.DefaultString = null;
            this.lblGroupScanSize.Location = new System.Drawing.Point(6, 63);
            this.lblGroupScanSize.Name = "lblGroupScanSize";
            this.lblGroupScanSize.Parameters = new object[0];
            this.lblGroupScanSize.Size = new System.Drawing.Size(174, 12);
            this.lblGroupScanSize.TabIndex = 6;
            this.lblGroupScanSize.Text = "MAIN.LABEL.GROUP_SCAN_SIZE";
            this.lblGroupScanSize.TranslationString = "MAIN.LABEL.GROUP_SCAN_SIZE";
            // 
            // lblGroupCoverSize
            // 
            this.lblGroupCoverSize.AutoSize = true;
            this.lblGroupCoverSize.DefaultString = null;
            this.lblGroupCoverSize.Location = new System.Drawing.Point(6, 51);
            this.lblGroupCoverSize.Name = "lblGroupCoverSize";
            this.lblGroupCoverSize.Parameters = new object[0];
            this.lblGroupCoverSize.Size = new System.Drawing.Size(182, 12);
            this.lblGroupCoverSize.TabIndex = 5;
            this.lblGroupCoverSize.Text = "MAIN.LABEL.GROUP_COVER_SIZE";
            this.lblGroupCoverSize.TranslationString = "MAIN.LABEL.GROUP_COVER_SIZE";
            // 
            // lblGroupDuration
            // 
            this.lblGroupDuration.AutoSize = true;
            this.lblGroupDuration.DefaultString = null;
            this.lblGroupDuration.Location = new System.Drawing.Point(6, 39);
            this.lblGroupDuration.Name = "lblGroupDuration";
            this.lblGroupDuration.Parameters = new object[0];
            this.lblGroupDuration.Size = new System.Drawing.Size(173, 12);
            this.lblGroupDuration.TabIndex = 4;
            this.lblGroupDuration.Text = "MAIN.LABEL.GROUP_DURATION";
            this.lblGroupDuration.TranslationString = "MAIN.LABEL.GROUP_DURATION";
            // 
            // lblGroupSongCount
            // 
            this.lblGroupSongCount.AutoSize = true;
            this.lblGroupSongCount.DefaultString = null;
            this.lblGroupSongCount.Location = new System.Drawing.Point(6, 27);
            this.lblGroupSongCount.Name = "lblGroupSongCount";
            this.lblGroupSongCount.Parameters = new object[0];
            this.lblGroupSongCount.Size = new System.Drawing.Size(189, 12);
            this.lblGroupSongCount.TabIndex = 3;
            this.lblGroupSongCount.Text = "MAIN.LABEL.GROUP_SONG_COUNT";
            this.lblGroupSongCount.TranslationString = "MAIN.LABEL.GROUP_SONG_COUNT";
            // 
            // lblGroupSize
            // 
            this.lblGroupSize.AutoSize = true;
            this.lblGroupSize.DefaultString = null;
            this.lblGroupSize.Location = new System.Drawing.Point(6, 15);
            this.lblGroupSize.Name = "lblGroupSize";
            this.lblGroupSize.Parameters = new object[0];
            this.lblGroupSize.Size = new System.Drawing.Size(139, 12);
            this.lblGroupSize.TabIndex = 2;
            this.lblGroupSize.Text = "MAIN.LABEL.GROUP_SIZE";
            this.lblGroupSize.TranslationString = "MAIN.LABEL.GROUP_SIZE";
            // 
            // grpSong
            // 
            this.grpSong.Controls.Add(this.lblSongDuration);
            this.grpSong.Controls.Add(this.lblSongTrackNo);
            this.grpSong.Controls.Add(this.lblSongTitle);
            this.grpSong.Controls.Add(this.lblSongAlbum);
            this.grpSong.Controls.Add(this.lblSongArtist);
            this.grpSong.Controls.Add(this.lblSongSize);
            this.grpSong.DefaultString = null;
            this.grpSong.Location = new System.Drawing.Point(535, 236);
            this.grpSong.Name = "grpSong";
            this.grpSong.Size = new System.Drawing.Size(237, 100);
            this.grpSong.TabIndex = 11;
            this.grpSong.TabStop = false;
            this.grpSong.Text = "MAIN.LABEL.SONG_GROUP";
            this.grpSong.TranslationString = "MAIN.LABEL.SONG_GROUP";
            // 
            // lblSongDuration
            // 
            this.lblSongDuration.AutoSize = true;
            this.lblSongDuration.DefaultString = null;
            this.lblSongDuration.Location = new System.Drawing.Point(6, 75);
            this.lblSongDuration.Name = "lblSongDuration";
            this.lblSongDuration.Parameters = new object[0];
            this.lblSongDuration.Size = new System.Drawing.Size(165, 12);
            this.lblSongDuration.TabIndex = 5;
            this.lblSongDuration.Text = "MAIN.LABEL.SONG_DURATION";
            this.lblSongDuration.TranslationString = "MAIN.LABEL.SONG_DURATION";
            // 
            // lblSongTrackNo
            // 
            this.lblSongTrackNo.AutoSize = true;
            this.lblSongTrackNo.DefaultString = null;
            this.lblSongTrackNo.Location = new System.Drawing.Point(6, 63);
            this.lblSongTrackNo.Name = "lblSongTrackNo";
            this.lblSongTrackNo.Parameters = new object[0];
            this.lblSongTrackNo.Size = new System.Drawing.Size(161, 12);
            this.lblSongTrackNo.TabIndex = 4;
            this.lblSongTrackNo.Text = "MAIN.LABEL.SONG_TRACKNO";
            this.lblSongTrackNo.TranslationString = "MAIN.LABEL.SONG_TRACKNO";
            // 
            // lblSongTitle
            // 
            this.lblSongTitle.AutoSize = true;
            this.lblSongTitle.DefaultString = null;
            this.lblSongTitle.Location = new System.Drawing.Point(6, 39);
            this.lblSongTitle.Name = "lblSongTitle";
            this.lblSongTitle.Parameters = new object[0];
            this.lblSongTitle.Size = new System.Drawing.Size(137, 12);
            this.lblSongTitle.TabIndex = 3;
            this.lblSongTitle.Text = "MAIN.LABEL.SONG_TITLE";
            this.lblSongTitle.TranslationString = "MAIN.LABEL.SONG_TITLE";
            // 
            // lblSongAlbum
            // 
            this.lblSongAlbum.AutoSize = true;
            this.lblSongAlbum.DefaultString = null;
            this.lblSongAlbum.Location = new System.Drawing.Point(6, 27);
            this.lblSongAlbum.Name = "lblSongAlbum";
            this.lblSongAlbum.Parameters = new object[0];
            this.lblSongAlbum.Size = new System.Drawing.Size(146, 12);
            this.lblSongAlbum.TabIndex = 2;
            this.lblSongAlbum.Text = "MAIN.LABEL.SONG_ALBUM";
            this.lblSongAlbum.TranslationString = "MAIN.LABEL.SONG_ALBUM";
            // 
            // lblSongArtist
            // 
            this.lblSongArtist.AutoSize = true;
            this.lblSongArtist.DefaultString = null;
            this.lblSongArtist.Location = new System.Drawing.Point(6, 15);
            this.lblSongArtist.Name = "lblSongArtist";
            this.lblSongArtist.Parameters = new object[0];
            this.lblSongArtist.Size = new System.Drawing.Size(147, 12);
            this.lblSongArtist.TabIndex = 1;
            this.lblSongArtist.Text = "MAIN.LABEL.SONG_ARTIST";
            this.lblSongArtist.TranslationString = "MAIN.LABEL.SONG_ARTIST";
            // 
            // lblSongSize
            // 
            this.lblSongSize.AutoSize = true;
            this.lblSongSize.DefaultString = null;
            this.lblSongSize.Location = new System.Drawing.Point(6, 51);
            this.lblSongSize.Name = "lblSongSize";
            this.lblSongSize.Parameters = new object[0];
            this.lblSongSize.Size = new System.Drawing.Size(131, 12);
            this.lblSongSize.TabIndex = 0;
            this.lblSongSize.Text = "MAIN.LABEL.SONG_SIZE";
            this.lblSongSize.TranslationString = "MAIN.LABEL.SONG_SIZE";
            // 
            // grpPlaylist
            // 
            this.grpPlaylist.Controls.Add(this.lblGroups);
            this.grpPlaylist.Controls.Add(this.lblScanSize);
            this.grpPlaylist.Controls.Add(this.lblSongCount);
            this.grpPlaylist.Controls.Add(this.lblDuration);
            this.grpPlaylist.Controls.Add(this.lblSize);
            this.grpPlaylist.Controls.Add(this.lblCoverSize);
            this.grpPlaylist.DefaultString = null;
            this.grpPlaylist.Location = new System.Drawing.Point(535, 24);
            this.grpPlaylist.Name = "grpPlaylist";
            this.grpPlaylist.Size = new System.Drawing.Size(237, 100);
            this.grpPlaylist.TabIndex = 10;
            this.grpPlaylist.TabStop = false;
            this.grpPlaylist.Text = "MAIN.LABEL.PLAYLIST_GROUP";
            this.grpPlaylist.TranslationString = "MAIN.LABEL.PLAYLIST_GROUP";
            // 
            // lblGroups
            // 
            this.lblGroups.AutoSize = true;
            this.lblGroups.DefaultString = "Groups: {0}";
            this.lblGroups.Location = new System.Drawing.Point(6, 15);
            this.lblGroups.Name = "lblGroups";
            this.lblGroups.Parameters = new object[0];
            this.lblGroups.Size = new System.Drawing.Size(154, 12);
            this.lblGroups.TabIndex = 2;
            this.lblGroups.Text = "MAIN.LABEL.GROUP_COUNT";
            this.lblGroups.TranslationString = "MAIN.LABEL.GROUP_COUNT";
            // 
            // lblScanSize
            // 
            this.lblScanSize.AutoSize = true;
            this.lblScanSize.DefaultString = "Songs: {0}";
            this.lblScanSize.Location = new System.Drawing.Point(6, 75);
            this.lblScanSize.Name = "lblScanSize";
            this.lblScanSize.Parameters = new object[0];
            this.lblScanSize.Size = new System.Drawing.Size(186, 12);
            this.lblScanSize.TabIndex = 9;
            this.lblScanSize.Text = "MAIN.LABEL.PLAYLIST_SCAN_SIZE";
            this.lblScanSize.TranslationString = "MAIN.LABEL.PLAYLIST_SCAN_SIZE";
            // 
            // lblSongCount
            // 
            this.lblSongCount.AutoSize = true;
            this.lblSongCount.DefaultString = "Songs: {0}";
            this.lblSongCount.Location = new System.Drawing.Point(6, 27);
            this.lblSongCount.Name = "lblSongCount";
            this.lblSongCount.Parameters = new object[0];
            this.lblSongCount.Size = new System.Drawing.Size(146, 12);
            this.lblSongCount.TabIndex = 3;
            this.lblSongCount.Text = "MAIN.LABEL.SONG_COUNT";
            this.lblSongCount.TranslationString = "MAIN.LABEL.SONG_COUNT";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.DefaultString = "Songs: {0}";
            this.lblDuration.Location = new System.Drawing.Point(6, 39);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Parameters = new object[0];
            this.lblDuration.Size = new System.Drawing.Size(185, 12);
            this.lblDuration.TabIndex = 4;
            this.lblDuration.Text = "MAIN.LABEL.PLAYLIST_DURATION";
            this.lblDuration.TranslationString = "MAIN.LABEL.PLAYLIST_DURATION";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.DefaultString = "Songs: {0}";
            this.lblSize.Location = new System.Drawing.Point(6, 51);
            this.lblSize.Name = "lblSize";
            this.lblSize.Parameters = new object[0];
            this.lblSize.Size = new System.Drawing.Size(151, 12);
            this.lblSize.TabIndex = 5;
            this.lblSize.Text = "MAIN.LABEL.PLAYLIST_SIZE";
            this.lblSize.TranslationString = "MAIN.LABEL.PLAYLIST_SIZE";
            // 
            // lblCoverSize
            // 
            this.lblCoverSize.AutoSize = true;
            this.lblCoverSize.DefaultString = "Songs: {0}";
            this.lblCoverSize.Location = new System.Drawing.Point(6, 63);
            this.lblCoverSize.Name = "lblCoverSize";
            this.lblCoverSize.Parameters = new object[0];
            this.lblCoverSize.Size = new System.Drawing.Size(194, 12);
            this.lblCoverSize.TabIndex = 6;
            this.lblCoverSize.Text = "MAIN.LABEL.PLAYLIST_COVER_SIZE";
            this.lblCoverSize.TranslationString = "MAIN.LABEL.PLAYLIST_COVER_SIZE";
            // 
            // lblSongs
            // 
            this.lblSongs.AutoSize = true;
            this.lblSongs.DefaultString = null;
            this.lblSongs.Location = new System.Drawing.Point(211, 9);
            this.lblSongs.Name = "lblSongs";
            this.lblSongs.Size = new System.Drawing.Size(110, 12);
            this.lblSongs.TabIndex = 8;
            this.lblSongs.Text = "MAIN.LABEL.SONGS";
            this.lblSongs.TranslationString = "MAIN.LABEL.SONGS";
            // 
            // lblPlaylists
            // 
            this.lblPlaylists.AutoSize = true;
            this.lblPlaylists.DefaultString = null;
            this.lblPlaylists.Location = new System.Drawing.Point(12, 9);
            this.lblPlaylists.Name = "lblPlaylists";
            this.lblPlaylists.Size = new System.Drawing.Size(130, 12);
            this.lblPlaylists.TabIndex = 1;
            this.lblPlaylists.Text = "MAIN.LABEL.PLAYLISTS";
            this.lblPlaylists.TranslationString = "MAIN.LABEL.PLAYLISTS";
            // 
            // MainTitle
            // 
            this.MainTitle.DefaultString = null;
            this.MainTitle.ParentForm = this;
            this.MainTitle.TranslationString = "TITLE.MAIN";
            // 
            // btnFixPlaylist
            // 
            this.btnFixPlaylist.DefaultString = null;
            this.btnFixPlaylist.Location = new System.Drawing.Point(535, 432);
            this.btnFixPlaylist.Name = "btnFixPlaylist";
            this.btnFixPlaylist.Size = new System.Drawing.Size(237, 23);
            this.btnFixPlaylist.TabIndex = 18;
            this.btnFixPlaylist.Text = "MAIN.BUTTON.FIX_PLAYLIST";
            this.btnFixPlaylist.TranslationString = "MAIN.BUTTON.FIX_PLAYLIST";
            this.btnFixPlaylist.UseVisualStyleBackColor = true;
            this.btnFixPlaylist.Click += new System.EventHandler(this.btnFixPlaylist_Click);
            // 
            // TooltipTranslator
            // 
            this.TooltipTranslator.DefaultStrings = ((System.Collections.Generic.Dictionary<System.Windows.Forms.Control, string>)(resources.GetObject("TooltipTranslator.DefaultStrings")));
            this.TooltipTranslator.ToolTips = this.Tooltips;
            this.TooltipTranslator.TranslationStrings = ((System.Collections.Generic.Dictionary<System.Windows.Forms.Control, string>)(resources.GetObject("TooltipTranslator.TranslationStrings")));
            // 
            // chkBulkMode
            // 
            this.chkBulkMode.AutoSize = true;
            this.chkBulkMode.DefaultString = null;
            this.chkBulkMode.Location = new System.Drawing.Point(535, 408);
            this.chkBulkMode.Name = "chkBulkMode";
            this.chkBulkMode.Size = new System.Drawing.Size(175, 16);
            this.chkBulkMode.TabIndex = 19;
            this.chkBulkMode.Text = "MAIN.LABEL.BULK_PLAYLIST";
            this.chkBulkMode.TranslationString = "MAIN.LABEL.BULK_PLAYLIST";
            this.chkBulkMode.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 498);
            this.Controls.Add(this.chkBulkMode);
            this.Controls.Add(this.btnFixPlaylist);
            this.Controls.Add(this.pgbProgress);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.rdbScans);
            this.Controls.Add(this.rdbAlbums);
            this.Controls.Add(this.rdbSongs);
            this.Controls.Add(this.grpGroup);
            this.Controls.Add(this.grpSong);
            this.Controls.Add(this.grpPlaylist);
            this.Controls.Add(this.lblSongs);
            this.Controls.Add(this.treSongs);
            this.Controls.Add(this.lblPlaylists);
            this.Controls.Add(this.lstPlaylists);
            this.Name = "MainForm";
            this.Text = "TITLE.MAIN";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.grpGroup.ResumeLayout(false);
            this.grpGroup.PerformLayout();
            this.grpSong.ResumeLayout(false);
            this.grpSong.PerformLayout();
            this.grpPlaylist.ResumeLayout(false);
            this.grpPlaylist.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstPlaylists;
        private Apex.Translation.Controls.TranslatableLabel lblPlaylists;
        private Apex.Translation.Controls.TranslatableLabelFormat lblGroups;
        private Apex.Translation.Controls.TranslatableLabelFormat lblSongCount;
        private Apex.Translation.Controls.TranslatableLabelFormat lblDuration;
        private Apex.Translation.Controls.TranslatableLabelFormat lblSize;
        private Apex.Translation.Controls.TranslatableLabelFormat lblCoverSize;
        private System.Windows.Forms.TreeView treSongs;
        private Apex.Translation.Controls.TranslatableLabel lblSongs;
        private System.Windows.Forms.ImageList imlCovers;
        private Apex.Translation.Controls.TranslatableLabelFormat lblScanSize;
        private Apex.Translation.Controls.TranslatableGroupBox grpPlaylist;
        private Apex.Translation.Controls.TranslatableGroupBox grpSong;
        private Apex.Translation.Controls.TranslatableLabelFormat lblSongSize;
        private Apex.Translation.Controls.TranslatableLabelFormat lblSongTrackNo;
        private Apex.Translation.Controls.TranslatableLabelFormat lblSongTitle;
        private Apex.Translation.Controls.TranslatableLabelFormat lblSongAlbum;
        private Apex.Translation.Controls.TranslatableLabelFormat lblSongArtist;
        private Apex.Translation.Controls.TranslatableGroupBox grpGroup;
        private Apex.Translation.Controls.TranslatableLabelFormat lblGroupSize;
        private Apex.Translation.Controls.TranslatableLabelFormat lblSongDuration;
        private Apex.Translation.Controls.TranslatableLabelFormat lblGroupScanSize;
        private Apex.Translation.Controls.TranslatableLabelFormat lblGroupCoverSize;
        private Apex.Translation.Controls.TranslatableLabelFormat lblGroupDuration;
        private Apex.Translation.Controls.TranslatableLabelFormat lblGroupSongCount;
        private Apex.Translation.Controls.TranslatableRadioButton rdbSongs;
        private Apex.Translation.Controls.TranslatableRadioButton rdbAlbums;
        private Apex.Translation.Controls.TranslatableRadioButton rdbScans;
        private Apex.Translation.Controls.TranslatableButton btnCopy;
        private Apex.Controls.InfoProgressBar pgbProgress;
        private Apex.Translation.Controls.TranslatableTitle MainTitle;
        private Apex.Translation.Controls.TranslatableButton btnFixPlaylist;
        private Apex.Translation.Controls.TranslatableTooltips TooltipTranslator;
        private System.Windows.Forms.ToolTip Tooltips;
        private Apex.Translation.Controls.TranslatableCheckBox chkBulkMode;
    }
}

