namespace TradingSystem.View
{
    partial class HoldingTransferedForm
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
            this.leftRightSplitter = new System.Windows.Forms.SplitContainer();
            this.topBottomSplitter = new System.Windows.Forms.SplitContainer();
            this.srcMainPanel = new System.Windows.Forms.Panel();
            this.srcTopPanel = new System.Windows.Forms.Panel();
            this.destMainPanel = new System.Windows.Forms.Panel();
            this.destTopPanel = new System.Windows.Forms.Panel();
            this.srcGridView = new Controls.GridView.TSDataGridView();
            this.destGridView = new Controls.GridView.TSDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.leftRightSplitter)).BeginInit();
            this.leftRightSplitter.Panel2.SuspendLayout();
            this.leftRightSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topBottomSplitter)).BeginInit();
            this.topBottomSplitter.Panel1.SuspendLayout();
            this.topBottomSplitter.Panel2.SuspendLayout();
            this.topBottomSplitter.SuspendLayout();
            this.srcMainPanel.SuspendLayout();
            this.destMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.srcGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.destGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // leftRightSplitter
            // 
            this.leftRightSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftRightSplitter.Location = new System.Drawing.Point(0, 0);
            this.leftRightSplitter.Name = "leftRightSplitter";
            // 
            // leftRightSplitter.Panel2
            // 
            this.leftRightSplitter.Panel2.Controls.Add(this.topBottomSplitter);
            this.leftRightSplitter.Size = new System.Drawing.Size(1125, 570);
            this.leftRightSplitter.SplitterDistance = 159;
            this.leftRightSplitter.TabIndex = 0;
            // 
            // topBottomSplitter
            // 
            this.topBottomSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topBottomSplitter.Location = new System.Drawing.Point(0, 0);
            this.topBottomSplitter.Name = "topBottomSplitter";
            this.topBottomSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // topBottomSplitter.Panel1
            // 
            this.topBottomSplitter.Panel1.Controls.Add(this.srcMainPanel);
            this.topBottomSplitter.Panel1.Controls.Add(this.srcTopPanel);
            // 
            // topBottomSplitter.Panel2
            // 
            this.topBottomSplitter.Panel2.Controls.Add(this.destMainPanel);
            this.topBottomSplitter.Panel2.Controls.Add(this.destTopPanel);
            this.topBottomSplitter.Size = new System.Drawing.Size(962, 570);
            this.topBottomSplitter.SplitterDistance = 391;
            this.topBottomSplitter.TabIndex = 0;
            // 
            // srcMainPanel
            // 
            this.srcMainPanel.Controls.Add(this.srcGridView);
            this.srcMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.srcMainPanel.Location = new System.Drawing.Point(0, 65);
            this.srcMainPanel.Name = "srcMainPanel";
            this.srcMainPanel.Size = new System.Drawing.Size(962, 326);
            this.srcMainPanel.TabIndex = 1;
            // 
            // srcTopPanel
            // 
            this.srcTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.srcTopPanel.Location = new System.Drawing.Point(0, 0);
            this.srcTopPanel.Name = "srcTopPanel";
            this.srcTopPanel.Size = new System.Drawing.Size(962, 65);
            this.srcTopPanel.TabIndex = 0;
            // 
            // destMainPanel
            // 
            this.destMainPanel.Controls.Add(this.destGridView);
            this.destMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.destMainPanel.Location = new System.Drawing.Point(0, 24);
            this.destMainPanel.Name = "destMainPanel";
            this.destMainPanel.Size = new System.Drawing.Size(962, 151);
            this.destMainPanel.TabIndex = 1;
            // 
            // destTopPanel
            // 
            this.destTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.destTopPanel.Location = new System.Drawing.Point(0, 0);
            this.destTopPanel.Name = "destTopPanel";
            this.destTopPanel.Size = new System.Drawing.Size(962, 24);
            this.destTopPanel.TabIndex = 0;
            // 
            // srcGridView
            // 
            this.srcGridView.AllowUserToAddRows = false;
            this.srcGridView.AllowUserToDeleteRows = false;
            this.srcGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.srcGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.srcGridView.Location = new System.Drawing.Point(0, 0);
            this.srcGridView.Name = "srcGridView";
            this.srcGridView.RowTemplate.Height = 23;
            this.srcGridView.Size = new System.Drawing.Size(962, 326);
            this.srcGridView.TabIndex = 0;
            // 
            // destGridView
            // 
            this.destGridView.AllowUserToAddRows = false;
            this.destGridView.AllowUserToDeleteRows = false;
            this.destGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.destGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.destGridView.Location = new System.Drawing.Point(0, 0);
            this.destGridView.Name = "destGridView";
            this.destGridView.RowTemplate.Height = 23;
            this.destGridView.Size = new System.Drawing.Size(962, 151);
            this.destGridView.TabIndex = 0;
            // 
            // HoldingTransferedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Controls.Add(this.leftRightSplitter);
            this.Name = "HoldingTransferedForm";
            this.leftRightSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftRightSplitter)).EndInit();
            this.leftRightSplitter.ResumeLayout(false);
            this.topBottomSplitter.Panel1.ResumeLayout(false);
            this.topBottomSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topBottomSplitter)).EndInit();
            this.topBottomSplitter.ResumeLayout(false);
            this.srcMainPanel.ResumeLayout(false);
            this.destMainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.srcGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.destGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer leftRightSplitter;
        private System.Windows.Forms.SplitContainer topBottomSplitter;
        private System.Windows.Forms.Panel srcMainPanel;
        private System.Windows.Forms.Panel srcTopPanel;
        private System.Windows.Forms.Panel destMainPanel;
        private System.Windows.Forms.Panel destTopPanel;
        private Controls.GridView.TSDataGridView srcGridView;
        private Controls.GridView.TSDataGridView destGridView;
    }
}
