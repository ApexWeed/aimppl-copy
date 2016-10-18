﻿namespace AIMPPL_Copy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaylistFixerForm));
            this.dgvMissing = new System.Windows.Forms.DataGridView();
            this.clmChange = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDestination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBrowse = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmSongBind = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlBottomRight = new System.Windows.Forms.Panel();
            this.btnSearch = new Apex.Translation.Controls.TranslatableButton();
            this.pnlBottomLeft = new System.Windows.Forms.Panel();
            this.btnApply = new Apex.Translation.Controls.TranslatableButton();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.FileDialogue = new System.Windows.Forms.OpenFileDialog();
            this.PlaylistFixerTitle = new Apex.Translation.Controls.TranslatableTitle();
            this.ColumnHeaders = new Apex.Translation.Controls.TranslatableColumnHeaders();
            this.DirectoryDialogue = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMissing)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.pnlBottomRight.SuspendLayout();
            this.pnlBottomLeft.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMissing
            // 
            this.dgvMissing.AllowUserToAddRows = false;
            this.dgvMissing.AllowUserToDeleteRows = false;
            this.dgvMissing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMissing.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmChange,
            this.clmSource,
            this.clmDestination,
            this.clmBrowse,
            this.clmSongBind});
            this.dgvMissing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMissing.Location = new System.Drawing.Point(0, 0);
            this.dgvMissing.Name = "dgvMissing";
            this.dgvMissing.RowTemplate.Height = 21;
            this.dgvMissing.Size = new System.Drawing.Size(986, 568);
            this.dgvMissing.TabIndex = 4;
            this.dgvMissing.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMissing_CellContentClick);
            this.dgvMissing.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMissing_CellContentDoubleClick);
            this.dgvMissing.Resize += new System.EventHandler(this.dgvMissing_Resize);
            // 
            // clmChange
            // 
            this.clmChange.HeaderText = "";
            this.clmChange.Name = "clmChange";
            this.clmChange.Width = 25;
            // 
            // clmSource
            // 
            this.clmSource.HeaderText = "Source";
            this.clmSource.Name = "clmSource";
            this.clmSource.ReadOnly = true;
            this.clmSource.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // clmDestination
            // 
            this.clmDestination.HeaderText = "Destination";
            this.clmDestination.Name = "clmDestination";
            // 
            // clmBrowse
            // 
            this.clmBrowse.HeaderText = "";
            this.clmBrowse.Name = "clmBrowse";
            // 
            // clmSongBind
            // 
            this.clmSongBind.HeaderText = "";
            this.clmSongBind.Name = "clmSongBind";
            this.clmSongBind.Visible = false;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.pnlBottomRight);
            this.pnlBottom.Controls.Add(this.pnlBottomLeft);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 568);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(986, 29);
            this.pnlBottom.TabIndex = 5;
            // 
            // pnlBottomRight
            // 
            this.pnlBottomRight.Controls.Add(this.btnSearch);
            this.pnlBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottomRight.Location = new System.Drawing.Point(480, 0);
            this.pnlBottomRight.Name = "pnlBottomRight";
            this.pnlBottomRight.Size = new System.Drawing.Size(506, 29);
            this.pnlBottomRight.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.DefaultString = null;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Location = new System.Drawing.Point(0, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(506, 29);
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
            this.pnlBottomLeft.Size = new System.Drawing.Size(480, 29);
            this.pnlBottomLeft.TabIndex = 1;
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
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.dgvMissing);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(986, 568);
            this.pnlTop.TabIndex = 6;
            // 
            // FileDialogue
            // 
            this.FileDialogue.FileName = "openFileDialog1";
            // 
            // PlaylistFixerTitle
            // 
            this.PlaylistFixerTitle.DefaultString = null;
            this.PlaylistFixerTitle.ParentForm = this;
            this.PlaylistFixerTitle.TranslationString = "TITLE.FIX_PLAYLIST";
            // 
            // ColumnHeaders
            // 
            this.ColumnHeaders.DefaultStrings = ((System.Collections.Generic.Dictionary<object, string>)(resources.GetObject("ColumnHeaders.DefaultStrings")));
            this.ColumnHeaders.TranslationStrings = ((System.Collections.Generic.Dictionary<object, string>)(resources.GetObject("ColumnHeaders.TranslationStrings")));
            // 
            // PlaylistFixerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 597);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBottom);
            this.Name = "PlaylistFixerForm";
            this.Text = "TITLE.FIX_PLAYLIST";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlaylistFixerForm_FormClosing);
            this.Resize += new System.EventHandler(this.PlaylistFixerForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMissing)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottomRight.ResumeLayout(false);
            this.pnlBottomLeft.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvMissing;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDestination;
        private System.Windows.Forms.DataGridViewButtonColumn clmBrowse;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSongBind;
        private System.Windows.Forms.Panel pnlBottom;
        private Apex.Translation.Controls.TranslatableButton btnApply;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.OpenFileDialog FileDialogue;
        private Apex.Translation.Controls.TranslatableTitle PlaylistFixerTitle;
        private Apex.Translation.Controls.TranslatableColumnHeaders ColumnHeaders;
        private System.Windows.Forms.Panel pnlBottomRight;
        private Apex.Translation.Controls.TranslatableButton btnSearch;
        private System.Windows.Forms.Panel pnlBottomLeft;
        private System.Windows.Forms.FolderBrowserDialog DirectoryDialogue;
    }
}