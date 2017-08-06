namespace AIMPPL_Copy
{
    partial class PlaylistFixerForm
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
            this.uiBottom = new System.Windows.Forms.Panel();
            this.uiApplyPanel = new System.Windows.Forms.Panel();
            this.uiApply = new Apex.Translation.Controls.TranslatableButton();
            this.uiSearchPanel = new System.Windows.Forms.Panel();
            this.uiSearch = new Apex.Translation.Controls.TranslatableButton();
            this.uiScanFilenamesPanel = new System.Windows.Forms.Panel();
            this.uiScanFilenames = new Apex.Translation.Controls.TranslatableCheckBox();
            this.uiScanTagsPanel = new System.Windows.Forms.Panel();
            this.uiScanTags = new Apex.Translation.Controls.TranslatableCheckBox();
            this.uiScanCuePanel = new System.Windows.Forms.Panel();
            this.uiScanCues = new Apex.Translation.Controls.TranslatableCheckBox();
            this.uiTop = new System.Windows.Forms.Panel();
            this.uiTree = new AIMPPL_Copy.PlaylistTree.PlaylistTreeControl();
            this.nodeTextBox1 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.FileDialogue = new System.Windows.Forms.OpenFileDialog();
            this.DirectoryDialogue = new System.Windows.Forms.FolderBrowserDialog();
            this.uiTitle = new Apex.Translation.Controls.TranslatableTitle();
            this.uiStatusStrip = new System.Windows.Forms.StatusStrip();
            this.uiStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.uiBottom.SuspendLayout();
            this.uiApplyPanel.SuspendLayout();
            this.uiSearchPanel.SuspendLayout();
            this.uiScanFilenamesPanel.SuspendLayout();
            this.uiScanTagsPanel.SuspendLayout();
            this.uiScanCuePanel.SuspendLayout();
            this.uiTop.SuspendLayout();
            this.uiStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiBottom
            // 
            this.uiBottom.Controls.Add(this.uiApplyPanel);
            this.uiBottom.Controls.Add(this.uiSearchPanel);
            this.uiBottom.Controls.Add(this.uiScanFilenamesPanel);
            this.uiBottom.Controls.Add(this.uiScanCuePanel);
            this.uiBottom.Controls.Add(this.uiScanTagsPanel);
            this.uiBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiBottom.Location = new System.Drawing.Point(0, 534);
            this.uiBottom.Name = "uiBottom";
            this.uiBottom.Size = new System.Drawing.Size(989, 29);
            this.uiBottom.TabIndex = 6;
            // 
            // uiApplyPanel
            // 
            this.uiApplyPanel.Controls.Add(this.uiApply);
            this.uiApplyPanel.Location = new System.Drawing.Point(0, 0);
            this.uiApplyPanel.Name = "uiApplyPanel";
            this.uiApplyPanel.Size = new System.Drawing.Size(247, 29);
            this.uiApplyPanel.TabIndex = 1;
            // 
            // uiApply
            // 
            this.uiApply.DefaultString = null;
            this.uiApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiApply.Location = new System.Drawing.Point(0, 0);
            this.uiApply.Name = "uiApply";
            this.uiApply.Size = new System.Drawing.Size(247, 29);
            this.uiApply.TabIndex = 0;
            this.uiApply.Text = "FIX_PLAYLIST.BUTTON.APPLY";
            this.uiApply.TranslationString = "FIX_PLAYLIST.BUTTON.APPLY";
            this.uiApply.UseVisualStyleBackColor = true;
            this.uiApply.Click += new System.EventHandler(this.uiApply_Click);
            // 
            // uiSearchPanel
            // 
            this.uiSearchPanel.Controls.Add(this.uiSearch);
            this.uiSearchPanel.Location = new System.Drawing.Point(100, 0);
            this.uiSearchPanel.Name = "uiSearchPanel";
            this.uiSearchPanel.Size = new System.Drawing.Size(247, 29);
            this.uiSearchPanel.TabIndex = 1;
            // 
            // uiSearch
            // 
            this.uiSearch.DefaultString = null;
            this.uiSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiSearch.Location = new System.Drawing.Point(0, 0);
            this.uiSearch.Name = "uiSearch";
            this.uiSearch.Size = new System.Drawing.Size(247, 29);
            this.uiSearch.TabIndex = 0;
            this.uiSearch.Text = "FIX_PLAYLIST.BUTTON.SEARCH";
            this.uiSearch.TranslationString = "FIX_PLAYLIST.BUTTON.SEARCH";
            this.uiSearch.UseVisualStyleBackColor = true;
            this.uiSearch.Click += new System.EventHandler(this.uiSearch_Click);
            // 
            // uiScanFilenamesPanel
            // 
            this.uiScanFilenamesPanel.Controls.Add(this.uiScanFilenames);
            this.uiScanFilenamesPanel.Location = new System.Drawing.Point(200, 0);
            this.uiScanFilenamesPanel.Name = "uiScanFilenamesPanel";
            this.uiScanFilenamesPanel.Size = new System.Drawing.Size(224, 29);
            this.uiScanFilenamesPanel.TabIndex = 2;
            // 
            // uiScanFilenames
            // 
            this.uiScanFilenames.AutoSize = true;
            this.uiScanFilenames.DefaultString = null;
            this.uiScanFilenames.Location = new System.Drawing.Point(0, 7);
            this.uiScanFilenames.Name = "uiScanFilenames";
            this.uiScanFilenames.Size = new System.Drawing.Size(232, 16);
            this.uiScanFilenames.TabIndex = 0;
            this.uiScanFilenames.Text = "FIX_PLAYLIST.LABEL.SCAN_FILENAMES";
            this.uiScanFilenames.TranslationString = "FIX_PLAYLIST.LABEL.SCAN_FILENAMES";
            this.uiScanFilenames.UseVisualStyleBackColor = true;
            // 
            // uiScanTagsPanel
            // 
            this.uiScanTagsPanel.Controls.Add(this.uiScanTags);
            this.uiScanTagsPanel.Location = new System.Drawing.Point(400, 0);
            this.uiScanTagsPanel.Name = "uiScanTagsPanel";
            this.uiScanTagsPanel.Size = new System.Drawing.Size(224, 29);
            this.uiScanTagsPanel.TabIndex = 1;
            // 
            // uiScanTags
            // 
            this.uiScanTags.AutoSize = true;
            this.uiScanTags.DefaultString = null;
            this.uiScanTags.Location = new System.Drawing.Point(0, 7);
            this.uiScanTags.Name = "uiScanTags";
            this.uiScanTags.Size = new System.Drawing.Size(200, 16);
            this.uiScanTags.TabIndex = 0;
            this.uiScanTags.Text = "FIX_PLAYLIST.LABEL.SCAN_TAGS";
            this.uiScanTags.TranslationString = "FIX_PLAYLIST.LABEL.SCAN_TAGS";
            this.uiScanTags.UseVisualStyleBackColor = true;
            this.uiScanTags.Click += new System.EventHandler(this.uiScanTags_Click);
            // 
            // uiScanCuePanel
            // 
            this.uiScanCuePanel.Controls.Add(this.uiScanCues);
            this.uiScanCuePanel.Location = new System.Drawing.Point(300, 0);
            this.uiScanCuePanel.Name = "uiScanCuePanel";
            this.uiScanCuePanel.Size = new System.Drawing.Size(271, 29);
            this.uiScanCuePanel.TabIndex = 2;
            // 
            // uiScanCues
            // 
            this.uiScanCues.AutoSize = true;
            this.uiScanCues.DefaultString = null;
            this.uiScanCues.Location = new System.Drawing.Point(6, 7);
            this.uiScanCues.Name = "uiScanCues";
            this.uiScanCues.Size = new System.Drawing.Size(193, 16);
            this.uiScanCues.TabIndex = 0;
            this.uiScanCues.Text = "FIX_PLAYLIST.LABEL.SCAN_CUE";
            this.uiScanCues.TranslationString = "FIX_PLAYLIST.LABEL.SCAN_CUE";
            this.uiScanCues.UseVisualStyleBackColor = true;
            // 
            // uiTop
            // 
            this.uiTop.Controls.Add(this.uiTree);
            this.uiTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTop.Location = new System.Drawing.Point(0, 0);
            this.uiTop.Name = "uiTop";
            this.uiTop.Size = new System.Drawing.Size(989, 534);
            this.uiTop.TabIndex = 7;
            // 
            // uiTree
            // 
            this.uiTree.DefaultDestination = "Destination";
            this.uiTree.DefaultName = "Name";
            this.uiTree.DefaultSource = "Source";
            this.uiTree.DestinationString = "FIX_PLAYLIST.LABEL.COLUMN.DESTINATION";
            this.uiTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTree.Location = new System.Drawing.Point(0, 0);
            this.uiTree.Name = "uiTree";
            this.uiTree.NameString = "FIX_PLAYLIST.LABEL.COLUMN.NAME";
            this.uiTree.Size = new System.Drawing.Size(989, 534);
            this.uiTree.SourceString = "FIX_PLAYLIST.LABEL.COLUMN.SOURCE";
            this.uiTree.TabIndex = 0;
            this.uiTree.DestinationClicked += new System.EventHandler<AIMPPL_Copy.PlaylistTree.PlaylistTreeControl.DestinationClickedEventArgs>(this.uiTree_DestinationClicked);
            // 
            // nodeTextBox1
            // 
            this.nodeTextBox1.DataPropertyName = "Text";
            this.nodeTextBox1.IncrementalSearchEnabled = true;
            this.nodeTextBox1.LeftMargin = 3;
            this.nodeTextBox1.ParentColumn = null;
            // 
            // FileDialogue
            // 
            this.FileDialogue.FileName = "openFileDialog1";
            // 
            // DirectoryDialogue
            // 
            this.DirectoryDialogue.ShowNewFolderButton = false;
            // 
            // uiTitle
            // 
            this.uiTitle.DefaultString = null;
            this.uiTitle.ParentForm = this;
            this.uiTitle.TranslationString = "TITLE.FIX_PLAYLIST";
            // 
            // uiStatusStrip
            // 
            this.uiStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uiStatus});
            this.uiStatusStrip.Location = new System.Drawing.Point(0, 563);
            this.uiStatusStrip.Name = "uiStatusStrip";
            this.uiStatusStrip.Size = new System.Drawing.Size(989, 22);
            this.uiStatusStrip.TabIndex = 8;
            this.uiStatusStrip.Text = "statusStrip1";
            // 
            // uiStatus
            // 
            this.uiStatus.Name = "uiStatus";
            this.uiStatus.Size = new System.Drawing.Size(118, 17);
            this.uiStatus.Text = "toolStripStatusLabel1";
            // 
            // PlaylistFixerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 585);
            this.Controls.Add(this.uiTop);
            this.Controls.Add(this.uiBottom);
            this.Controls.Add(this.uiStatusStrip);
            this.Name = "PlaylistFixerForm";
            this.Text = "TITLE.FIX_PLAYLIST";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlaylistFixerForm_FormClosing);
            this.Load += new System.EventHandler(this.PlaylistFixerForm_Load);
            this.SizeChanged += new System.EventHandler(this.PlaylistFixerForm_SizeChanged);
            this.uiBottom.ResumeLayout(false);
            this.uiApplyPanel.ResumeLayout(false);
            this.uiSearchPanel.ResumeLayout(false);
            this.uiScanFilenamesPanel.ResumeLayout(false);
            this.uiScanFilenamesPanel.PerformLayout();
            this.uiScanTagsPanel.ResumeLayout(false);
            this.uiScanTagsPanel.PerformLayout();
            this.uiScanCuePanel.ResumeLayout(false);
            this.uiScanCuePanel.PerformLayout();
            this.uiTop.ResumeLayout(false);
            this.uiStatusStrip.ResumeLayout(false);
            this.uiStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel uiBottom;
        private Apex.Translation.Controls.TranslatableButton uiSearch;
        private System.Windows.Forms.Panel uiApplyPanel;
        private Apex.Translation.Controls.TranslatableButton uiApply;
        private System.Windows.Forms.Panel uiTop;
        private PlaylistTree.PlaylistTreeControl uiTree;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox1;
        private System.Windows.Forms.OpenFileDialog FileDialogue;
        private System.Windows.Forms.FolderBrowserDialog DirectoryDialogue;
        private Apex.Translation.Controls.TranslatableCheckBox uiScanTags;
        private System.Windows.Forms.Panel uiSearchPanel;
        private System.Windows.Forms.Panel uiScanCuePanel;
        private System.Windows.Forms.Panel uiScanTagsPanel;
        private Apex.Translation.Controls.TranslatableCheckBox uiScanCues;
        private Apex.Translation.Controls.TranslatableTitle uiTitle;
        private System.Windows.Forms.StatusStrip uiStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel uiStatus;
        private System.Windows.Forms.Panel uiScanFilenamesPanel;
        private Apex.Translation.Controls.TranslatableCheckBox uiScanFilenames;
    }
}