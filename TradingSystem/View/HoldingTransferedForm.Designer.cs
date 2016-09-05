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
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.cbDestTradeInst = new System.Windows.Forms.ComboBox();
            this.cbDestPortfolio = new System.Windows.Forms.ComboBox();
            this.cbDestFundCode = new System.Windows.Forms.ComboBox();
            this.cbSrcTradeInst = new System.Windows.Forms.ComboBox();
            this.cbSrcPortfolio = new System.Windows.Forms.ComboBox();
            this.cbSrcFundCode = new System.Windows.Forms.ComboBox();
            this.cbOpertionType = new System.Windows.Forms.ComboBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.lblDestTradeInst = new System.Windows.Forms.Label();
            this.lblDestPortfolio = new System.Windows.Forms.Label();
            this.lblDestFundCode = new System.Windows.Forms.Label();
            this.lblSrcTradeInst = new System.Windows.Forms.Label();
            this.lblSrcPortfolio = new System.Windows.Forms.Label();
            this.lblSrcFundCode = new System.Windows.Forms.Label();
            this.lblOperationType = new System.Windows.Forms.Label();
            this.topBottomSplitter = new System.Windows.Forms.SplitContainer();
            this.srcMainPanel = new System.Windows.Forms.Panel();
            this.srcGridView = new Controls.GridView.TSDataGridView();
            this.srcTopPanel = new System.Windows.Forms.Panel();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCalc = new System.Windows.Forms.Button();
            this.lblUnit = new System.Windows.Forms.Label();
            this.tbCopies = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.cbTemplate = new System.Windows.Forms.ComboBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.lblSrcCaptin = new System.Windows.Forms.Label();
            this.destMainPanel = new System.Windows.Forms.Panel();
            this.destGridView = new Controls.GridView.TSDataGridView();
            this.destTopPanel = new System.Windows.Forms.Panel();
            this.lblDestCaptin = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.leftRightSplitter)).BeginInit();
            this.leftRightSplitter.Panel1.SuspendLayout();
            this.leftRightSplitter.Panel2.SuspendLayout();
            this.leftRightSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topBottomSplitter)).BeginInit();
            this.topBottomSplitter.Panel1.SuspendLayout();
            this.topBottomSplitter.Panel2.SuspendLayout();
            this.topBottomSplitter.SuspendLayout();
            this.srcMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.srcGridView)).BeginInit();
            this.srcTopPanel.SuspendLayout();
            this.destMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.destGridView)).BeginInit();
            this.destTopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftRightSplitter
            // 
            this.leftRightSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftRightSplitter.Location = new System.Drawing.Point(0, 0);
            this.leftRightSplitter.Name = "leftRightSplitter";
            // 
            // leftRightSplitter.Panel1
            // 
            this.leftRightSplitter.Panel1.Controls.Add(this.rtbNotes);
            this.leftRightSplitter.Panel1.Controls.Add(this.cbDestTradeInst);
            this.leftRightSplitter.Panel1.Controls.Add(this.cbDestPortfolio);
            this.leftRightSplitter.Panel1.Controls.Add(this.cbDestFundCode);
            this.leftRightSplitter.Panel1.Controls.Add(this.cbSrcTradeInst);
            this.leftRightSplitter.Panel1.Controls.Add(this.cbSrcPortfolio);
            this.leftRightSplitter.Panel1.Controls.Add(this.cbSrcFundCode);
            this.leftRightSplitter.Panel1.Controls.Add(this.cbOpertionType);
            this.leftRightSplitter.Panel1.Controls.Add(this.lblNotes);
            this.leftRightSplitter.Panel1.Controls.Add(this.lblDestTradeInst);
            this.leftRightSplitter.Panel1.Controls.Add(this.lblDestPortfolio);
            this.leftRightSplitter.Panel1.Controls.Add(this.lblDestFundCode);
            this.leftRightSplitter.Panel1.Controls.Add(this.lblSrcTradeInst);
            this.leftRightSplitter.Panel1.Controls.Add(this.lblSrcPortfolio);
            this.leftRightSplitter.Panel1.Controls.Add(this.lblSrcFundCode);
            this.leftRightSplitter.Panel1.Controls.Add(this.lblOperationType);
            // 
            // leftRightSplitter.Panel2
            // 
            this.leftRightSplitter.Panel2.Controls.Add(this.topBottomSplitter);
            this.leftRightSplitter.Size = new System.Drawing.Size(1125, 570);
            this.leftRightSplitter.SplitterDistance = 218;
            this.leftRightSplitter.TabIndex = 0;
            // 
            // rtbNotes
            // 
            this.rtbNotes.Location = new System.Drawing.Point(104, 220);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(100, 96);
            this.rtbNotes.TabIndex = 0;
            this.rtbNotes.Text = "";
            // 
            // cbDestTradeInst
            // 
            this.cbDestTradeInst.FormattingEnabled = true;
            this.cbDestTradeInst.Location = new System.Drawing.Point(101, 190);
            this.cbDestTradeInst.Name = "cbDestTradeInst";
            this.cbDestTradeInst.Size = new System.Drawing.Size(103, 20);
            this.cbDestTradeInst.TabIndex = 14;
            // 
            // cbDestPortfolio
            // 
            this.cbDestPortfolio.FormattingEnabled = true;
            this.cbDestPortfolio.Location = new System.Drawing.Point(101, 162);
            this.cbDestPortfolio.Name = "cbDestPortfolio";
            this.cbDestPortfolio.Size = new System.Drawing.Size(103, 20);
            this.cbDestPortfolio.TabIndex = 13;
            // 
            // cbDestFundCode
            // 
            this.cbDestFundCode.FormattingEnabled = true;
            this.cbDestFundCode.Location = new System.Drawing.Point(101, 133);
            this.cbDestFundCode.Name = "cbDestFundCode";
            this.cbDestFundCode.Size = new System.Drawing.Size(103, 20);
            this.cbDestFundCode.TabIndex = 12;
            // 
            // cbSrcTradeInst
            // 
            this.cbSrcTradeInst.FormattingEnabled = true;
            this.cbSrcTradeInst.Location = new System.Drawing.Point(101, 103);
            this.cbSrcTradeInst.Name = "cbSrcTradeInst";
            this.cbSrcTradeInst.Size = new System.Drawing.Size(103, 20);
            this.cbSrcTradeInst.TabIndex = 11;
            // 
            // cbSrcPortfolio
            // 
            this.cbSrcPortfolio.FormattingEnabled = true;
            this.cbSrcPortfolio.Location = new System.Drawing.Point(101, 75);
            this.cbSrcPortfolio.Name = "cbSrcPortfolio";
            this.cbSrcPortfolio.Size = new System.Drawing.Size(103, 20);
            this.cbSrcPortfolio.TabIndex = 10;
            // 
            // cbSrcFundCode
            // 
            this.cbSrcFundCode.FormattingEnabled = true;
            this.cbSrcFundCode.Location = new System.Drawing.Point(101, 46);
            this.cbSrcFundCode.Name = "cbSrcFundCode";
            this.cbSrcFundCode.Size = new System.Drawing.Size(103, 20);
            this.cbSrcFundCode.TabIndex = 9;
            // 
            // cbOpertionType
            // 
            this.cbOpertionType.FormattingEnabled = true;
            this.cbOpertionType.Location = new System.Drawing.Point(102, 16);
            this.cbOpertionType.Name = "cbOpertionType";
            this.cbOpertionType.Size = new System.Drawing.Size(103, 20);
            this.cbOpertionType.TabIndex = 8;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(55, 220);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(41, 12);
            this.lblNotes.TabIndex = 7;
            this.lblNotes.Text = "备注：";
            // 
            // lblDestTradeInst
            // 
            this.lblDestTradeInst.AutoSize = true;
            this.lblDestTradeInst.Location = new System.Drawing.Point(8, 193);
            this.lblDestTradeInst.Name = "lblDestTradeInst";
            this.lblDestTradeInst.Size = new System.Drawing.Size(89, 12);
            this.lblDestTradeInst.TabIndex = 6;
            this.lblDestTradeInst.Text = "目标交易实例：";
            // 
            // lblDestPortfolio
            // 
            this.lblDestPortfolio.AutoSize = true;
            this.lblDestPortfolio.Location = new System.Drawing.Point(8, 165);
            this.lblDestPortfolio.Name = "lblDestPortfolio";
            this.lblDestPortfolio.Size = new System.Drawing.Size(89, 12);
            this.lblDestPortfolio.TabIndex = 5;
            this.lblDestPortfolio.Text = "目标交易组合：";
            // 
            // lblDestFundCode
            // 
            this.lblDestFundCode.AutoSize = true;
            this.lblDestFundCode.Location = new System.Drawing.Point(8, 136);
            this.lblDestFundCode.Name = "lblDestFundCode";
            this.lblDestFundCode.Size = new System.Drawing.Size(89, 12);
            this.lblDestFundCode.TabIndex = 4;
            this.lblDestFundCode.Text = "目标基金编号：";
            // 
            // lblSrcTradeInst
            // 
            this.lblSrcTradeInst.AutoSize = true;
            this.lblSrcTradeInst.Location = new System.Drawing.Point(20, 107);
            this.lblSrcTradeInst.Name = "lblSrcTradeInst";
            this.lblSrcTradeInst.Size = new System.Drawing.Size(77, 12);
            this.lblSrcTradeInst.TabIndex = 3;
            this.lblSrcTradeInst.Text = "源交易实例：";
            // 
            // lblSrcPortfolio
            // 
            this.lblSrcPortfolio.AutoSize = true;
            this.lblSrcPortfolio.Location = new System.Drawing.Point(20, 79);
            this.lblSrcPortfolio.Name = "lblSrcPortfolio";
            this.lblSrcPortfolio.Size = new System.Drawing.Size(77, 12);
            this.lblSrcPortfolio.TabIndex = 2;
            this.lblSrcPortfolio.Text = "源交易组合：";
            // 
            // lblSrcFundCode
            // 
            this.lblSrcFundCode.AutoSize = true;
            this.lblSrcFundCode.Location = new System.Drawing.Point(20, 49);
            this.lblSrcFundCode.Name = "lblSrcFundCode";
            this.lblSrcFundCode.Size = new System.Drawing.Size(77, 12);
            this.lblSrcFundCode.TabIndex = 1;
            this.lblSrcFundCode.Text = "源基金编号：";
            // 
            // lblOperationType
            // 
            this.lblOperationType.AutoSize = true;
            this.lblOperationType.Location = new System.Drawing.Point(32, 20);
            this.lblOperationType.Name = "lblOperationType";
            this.lblOperationType.Size = new System.Drawing.Size(65, 12);
            this.lblOperationType.TabIndex = 0;
            this.lblOperationType.Text = "操作类型：";
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
            this.topBottomSplitter.Size = new System.Drawing.Size(903, 570);
            this.topBottomSplitter.SplitterDistance = 391;
            this.topBottomSplitter.TabIndex = 0;
            // 
            // srcMainPanel
            // 
            this.srcMainPanel.Controls.Add(this.srcGridView);
            this.srcMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.srcMainPanel.Location = new System.Drawing.Point(0, 65);
            this.srcMainPanel.Name = "srcMainPanel";
            this.srcMainPanel.Size = new System.Drawing.Size(903, 326);
            this.srcMainPanel.TabIndex = 1;
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
            this.srcGridView.Size = new System.Drawing.Size(903, 326);
            this.srcGridView.TabIndex = 0;
            // 
            // srcTopPanel
            // 
            this.srcTopPanel.Controls.Add(this.btnTransfer);
            this.srcTopPanel.Controls.Add(this.btnRefresh);
            this.srcTopPanel.Controls.Add(this.btnCalc);
            this.srcTopPanel.Controls.Add(this.lblUnit);
            this.srcTopPanel.Controls.Add(this.tbCopies);
            this.srcTopPanel.Controls.Add(this.lblAmount);
            this.srcTopPanel.Controls.Add(this.cbTemplate);
            this.srcTopPanel.Controls.Add(this.lblTemplate);
            this.srcTopPanel.Controls.Add(this.lblSrcCaptin);
            this.srcTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.srcTopPanel.Location = new System.Drawing.Point(0, 0);
            this.srcTopPanel.Name = "srcTopPanel";
            this.srcTopPanel.Size = new System.Drawing.Size(903, 65);
            this.srcTopPanel.TabIndex = 0;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(560, 26);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(75, 23);
            this.btnTransfer.TabIndex = 8;
            this.btnTransfer.Text = "划转";
            this.btnTransfer.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(470, 28);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "刷新持仓";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(331, 27);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(75, 23);
            this.btnCalc.TabIndex = 0;
            this.btnCalc.Text = "计算";
            this.btnCalc.UseVisualStyleBackColor = true;
            // 
            // lblUnit
            // 
            this.lblUnit.Location = new System.Drawing.Point(285, 35);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(47, 12);
            this.lblUnit.TabIndex = 0;
            this.lblUnit.Text = "套手";
            // 
            // tbCopies
            // 
            this.tbCopies.Location = new System.Drawing.Point(233, 29);
            this.tbCopies.Name = "tbCopies";
            this.tbCopies.Size = new System.Drawing.Size(37, 21);
            this.tbCopies.TabIndex = 4;
            // 
            // lblAmount
            // 
            this.lblAmount.Location = new System.Drawing.Point(181, 34);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(47, 12);
            this.lblAmount.TabIndex = 0;
            this.lblAmount.Text = "数量";
            // 
            // cbTemplate
            // 
            this.cbTemplate.FormattingEnabled = true;
            this.cbTemplate.Location = new System.Drawing.Point(69, 31);
            this.cbTemplate.Name = "cbTemplate";
            this.cbTemplate.Size = new System.Drawing.Size(98, 20);
            this.cbTemplate.TabIndex = 2;
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.Location = new System.Drawing.Point(6, 34);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(53, 12);
            this.lblTemplate.TabIndex = 1;
            this.lblTemplate.Text = "现货模板";
            // 
            // lblSrcCaptin
            // 
            this.lblSrcCaptin.AutoSize = true;
            this.lblSrcCaptin.Location = new System.Drawing.Point(4, 6);
            this.lblSrcCaptin.Name = "lblSrcCaptin";
            this.lblSrcCaptin.Size = new System.Drawing.Size(113, 12);
            this.lblSrcCaptin.TabIndex = 0;
            this.lblSrcCaptin.Text = "源（交易实例）持仓";
            // 
            // destMainPanel
            // 
            this.destMainPanel.Controls.Add(this.destGridView);
            this.destMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.destMainPanel.Location = new System.Drawing.Point(0, 24);
            this.destMainPanel.Name = "destMainPanel";
            this.destMainPanel.Size = new System.Drawing.Size(903, 151);
            this.destMainPanel.TabIndex = 1;
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
            this.destGridView.Size = new System.Drawing.Size(903, 151);
            this.destGridView.TabIndex = 0;
            // 
            // destTopPanel
            // 
            this.destTopPanel.Controls.Add(this.lblDestCaptin);
            this.destTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.destTopPanel.Location = new System.Drawing.Point(0, 0);
            this.destTopPanel.Name = "destTopPanel";
            this.destTopPanel.Size = new System.Drawing.Size(903, 24);
            this.destTopPanel.TabIndex = 0;
            // 
            // lblDestCaptin
            // 
            this.lblDestCaptin.AutoSize = true;
            this.lblDestCaptin.Location = new System.Drawing.Point(3, 9);
            this.lblDestCaptin.Name = "lblDestCaptin";
            this.lblDestCaptin.Size = new System.Drawing.Size(149, 12);
            this.lblDestCaptin.TabIndex = 0;
            this.lblDestCaptin.Text = "目标组合（交易实例）持仓";
            // 
            // HoldingTransferedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Controls.Add(this.leftRightSplitter);
            this.Name = "HoldingTransferedForm";
            this.leftRightSplitter.Panel1.ResumeLayout(false);
            this.leftRightSplitter.Panel1.PerformLayout();
            this.leftRightSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftRightSplitter)).EndInit();
            this.leftRightSplitter.ResumeLayout(false);
            this.topBottomSplitter.Panel1.ResumeLayout(false);
            this.topBottomSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topBottomSplitter)).EndInit();
            this.topBottomSplitter.ResumeLayout(false);
            this.srcMainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.srcGridView)).EndInit();
            this.srcTopPanel.ResumeLayout(false);
            this.srcTopPanel.PerformLayout();
            this.destMainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.destGridView)).EndInit();
            this.destTopPanel.ResumeLayout(false);
            this.destTopPanel.PerformLayout();
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
        private System.Windows.Forms.Label lblSrcCaptin;
        private System.Windows.Forms.Label lblDestCaptin;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.ComboBox cbDestTradeInst;
        private System.Windows.Forms.ComboBox cbDestPortfolio;
        private System.Windows.Forms.ComboBox cbDestFundCode;
        private System.Windows.Forms.ComboBox cbSrcTradeInst;
        private System.Windows.Forms.ComboBox cbSrcPortfolio;
        private System.Windows.Forms.ComboBox cbSrcFundCode;
        private System.Windows.Forms.ComboBox cbOpertionType;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Label lblDestTradeInst;
        private System.Windows.Forms.Label lblDestPortfolio;
        private System.Windows.Forms.Label lblDestFundCode;
        private System.Windows.Forms.Label lblSrcTradeInst;
        private System.Windows.Forms.Label lblSrcPortfolio;
        private System.Windows.Forms.Label lblSrcFundCode;
        private System.Windows.Forms.Label lblOperationType;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.TextBox tbCopies;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.ComboBox cbTemplate;
        private System.Windows.Forms.Label lblTemplate;
    }
}
