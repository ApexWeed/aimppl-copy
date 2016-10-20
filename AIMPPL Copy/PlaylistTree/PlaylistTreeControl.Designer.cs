﻿namespace AIMPPL_Copy.PlaylistTree
{
    partial class PlaylistTreeControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView = new Aga.Controls.Tree.TreeViewAdv();
            this.colName = new Aga.Controls.Tree.TreeColumn();
            this.colSource = new Aga.Controls.Tree.TreeColumn();
            this.colDestination = new Aga.Controls.Tree.TreeColumn();
            this.ntbName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.ncbCheck = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.nbtSource = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nbtDestination = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.BackColor = System.Drawing.SystemColors.Window;
            this.treeView.Columns.Add(this.colName);
            this.treeView.Columns.Add(this.colSource);
            this.treeView.Columns.Add(this.colDestination);
            this.treeView.DefaultToolTipProvider = null;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeView.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Model = null;
            this.treeView.Name = "treeView";
            this.treeView.NodeControls.Add(this.ncbCheck);
            this.treeView.NodeControls.Add(this.ntbName);
            this.treeView.NodeControls.Add(this.nbtSource);
            this.treeView.NodeControls.Add(this.nbtDestination);
            this.treeView.SelectedNode = null;
            this.treeView.Size = new System.Drawing.Size(274, 186);
            this.treeView.TabIndex = 0;
            this.treeView.Text = "treeViewAdv1";
            this.treeView.UseColumns = true;
            // 
            // colName
            // 
            this.colName.Header = "Name";
            this.colName.SortOrder = System.Windows.Forms.SortOrder.None;
            this.colName.TooltipText = null;
            // 
            // colSource
            // 
            this.colSource.Header = "Source";
            this.colSource.SortOrder = System.Windows.Forms.SortOrder.None;
            this.colSource.TooltipText = null;
            // 
            // colDestination
            // 
            this.colDestination.Header = "Destination";
            this.colDestination.SortOrder = System.Windows.Forms.SortOrder.None;
            this.colDestination.TooltipText = null;
            // 
            // ntbName
            // 
            this.ntbName.DataPropertyName = "Text";
            this.ntbName.IncrementalSearchEnabled = true;
            this.ntbName.LeftMargin = 3;
            this.ntbName.ParentColumn = this.colName;
            // 
            // ncbCheck
            // 
            this.ncbCheck.DataPropertyName = "IsChecked";
            this.ncbCheck.LeftMargin = 0;
            this.ncbCheck.ParentColumn = this.colName;
            // 
            // nbtSource
            // 
            this.nbtSource.DataPropertyName = "SourceFilename";
            this.nbtSource.IncrementalSearchEnabled = true;
            this.nbtSource.LeftMargin = 3;
            this.nbtSource.ParentColumn = this.colSource;
            // 
            // nbtDestination
            // 
            this.nbtDestination.DataPropertyName = "DestinationFilename";
            this.nbtDestination.IncrementalSearchEnabled = true;
            this.nbtDestination.LeftMargin = 3;
            this.nbtDestination.ParentColumn = this.colDestination;
            // 
            // PlaylistTreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Name = "PlaylistTreeControl";
            this.Size = new System.Drawing.Size(274, 186);
            this.ResumeLayout(false);

        }

        #endregion

        private Aga.Controls.Tree.TreeViewAdv treeView;
        private Aga.Controls.Tree.TreeColumn colName;
        private Aga.Controls.Tree.TreeColumn colSource;
        private Aga.Controls.Tree.TreeColumn colDestination;
        private Aga.Controls.Tree.NodeControls.NodeTextBox ntbName;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox ncbCheck;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nbtSource;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nbtDestination;
    }
}
