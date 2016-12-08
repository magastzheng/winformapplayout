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
            this.tabCommandMng = new System.Windows.Forms.TabControl();
            this.tpSummary = new System.Windows.Forms.TabPage();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.tbCancelCause = new System.Windows.Forms.TextBox();
            this.lblCancelCause = new System.Windows.Forms.Label();
            this.tbModifyCause = new System.Windows.Forms.TextBox();
            this.lblModifyCause = new System.Windows.Forms.Label();
            this.tbCancelTime = new System.Windows.Forms.TextBox();
            this.lblCancelTime = new System.Windows.Forms.Label();
            this.tbModifyTime = new System.Windows.Forms.TextBox();
            this.lblModifyTime = new System.Windows.Forms.Label();
            this.tbCancelPerson = new System.Windows.Forms.TextBox();
            this.lblCancelPerson = new System.Windows.Forms.Label();
            this.tbModifyPerson = new System.Windows.Forms.TextBox();
            this.lblModifyPerson = new System.Windows.Forms.Label();
            this.tbSubmitPerson = new System.Windows.Forms.TextBox();
            this.lblSubmitPerson = new System.Windows.Forms.Label();
            this.tbDealStatus = new System.Windows.Forms.TextBox();
            this.lblDealStatus = new System.Windows.Forms.Label();
            this.tbEntrustStatus = new System.Windows.Forms.TextBox();
            this.lblEntrustStatus = new System.Windows.Forms.Label();
            this.tbDispatchStatus = new System.Windows.Forms.TextBox();
            this.lblDispatchStatus = new System.Windows.Forms.Label();
            this.tbApprovalStatus = new System.Windows.Forms.TextBox();
            this.lblApprovalStatus = new System.Windows.Forms.Label();
            this.tbCommandStatus = new System.Windows.Forms.TextBox();
            this.lblCommandStatus = new System.Windows.Forms.Label();
            this.tbEndTime = new System.Windows.Forms.TextBox();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.tbEndDate = new System.Windows.Forms.TextBox();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.tbStartTime = new System.Windows.Forms.TextBox();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.tbStartDate = new System.Windows.Forms.TextBox();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.tbSubmitTime = new System.Windows.Forms.TextBox();
            this.lblSubmitTime = new System.Windows.Forms.Label();
            this.tbSubmitDate = new System.Windows.Forms.TextBox();
            this.lblSubmitDate = new System.Windows.Forms.Label();
            this.tbAveragePrice = new System.Windows.Forms.TextBox();
            this.lblAveragePrice = new System.Windows.Forms.Label();
            this.tbDealAmount = new System.Windows.Forms.TextBox();
            this.lblDealAmount = new System.Windows.Forms.Label();
            this.tbCommandAmount = new System.Windows.Forms.TextBox();
            this.lblCommandAmount = new System.Windows.Forms.Label();
            this.tbCommandPrice = new System.Windows.Forms.TextBox();
            this.lblCommandPrice = new System.Windows.Forms.Label();
            this.tbPriceMode = new System.Windows.Forms.TextBox();
            this.lblPriceMode = new System.Windows.Forms.Label();
            this.tbSecuName = new System.Windows.Forms.TextBox();
            this.lblSecuName = new System.Windows.Forms.Label();
            this.tbPortName = new System.Windows.Forms.TextBox();
            this.lblPortName = new System.Windows.Forms.Label();
            this.tbFundName = new System.Windows.Forms.TextBox();
            this.lblFundName = new System.Windows.Forms.Label();
            this.tbCommandId = new System.Windows.Forms.TextBox();
            this.lblCommandId = new System.Windows.Forms.Label();
            this.tpSecurity = new System.Windows.Forms.TabPage();
            this.tpEntrust = new System.Windows.Forms.TabPage();
            this.tpDeal = new System.Windows.Forms.TabPage();
            this.cmToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbModify = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.dealGridView = new Controls.GridView.TSDataGridView();
            this.secuGridView = new Controls.GridView.TSDataGridView();
            this.entrustGridView = new Controls.GridView.TSDataGridView();
            this.panelTop.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topDownSplitter)).BeginInit();
            this.topDownSplitter.Panel1.SuspendLayout();
            this.topDownSplitter.Panel2.SuspendLayout();
            this.topDownSplitter.SuspendLayout();
            this.tabCommandMng.SuspendLayout();
            this.tpSummary.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.tpSecurity.SuspendLayout();
            this.tpEntrust.SuspendLayout();
            this.tpDeal.SuspendLayout();
            this.cmToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dealGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secuGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entrustGridView)).BeginInit();
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
            this.gridView.Size = new System.Drawing.Size(1125, 197);
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
            // 
            // topDownSplitter.Panel2
            // 
            this.topDownSplitter.Panel2.Controls.Add(this.tabCommandMng);
            this.topDownSplitter.Size = new System.Drawing.Size(1125, 506);
            this.topDownSplitter.SplitterDistance = 225;
            this.topDownSplitter.TabIndex = 0;
            // 
            // childBottomPanel
            // 
            this.childBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.childBottomPanel.Location = new System.Drawing.Point(0, 197);
            this.childBottomPanel.Name = "childBottomPanel";
            this.childBottomPanel.Size = new System.Drawing.Size(1125, 28);
            this.childBottomPanel.TabIndex = 0;
            // 
            // tabCommandMng
            // 
            this.tabCommandMng.Controls.Add(this.tpSummary);
            this.tabCommandMng.Controls.Add(this.tpSecurity);
            this.tabCommandMng.Controls.Add(this.tpEntrust);
            this.tabCommandMng.Controls.Add(this.tpDeal);
            this.tabCommandMng.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCommandMng.Location = new System.Drawing.Point(0, 0);
            this.tabCommandMng.Name = "tabCommandMng";
            this.tabCommandMng.SelectedIndex = 0;
            this.tabCommandMng.Size = new System.Drawing.Size(1125, 277);
            this.tabCommandMng.TabIndex = 0;
            // 
            // tpSummary
            // 
            this.tpSummary.Controls.Add(this.panelSummary);
            this.tpSummary.Location = new System.Drawing.Point(4, 22);
            this.tpSummary.Name = "tpSummary";
            this.tpSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tpSummary.Size = new System.Drawing.Size(1117, 251);
            this.tpSummary.TabIndex = 0;
            this.tpSummary.Text = "指令明细";
            this.tpSummary.UseVisualStyleBackColor = true;
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSummary.Controls.Add(this.tbNotes);
            this.panelSummary.Controls.Add(this.lblNotes);
            this.panelSummary.Controls.Add(this.tbCancelCause);
            this.panelSummary.Controls.Add(this.lblCancelCause);
            this.panelSummary.Controls.Add(this.tbModifyCause);
            this.panelSummary.Controls.Add(this.lblModifyCause);
            this.panelSummary.Controls.Add(this.tbCancelTime);
            this.panelSummary.Controls.Add(this.lblCancelTime);
            this.panelSummary.Controls.Add(this.tbModifyTime);
            this.panelSummary.Controls.Add(this.lblModifyTime);
            this.panelSummary.Controls.Add(this.tbCancelPerson);
            this.panelSummary.Controls.Add(this.lblCancelPerson);
            this.panelSummary.Controls.Add(this.tbModifyPerson);
            this.panelSummary.Controls.Add(this.lblModifyPerson);
            this.panelSummary.Controls.Add(this.tbSubmitPerson);
            this.panelSummary.Controls.Add(this.lblSubmitPerson);
            this.panelSummary.Controls.Add(this.tbDealStatus);
            this.panelSummary.Controls.Add(this.lblDealStatus);
            this.panelSummary.Controls.Add(this.tbEntrustStatus);
            this.panelSummary.Controls.Add(this.lblEntrustStatus);
            this.panelSummary.Controls.Add(this.tbDispatchStatus);
            this.panelSummary.Controls.Add(this.lblDispatchStatus);
            this.panelSummary.Controls.Add(this.tbApprovalStatus);
            this.panelSummary.Controls.Add(this.lblApprovalStatus);
            this.panelSummary.Controls.Add(this.tbCommandStatus);
            this.panelSummary.Controls.Add(this.lblCommandStatus);
            this.panelSummary.Controls.Add(this.tbEndTime);
            this.panelSummary.Controls.Add(this.lblEndTime);
            this.panelSummary.Controls.Add(this.tbEndDate);
            this.panelSummary.Controls.Add(this.lblEndDate);
            this.panelSummary.Controls.Add(this.tbStartTime);
            this.panelSummary.Controls.Add(this.lblStartTime);
            this.panelSummary.Controls.Add(this.tbStartDate);
            this.panelSummary.Controls.Add(this.lblStartDate);
            this.panelSummary.Controls.Add(this.tbSubmitTime);
            this.panelSummary.Controls.Add(this.lblSubmitTime);
            this.panelSummary.Controls.Add(this.tbSubmitDate);
            this.panelSummary.Controls.Add(this.lblSubmitDate);
            this.panelSummary.Controls.Add(this.tbAveragePrice);
            this.panelSummary.Controls.Add(this.lblAveragePrice);
            this.panelSummary.Controls.Add(this.tbDealAmount);
            this.panelSummary.Controls.Add(this.lblDealAmount);
            this.panelSummary.Controls.Add(this.tbCommandAmount);
            this.panelSummary.Controls.Add(this.lblCommandAmount);
            this.panelSummary.Controls.Add(this.tbCommandPrice);
            this.panelSummary.Controls.Add(this.lblCommandPrice);
            this.panelSummary.Controls.Add(this.tbPriceMode);
            this.panelSummary.Controls.Add(this.lblPriceMode);
            this.panelSummary.Controls.Add(this.tbSecuName);
            this.panelSummary.Controls.Add(this.lblSecuName);
            this.panelSummary.Controls.Add(this.tbPortName);
            this.panelSummary.Controls.Add(this.lblPortName);
            this.panelSummary.Controls.Add(this.tbFundName);
            this.panelSummary.Controls.Add(this.lblFundName);
            this.panelSummary.Controls.Add(this.tbCommandId);
            this.panelSummary.Controls.Add(this.lblCommandId);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSummary.Location = new System.Drawing.Point(3, 3);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(1111, 245);
            this.panelSummary.TabIndex = 0;
            // 
            // tbNotes
            // 
            this.tbNotes.Location = new System.Drawing.Point(713, 149);
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(290, 21);
            this.tbNotes.TabIndex = 55;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(646, 158);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(65, 12);
            this.lblNotes.TabIndex = 54;
            this.lblNotes.Text = "指令备注：";
            // 
            // tbCancelCause
            // 
            this.tbCancelCause.Location = new System.Drawing.Point(321, 149);
            this.tbCancelCause.Name = "tbCancelCause";
            this.tbCancelCause.Size = new System.Drawing.Size(300, 21);
            this.tbCancelCause.TabIndex = 53;
            // 
            // lblCancelCause
            // 
            this.lblCancelCause.AutoSize = true;
            this.lblCancelCause.Location = new System.Drawing.Point(232, 158);
            this.lblCancelCause.Name = "lblCancelCause";
            this.lblCancelCause.Size = new System.Drawing.Size(89, 12);
            this.lblCancelCause.TabIndex = 52;
            this.lblCancelCause.Text = "撤销指令原因：";
            // 
            // tbModifyCause
            // 
            this.tbModifyCause.Location = new System.Drawing.Point(110, 149);
            this.tbModifyCause.Name = "tbModifyCause";
            this.tbModifyCause.Size = new System.Drawing.Size(100, 21);
            this.tbModifyCause.TabIndex = 51;
            // 
            // lblModifyCause
            // 
            this.lblModifyCause.AutoSize = true;
            this.lblModifyCause.Location = new System.Drawing.Point(21, 158);
            this.lblModifyCause.Name = "lblModifyCause";
            this.lblModifyCause.Size = new System.Drawing.Size(89, 12);
            this.lblModifyCause.TabIndex = 50;
            this.lblModifyCause.Text = "修改指令原因：";
            // 
            // tbCancelTime
            // 
            this.tbCancelTime.Location = new System.Drawing.Point(903, 120);
            this.tbCancelTime.Name = "tbCancelTime";
            this.tbCancelTime.Size = new System.Drawing.Size(100, 21);
            this.tbCancelTime.TabIndex = 49;
            // 
            // lblCancelTime
            // 
            this.lblCancelTime.AutoSize = true;
            this.lblCancelTime.Location = new System.Drawing.Point(840, 129);
            this.lblCancelTime.Name = "lblCancelTime";
            this.lblCancelTime.Size = new System.Drawing.Size(65, 12);
            this.lblCancelTime.TabIndex = 48;
            this.lblCancelTime.Text = "撤销时间：";
            // 
            // tbModifyTime
            // 
            this.tbModifyTime.Location = new System.Drawing.Point(711, 120);
            this.tbModifyTime.Name = "tbModifyTime";
            this.tbModifyTime.Size = new System.Drawing.Size(100, 21);
            this.tbModifyTime.TabIndex = 47;
            // 
            // lblModifyTime
            // 
            this.lblModifyTime.AutoSize = true;
            this.lblModifyTime.Location = new System.Drawing.Point(646, 129);
            this.lblModifyTime.Name = "lblModifyTime";
            this.lblModifyTime.Size = new System.Drawing.Size(65, 12);
            this.lblModifyTime.TabIndex = 46;
            this.lblModifyTime.Text = "修改时间：";
            // 
            // tbCancelPerson
            // 
            this.tbCancelPerson.Location = new System.Drawing.Point(521, 120);
            this.tbCancelPerson.Name = "tbCancelPerson";
            this.tbCancelPerson.Size = new System.Drawing.Size(100, 21);
            this.tbCancelPerson.TabIndex = 45;
            // 
            // lblCancelPerson
            // 
            this.lblCancelPerson.AutoSize = true;
            this.lblCancelPerson.Location = new System.Drawing.Point(457, 129);
            this.lblCancelPerson.Name = "lblCancelPerson";
            this.lblCancelPerson.Size = new System.Drawing.Size(65, 12);
            this.lblCancelPerson.TabIndex = 44;
            this.lblCancelPerson.Text = "撤 销 人：";
            // 
            // tbModifyPerson
            // 
            this.tbModifyPerson.Location = new System.Drawing.Point(321, 120);
            this.tbModifyPerson.Name = "tbModifyPerson";
            this.tbModifyPerson.Size = new System.Drawing.Size(100, 21);
            this.tbModifyPerson.TabIndex = 43;
            // 
            // lblModifyPerson
            // 
            this.lblModifyPerson.AutoSize = true;
            this.lblModifyPerson.Location = new System.Drawing.Point(255, 129);
            this.lblModifyPerson.Name = "lblModifyPerson";
            this.lblModifyPerson.Size = new System.Drawing.Size(65, 12);
            this.lblModifyPerson.TabIndex = 42;
            this.lblModifyPerson.Text = "修 改 人：";
            // 
            // tbSubmitPerson
            // 
            this.tbSubmitPerson.Location = new System.Drawing.Point(110, 120);
            this.tbSubmitPerson.Name = "tbSubmitPerson";
            this.tbSubmitPerson.Size = new System.Drawing.Size(100, 21);
            this.tbSubmitPerson.TabIndex = 41;
            // 
            // lblSubmitPerson
            // 
            this.lblSubmitPerson.AutoSize = true;
            this.lblSubmitPerson.Location = new System.Drawing.Point(43, 129);
            this.lblSubmitPerson.Name = "lblSubmitPerson";
            this.lblSubmitPerson.Size = new System.Drawing.Size(65, 12);
            this.lblSubmitPerson.TabIndex = 40;
            this.lblSubmitPerson.Text = "下 达 人：";
            // 
            // tbDealStatus
            // 
            this.tbDealStatus.Location = new System.Drawing.Point(903, 91);
            this.tbDealStatus.Name = "tbDealStatus";
            this.tbDealStatus.Size = new System.Drawing.Size(100, 21);
            this.tbDealStatus.TabIndex = 39;
            // 
            // lblDealStatus
            // 
            this.lblDealStatus.AutoSize = true;
            this.lblDealStatus.Location = new System.Drawing.Point(840, 100);
            this.lblDealStatus.Name = "lblDealStatus";
            this.lblDealStatus.Size = new System.Drawing.Size(65, 12);
            this.lblDealStatus.TabIndex = 38;
            this.lblDealStatus.Text = "成交状态：";
            // 
            // tbEntrustStatus
            // 
            this.tbEntrustStatus.Location = new System.Drawing.Point(711, 91);
            this.tbEntrustStatus.Name = "tbEntrustStatus";
            this.tbEntrustStatus.Size = new System.Drawing.Size(100, 21);
            this.tbEntrustStatus.TabIndex = 37;
            // 
            // lblEntrustStatus
            // 
            this.lblEntrustStatus.AutoSize = true;
            this.lblEntrustStatus.Location = new System.Drawing.Point(646, 100);
            this.lblEntrustStatus.Name = "lblEntrustStatus";
            this.lblEntrustStatus.Size = new System.Drawing.Size(65, 12);
            this.lblEntrustStatus.TabIndex = 36;
            this.lblEntrustStatus.Text = "委托状态：";
            // 
            // tbDispatchStatus
            // 
            this.tbDispatchStatus.Location = new System.Drawing.Point(521, 91);
            this.tbDispatchStatus.Name = "tbDispatchStatus";
            this.tbDispatchStatus.Size = new System.Drawing.Size(100, 21);
            this.tbDispatchStatus.TabIndex = 35;
            // 
            // lblDispatchStatus
            // 
            this.lblDispatchStatus.AutoSize = true;
            this.lblDispatchStatus.Location = new System.Drawing.Point(457, 100);
            this.lblDispatchStatus.Name = "lblDispatchStatus";
            this.lblDispatchStatus.Size = new System.Drawing.Size(65, 12);
            this.lblDispatchStatus.TabIndex = 34;
            this.lblDispatchStatus.Text = "分发状态：";
            // 
            // tbApprovalStatus
            // 
            this.tbApprovalStatus.Location = new System.Drawing.Point(321, 91);
            this.tbApprovalStatus.Name = "tbApprovalStatus";
            this.tbApprovalStatus.Size = new System.Drawing.Size(100, 21);
            this.tbApprovalStatus.TabIndex = 33;
            // 
            // lblApprovalStatus
            // 
            this.lblApprovalStatus.AutoSize = true;
            this.lblApprovalStatus.Location = new System.Drawing.Point(255, 100);
            this.lblApprovalStatus.Name = "lblApprovalStatus";
            this.lblApprovalStatus.Size = new System.Drawing.Size(65, 12);
            this.lblApprovalStatus.TabIndex = 32;
            this.lblApprovalStatus.Text = "审批状态：";
            // 
            // tbCommandStatus
            // 
            this.tbCommandStatus.Location = new System.Drawing.Point(110, 91);
            this.tbCommandStatus.Name = "tbCommandStatus";
            this.tbCommandStatus.Size = new System.Drawing.Size(100, 21);
            this.tbCommandStatus.TabIndex = 31;
            // 
            // lblCommandStatus
            // 
            this.lblCommandStatus.AutoSize = true;
            this.lblCommandStatus.Location = new System.Drawing.Point(43, 100);
            this.lblCommandStatus.Name = "lblCommandStatus";
            this.lblCommandStatus.Size = new System.Drawing.Size(65, 12);
            this.lblCommandStatus.TabIndex = 30;
            this.lblCommandStatus.Text = "指令状态：";
            // 
            // tbEndTime
            // 
            this.tbEndTime.Location = new System.Drawing.Point(903, 63);
            this.tbEndTime.Name = "tbEndTime";
            this.tbEndTime.Size = new System.Drawing.Size(100, 21);
            this.tbEndTime.TabIndex = 29;
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(840, 72);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(65, 12);
            this.lblEndTime.TabIndex = 28;
            this.lblEndTime.Text = "结束时间：";
            // 
            // tbEndDate
            // 
            this.tbEndDate.Location = new System.Drawing.Point(711, 63);
            this.tbEndDate.Name = "tbEndDate";
            this.tbEndDate.Size = new System.Drawing.Size(100, 21);
            this.tbEndDate.TabIndex = 27;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(646, 72);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(65, 12);
            this.lblEndDate.TabIndex = 26;
            this.lblEndDate.Text = "结束日期：";
            // 
            // tbStartTime
            // 
            this.tbStartTime.Location = new System.Drawing.Point(521, 63);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.Size = new System.Drawing.Size(100, 21);
            this.tbStartTime.TabIndex = 25;
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(457, 72);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(65, 12);
            this.lblStartTime.TabIndex = 24;
            this.lblStartTime.Text = "开始时间：";
            // 
            // tbStartDate
            // 
            this.tbStartDate.Location = new System.Drawing.Point(321, 63);
            this.tbStartDate.Name = "tbStartDate";
            this.tbStartDate.Size = new System.Drawing.Size(100, 21);
            this.tbStartDate.TabIndex = 23;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(255, 72);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(65, 12);
            this.lblStartDate.TabIndex = 22;
            this.lblStartDate.Text = "开始日期：";
            // 
            // tbSubmitTime
            // 
            this.tbSubmitTime.Location = new System.Drawing.Point(110, 63);
            this.tbSubmitTime.Name = "tbSubmitTime";
            this.tbSubmitTime.Size = new System.Drawing.Size(100, 21);
            this.tbSubmitTime.TabIndex = 21;
            // 
            // lblSubmitTime
            // 
            this.lblSubmitTime.AutoSize = true;
            this.lblSubmitTime.Location = new System.Drawing.Point(43, 72);
            this.lblSubmitTime.Name = "lblSubmitTime";
            this.lblSubmitTime.Size = new System.Drawing.Size(65, 12);
            this.lblSubmitTime.TabIndex = 20;
            this.lblSubmitTime.Text = "下达时间：";
            // 
            // tbSubmitDate
            // 
            this.tbSubmitDate.Enabled = false;
            this.tbSubmitDate.Location = new System.Drawing.Point(903, 36);
            this.tbSubmitDate.Name = "tbSubmitDate";
            this.tbSubmitDate.Size = new System.Drawing.Size(100, 21);
            this.tbSubmitDate.TabIndex = 19;
            // 
            // lblSubmitDate
            // 
            this.lblSubmitDate.AutoSize = true;
            this.lblSubmitDate.Location = new System.Drawing.Point(840, 45);
            this.lblSubmitDate.Name = "lblSubmitDate";
            this.lblSubmitDate.Size = new System.Drawing.Size(65, 12);
            this.lblSubmitDate.TabIndex = 18;
            this.lblSubmitDate.Text = "下达日期：";
            // 
            // tbAveragePrice
            // 
            this.tbAveragePrice.Enabled = false;
            this.tbAveragePrice.Location = new System.Drawing.Point(711, 36);
            this.tbAveragePrice.Name = "tbAveragePrice";
            this.tbAveragePrice.Size = new System.Drawing.Size(100, 21);
            this.tbAveragePrice.TabIndex = 17;
            // 
            // lblAveragePrice
            // 
            this.lblAveragePrice.AutoSize = true;
            this.lblAveragePrice.Location = new System.Drawing.Point(646, 45);
            this.lblAveragePrice.Name = "lblAveragePrice";
            this.lblAveragePrice.Size = new System.Drawing.Size(65, 12);
            this.lblAveragePrice.TabIndex = 16;
            this.lblAveragePrice.Text = "平均价格：";
            // 
            // tbDealAmount
            // 
            this.tbDealAmount.Enabled = false;
            this.tbDealAmount.Location = new System.Drawing.Point(521, 36);
            this.tbDealAmount.Name = "tbDealAmount";
            this.tbDealAmount.Size = new System.Drawing.Size(100, 21);
            this.tbDealAmount.TabIndex = 15;
            // 
            // lblDealAmount
            // 
            this.lblDealAmount.AutoSize = true;
            this.lblDealAmount.Location = new System.Drawing.Point(457, 45);
            this.lblDealAmount.Name = "lblDealAmount";
            this.lblDealAmount.Size = new System.Drawing.Size(65, 12);
            this.lblDealAmount.TabIndex = 14;
            this.lblDealAmount.Text = "成交数量：";
            // 
            // tbCommandAmount
            // 
            this.tbCommandAmount.Enabled = false;
            this.tbCommandAmount.Location = new System.Drawing.Point(321, 36);
            this.tbCommandAmount.Name = "tbCommandAmount";
            this.tbCommandAmount.Size = new System.Drawing.Size(100, 21);
            this.tbCommandAmount.TabIndex = 13;
            // 
            // lblCommandAmount
            // 
            this.lblCommandAmount.AutoSize = true;
            this.lblCommandAmount.Location = new System.Drawing.Point(255, 45);
            this.lblCommandAmount.Name = "lblCommandAmount";
            this.lblCommandAmount.Size = new System.Drawing.Size(65, 12);
            this.lblCommandAmount.TabIndex = 12;
            this.lblCommandAmount.Text = "指令数量：";
            // 
            // tbCommandPrice
            // 
            this.tbCommandPrice.Enabled = false;
            this.tbCommandPrice.Location = new System.Drawing.Point(110, 36);
            this.tbCommandPrice.Name = "tbCommandPrice";
            this.tbCommandPrice.Size = new System.Drawing.Size(100, 21);
            this.tbCommandPrice.TabIndex = 11;
            // 
            // lblCommandPrice
            // 
            this.lblCommandPrice.AutoSize = true;
            this.lblCommandPrice.Location = new System.Drawing.Point(43, 45);
            this.lblCommandPrice.Name = "lblCommandPrice";
            this.lblCommandPrice.Size = new System.Drawing.Size(65, 12);
            this.lblCommandPrice.TabIndex = 10;
            this.lblCommandPrice.Text = "指令价格：";
            // 
            // tbPriceMode
            // 
            this.tbPriceMode.Enabled = false;
            this.tbPriceMode.Location = new System.Drawing.Point(903, 8);
            this.tbPriceMode.Name = "tbPriceMode";
            this.tbPriceMode.Size = new System.Drawing.Size(100, 21);
            this.tbPriceMode.TabIndex = 9;
            // 
            // lblPriceMode
            // 
            this.lblPriceMode.AutoSize = true;
            this.lblPriceMode.Location = new System.Drawing.Point(840, 17);
            this.lblPriceMode.Name = "lblPriceMode";
            this.lblPriceMode.Size = new System.Drawing.Size(65, 12);
            this.lblPriceMode.TabIndex = 8;
            this.lblPriceMode.Text = "价格模式：";
            // 
            // tbSecuName
            // 
            this.tbSecuName.Enabled = false;
            this.tbSecuName.Location = new System.Drawing.Point(711, 8);
            this.tbSecuName.Name = "tbSecuName";
            this.tbSecuName.Size = new System.Drawing.Size(100, 21);
            this.tbSecuName.TabIndex = 7;
            // 
            // lblSecuName
            // 
            this.lblSecuName.AutoSize = true;
            this.lblSecuName.Location = new System.Drawing.Point(646, 17);
            this.lblSecuName.Name = "lblSecuName";
            this.lblSecuName.Size = new System.Drawing.Size(65, 12);
            this.lblSecuName.TabIndex = 6;
            this.lblSecuName.Text = "证券名称：";
            // 
            // tbPortName
            // 
            this.tbPortName.Enabled = false;
            this.tbPortName.Location = new System.Drawing.Point(521, 8);
            this.tbPortName.Name = "tbPortName";
            this.tbPortName.Size = new System.Drawing.Size(100, 21);
            this.tbPortName.TabIndex = 5;
            // 
            // lblPortName
            // 
            this.lblPortName.AutoSize = true;
            this.lblPortName.Location = new System.Drawing.Point(457, 17);
            this.lblPortName.Name = "lblPortName";
            this.lblPortName.Size = new System.Drawing.Size(65, 12);
            this.lblPortName.TabIndex = 4;
            this.lblPortName.Text = "组合名称：";
            // 
            // tbFundName
            // 
            this.tbFundName.Enabled = false;
            this.tbFundName.Location = new System.Drawing.Point(321, 8);
            this.tbFundName.Name = "tbFundName";
            this.tbFundName.Size = new System.Drawing.Size(100, 21);
            this.tbFundName.TabIndex = 3;
            // 
            // lblFundName
            // 
            this.lblFundName.AutoSize = true;
            this.lblFundName.Location = new System.Drawing.Point(255, 17);
            this.lblFundName.Name = "lblFundName";
            this.lblFundName.Size = new System.Drawing.Size(65, 12);
            this.lblFundName.TabIndex = 2;
            this.lblFundName.Text = "基金名称：";
            // 
            // tbCommandId
            // 
            this.tbCommandId.Enabled = false;
            this.tbCommandId.Location = new System.Drawing.Point(110, 8);
            this.tbCommandId.Name = "tbCommandId";
            this.tbCommandId.Size = new System.Drawing.Size(100, 21);
            this.tbCommandId.TabIndex = 1;
            // 
            // lblCommandId
            // 
            this.lblCommandId.AutoSize = true;
            this.lblCommandId.Location = new System.Drawing.Point(43, 17);
            this.lblCommandId.Name = "lblCommandId";
            this.lblCommandId.Size = new System.Drawing.Size(65, 12);
            this.lblCommandId.TabIndex = 0;
            this.lblCommandId.Text = "指令序号：";
            // 
            // tpSecurity
            // 
            this.tpSecurity.Controls.Add(this.secuGridView);
            this.tpSecurity.Location = new System.Drawing.Point(4, 22);
            this.tpSecurity.Name = "tpSecurity";
            this.tpSecurity.Padding = new System.Windows.Forms.Padding(3);
            this.tpSecurity.Size = new System.Drawing.Size(1117, 251);
            this.tpSecurity.TabIndex = 1;
            this.tpSecurity.Text = "指令证券";
            this.tpSecurity.UseVisualStyleBackColor = true;
            // 
            // tpEntrust
            // 
            this.tpEntrust.Controls.Add(this.entrustGridView);
            this.tpEntrust.Location = new System.Drawing.Point(4, 22);
            this.tpEntrust.Name = "tpEntrust";
            this.tpEntrust.Padding = new System.Windows.Forms.Padding(3);
            this.tpEntrust.Size = new System.Drawing.Size(1117, 251);
            this.tpEntrust.TabIndex = 2;
            this.tpEntrust.Text = "指令委托";
            this.tpEntrust.UseVisualStyleBackColor = true;
            // 
            // tpDeal
            // 
            this.tpDeal.Controls.Add(this.dealGridView);
            this.tpDeal.Location = new System.Drawing.Point(4, 22);
            this.tpDeal.Name = "tpDeal";
            this.tpDeal.Padding = new System.Windows.Forms.Padding(3);
            this.tpDeal.Size = new System.Drawing.Size(1117, 251);
            this.tpDeal.TabIndex = 3;
            this.tpDeal.Text = "指令成交";
            this.tpDeal.UseVisualStyleBackColor = true;
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
            // dealGridView
            // 
            this.dealGridView.AllowUserToAddRows = false;
            this.dealGridView.AllowUserToDeleteRows = false;
            this.dealGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dealGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dealGridView.Location = new System.Drawing.Point(3, 3);
            this.dealGridView.Name = "dealGridView";
            this.dealGridView.RowTemplate.Height = 23;
            this.dealGridView.Size = new System.Drawing.Size(1111, 245);
            this.dealGridView.TabIndex = 0;
            // 
            // secuGridView
            // 
            this.secuGridView.AllowUserToAddRows = false;
            this.secuGridView.AllowUserToDeleteRows = false;
            this.secuGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.secuGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secuGridView.Location = new System.Drawing.Point(3, 3);
            this.secuGridView.Name = "secuGridView";
            this.secuGridView.RowTemplate.Height = 23;
            this.secuGridView.Size = new System.Drawing.Size(1111, 245);
            this.secuGridView.TabIndex = 0;
            // 
            // entrustGridView
            // 
            this.entrustGridView.AllowUserToAddRows = false;
            this.entrustGridView.AllowUserToDeleteRows = false;
            this.entrustGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.entrustGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entrustGridView.Location = new System.Drawing.Point(3, 3);
            this.entrustGridView.Name = "entrustGridView";
            this.entrustGridView.RowTemplate.Height = 23;
            this.entrustGridView.Size = new System.Drawing.Size(1111, 245);
            this.entrustGridView.TabIndex = 0;
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
            this.topDownSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topDownSplitter)).EndInit();
            this.topDownSplitter.ResumeLayout(false);
            this.tabCommandMng.ResumeLayout(false);
            this.tpSummary.ResumeLayout(false);
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            this.tpSecurity.ResumeLayout(false);
            this.tpEntrust.ResumeLayout(false);
            this.tpDeal.ResumeLayout(false);
            this.cmToolStrip.ResumeLayout(false);
            this.cmToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dealGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secuGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entrustGridView)).EndInit();
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
        private System.Windows.Forms.TabControl tabCommandMng;
        private System.Windows.Forms.TabPage tpSummary;
        private System.Windows.Forms.TabPage tpSecurity;
        private System.Windows.Forms.TabPage tpEntrust;
        private System.Windows.Forms.TabPage tpDeal;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.TextBox tbNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox tbCancelCause;
        private System.Windows.Forms.Label lblCancelCause;
        private System.Windows.Forms.TextBox tbModifyCause;
        private System.Windows.Forms.Label lblModifyCause;
        private System.Windows.Forms.TextBox tbCancelTime;
        private System.Windows.Forms.Label lblCancelTime;
        private System.Windows.Forms.TextBox tbModifyTime;
        private System.Windows.Forms.Label lblModifyTime;
        private System.Windows.Forms.TextBox tbCancelPerson;
        private System.Windows.Forms.Label lblCancelPerson;
        private System.Windows.Forms.TextBox tbModifyPerson;
        private System.Windows.Forms.Label lblModifyPerson;
        private System.Windows.Forms.TextBox tbSubmitPerson;
        private System.Windows.Forms.Label lblSubmitPerson;
        private System.Windows.Forms.TextBox tbDealStatus;
        private System.Windows.Forms.Label lblDealStatus;
        private System.Windows.Forms.TextBox tbEntrustStatus;
        private System.Windows.Forms.Label lblEntrustStatus;
        private System.Windows.Forms.TextBox tbDispatchStatus;
        private System.Windows.Forms.Label lblDispatchStatus;
        private System.Windows.Forms.TextBox tbApprovalStatus;
        private System.Windows.Forms.Label lblApprovalStatus;
        private System.Windows.Forms.TextBox tbCommandStatus;
        private System.Windows.Forms.Label lblCommandStatus;
        private System.Windows.Forms.TextBox tbEndTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.TextBox tbEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.TextBox tbStartTime;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.TextBox tbStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.TextBox tbSubmitTime;
        private System.Windows.Forms.Label lblSubmitTime;
        private System.Windows.Forms.TextBox tbSubmitDate;
        private System.Windows.Forms.Label lblSubmitDate;
        private System.Windows.Forms.TextBox tbAveragePrice;
        private System.Windows.Forms.Label lblAveragePrice;
        private System.Windows.Forms.TextBox tbDealAmount;
        private System.Windows.Forms.Label lblDealAmount;
        private System.Windows.Forms.TextBox tbCommandAmount;
        private System.Windows.Forms.Label lblCommandAmount;
        private System.Windows.Forms.TextBox tbCommandPrice;
        private System.Windows.Forms.Label lblCommandPrice;
        private System.Windows.Forms.TextBox tbPriceMode;
        private System.Windows.Forms.Label lblPriceMode;
        private System.Windows.Forms.TextBox tbSecuName;
        private System.Windows.Forms.Label lblSecuName;
        private System.Windows.Forms.TextBox tbPortName;
        private System.Windows.Forms.Label lblPortName;
        private System.Windows.Forms.TextBox tbFundName;
        private System.Windows.Forms.Label lblFundName;
        private System.Windows.Forms.TextBox tbCommandId;
        private System.Windows.Forms.Label lblCommandId;
        private Controls.GridView.TSDataGridView secuGridView;
        private Controls.GridView.TSDataGridView entrustGridView;
        private Controls.GridView.TSDataGridView dealGridView;
    }
}
