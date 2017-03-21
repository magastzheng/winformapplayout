namespace TradingSystem.Dialog
{
    partial class TradeInstanceModifyDialog
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblCaption = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblInstanceId = new System.Windows.Forms.Label();
            this.tbInstanceId = new System.Windows.Forms.TextBox();
            this.tbInstanceCode = new System.Windows.Forms.TextBox();
            this.lblInstanceCode = new System.Windows.Forms.Label();
            this.lblMonitorUnit = new System.Windows.Forms.Label();
            this.cbMonitorUnit = new System.Windows.Forms.ComboBox();
            this.cbTemplate = new System.Windows.Forms.ComboBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.cbFundCode = new System.Windows.Forms.ComboBox();
            this.lblFundCode = new System.Windows.Forms.Label();
            this.cbAssetUnit = new System.Windows.Forms.ComboBox();
            this.lblAssetUnit = new System.Windows.Forms.Label();
            this.cbPortfolio = new System.Windows.Forms.ComboBox();
            this.lblPortfolio = new System.Windows.Forms.Label();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
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
            this.panelTop.Size = new System.Drawing.Size(351, 48);
            this.panelTop.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnConfirm);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 334);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(351, 38);
            this.panelBottom.TabIndex = 1;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tbNotes);
            this.panelMain.Controls.Add(this.lblNotes);
            this.panelMain.Controls.Add(this.cbPortfolio);
            this.panelMain.Controls.Add(this.lblPortfolio);
            this.panelMain.Controls.Add(this.cbAssetUnit);
            this.panelMain.Controls.Add(this.lblAssetUnit);
            this.panelMain.Controls.Add(this.cbFundCode);
            this.panelMain.Controls.Add(this.lblFundCode);
            this.panelMain.Controls.Add(this.cbTemplate);
            this.panelMain.Controls.Add(this.lblTemplate);
            this.panelMain.Controls.Add(this.cbMonitorUnit);
            this.panelMain.Controls.Add(this.lblMonitorUnit);
            this.panelMain.Controls.Add(this.tbInstanceCode);
            this.panelMain.Controls.Add(this.lblInstanceCode);
            this.panelMain.Controls.Add(this.tbInstanceId);
            this.panelMain.Controls.Add(this.lblInstanceId);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 48);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(351, 286);
            this.panelMain.TabIndex = 2;
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("SimSun", 12F);
            this.lblCaption.Location = new System.Drawing.Point(104, 18);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(120, 16);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "编辑实例的信息";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(43, 7);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(230, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblInstanceId
            // 
            this.lblInstanceId.AutoSize = true;
            this.lblInstanceId.Location = new System.Drawing.Point(37, 15);
            this.lblInstanceId.Name = "lblInstanceId";
            this.lblInstanceId.Size = new System.Drawing.Size(65, 12);
            this.lblInstanceId.TabIndex = 0;
            this.lblInstanceId.Text = "实例序号：";
            // 
            // tbInstanceId
            // 
            this.tbInstanceId.Location = new System.Drawing.Point(107, 10);
            this.tbInstanceId.Name = "tbInstanceId";
            this.tbInstanceId.Size = new System.Drawing.Size(209, 21);
            this.tbInstanceId.TabIndex = 1;
            // 
            // tbInstanceCode
            // 
            this.tbInstanceCode.Location = new System.Drawing.Point(107, 43);
            this.tbInstanceCode.Name = "tbInstanceCode";
            this.tbInstanceCode.Size = new System.Drawing.Size(209, 21);
            this.tbInstanceCode.TabIndex = 3;
            // 
            // lblInstanceCode
            // 
            this.lblInstanceCode.AutoSize = true;
            this.lblInstanceCode.Location = new System.Drawing.Point(37, 48);
            this.lblInstanceCode.Name = "lblInstanceCode";
            this.lblInstanceCode.Size = new System.Drawing.Size(65, 12);
            this.lblInstanceCode.TabIndex = 2;
            this.lblInstanceCode.Text = "实例编号：";
            // 
            // lblMonitorUnit
            // 
            this.lblMonitorUnit.AutoSize = true;
            this.lblMonitorUnit.Location = new System.Drawing.Point(37, 80);
            this.lblMonitorUnit.Name = "lblMonitorUnit";
            this.lblMonitorUnit.Size = new System.Drawing.Size(65, 12);
            this.lblMonitorUnit.TabIndex = 4;
            this.lblMonitorUnit.Text = "监控单元：";
            // 
            // cbMonitorUnit
            // 
            this.cbMonitorUnit.FormattingEnabled = true;
            this.cbMonitorUnit.Location = new System.Drawing.Point(107, 76);
            this.cbMonitorUnit.Name = "cbMonitorUnit";
            this.cbMonitorUnit.Size = new System.Drawing.Size(209, 20);
            this.cbMonitorUnit.TabIndex = 5;
            // 
            // cbTemplate
            // 
            this.cbTemplate.FormattingEnabled = true;
            this.cbTemplate.Location = new System.Drawing.Point(107, 108);
            this.cbTemplate.Name = "cbTemplate";
            this.cbTemplate.Size = new System.Drawing.Size(209, 20);
            this.cbTemplate.TabIndex = 7;
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.Location = new System.Drawing.Point(37, 112);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(65, 12);
            this.lblTemplate.TabIndex = 6;
            this.lblTemplate.Text = "现货模板：";
            // 
            // cbFundCode
            // 
            this.cbFundCode.FormattingEnabled = true;
            this.cbFundCode.Location = new System.Drawing.Point(107, 140);
            this.cbFundCode.Name = "cbFundCode";
            this.cbFundCode.Size = new System.Drawing.Size(209, 20);
            this.cbFundCode.TabIndex = 9;
            // 
            // lblFundCode
            // 
            this.lblFundCode.AutoSize = true;
            this.lblFundCode.Location = new System.Drawing.Point(37, 144);
            this.lblFundCode.Name = "lblFundCode";
            this.lblFundCode.Size = new System.Drawing.Size(65, 12);
            this.lblFundCode.TabIndex = 8;
            this.lblFundCode.Text = "基金编号：";
            // 
            // cbAssetUnit
            // 
            this.cbAssetUnit.FormattingEnabled = true;
            this.cbAssetUnit.Location = new System.Drawing.Point(108, 172);
            this.cbAssetUnit.Name = "cbAssetUnit";
            this.cbAssetUnit.Size = new System.Drawing.Size(209, 20);
            this.cbAssetUnit.TabIndex = 11;
            // 
            // lblAssetUnit
            // 
            this.lblAssetUnit.AutoSize = true;
            this.lblAssetUnit.Location = new System.Drawing.Point(38, 176);
            this.lblAssetUnit.Name = "lblAssetUnit";
            this.lblAssetUnit.Size = new System.Drawing.Size(65, 12);
            this.lblAssetUnit.TabIndex = 10;
            this.lblAssetUnit.Text = "资产单元：";
            // 
            // cbPortfolio
            // 
            this.cbPortfolio.FormattingEnabled = true;
            this.cbPortfolio.Location = new System.Drawing.Point(107, 204);
            this.cbPortfolio.Name = "cbPortfolio";
            this.cbPortfolio.Size = new System.Drawing.Size(209, 20);
            this.cbPortfolio.TabIndex = 13;
            // 
            // lblPortfolio
            // 
            this.lblPortfolio.AutoSize = true;
            this.lblPortfolio.Location = new System.Drawing.Point(37, 208);
            this.lblPortfolio.Name = "lblPortfolio";
            this.lblPortfolio.Size = new System.Drawing.Size(65, 12);
            this.lblPortfolio.TabIndex = 12;
            this.lblPortfolio.Text = "组合编号：";
            // 
            // tbNotes
            // 
            this.tbNotes.Location = new System.Drawing.Point(106, 236);
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(209, 21);
            this.tbNotes.TabIndex = 15;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(36, 241);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(65, 12);
            this.lblNotes.TabIndex = 14;
            this.lblNotes.Text = "备    注：";
            // 
            // TradeInstanceModifyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(351, 372);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "TradeInstanceModifyDialog";
            this.Text = "修改交易实例信息";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TextBox tbInstanceCode;
        private System.Windows.Forms.Label lblInstanceCode;
        private System.Windows.Forms.TextBox tbInstanceId;
        private System.Windows.Forms.Label lblInstanceId;
        private System.Windows.Forms.Label lblMonitorUnit;
        private System.Windows.Forms.ComboBox cbMonitorUnit;
        private System.Windows.Forms.TextBox tbNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.ComboBox cbPortfolio;
        private System.Windows.Forms.Label lblPortfolio;
        private System.Windows.Forms.ComboBox cbAssetUnit;
        private System.Windows.Forms.Label lblAssetUnit;
        private System.Windows.Forms.ComboBox cbFundCode;
        private System.Windows.Forms.Label lblFundCode;
        private System.Windows.Forms.ComboBox cbTemplate;
        private System.Windows.Forms.Label lblTemplate;
    }
}
