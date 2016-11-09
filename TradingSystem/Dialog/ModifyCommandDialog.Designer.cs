namespace TradingSystem.Dialog
{
    partial class ModifyCommandDialog
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
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.childMainPanel = new System.Windows.Forms.Panel();
            this.gridView = new Controls.GridView.TSDataGridView();
            this.childBottomPanel = new System.Windows.Forms.Panel();
            this.btnCalc = new System.Windows.Forms.Button();
            this.lblAdjProportion = new System.Windows.Forms.Label();
            this.tbAdjProportion = new System.Windows.Forms.TextBox();
            this.lblFuture = new System.Windows.Forms.Label();
            this.tbFutures = new System.Windows.Forms.TextBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.tbTemplate = new System.Windows.Forms.TextBox();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.tbEndTime = new System.Windows.Forms.TextBox();
            this.tbStartTime = new System.Windows.Forms.TextBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.tbEndDate = new System.Windows.Forms.TextBox();
            this.tbStartDate = new System.Windows.Forms.TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblPoint = new System.Windows.Forms.Label();
            this.cbSubmitPerson = new System.Windows.Forms.ComboBox();
            this.lblSubmitPerson = new System.Windows.Forms.Label();
            this.tbBasisPoint = new System.Windows.Forms.TextBox();
            this.lblBasisPoint = new System.Windows.Forms.Label();
            this.childTopPanel = new System.Windows.Forms.Panel();
            this.tbCommandType = new System.Windows.Forms.TextBox();
            this.lblCommandType = new System.Windows.Forms.Label();
            this.tbInstCode = new System.Windows.Forms.TextBox();
            this.tbInstNo = new System.Windows.Forms.TextBox();
            this.tbPortfolioName = new System.Windows.Forms.TextBox();
            this.lblInstCode = new System.Windows.Forms.Label();
            this.lblInstNo = new System.Windows.Forms.Label();
            this.lblPortName = new System.Windows.Forms.Label();
            this.tbSubmitTime = new System.Windows.Forms.TextBox();
            this.tbExecuteStage = new System.Windows.Forms.TextBox();
            this.tbFundName = new System.Windows.Forms.TextBox();
            this.lblSubmitTime = new System.Windows.Forms.Label();
            this.lblExecuteStage = new System.Windows.Forms.Label();
            this.lblFundName = new System.Windows.Forms.Label();
            this.tbSubmitDate = new System.Windows.Forms.TextBox();
            this.tbArbType = new System.Windows.Forms.TextBox();
            this.tbCommandId = new System.Windows.Forms.TextBox();
            this.lblSubmitDate = new System.Windows.Forms.Label();
            this.lblArbType = new System.Windows.Forms.Label();
            this.lblCommandId = new System.Windows.Forms.Label();
            this.bottomPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.childMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.childBottomPanel.SuspendLayout();
            this.childTopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(886, 35);
            this.topPanel.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottomPanel.Controls.Add(this.btnCancel);
            this.bottomPanel.Controls.Add(this.btnConfirm);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 523);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(886, 36);
            this.bottomPanel.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(405, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(283, 6);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 23;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.childMainPanel);
            this.mainPanel.Controls.Add(this.childBottomPanel);
            this.mainPanel.Controls.Add(this.childTopPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 35);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(886, 488);
            this.mainPanel.TabIndex = 2;
            // 
            // childMainPanel
            // 
            this.childMainPanel.Controls.Add(this.gridView);
            this.childMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.childMainPanel.Location = new System.Drawing.Point(0, 90);
            this.childMainPanel.Name = "childMainPanel";
            this.childMainPanel.Size = new System.Drawing.Size(886, 228);
            this.childMainPanel.TabIndex = 2;
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
            this.gridView.Size = new System.Drawing.Size(886, 228);
            this.gridView.TabIndex = 0;
            // 
            // childBottomPanel
            // 
            this.childBottomPanel.Controls.Add(this.btnCalc);
            this.childBottomPanel.Controls.Add(this.lblAdjProportion);
            this.childBottomPanel.Controls.Add(this.tbAdjProportion);
            this.childBottomPanel.Controls.Add(this.lblFuture);
            this.childBottomPanel.Controls.Add(this.tbFutures);
            this.childBottomPanel.Controls.Add(this.lblTemplate);
            this.childBottomPanel.Controls.Add(this.tbTemplate);
            this.childBottomPanel.Controls.Add(this.tbNotes);
            this.childBottomPanel.Controls.Add(this.lblNotes);
            this.childBottomPanel.Controls.Add(this.tbEndTime);
            this.childBottomPanel.Controls.Add(this.tbStartTime);
            this.childBottomPanel.Controls.Add(this.lblTime);
            this.childBottomPanel.Controls.Add(this.tbEndDate);
            this.childBottomPanel.Controls.Add(this.tbStartDate);
            this.childBottomPanel.Controls.Add(this.lblDate);
            this.childBottomPanel.Controls.Add(this.lblPoint);
            this.childBottomPanel.Controls.Add(this.cbSubmitPerson);
            this.childBottomPanel.Controls.Add(this.lblSubmitPerson);
            this.childBottomPanel.Controls.Add(this.tbBasisPoint);
            this.childBottomPanel.Controls.Add(this.lblBasisPoint);
            this.childBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.childBottomPanel.Location = new System.Drawing.Point(0, 318);
            this.childBottomPanel.Name = "childBottomPanel";
            this.childBottomPanel.Size = new System.Drawing.Size(886, 170);
            this.childBottomPanel.TabIndex = 1;
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(460, 123);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(75, 23);
            this.btnCalc.TabIndex = 22;
            this.btnCalc.Text = "计算";
            this.btnCalc.UseVisualStyleBackColor = true;
            // 
            // lblAdjProportion
            // 
            this.lblAdjProportion.AutoSize = true;
            this.lblAdjProportion.Location = new System.Drawing.Point(458, 82);
            this.lblAdjProportion.Name = "lblAdjProportion";
            this.lblAdjProportion.Size = new System.Drawing.Size(71, 12);
            this.lblAdjProportion.TabIndex = 21;
            this.lblAdjProportion.Text = "调整后比例:";
            // 
            // tbAdjProportion
            // 
            this.tbAdjProportion.Location = new System.Drawing.Point(534, 73);
            this.tbAdjProportion.Name = "tbAdjProportion";
            this.tbAdjProportion.Size = new System.Drawing.Size(93, 21);
            this.tbAdjProportion.TabIndex = 20;
            // 
            // lblFuture
            // 
            this.lblFuture.AutoSize = true;
            this.lblFuture.Location = new System.Drawing.Point(458, 52);
            this.lblFuture.Name = "lblFuture";
            this.lblFuture.Size = new System.Drawing.Size(59, 12);
            this.lblFuture.TabIndex = 19;
            this.lblFuture.Text = "期货代码:";
            // 
            // tbFutures
            // 
            this.tbFutures.Enabled = false;
            this.tbFutures.Location = new System.Drawing.Point(534, 43);
            this.tbFutures.Name = "tbFutures";
            this.tbFutures.Size = new System.Drawing.Size(93, 21);
            this.tbFutures.TabIndex = 18;
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.Location = new System.Drawing.Point(458, 21);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(59, 12);
            this.lblTemplate.TabIndex = 17;
            this.lblTemplate.Text = "现货模板:";
            // 
            // tbTemplate
            // 
            this.tbTemplate.Enabled = false;
            this.tbTemplate.Location = new System.Drawing.Point(534, 13);
            this.tbTemplate.Name = "tbTemplate";
            this.tbTemplate.Size = new System.Drawing.Size(93, 21);
            this.tbTemplate.TabIndex = 16;
            // 
            // tbNotes
            // 
            this.tbNotes.Location = new System.Drawing.Point(77, 126);
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(93, 21);
            this.tbNotes.TabIndex = 15;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(24, 129);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(35, 12);
            this.lblNotes.TabIndex = 14;
            this.lblNotes.Text = "备注:";
            // 
            // tbEndTime
            // 
            this.tbEndTime.Location = new System.Drawing.Point(192, 93);
            this.tbEndTime.Name = "tbEndTime";
            this.tbEndTime.Size = new System.Drawing.Size(93, 21);
            this.tbEndTime.TabIndex = 13;
            // 
            // tbStartTime
            // 
            this.tbStartTime.Location = new System.Drawing.Point(78, 93);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.Size = new System.Drawing.Size(93, 21);
            this.tbStartTime.TabIndex = 12;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(24, 102);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(35, 12);
            this.lblTime.TabIndex = 11;
            this.lblTime.Text = "时间:";
            // 
            // tbEndDate
            // 
            this.tbEndDate.Location = new System.Drawing.Point(192, 63);
            this.tbEndDate.Name = "tbEndDate";
            this.tbEndDate.Size = new System.Drawing.Size(93, 21);
            this.tbEndDate.TabIndex = 10;
            // 
            // tbStartDate
            // 
            this.tbStartDate.Location = new System.Drawing.Point(78, 63);
            this.tbStartDate.Name = "tbStartDate";
            this.tbStartDate.Size = new System.Drawing.Size(93, 21);
            this.tbStartDate.TabIndex = 9;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(24, 72);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(35, 12);
            this.lblDate.TabIndex = 8;
            this.lblDate.Text = "日期:";
            // 
            // lblPoint
            // 
            this.lblPoint.AutoSize = true;
            this.lblPoint.Location = new System.Drawing.Point(176, 14);
            this.lblPoint.Name = "lblPoint";
            this.lblPoint.Size = new System.Drawing.Size(17, 12);
            this.lblPoint.TabIndex = 7;
            this.lblPoint.Text = "点";
            // 
            // cbSubmitPerson
            // 
            this.cbSubmitPerson.Enabled = false;
            this.cbSubmitPerson.FormattingEnabled = true;
            this.cbSubmitPerson.Location = new System.Drawing.Point(78, 35);
            this.cbSubmitPerson.Name = "cbSubmitPerson";
            this.cbSubmitPerson.Size = new System.Drawing.Size(121, 20);
            this.cbSubmitPerson.TabIndex = 6;
            // 
            // lblSubmitPerson
            // 
            this.lblSubmitPerson.AutoSize = true;
            this.lblSubmitPerson.Location = new System.Drawing.Point(24, 39);
            this.lblSubmitPerson.Name = "lblSubmitPerson";
            this.lblSubmitPerson.Size = new System.Drawing.Size(47, 12);
            this.lblSubmitPerson.TabIndex = 5;
            this.lblSubmitPerson.Text = "下达人:";
            // 
            // tbBasisPoint
            // 
            this.tbBasisPoint.Location = new System.Drawing.Point(77, 8);
            this.tbBasisPoint.Name = "tbBasisPoint";
            this.tbBasisPoint.Size = new System.Drawing.Size(93, 21);
            this.tbBasisPoint.TabIndex = 4;
            // 
            // lblBasisPoint
            // 
            this.lblBasisPoint.AutoSize = true;
            this.lblBasisPoint.Location = new System.Drawing.Point(24, 12);
            this.lblBasisPoint.Name = "lblBasisPoint";
            this.lblBasisPoint.Size = new System.Drawing.Size(59, 12);
            this.lblBasisPoint.TabIndex = 0;
            this.lblBasisPoint.Text = "基差点位:";
            // 
            // childTopPanel
            // 
            this.childTopPanel.BackColor = System.Drawing.Color.Silver;
            this.childTopPanel.Controls.Add(this.tbCommandType);
            this.childTopPanel.Controls.Add(this.lblCommandType);
            this.childTopPanel.Controls.Add(this.tbInstCode);
            this.childTopPanel.Controls.Add(this.tbInstNo);
            this.childTopPanel.Controls.Add(this.tbPortfolioName);
            this.childTopPanel.Controls.Add(this.lblInstCode);
            this.childTopPanel.Controls.Add(this.lblInstNo);
            this.childTopPanel.Controls.Add(this.lblPortName);
            this.childTopPanel.Controls.Add(this.tbSubmitTime);
            this.childTopPanel.Controls.Add(this.tbExecuteStage);
            this.childTopPanel.Controls.Add(this.tbFundName);
            this.childTopPanel.Controls.Add(this.lblSubmitTime);
            this.childTopPanel.Controls.Add(this.lblExecuteStage);
            this.childTopPanel.Controls.Add(this.lblFundName);
            this.childTopPanel.Controls.Add(this.tbSubmitDate);
            this.childTopPanel.Controls.Add(this.tbArbType);
            this.childTopPanel.Controls.Add(this.tbCommandId);
            this.childTopPanel.Controls.Add(this.lblSubmitDate);
            this.childTopPanel.Controls.Add(this.lblArbType);
            this.childTopPanel.Controls.Add(this.lblCommandId);
            this.childTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.childTopPanel.Location = new System.Drawing.Point(0, 0);
            this.childTopPanel.Name = "childTopPanel";
            this.childTopPanel.Size = new System.Drawing.Size(886, 90);
            this.childTopPanel.TabIndex = 0;
            // 
            // tbCommandType
            // 
            this.tbCommandType.Enabled = false;
            this.tbCommandType.Location = new System.Drawing.Point(729, 8);
            this.tbCommandType.Name = "tbCommandType";
            this.tbCommandType.Size = new System.Drawing.Size(100, 21);
            this.tbCommandType.TabIndex = 19;
            // 
            // lblCommandType
            // 
            this.lblCommandType.AutoSize = true;
            this.lblCommandType.Location = new System.Drawing.Point(662, 13);
            this.lblCommandType.Name = "lblCommandType";
            this.lblCommandType.Size = new System.Drawing.Size(59, 12);
            this.lblCommandType.TabIndex = 18;
            this.lblCommandType.Text = "指令类型:";
            // 
            // tbInstCode
            // 
            this.tbInstCode.Enabled = false;
            this.tbInstCode.Location = new System.Drawing.Point(531, 60);
            this.tbInstCode.Name = "tbInstCode";
            this.tbInstCode.Size = new System.Drawing.Size(100, 21);
            this.tbInstCode.TabIndex = 17;
            // 
            // tbInstNo
            // 
            this.tbInstNo.Enabled = false;
            this.tbInstNo.Location = new System.Drawing.Point(531, 34);
            this.tbInstNo.Name = "tbInstNo";
            this.tbInstNo.Size = new System.Drawing.Size(100, 21);
            this.tbInstNo.TabIndex = 16;
            // 
            // tbPortfolioName
            // 
            this.tbPortfolioName.Enabled = false;
            this.tbPortfolioName.Location = new System.Drawing.Point(531, 6);
            this.tbPortfolioName.Name = "tbPortfolioName";
            this.tbPortfolioName.Size = new System.Drawing.Size(100, 21);
            this.tbPortfolioName.TabIndex = 15;
            // 
            // lblInstCode
            // 
            this.lblInstCode.AutoSize = true;
            this.lblInstCode.Location = new System.Drawing.Point(464, 63);
            this.lblInstCode.Name = "lblInstCode";
            this.lblInstCode.Size = new System.Drawing.Size(59, 12);
            this.lblInstCode.TabIndex = 14;
            this.lblInstCode.Text = "实例编号:";
            // 
            // lblInstNo
            // 
            this.lblInstNo.AutoSize = true;
            this.lblInstNo.Location = new System.Drawing.Point(464, 37);
            this.lblInstNo.Name = "lblInstNo";
            this.lblInstNo.Size = new System.Drawing.Size(59, 12);
            this.lblInstNo.TabIndex = 13;
            this.lblInstNo.Text = "实例序号:";
            // 
            // lblPortName
            // 
            this.lblPortName.AutoSize = true;
            this.lblPortName.Location = new System.Drawing.Point(464, 11);
            this.lblPortName.Name = "lblPortName";
            this.lblPortName.Size = new System.Drawing.Size(59, 12);
            this.lblPortName.TabIndex = 12;
            this.lblPortName.Text = "组合名称:";
            // 
            // tbSubmitTime
            // 
            this.tbSubmitTime.Enabled = false;
            this.tbSubmitTime.Location = new System.Drawing.Point(311, 62);
            this.tbSubmitTime.Name = "tbSubmitTime";
            this.tbSubmitTime.Size = new System.Drawing.Size(100, 21);
            this.tbSubmitTime.TabIndex = 11;
            // 
            // tbExecuteStage
            // 
            this.tbExecuteStage.Enabled = false;
            this.tbExecuteStage.Location = new System.Drawing.Point(311, 36);
            this.tbExecuteStage.Name = "tbExecuteStage";
            this.tbExecuteStage.Size = new System.Drawing.Size(100, 21);
            this.tbExecuteStage.TabIndex = 10;
            // 
            // tbFundName
            // 
            this.tbFundName.Enabled = false;
            this.tbFundName.Location = new System.Drawing.Point(311, 8);
            this.tbFundName.Name = "tbFundName";
            this.tbFundName.Size = new System.Drawing.Size(100, 21);
            this.tbFundName.TabIndex = 9;
            // 
            // lblSubmitTime
            // 
            this.lblSubmitTime.AutoSize = true;
            this.lblSubmitTime.Location = new System.Drawing.Point(244, 65);
            this.lblSubmitTime.Name = "lblSubmitTime";
            this.lblSubmitTime.Size = new System.Drawing.Size(59, 12);
            this.lblSubmitTime.TabIndex = 8;
            this.lblSubmitTime.Text = "下达时间:";
            // 
            // lblExecuteStage
            // 
            this.lblExecuteStage.AutoSize = true;
            this.lblExecuteStage.Location = new System.Drawing.Point(220, 39);
            this.lblExecuteStage.Name = "lblExecuteStage";
            this.lblExecuteStage.Size = new System.Drawing.Size(83, 12);
            this.lblExecuteStage.TabIndex = 7;
            this.lblExecuteStage.Text = "指令执行阶段:";
            // 
            // lblFundName
            // 
            this.lblFundName.AutoSize = true;
            this.lblFundName.Location = new System.Drawing.Point(244, 13);
            this.lblFundName.Name = "lblFundName";
            this.lblFundName.Size = new System.Drawing.Size(59, 12);
            this.lblFundName.TabIndex = 6;
            this.lblFundName.Text = "基金名称:";
            // 
            // tbSubmitDate
            // 
            this.tbSubmitDate.Enabled = false;
            this.tbSubmitDate.Location = new System.Drawing.Point(70, 62);
            this.tbSubmitDate.Name = "tbSubmitDate";
            this.tbSubmitDate.Size = new System.Drawing.Size(100, 21);
            this.tbSubmitDate.TabIndex = 5;
            // 
            // tbArbType
            // 
            this.tbArbType.Enabled = false;
            this.tbArbType.Location = new System.Drawing.Point(70, 36);
            this.tbArbType.Name = "tbArbType";
            this.tbArbType.Size = new System.Drawing.Size(100, 21);
            this.tbArbType.TabIndex = 4;
            // 
            // tbCommandId
            // 
            this.tbCommandId.Enabled = false;
            this.tbCommandId.Location = new System.Drawing.Point(70, 8);
            this.tbCommandId.Name = "tbCommandId";
            this.tbCommandId.Size = new System.Drawing.Size(100, 21);
            this.tbCommandId.TabIndex = 3;
            // 
            // lblSubmitDate
            // 
            this.lblSubmitDate.AutoSize = true;
            this.lblSubmitDate.Location = new System.Drawing.Point(3, 65);
            this.lblSubmitDate.Name = "lblSubmitDate";
            this.lblSubmitDate.Size = new System.Drawing.Size(59, 12);
            this.lblSubmitDate.TabIndex = 2;
            this.lblSubmitDate.Text = "下达日期:";
            // 
            // lblArbType
            // 
            this.lblArbType.AutoSize = true;
            this.lblArbType.Location = new System.Drawing.Point(3, 39);
            this.lblArbType.Name = "lblArbType";
            this.lblArbType.Size = new System.Drawing.Size(59, 12);
            this.lblArbType.TabIndex = 1;
            this.lblArbType.Text = "套利类型:";
            // 
            // lblCommandId
            // 
            this.lblCommandId.AutoSize = true;
            this.lblCommandId.Location = new System.Drawing.Point(3, 13);
            this.lblCommandId.Name = "lblCommandId";
            this.lblCommandId.Size = new System.Drawing.Size(59, 12);
            this.lblCommandId.TabIndex = 0;
            this.lblCommandId.Text = "指令序号:";
            // 
            // ModifyCommandDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(886, 559);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Name = "ModifyCommandDialog";
            this.Text = "指令修改";
            this.bottomPanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.childMainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.childBottomPanel.ResumeLayout(false);
            this.childBottomPanel.PerformLayout();
            this.childTopPanel.ResumeLayout(false);
            this.childTopPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel childMainPanel;
        private System.Windows.Forms.Panel childBottomPanel;
        private System.Windows.Forms.Panel childTopPanel;
        private System.Windows.Forms.Label lblAdjProportion;
        private System.Windows.Forms.TextBox tbAdjProportion;
        private System.Windows.Forms.Label lblFuture;
        private System.Windows.Forms.TextBox tbFutures;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.TextBox tbTemplate;
        private System.Windows.Forms.TextBox tbNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox tbEndTime;
        private System.Windows.Forms.TextBox tbStartTime;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.TextBox tbEndDate;
        private System.Windows.Forms.TextBox tbStartDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblPoint;
        private System.Windows.Forms.ComboBox cbSubmitPerson;
        private System.Windows.Forms.Label lblSubmitPerson;
        private System.Windows.Forms.TextBox tbBasisPoint;
        private System.Windows.Forms.Label lblBasisPoint;
        private System.Windows.Forms.TextBox tbCommandType;
        private System.Windows.Forms.Label lblCommandType;
        private System.Windows.Forms.TextBox tbInstCode;
        private System.Windows.Forms.TextBox tbInstNo;
        private System.Windows.Forms.TextBox tbPortfolioName;
        private System.Windows.Forms.Label lblInstCode;
        private System.Windows.Forms.Label lblInstNo;
        private System.Windows.Forms.Label lblPortName;
        private System.Windows.Forms.TextBox tbSubmitTime;
        private System.Windows.Forms.TextBox tbExecuteStage;
        private System.Windows.Forms.TextBox tbFundName;
        private System.Windows.Forms.Label lblSubmitTime;
        private System.Windows.Forms.Label lblExecuteStage;
        private System.Windows.Forms.Label lblFundName;
        private System.Windows.Forms.TextBox tbSubmitDate;
        private System.Windows.Forms.TextBox tbArbType;
        private System.Windows.Forms.TextBox tbCommandId;
        private System.Windows.Forms.Label lblSubmitDate;
        private System.Windows.Forms.Label lblArbType;
        private System.Windows.Forms.Label lblCommandId;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCalc;
        private Controls.GridView.TSDataGridView gridView;
    }
}
