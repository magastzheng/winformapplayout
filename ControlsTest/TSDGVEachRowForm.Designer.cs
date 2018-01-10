namespace ControlsTest
{
    partial class TSDGVEachRowForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridviewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsDataGridView1 = new Controls.GridView.TSDataGridView();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unSelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.gridviewContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tsDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tsDataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(767, 329);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 335);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(767, 82);
            this.panel2.TabIndex = 1;
            // 
            // gridviewContextMenu
            // 
            this.gridviewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.unSelectAllToolStripMenuItem});
            this.gridviewContextMenu.Name = "gridviewContextMenu";
            this.gridviewContextMenu.Size = new System.Drawing.Size(153, 70);
            // 
            // tsDataGridView1
            // 
            this.tsDataGridView1.AllowUserToAddRows = false;
            this.tsDataGridView1.AllowUserToDeleteRows = false;
            this.tsDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tsDataGridView1.ContextMenuStrip = this.gridviewContextMenu;
            this.tsDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.tsDataGridView1.Name = "tsDataGridView1";
            this.tsDataGridView1.RowTemplate.Height = 23;
            this.tsDataGridView1.Size = new System.Drawing.Size(765, 327);
            this.tsDataGridView1.TabIndex = 0;
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.selectAllToolStripMenuItem.Text = "全选";
            // 
            // unSelectAllToolStripMenuItem
            // 
            this.unSelectAllToolStripMenuItem.Name = "unSelectAllToolStripMenuItem";
            this.unSelectAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.unSelectAllToolStripMenuItem.Text = "反选";
            // 
            // TSDGVEachRowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 417);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "TSDGVEachRowForm";
            this.Text = "TSDGVEachRowForm";
            this.panel1.ResumeLayout(false);
            this.gridviewContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tsDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Controls.GridView.TSDataGridView tsDataGridView1;
        private System.Windows.Forms.ContextMenuStrip gridviewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unSelectAllToolStripMenuItem;
    }
}