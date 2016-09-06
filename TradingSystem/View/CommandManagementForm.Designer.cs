namespace TradingSystem.View
{
    partial class CommandManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommandManagementForm));
            this.gridView = new Controls.GridView.TSDataGridView();
            this.topDownSplitter = new System.Windows.Forms.SplitContainer();
            this.childBottomPanel = new System.Windows.Forms.Panel();
            this.cmToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbModify = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.panelTop.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topDownSplitter)).BeginInit();
            this.topDownSplitter.Panel1.SuspendLayout();
            this.topDownSplitter.SuspendLayout();
            this.cmToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.cmToolStrip);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.topDownSplitter);
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
            this.gridView.Size = new System.Drawing.Size(1125, 328);
            this.gridView.TabIndex = 0;
            // 
            // topDownSplitter
            // 
            this.topDownSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topDownSplitter.Location = new System.Drawing.Point(0, 0);
            this.topDownSplitter.Name = "topDownSplitter";
            this.topDownSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // topDownSplitter.Panel1
            // 
            this.topDownSplitter.Panel1.Controls.Add(this.gridView);
            this.topDownSplitter.Panel1.Controls.Add(this.childBottomPanel);
            this.topDownSplitter.Size = new System.Drawing.Size(1125, 506);
            this.topDownSplitter.SplitterDistance = 356;
            this.topDownSplitter.TabIndex = 0;
            // 
            // childBottomPanel
            // 
            this.childBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.childBottomPanel.Location = new System.Drawing.Point(0, 328);
            this.childBottomPanel.Name = "childBottomPanel";
            this.childBottomPanel.Size = new System.Drawing.Size(1125, 28);
            this.childBottomPanel.TabIndex = 0;
            // 
            // cmToolStrip
            // 
            this.cmToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbModify,
            this.tsbCancel,
            this.tsbRefresh});
            this.cmToolStrip.Location = new System.Drawing.Point(0, 0);
            this.cmToolStrip.Name = "cmToolStrip";
            this.cmToolStrip.Size = new System.Drawing.Size(1125, 25);
            this.cmToolStrip.TabIndex = 0;
            this.cmToolStrip.Text = "toolStrip1";
            // 
            // tsbModify
            // 
            this.tsbModify.Image = ((System.Drawing.Image)(resources.GetObject("tsbModify.Image")));
            this.tsbModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModify.Name = "tsbModify";
            this.tsbModify.Size = new System.Drawing.Size(52, 22);
            this.tsbModify.Text = "修改";
            // 
            // tsbCancel
            // 
            this.tsbCancel.Image = global::TradingSystem.Properties.Resources.undo;
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(52, 22);
            this.tsbCancel.Text = "撤销";
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::TradingSystem.Properties.Resources.refresh2;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(52, 22);
            this.tsbRefresh.Text = "刷新";
            // 
            // CommandManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Name = "CommandManagementForm";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.topDownSplitter.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topDownSplitter)).EndInit();
            this.topDownSplitter.ResumeLayout(false);
            this.cmToolStrip.ResumeLayout(false);
            this.cmToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.GridView.TSDataGridView gridView;
        private System.Windows.Forms.SplitContainer topDownSplitter;
        private System.Windows.Forms.Panel childBottomPanel;
        private System.Windows.Forms.ToolStrip cmToolStrip;
        private System.Windows.Forms.ToolStripButton tsbModify;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
    }
}
