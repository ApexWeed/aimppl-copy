namespace AIMPPL_Copy
{
    partial class BulkPlaylistFixerForm
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
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlBottomRight = new System.Windows.Forms.Panel();
            this.pnlBottomRightRight = new System.Windows.Forms.Panel();
            this.chkScanTags = new Apex.Translation.Controls.TranslatableCheckBox();
            this.pnlBottomRightLeft = new System.Windows.Forms.Panel();
            this.btnSearch = new Apex.Translation.Controls.TranslatableButton();
            this.pnlBottomLeft = new System.Windows.Forms.Panel();
            this.btnApply = new Apex.Translation.Controls.TranslatableButton();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.ptcTree = new AIMPPL_Copy.PlaylistTree.PlaylistTreeControl();
            this.nodeTextBox1 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.FileDialogue = new System.Windows.Forms.OpenFileDialog();
            this.DirectoryDialogue = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlBottom.SuspendLayout();
            this.pnlBottomRight.SuspendLayout();
            this.pnlBottomRightRight.SuspendLayout();
            this.pnlBottomRightLeft.SuspendLayout();
            this.pnlBottomLeft.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.pnlBottomRight);
            this.pnlBottom.Controls.Add(this.pnlBottomLeft);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 556);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(989, 29);
            this.pnlBottom.TabIndex = 6;
            // 
            // pnlBottomRight
            // 
            this.pnlBottomRight.Controls.Add(this.pnlBottomRightRight);
            this.pnlBottomRight.Controls.Add(this.pnlBottomRightLeft);
            this.pnlBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottomRight.Location = new System.Drawing.Point(329, 0);
            this.pnlBottomRight.Name = "pnlBottomRight";
            this.pnlBottomRight.Size = new System.Drawing.Size(660, 29);
            this.pnlBottomRight.TabIndex = 2;
            // 
            // pnlBottomRightRight
            // 
            this.pnlBottomRightRight.Controls.Add(this.chkScanTags);
            this.pnlBottomRightRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottomRightRight.Location = new System.Drawing.Point(330, 0);
            this.pnlBottomRightRight.Name = "pnlBottomRightRight";
            this.pnlBottomRightRight.Size = new System.Drawing.Size(330, 29);
            this.pnlBottomRightRight.TabIndex = 2;
            // 
            // chkScanTags
            // 
            this.chkScanTags.AutoSize = true;
            this.chkScanTags.DefaultString = null;
            this.chkScanTags.Location = new System.Drawing.Point(6, 7);
            this.chkScanTags.Name = "chkScanTags";
            this.chkScanTags.Size = new System.Drawing.Size(200, 16);
            this.chkScanTags.TabIndex = 0;
            this.chkScanTags.Text = "FIX_PLAYLIST.LABEL.SCAN_TAGS";
            this.chkScanTags.TranslationString = "FIX_PLAYLIST.LABEL.SCAN_TAGS";
            this.chkScanTags.UseVisualStyleBackColor = true;
            this.chkScanTags.Click += new System.EventHandler(this.chkScanTags_Click);
            // 
            // pnlBottomRightLeft
            // 
            this.pnlBottomRightLeft.Controls.Add(this.btnSearch);
            this.pnlBottomRightLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBottomRightLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlBottomRightLeft.Name = "pnlBottomRightLeft";
            this.pnlBottomRightLeft.Size = new System.Drawing.Size(330, 29);
            this.pnlBottomRightLeft.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.DefaultString = null;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Location = new System.Drawing.Point(0, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(330, 29);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "FIX_PLAYLIST.BUTTON.SEARCH";
            this.btnSearch.TranslationString = "FIX_PLAYLIST.BUTTON.SEARCH";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlBottomLeft
            // 
            this.pnlBottomLeft.Controls.Add(this.btnApply);
            this.pnlBottomLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBottomLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlBottomLeft.Name = "pnlBottomLeft";
            this.pnlBottomLeft.Size = new System.Drawing.Size(329, 29);
            this.pnlBottomLeft.TabIndex = 1;
            // 
            // btnApply
            // 
            this.btnApply.DefaultString = null;
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnApply.Location = new System.Drawing.Point(0, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(329, 29);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "FIX_PLAYLIST.BUTTON.APPLY";
            this.btnApply.TranslationString = "FIX_PLAYLIST.BUTTON.APPLY";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.ptcTree);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(989, 556);
            this.pnlTop.TabIndex = 7;
            // 
            // ptcTree
            // 
            this.ptcTree.DefaultDestination = "Destination";
            this.ptcTree.DefaultName = "Name";
            this.ptcTree.DefaultSource = "Source";
            this.ptcTree.DestinationString = "FIX_BATCH_PLAYLIST.LABEL.COLUMN.DESTINATION";
            this.ptcTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ptcTree.Location = new System.Drawing.Point(0, 0);
            this.ptcTree.Name = "ptcTree";
            this.ptcTree.NameString = "FIX_BATCH_PLAYLIST.LABEL.COLUMN.NAME";
            this.ptcTree.Size = new System.Drawing.Size(989, 556);
            this.ptcTree.SourceString = "FIX_BATCH_PLAYLIST.LABEL.COLUMN.SOURCE";
            this.ptcTree.TabIndex = 0;
            this.ptcTree.DestinationClicked += new System.EventHandler<AIMPPL_Copy.PlaylistTree.PlaylistTreeControl.DestinationClickedEventArgs>(this.ptcTree_DestinationClicked);
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
            // BulkPlaylistFixerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 585);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBottom);
            this.Name = "BulkPlaylistFixerForm";
            this.Text = "BulkPlaylistFixerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BulkPlaylistFixerForm_FormClosing);
            this.Load += new System.EventHandler(this.BulkPlaylistFixerForm_Load);
            this.SizeChanged += new System.EventHandler(this.BulkPlaylistFixerForm_SizeChanged);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottomRight.ResumeLayout(false);
            this.pnlBottomRightRight.ResumeLayout(false);
            this.pnlBottomRightRight.PerformLayout();
            this.pnlBottomRightLeft.ResumeLayout(false);
            this.pnlBottomLeft.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlBottomRight;
        private Apex.Translation.Controls.TranslatableButton btnSearch;
        private System.Windows.Forms.Panel pnlBottomLeft;
        private Apex.Translation.Controls.TranslatableButton btnApply;
        private System.Windows.Forms.Panel pnlTop;
        private PlaylistTree.PlaylistTreeControl ptcTree;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox1;
        private System.Windows.Forms.OpenFileDialog FileDialogue;
        private System.Windows.Forms.FolderBrowserDialog DirectoryDialogue;
        private System.Windows.Forms.Panel pnlBottomRightRight;
        private Apex.Translation.Controls.TranslatableCheckBox chkScanTags;
        private System.Windows.Forms.Panel pnlBottomRightLeft;
    }
}