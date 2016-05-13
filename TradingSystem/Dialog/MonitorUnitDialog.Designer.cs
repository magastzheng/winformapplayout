namespace TradingSystem.Dialog
{
    partial class MonitorUnitDialog
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
            this.lblMonitorUnitId = new System.Windows.Forms.Label();
            this.lblMonitorUnitName = new System.Windows.Forms.Label();
            this.lblAccountType = new System.Windows.Forms.Label();
            this.lblPortfolioId = new System.Windows.Forms.Label();
            this.lblstocktemplate = new System.Windows.Forms.Label();
            this.tbMonitorUnitId = new System.Windows.Forms.TextBox();
            this.tbMonitorUnitName = new System.Windows.Forms.TextBox();
            this.cbAccountType = new System.Windows.Forms.ComboBox();
            this.cbPortfolioId = new System.Windows.Forms.ComboBox();
            this.cbStockTemplate = new System.Windows.Forms.ComboBox();
            this.lblFuturesContract = new System.Windows.Forms.Label();
            this.lbDropdown = new System.Windows.Forms.ListBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.acFuturesContracts = new Controls.AutoComplete();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(330, 62);
            this.panelTop.TabIndex = 0;
            // 
            // lblMonitorUnitId
            // 
            this.lblMonitorUnitId.AutoSize = true;
            this.lblMonitorUnitId.Location = new System.Drawing.Point(28, 83);
            this.lblMonitorUnitId.Name = "lblMonitorUnitId";
            this.lblMonitorUnitId.Size = new System.Drawing.Size(53, 12);
            this.lblMonitorUnitId.TabIndex = 1;
            this.lblMonitorUnitId.Text = "监控序号";
            // 
            // lblMonitorUnitName
            // 
            this.lblMonitorUnitName.AutoSize = true;
            this.lblMonitorUnitName.Location = new System.Drawing.Point(28, 116);
            this.lblMonitorUnitName.Name = "lblMonitorUnitName";
            this.lblMonitorUnitName.Size = new System.Drawing.Size(53, 12);
            this.lblMonitorUnitName.TabIndex = 2;
            this.lblMonitorUnitName.Text = "监控名称";
            // 
            // lblAccountType
            // 
            this.lblAccountType.AutoSize = true;
            this.lblAccountType.Location = new System.Drawing.Point(28, 149);
            this.lblAccountType.Name = "lblAccountType";
            this.lblAccountType.Size = new System.Drawing.Size(53, 12);
            this.lblAccountType.TabIndex = 3;
            this.lblAccountType.Text = "账户类型";
            // 
            // lblPortfolioId
            // 
            this.lblPortfolioId.AutoSize = true;
            this.lblPortfolioId.Location = new System.Drawing.Point(28, 182);
            this.lblPortfolioId.Name = "lblPortfolioId";
            this.lblPortfolioId.Size = new System.Drawing.Size(53, 12);
            this.lblPortfolioId.TabIndex = 4;
            this.lblPortfolioId.Text = "组合序号";
            // 
            // lblstocktemplate
            // 
            this.lblstocktemplate.AutoSize = true;
            this.lblstocktemplate.Location = new System.Drawing.Point(28, 215);
            this.lblstocktemplate.Name = "lblstocktemplate";
            this.lblstocktemplate.Size = new System.Drawing.Size(53, 12);
            this.lblstocktemplate.TabIndex = 5;
            this.lblstocktemplate.Text = "现货模板";
            // 
            // tbMonitorUnitId
            // 
            this.tbMonitorUnitId.Enabled = false;
            this.tbMonitorUnitId.Location = new System.Drawing.Point(87, 74);
            this.tbMonitorUnitId.Name = "tbMonitorUnitId";
            this.tbMonitorUnitId.Size = new System.Drawing.Size(212, 21);
            this.tbMonitorUnitId.TabIndex = 7;
            this.tbMonitorUnitId.Text = "0";
            // 
            // tbMonitorUnitName
            // 
            this.tbMonitorUnitName.Location = new System.Drawing.Point(87, 107);
            this.tbMonitorUnitName.Name = "tbMonitorUnitName";
            this.tbMonitorUnitName.Size = new System.Drawing.Size(212, 21);
            this.tbMonitorUnitName.TabIndex = 8;
            // 
            // cbAccountType
            // 
            this.cbAccountType.FormattingEnabled = true;
            this.cbAccountType.Location = new System.Drawing.Point(87, 146);
            this.cbAccountType.Name = "cbAccountType";
            this.cbAccountType.Size = new System.Drawing.Size(212, 20);
            this.cbAccountType.TabIndex = 9;
            // 
            // cbPortfolioId
            // 
            this.cbPortfolioId.FormattingEnabled = true;
            this.cbPortfolioId.Location = new System.Drawing.Point(87, 179);
            this.cbPortfolioId.Name = "cbPortfolioId";
            this.cbPortfolioId.Size = new System.Drawing.Size(212, 20);
            this.cbPortfolioId.TabIndex = 10;
            // 
            // cbStockTemplate
            // 
            this.cbStockTemplate.FormattingEnabled = true;
            this.cbStockTemplate.Location = new System.Drawing.Point(87, 212);
            this.cbStockTemplate.Name = "cbStockTemplate";
            this.cbStockTemplate.Size = new System.Drawing.Size(212, 20);
            this.cbStockTemplate.TabIndex = 11;
            // 
            // lblFuturesContract
            // 
            this.lblFuturesContract.AutoSize = true;
            this.lblFuturesContract.Location = new System.Drawing.Point(28, 248);
            this.lblFuturesContract.Name = "lblFuturesContract";
            this.lblFuturesContract.Size = new System.Drawing.Size(53, 12);
            this.lblFuturesContract.TabIndex = 6;
            this.lblFuturesContract.Text = "空头合约";
            // 
            // lbDropdown
            // 
            this.lbDropdown.FormattingEnabled = true;
            this.lbDropdown.ItemHeight = 12;
            this.lbDropdown.Location = new System.Drawing.Point(87, 268);
            this.lbDropdown.Name = "lbDropdown";
            this.lbDropdown.Size = new System.Drawing.Size(170, 88);
            this.lbDropdown.TabIndex = 13;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(110, 356);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 14;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.Button_Confirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(224, 356);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // acFuturesContracts
            // 
            this.acFuturesContracts.Location = new System.Drawing.Point(81, 238);
            this.acFuturesContracts.Name = "acFuturesContracts";
            this.acFuturesContracts.Size = new System.Drawing.Size(188, 33);
            this.acFuturesContracts.TabIndex = 12;
            // 
            // MonitorUnitDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(330, 391);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lbDropdown);
            this.Controls.Add(this.acFuturesContracts);
            this.Controls.Add(this.cbStockTemplate);
            this.Controls.Add(this.cbPortfolioId);
            this.Controls.Add(this.cbAccountType);
            this.Controls.Add(this.tbMonitorUnitName);
            this.Controls.Add(this.tbMonitorUnitId);
            this.Controls.Add(this.lblFuturesContract);
            this.Controls.Add(this.lblstocktemplate);
            this.Controls.Add(this.lblPortfolioId);
            this.Controls.Add(this.lblAccountType);
            this.Controls.Add(this.lblMonitorUnitName);
            this.Controls.Add(this.lblMonitorUnitId);
            this.Controls.Add(this.panelTop);
            this.Name = "MonitorUnitDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblMonitorUnitId;
        private System.Windows.Forms.Label lblMonitorUnitName;
        private System.Windows.Forms.Label lblAccountType;
        private System.Windows.Forms.Label lblPortfolioId;
        private System.Windows.Forms.Label lblstocktemplate;
        private System.Windows.Forms.TextBox tbMonitorUnitId;
        private System.Windows.Forms.TextBox tbMonitorUnitName;
        private System.Windows.Forms.ComboBox cbAccountType;
        private System.Windows.Forms.ComboBox cbPortfolioId;
        private System.Windows.Forms.ComboBox cbStockTemplate;
        private System.Windows.Forms.Label lblFuturesContract;
        private Controls.AutoComplete acFuturesContracts;
        private System.Windows.Forms.ListBox lbDropdown;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}
