namespace TradingSystem.View
{
    partial class ClosePositionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        //private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            //if (disposing && (components != null))
            //{
            //    components.Dispose();
            //}
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tsChildMain = new System.Windows.Forms.ToolStrip();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mainSplitter = new System.Windows.Forms.SplitContainer();
            this.closeBottomPanel = new System.Windows.Forms.Panel();
            this.closeGridView = new Controls.GridView.TSDataGridView();
            this.securityGridView = new Controls.GridView.TSDataGridView();
            this.panelTop.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tsChildMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitter)).BeginInit();
            this.mainSplitter.Panel1.SuspendLayout();
            this.mainSplitter.Panel2.SuspendLayout();
            this.mainSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.tsChildMain);
            // 
            // panelBottom
            // 
            this.panelBottom.Location = new System.Drawing.Point(0, 450);
            this.panelBottom.Size = new System.Drawing.Size(1125, 120);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.mainSplitter);
            this.panelMain.Size = new System.Drawing.Size(1125, 418);
            // 
            // tsChildMain
            // 
            this.tsChildMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsChildMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.tsSeparator1});
            this.tsChildMain.Location = new System.Drawing.Point(0, 0);
            this.tsChildMain.Name = "tsChildMain";
            this.tsChildMain.Size = new System.Drawing.Size(1125, 32);
            this.tsChildMain.TabIndex = 0;
            this.tsChildMain.Text = "toolStrip1";
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::TradingSystem.Properties.Resources.refresh2;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(52, 29);
            this.tsbRefresh.Text = "刷新";
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // mainSplitter
            // 
            this.mainSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitter.Location = new System.Drawing.Point(0, 0);
            this.mainSplitter.Name = "mainSplitter";
            this.mainSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSplitter.Panel1
            // 
            this.mainSplitter.Panel1.Controls.Add(this.closeGridView);
            this.mainSplitter.Panel1.Controls.Add(this.closeBottomPanel);
            // 
            // mainSplitter.Panel2
            // 
            this.mainSplitter.Panel2.Controls.Add(this.securityGridView);
            this.mainSplitter.Size = new System.Drawing.Size(1125, 418);
            this.mainSplitter.SplitterDistance = 201;
            this.mainSplitter.TabIndex = 0;
            // 
            // closeBottomPanel
            // 
            this.closeBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.closeBottomPanel.Location = new System.Drawing.Point(0, 170);
            this.closeBottomPanel.Name = "closeBottomPanel";
            this.closeBottomPanel.Size = new System.Drawing.Size(1125, 31);
            this.closeBottomPanel.TabIndex = 0;
            // 
            // closeGridView
            // 
            this.closeGridView.AllowUserToAddRows = false;
            this.closeGridView.AllowUserToDeleteRows = false;
            this.closeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.closeGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closeGridView.Location = new System.Drawing.Point(0, 0);
            this.closeGridView.Name = "closeGridView";
            this.closeGridView.RowTemplate.Height = 23;
            this.closeGridView.Size = new System.Drawing.Size(1125, 170);
            this.closeGridView.TabIndex = 1;
            // 
            // securityGridView
            // 
            this.securityGridView.AllowUserToAddRows = false;
            this.securityGridView.AllowUserToDeleteRows = false;
            this.securityGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.securityGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.securityGridView.Location = new System.Drawing.Point(0, 0);
            this.securityGridView.Name = "securityGridView";
            this.securityGridView.RowTemplate.Height = 23;
            this.securityGridView.Size = new System.Drawing.Size(1125, 213);
            this.securityGridView.TabIndex = 0;
            // 
            // ClosePositionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Name = "ClosePositionForm";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.tsChildMain.ResumeLayout(false);
            this.tsChildMain.PerformLayout();
            this.mainSplitter.Panel1.ResumeLayout(false);
            this.mainSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitter)).EndInit();
            this.mainSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.closeGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsChildMain;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripSeparator tsSeparator1;
        private System.Windows.Forms.SplitContainer mainSplitter;
        private Controls.GridView.TSDataGridView closeGridView;
        private System.Windows.Forms.Panel closeBottomPanel;
        private Controls.GridView.TSDataGridView securityGridView;
    }
}
