namespace ControlsTest
{
    partial class TreeViewForm
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
            this.lblNodeName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.tsNavBarContainer1 = new Controls.TSNavBarContainer();
            this.tsTreeView1 = new Controls.TSTreeView();
            this.panel1.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNodeName
            // 
            this.lblNodeName.AutoSize = true;
            this.lblNodeName.Location = new System.Drawing.Point(152, 9);
            this.lblNodeName.Name = "lblNodeName";
            this.lblNodeName.Size = new System.Drawing.Size(41, 12);
            this.lblNodeName.TabIndex = 1;
            this.lblNodeName.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tsTreeView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(146, 448);
            this.panel1.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(166, 43);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(66, 159);
            this.treeView1.TabIndex = 3;
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.tsNavBarContainer1);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Location = new System.Drawing.Point(337, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(200, 448);
            this.rightPanel.TabIndex = 4;
            // 
            // tsNavBarContainer1
            // 
            this.tsNavBarContainer1.AutoScroll = true;
            this.tsNavBarContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(140)))), ((int)(((byte)(225)))));
            this.tsNavBarContainer1.BarMargin = 5;
            this.tsNavBarContainer1.BarSpace = 1;
            this.tsNavBarContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsNavBarContainer1.Location = new System.Drawing.Point(0, 0);
            this.tsNavBarContainer1.Name = "tsNavBarContainer1";
            this.tsNavBarContainer1.SelectedIndex = -1;
            this.tsNavBarContainer1.Size = new System.Drawing.Size(200, 448);
            this.tsNavBarContainer1.TabIndex = 0;
            // 
            // tsTreeView1
            // 
            this.tsTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsTreeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.tsTreeView1.ItemHeight = 30;
            this.tsTreeView1.Location = new System.Drawing.Point(0, 0);
            this.tsTreeView1.Name = "tsTreeView1";
            this.tsTreeView1.NodeCollapseImage = null;
            this.tsTreeView1.NodeExpandedImage = null;
            this.tsTreeView1.NodeFont = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.tsTreeView1.NodeImage = null;
            this.tsTreeView1.NodeImageSize = new System.Drawing.Size(18, 30);
            this.tsTreeView1.NodeOffset = 5;
            this.tsTreeView1.ShowLines = false;
            this.tsTreeView1.ShowPlusMinus = false;
            this.tsTreeView1.Size = new System.Drawing.Size(146, 448);
            this.tsTreeView1.TabIndex = 0;
            // 
            // TreeViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 448);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblNodeName);
            this.Name = "TreeViewForm";
            this.Text = "TreeViewForm";
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.rightPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.TSTreeView tsTreeView1;
        private System.Windows.Forms.Label lblNodeName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel rightPanel;
        private Controls.TSNavBarContainer tsNavBarContainer1;
    }
}