namespace TradingSystem.Dialog
{
    partial class OpenPositionDialog
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblCaption = new System.Windows.Forms.Label();
            this.lblPortfolio = new System.Windows.Forms.Label();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.lblFutures = new System.Windows.Forms.Label();
            this.lblCopies = new System.Windows.Forms.Label();
            this.lblPoint = new System.Windows.Forms.Label();
            this.lblInstancCode = new System.Windows.Forms.Label();
            this.tbPortfolio = new System.Windows.Forms.TextBox();
            this.tbTemlate = new System.Windows.Forms.TextBox();
            this.tbFutures = new System.Windows.Forms.TextBox();
            this.tbCopies = new System.Windows.Forms.TextBox();
            this.tbBias = new System.Windows.Forms.TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.tbStartDate = new System.Windows.Forms.TextBox();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.tbEndDate = new System.Windows.Forms.TextBox();
            this.tbEndTime = new System.Windows.Forms.TextBox();
            this.lblTimeTo = new System.Windows.Forms.Label();
            this.tbStartTime = new System.Windows.Forms.TextBox();
            this.lblCopyUnit = new System.Windows.Forms.Label();
            this.lblBaisUnit = new System.Windows.Forms.Label();
            this.cbInstanceCode = new System.Windows.Forms.ComboBox();
            this.ckbInstanceCode = new System.Windows.Forms.CheckBox();
            this.rtbComment = new System.Windows.Forms.RichTextBox();
            this.panelTop.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTop.Controls.Add(this.lblCaption);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(526, 52);
            this.panelTop.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 230);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(526, 47);
            this.panelBottom.TabIndex = 1;
            // 
            // panelMain
            // 
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMain.Controls.Add(this.rtbComment);
            this.panelMain.Controls.Add(this.ckbInstanceCode);
            this.panelMain.Controls.Add(this.cbInstanceCode);
            this.panelMain.Controls.Add(this.lblBaisUnit);
            this.panelMain.Controls.Add(this.lblCopyUnit);
            this.panelMain.Controls.Add(this.tbEndTime);
            this.panelMain.Controls.Add(this.lblTimeTo);
            this.panelMain.Controls.Add(this.tbStartTime);
            this.panelMain.Controls.Add(this.tbEndDate);
            this.panelMain.Controls.Add(this.lblDateTo);
            this.panelMain.Controls.Add(this.tbStartDate);
            this.panelMain.Controls.Add(this.lblComment);
            this.panelMain.Controls.Add(this.lblTime);
            this.panelMain.Controls.Add(this.lblDate);
            this.panelMain.Controls.Add(this.tbBias);
            this.panelMain.Controls.Add(this.tbCopies);
            this.panelMain.Controls.Add(this.tbFutures);
            this.panelMain.Controls.Add(this.tbTemlate);
            this.panelMain.Controls.Add(this.tbPortfolio);
            this.panelMain.Controls.Add(this.lblInstancCode);
            this.panelMain.Controls.Add(this.lblPoint);
            this.panelMain.Controls.Add(this.lblCopies);
            this.panelMain.Controls.Add(this.lblFutures);
            this.panelMain.Controls.Add(this.lblTemplate);
            this.panelMain.Controls.Add(this.lblPortfolio);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 52);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(3);
            this.panelMain.Size = new System.Drawing.Size(526, 178);
            this.panelMain.TabIndex = 2;
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Location = new System.Drawing.Point(146, 19);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(101, 12);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "期现套利开仓确认";
            // 
            // lblPortfolio
            // 
            this.lblPortfolio.AutoSize = true;
            this.lblPortfolio.Location = new System.Drawing.Point(40, 15);
            this.lblPortfolio.Name = "lblPortfolio";
            this.lblPortfolio.Size = new System.Drawing.Size(59, 12);
            this.lblPortfolio.TabIndex = 0;
            this.lblPortfolio.Text = "套利组合:";
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.Location = new System.Drawing.Point(40, 45);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(59, 12);
            this.lblTemplate.TabIndex = 1;
            this.lblTemplate.Text = "现货模板:";
            // 
            // lblFutures
            // 
            this.lblFutures.AutoSize = true;
            this.lblFutures.Location = new System.Drawing.Point(40, 71);
            this.lblFutures.Name = "lblFutures";
            this.lblFutures.Size = new System.Drawing.Size(59, 12);
            this.lblFutures.TabIndex = 2;
            this.lblFutures.Text = "期货合约:";
            // 
            // lblCopies
            // 
            this.lblCopies.AutoSize = true;
            this.lblCopies.Location = new System.Drawing.Point(40, 98);
            this.lblCopies.Name = "lblCopies";
            this.lblCopies.Size = new System.Drawing.Size(59, 12);
            this.lblCopies.TabIndex = 3;
            this.lblCopies.Text = "操作份数:";
            // 
            // lblPoint
            // 
            this.lblPoint.AutoSize = true;
            this.lblPoint.Location = new System.Drawing.Point(40, 126);
            this.lblPoint.Name = "lblPoint";
            this.lblPoint.Size = new System.Drawing.Size(59, 12);
            this.lblPoint.TabIndex = 4;
            this.lblPoint.Text = "基    差:";
            // 
            // lblInstancCode
            // 
            this.lblInstancCode.AutoSize = true;
            this.lblInstancCode.Location = new System.Drawing.Point(15, 152);
            this.lblInstancCode.Name = "lblInstancCode";
            this.lblInstancCode.Size = new System.Drawing.Size(83, 12);
            this.lblInstancCode.TabIndex = 5;
            this.lblInstancCode.Text = "交易实例编号:";
            // 
            // tbPortfolio
            // 
            this.tbPortfolio.Enabled = false;
            this.tbPortfolio.Location = new System.Drawing.Point(105, 10);
            this.tbPortfolio.Name = "tbPortfolio";
            this.tbPortfolio.Size = new System.Drawing.Size(142, 21);
            this.tbPortfolio.TabIndex = 6;
            // 
            // tbTemlate
            // 
            this.tbTemlate.Enabled = false;
            this.tbTemlate.Location = new System.Drawing.Point(105, 39);
            this.tbTemlate.Name = "tbTemlate";
            this.tbTemlate.Size = new System.Drawing.Size(142, 21);
            this.tbTemlate.TabIndex = 7;
            // 
            // tbFutures
            // 
            this.tbFutures.Enabled = false;
            this.tbFutures.Location = new System.Drawing.Point(105, 66);
            this.tbFutures.Name = "tbFutures";
            this.tbFutures.Size = new System.Drawing.Size(142, 21);
            this.tbFutures.TabIndex = 8;
            // 
            // tbCopies
            // 
            this.tbCopies.Enabled = false;
            this.tbCopies.Location = new System.Drawing.Point(105, 93);
            this.tbCopies.Name = "tbCopies";
            this.tbCopies.Size = new System.Drawing.Size(100, 21);
            this.tbCopies.TabIndex = 9;
            // 
            // tbBias
            // 
            this.tbBias.Location = new System.Drawing.Point(105, 120);
            this.tbBias.Name = "tbBias";
            this.tbBias.Size = new System.Drawing.Size(107, 21);
            this.tbBias.TabIndex = 10;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(286, 15);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(59, 12);
            this.lblDate.TabIndex = 12;
            this.lblDate.Text = "日    期:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(287, 47);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(59, 12);
            this.lblTime.TabIndex = 13;
            this.lblTime.Text = "时    间:";
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(287, 75);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(59, 12);
            this.lblComment.TabIndex = 14;
            this.lblComment.Text = "备    注:";
            // 
            // tbStartDate
            // 
            this.tbStartDate.Location = new System.Drawing.Point(349, 10);
            this.tbStartDate.Name = "tbStartDate";
            this.tbStartDate.Size = new System.Drawing.Size(62, 21);
            this.tbStartDate.TabIndex = 15;
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Location = new System.Drawing.Point(420, 19);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(11, 12);
            this.lblDateTo.TabIndex = 16;
            this.lblDateTo.Text = "~";
            // 
            // tbEndDate
            // 
            this.tbEndDate.Location = new System.Drawing.Point(444, 10);
            this.tbEndDate.Name = "tbEndDate";
            this.tbEndDate.Size = new System.Drawing.Size(62, 21);
            this.tbEndDate.TabIndex = 17;
            // 
            // tbEndTime
            // 
            this.tbEndTime.Location = new System.Drawing.Point(444, 42);
            this.tbEndTime.Name = "tbEndTime";
            this.tbEndTime.Size = new System.Drawing.Size(62, 21);
            this.tbEndTime.TabIndex = 20;
            // 
            // lblTimeTo
            // 
            this.lblTimeTo.AutoSize = true;
            this.lblTimeTo.Location = new System.Drawing.Point(420, 51);
            this.lblTimeTo.Name = "lblTimeTo";
            this.lblTimeTo.Size = new System.Drawing.Size(11, 12);
            this.lblTimeTo.TabIndex = 19;
            this.lblTimeTo.Text = "~";
            // 
            // tbStartTime
            // 
            this.tbStartTime.Location = new System.Drawing.Point(349, 42);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.Size = new System.Drawing.Size(62, 21);
            this.tbStartTime.TabIndex = 18;
            // 
            // lblCopyUnit
            // 
            this.lblCopyUnit.AutoSize = true;
            this.lblCopyUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCopyUnit.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCopyUnit.Location = new System.Drawing.Point(210, 94);
            this.lblCopyUnit.Name = "lblCopyUnit";
            this.lblCopyUnit.Padding = new System.Windows.Forms.Padding(3);
            this.lblCopyUnit.Size = new System.Drawing.Size(37, 20);
            this.lblCopyUnit.TabIndex = 21;
            this.lblCopyUnit.Text = "套手";
            // 
            // lblBaisUnit
            // 
            this.lblBaisUnit.AutoSize = true;
            this.lblBaisUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBaisUnit.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblBaisUnit.Location = new System.Drawing.Point(218, 121);
            this.lblBaisUnit.Name = "lblBaisUnit";
            this.lblBaisUnit.Padding = new System.Windows.Forms.Padding(3);
            this.lblBaisUnit.Size = new System.Drawing.Size(25, 20);
            this.lblBaisUnit.TabIndex = 22;
            this.lblBaisUnit.Text = "点";
            // 
            // cbInstanceCode
            // 
            this.cbInstanceCode.FormattingEnabled = true;
            this.cbInstanceCode.Location = new System.Drawing.Point(105, 147);
            this.cbInstanceCode.Name = "cbInstanceCode";
            this.cbInstanceCode.Size = new System.Drawing.Size(175, 20);
            this.cbInstanceCode.TabIndex = 23;
            // 
            // ckbInstanceCode
            // 
            this.ckbInstanceCode.AutoSize = true;
            this.ckbInstanceCode.Location = new System.Drawing.Point(287, 150);
            this.ckbInstanceCode.Name = "ckbInstanceCode";
            this.ckbInstanceCode.Size = new System.Drawing.Size(120, 16);
            this.ckbInstanceCode.TabIndex = 24;
            this.ckbInstanceCode.Text = "手工指定交易实例";
            this.ckbInstanceCode.UseVisualStyleBackColor = true;
            // 
            // rtbComment
            // 
            this.rtbComment.Location = new System.Drawing.Point(349, 75);
            this.rtbComment.Name = "rtbComment";
            this.rtbComment.Size = new System.Drawing.Size(157, 66);
            this.rtbComment.TabIndex = 25;
            this.rtbComment.Text = "";
            // 
            // OpenPositionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(526, 277);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "OpenPositionDialog";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.CheckBox ckbInstanceCode;
        private System.Windows.Forms.ComboBox cbInstanceCode;
        private System.Windows.Forms.Label lblBaisUnit;
        private System.Windows.Forms.Label lblCopyUnit;
        private System.Windows.Forms.TextBox tbEndTime;
        private System.Windows.Forms.Label lblTimeTo;
        private System.Windows.Forms.TextBox tbStartTime;
        private System.Windows.Forms.TextBox tbEndDate;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.TextBox tbStartDate;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox tbBias;
        private System.Windows.Forms.TextBox tbCopies;
        private System.Windows.Forms.TextBox tbFutures;
        private System.Windows.Forms.TextBox tbTemlate;
        private System.Windows.Forms.TextBox tbPortfolio;
        private System.Windows.Forms.Label lblInstancCode;
        private System.Windows.Forms.Label lblPoint;
        private System.Windows.Forms.Label lblCopies;
        private System.Windows.Forms.Label lblFutures;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.Label lblPortfolio;
        private System.Windows.Forms.RichTextBox rtbComment;
    }
}
