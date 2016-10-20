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
            this.pnlBottomLeft = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.nodeTextBox1 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.btnSearch = new Apex.Translation.Controls.TranslatableButton();
            this.btnApply = new Apex.Translation.Controls.TranslatableButton();
            this.ptcTree = new AIMPPL_Copy.PlaylistTree.PlaylistTreeControl();
            this.pnlBottom.SuspendLayout();
            this.pnlBottomRight.SuspendLayout();
            this.pnlBottomLeft.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.pnlBottomRight);
            this.pnlBottom.Controls.Add(this.pnlBottomLeft);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 464);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(783, 29);
            this.pnlBottom.TabIndex = 6;
            // 
            // pnlBottomRight
            // 
            this.pnlBottomRight.Controls.Add(this.btnSearch);
            this.pnlBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottomRight.Location = new System.Drawing.Point(480, 0);
            this.pnlBottomRight.Name = "pnlBottomRight";
            this.pnlBottomRight.Size = new System.Drawing.Size(303, 29);
            this.pnlBottomRight.TabIndex = 2;
            // 
            // pnlBottomLeft
            // 
            this.pnlBottomLeft.Controls.Add(this.btnApply);
            this.pnlBottomLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBottomLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlBottomLeft.Name = "pnlBottomLeft";
            this.pnlBottomLeft.Size = new System.Drawing.Size(480, 29);
            this.pnlBottomLeft.TabIndex = 1;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.ptcTree);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(783, 464);
            this.pnlTop.TabIndex = 7;
            // 
            // nodeTextBox1
            // 
            this.nodeTextBox1.DataPropertyName = "Text";
            this.nodeTextBox1.IncrementalSearchEnabled = true;
            this.nodeTextBox1.LeftMargin = 3;
            this.nodeTextBox1.ParentColumn = null;
            // 
            // btnSearch
            // 
            this.btnSearch.DefaultString = null;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Location = new System.Drawing.Point(0, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(303, 29);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "FIX_PLAYLIST.BUTTON.SEARCH";
            this.btnSearch.TranslationString = "FIX_PLAYLIST.BUTTON.SEARCH";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.DefaultString = null;
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnApply.Location = new System.Drawing.Point(0, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(480, 29);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "FIX_PLAYLIST.BUTTON.APPLY";
            this.btnApply.TranslationString = "FIX_PLAYLIST.BUTTON.APPLY";
            this.btnApply.UseVisualStyleBackColor = true;
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
            this.ptcTree.Size = new System.Drawing.Size(783, 464);
            this.ptcTree.SourceString = "FIX_BATCH_PLAYLIST.LABEL.COLUMN.SOURCE";
            this.ptcTree.TabIndex = 0;
            // 
            // BulkPlaylistFixerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 493);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBottom);
            this.Name = "BulkPlaylistFixerForm";
            this.Text = "BulkPlaylistFixerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BulkPlaylistFixerForm_FormClosing);
            this.Load += new System.EventHandler(this.BulkPlaylistFixerForm_Load);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottomRight.ResumeLayout(false);
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
    }
}