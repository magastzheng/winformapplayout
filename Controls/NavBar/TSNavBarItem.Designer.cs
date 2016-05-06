namespace Controls
{
    partial class TSNavBarItem
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
            this._button = new System.Windows.Forms.Button();
            this._tsTreeView = new Controls.TSTreeView();
            this.SuspendLayout();
            // 
            // _button
            // 
            this._button.Dock = System.Windows.Forms.DockStyle.Top;
            this._button.Location = new System.Drawing.Point(0, 0);
            this._button.Name = "_button";
            this._button.Size = new System.Drawing.Size(211, 31);
            this._button.TabIndex = 0;
            this._button.UseVisualStyleBackColor = true;
            // 
            // _tsTreeView
            // 
            this._tsTreeView.Dock = System.Windows.Forms.DockStyle.Top;
            this._tsTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this._tsTreeView.ItemHeight = 24;
            this._tsTreeView.Location = new System.Drawing.Point(0, 31);
            this._tsTreeView.Name = "_tsTreeView";
            this._tsTreeView.NodeCollapseImage = null;
            this._tsTreeView.NodeExpandedImage = null;
            this._tsTreeView.NodeFont = new System.Drawing.Font("Microsoft YaHei", 12F);
            this._tsTreeView.NodeImageSize = new System.Drawing.Size(18, 25);
            this._tsTreeView.NodeOffset = 5;
            this._tsTreeView.ShowLines = false;
            this._tsTreeView.ShowPlusMinus = false;
            this._tsTreeView.Size = new System.Drawing.Size(211, 113);
            this._tsTreeView.TabIndex = 1;
            this._tsTreeView.LeafItemClick += new TreeViewItemClick(TreeView_ItemClick);
            // 
            // TSNavBarItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._tsTreeView);
            this.Controls.Add(this._button);
            this.Name = "TSNavBarItem";
            this.Size = new System.Drawing.Size(211, 142);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _button;
        private TSTreeView _tsTreeView;
    }
}
