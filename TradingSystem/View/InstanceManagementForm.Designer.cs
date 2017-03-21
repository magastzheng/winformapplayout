namespace TradingSystem.View
{
    partial class InstanceManagementForm
    {
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridView = new Controls.GridView.TSDataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbArchive = new System.Windows.Forms.ToolStripButton();
            this.tsbModify = new System.Windows.Forms.ToolStripButton();
            this.panelTop.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.toolStrip1);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.gridView);
            // 
            // gridView
            // 
            this.gridView.AllowUserToAddRows = false;
            this.gridView.AllowUserToDeleteRows = false;
            this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridView.Location = new System.Drawing.Point(0, 0);
            this.gridView.Name = "gridView";
            this.gridView.RowTemplate.Height = 23;
            this.gridView.Size = new System.Drawing.Size(1125, 506);
            this.gridView.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbModify,
            this.tsbRefresh,
            this.tsbArchive});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1125, 32);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip";
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::TradingSystem.Properties.Resources.refresh2;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(52, 29);
            this.tsbRefresh.Text = "刷新";
            // 
            // tsbArchive
            // 
            this.tsbArchive.Image = global::TradingSystem.Properties.Resources.archive;
            this.tsbArchive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbArchive.Name = "tsbArchive";
            this.tsbArchive.Size = new System.Drawing.Size(52, 29);
            this.tsbArchive.Text = "归档";
            // 
            // tsbModify
            // 
            this.tsbModify.Image = global::TradingSystem.Properties.Resources.edit;
            this.tsbModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModify.Name = "tsbModify";
            this.tsbModify.Size = new System.Drawing.Size(52, 29);
            this.tsbModify.Text = "修改";
            // 
            // InstanceManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Name = "InstanceManagementForm";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.GridView.TSDataGridView gridView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripButton tsbArchive;
        private System.Windows.Forms.ToolStripButton tsbModify;
    }
}
