namespace TradingSystem.Dialog
{
    partial class GeneralSettingDailog
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
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.lblCaption = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbBasicSetting = new System.Windows.Forms.GroupBox();
            this.lblOddShareMode = new System.Windows.Forms.Label();
            this.cbOddShareMode = new System.Windows.Forms.ComboBox();
            this.cbSseEntrustPriceType = new System.Windows.Forms.ComboBox();
            this.lblSseEntrustPriceType = new System.Windows.Forms.Label();
            this.cbSzseEntrustPriceType = new System.Windows.Forms.ComboBox();
            this.lblSzseEntrustPriceType = new System.Windows.Forms.Label();
            this.lblSpotLimitEntrustRatio = new System.Windows.Forms.Label();
            this.lblFutuLimitEntrustRatio = new System.Windows.Forms.Label();
            this.gbPriceSetting = new System.Windows.Forms.GroupBox();
            this.cbFutuSellPrice = new System.Windows.Forms.ComboBox();
            this.lblFutuSellPrice = new System.Windows.Forms.Label();
            this.cbFutuBuyPrice = new System.Windows.Forms.ComboBox();
            this.lblFutuBuyPrice = new System.Windows.Forms.Label();
            this.cbSpotSellPrice = new System.Windows.Forms.ComboBox();
            this.lblSpotSellPrice = new System.Windows.Forms.Label();
            this.cbSpotBuyPrice = new System.Windows.Forms.ComboBox();
            this.lblSpotBuyPrice = new System.Windows.Forms.Label();
            this.tbSpotLimitEntrustRatio = new System.Windows.Forms.TextBox();
            this.tbFutuLimitEntrustRatio = new System.Windows.Forms.TextBox();
            this.topPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.gbBasicSetting.SuspendLayout();
            this.gbPriceSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topPanel.Controls.Add(this.lblCaption);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(538, 46);
            this.topPanel.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottomPanel.Controls.Add(this.btnCancel);
            this.bottomPanel.Controls.Add(this.btnConfirm);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 342);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(538, 40);
            this.bottomPanel.TabIndex = 1;
            // 
            // mainPanel
            // 
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Controls.Add(this.gbPriceSetting);
            this.mainPanel.Controls.Add(this.gbBasicSetting);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 46);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(538, 296);
            this.mainPanel.TabIndex = 2;
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Location = new System.Drawing.Point(147, 13);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(77, 12);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "个人参数设置";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(325, 6);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(440, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // gbBasicSetting
            // 
            this.gbBasicSetting.Controls.Add(this.tbFutuLimitEntrustRatio);
            this.gbBasicSetting.Controls.Add(this.tbSpotLimitEntrustRatio);
            this.gbBasicSetting.Controls.Add(this.lblFutuLimitEntrustRatio);
            this.gbBasicSetting.Controls.Add(this.lblSpotLimitEntrustRatio);
            this.gbBasicSetting.Controls.Add(this.cbSzseEntrustPriceType);
            this.gbBasicSetting.Controls.Add(this.lblSzseEntrustPriceType);
            this.gbBasicSetting.Controls.Add(this.cbSseEntrustPriceType);
            this.gbBasicSetting.Controls.Add(this.lblSseEntrustPriceType);
            this.gbBasicSetting.Controls.Add(this.cbOddShareMode);
            this.gbBasicSetting.Controls.Add(this.lblOddShareMode);
            this.gbBasicSetting.Location = new System.Drawing.Point(13, 34);
            this.gbBasicSetting.Name = "gbBasicSetting";
            this.gbBasicSetting.Size = new System.Drawing.Size(265, 245);
            this.gbBasicSetting.TabIndex = 0;
            this.gbBasicSetting.TabStop = false;
            this.gbBasicSetting.Text = "基本设置";
            // 
            // lblOddShareMode
            // 
            this.lblOddShareMode.AutoSize = true;
            this.lblOddShareMode.Location = new System.Drawing.Point(7, 30);
            this.lblOddShareMode.Name = "lblOddShareMode";
            this.lblOddShareMode.Size = new System.Drawing.Size(137, 12);
            this.lblOddShareMode.TabIndex = 0;
            this.lblOddShareMode.Text = "委托数量零股处理模式：";
            // 
            // cbOddShareMode
            // 
            this.cbOddShareMode.FormattingEnabled = true;
            this.cbOddShareMode.Location = new System.Drawing.Point(140, 25);
            this.cbOddShareMode.Name = "cbOddShareMode";
            this.cbOddShareMode.Size = new System.Drawing.Size(121, 20);
            this.cbOddShareMode.TabIndex = 1;
            // 
            // cbSseEntrustPriceType
            // 
            this.cbSseEntrustPriceType.FormattingEnabled = true;
            this.cbSseEntrustPriceType.Location = new System.Drawing.Point(140, 61);
            this.cbSseEntrustPriceType.Name = "cbSseEntrustPriceType";
            this.cbSseEntrustPriceType.Size = new System.Drawing.Size(121, 20);
            this.cbSseEntrustPriceType.TabIndex = 3;
            // 
            // lblSseEntrustPriceType
            // 
            this.lblSseEntrustPriceType.AutoSize = true;
            this.lblSseEntrustPriceType.Location = new System.Drawing.Point(19, 65);
            this.lblSseEntrustPriceType.Name = "lblSseEntrustPriceType";
            this.lblSseEntrustPriceType.Size = new System.Drawing.Size(125, 12);
            this.lblSseEntrustPriceType.TabIndex = 2;
            this.lblSseEntrustPriceType.Text = "上交所市价委托方式：";
            // 
            // cbSzseEntrustPriceType
            // 
            this.cbSzseEntrustPriceType.FormattingEnabled = true;
            this.cbSzseEntrustPriceType.Location = new System.Drawing.Point(140, 98);
            this.cbSzseEntrustPriceType.Name = "cbSzseEntrustPriceType";
            this.cbSzseEntrustPriceType.Size = new System.Drawing.Size(121, 20);
            this.cbSzseEntrustPriceType.TabIndex = 5;
            // 
            // lblSzseEntrustPriceType
            // 
            this.lblSzseEntrustPriceType.AutoSize = true;
            this.lblSzseEntrustPriceType.Location = new System.Drawing.Point(19, 102);
            this.lblSzseEntrustPriceType.Name = "lblSzseEntrustPriceType";
            this.lblSzseEntrustPriceType.Size = new System.Drawing.Size(125, 12);
            this.lblSzseEntrustPriceType.TabIndex = 4;
            this.lblSzseEntrustPriceType.Text = "深交所市价委托方式：";
            // 
            // lblSpotLimitEntrustRatio
            // 
            this.lblSpotLimitEntrustRatio.AutoSize = true;
            this.lblSpotLimitEntrustRatio.Location = new System.Drawing.Point(31, 140);
            this.lblSpotLimitEntrustRatio.Name = "lblSpotLimitEntrustRatio";
            this.lblSpotLimitEntrustRatio.Size = new System.Drawing.Size(113, 12);
            this.lblSpotLimitEntrustRatio.TabIndex = 6;
            this.lblSpotLimitEntrustRatio.Text = "现货最小委托比例：";
            // 
            // lblFutuLimitEntrustRatio
            // 
            this.lblFutuLimitEntrustRatio.AutoSize = true;
            this.lblFutuLimitEntrustRatio.Location = new System.Drawing.Point(31, 178);
            this.lblFutuLimitEntrustRatio.Name = "lblFutuLimitEntrustRatio";
            this.lblFutuLimitEntrustRatio.Size = new System.Drawing.Size(113, 12);
            this.lblFutuLimitEntrustRatio.TabIndex = 8;
            this.lblFutuLimitEntrustRatio.Text = "期货最小委托比例：";
            // 
            // gbPriceSetting
            // 
            this.gbPriceSetting.Controls.Add(this.cbFutuSellPrice);
            this.gbPriceSetting.Controls.Add(this.lblFutuSellPrice);
            this.gbPriceSetting.Controls.Add(this.cbFutuBuyPrice);
            this.gbPriceSetting.Controls.Add(this.lblFutuBuyPrice);
            this.gbPriceSetting.Controls.Add(this.cbSpotSellPrice);
            this.gbPriceSetting.Controls.Add(this.lblSpotSellPrice);
            this.gbPriceSetting.Controls.Add(this.cbSpotBuyPrice);
            this.gbPriceSetting.Controls.Add(this.lblSpotBuyPrice);
            this.gbPriceSetting.Location = new System.Drawing.Point(302, 34);
            this.gbPriceSetting.Name = "gbPriceSetting";
            this.gbPriceSetting.Size = new System.Drawing.Size(213, 245);
            this.gbPriceSetting.TabIndex = 1;
            this.gbPriceSetting.TabStop = false;
            this.gbPriceSetting.Text = "价格设置";
            // 
            // cbFutuSellPrice
            // 
            this.cbFutuSellPrice.FormattingEnabled = true;
            this.cbFutuSellPrice.Location = new System.Drawing.Point(83, 134);
            this.cbFutuSellPrice.Name = "cbFutuSellPrice";
            this.cbFutuSellPrice.Size = new System.Drawing.Size(121, 20);
            this.cbFutuSellPrice.TabIndex = 7;
            // 
            // lblFutuSellPrice
            // 
            this.lblFutuSellPrice.AutoSize = true;
            this.lblFutuSellPrice.Location = new System.Drawing.Point(7, 137);
            this.lblFutuSellPrice.Name = "lblFutuSellPrice";
            this.lblFutuSellPrice.Size = new System.Drawing.Size(77, 12);
            this.lblFutuSellPrice.TabIndex = 6;
            this.lblFutuSellPrice.Text = "期货委卖价：";
            // 
            // cbFutuBuyPrice
            // 
            this.cbFutuBuyPrice.FormattingEnabled = true;
            this.cbFutuBuyPrice.Location = new System.Drawing.Point(83, 98);
            this.cbFutuBuyPrice.Name = "cbFutuBuyPrice";
            this.cbFutuBuyPrice.Size = new System.Drawing.Size(121, 20);
            this.cbFutuBuyPrice.TabIndex = 5;
            // 
            // lblFutuBuyPrice
            // 
            this.lblFutuBuyPrice.AutoSize = true;
            this.lblFutuBuyPrice.Location = new System.Drawing.Point(7, 102);
            this.lblFutuBuyPrice.Name = "lblFutuBuyPrice";
            this.lblFutuBuyPrice.Size = new System.Drawing.Size(77, 12);
            this.lblFutuBuyPrice.TabIndex = 4;
            this.lblFutuBuyPrice.Text = "期货委买价：";
            // 
            // cbSpotSellPrice
            // 
            this.cbSpotSellPrice.FormattingEnabled = true;
            this.cbSpotSellPrice.Location = new System.Drawing.Point(83, 63);
            this.cbSpotSellPrice.Name = "cbSpotSellPrice";
            this.cbSpotSellPrice.Size = new System.Drawing.Size(121, 20);
            this.cbSpotSellPrice.TabIndex = 3;
            // 
            // lblSpotSellPrice
            // 
            this.lblSpotSellPrice.AutoSize = true;
            this.lblSpotSellPrice.Location = new System.Drawing.Point(7, 67);
            this.lblSpotSellPrice.Name = "lblSpotSellPrice";
            this.lblSpotSellPrice.Size = new System.Drawing.Size(77, 12);
            this.lblSpotSellPrice.TabIndex = 2;
            this.lblSpotSellPrice.Text = "现货委卖价：";
            // 
            // cbSpotBuyPrice
            // 
            this.cbSpotBuyPrice.FormattingEnabled = true;
            this.cbSpotBuyPrice.Location = new System.Drawing.Point(83, 28);
            this.cbSpotBuyPrice.Name = "cbSpotBuyPrice";
            this.cbSpotBuyPrice.Size = new System.Drawing.Size(121, 20);
            this.cbSpotBuyPrice.TabIndex = 1;
            // 
            // lblSpotBuyPrice
            // 
            this.lblSpotBuyPrice.AutoSize = true;
            this.lblSpotBuyPrice.Location = new System.Drawing.Point(7, 31);
            this.lblSpotBuyPrice.Name = "lblSpotBuyPrice";
            this.lblSpotBuyPrice.Size = new System.Drawing.Size(77, 12);
            this.lblSpotBuyPrice.TabIndex = 0;
            this.lblSpotBuyPrice.Text = "现货委买价：";
            // 
            // tbSpotLimitEntrustRatio
            // 
            this.tbSpotLimitEntrustRatio.Location = new System.Drawing.Point(140, 135);
            this.tbSpotLimitEntrustRatio.Name = "tbSpotLimitEntrustRatio";
            this.tbSpotLimitEntrustRatio.Size = new System.Drawing.Size(119, 21);
            this.tbSpotLimitEntrustRatio.TabIndex = 9;
            // 
            // tbFutuLimitEntrustRatio
            // 
            this.tbFutuLimitEntrustRatio.Location = new System.Drawing.Point(140, 174);
            this.tbFutuLimitEntrustRatio.Name = "tbFutuLimitEntrustRatio";
            this.tbFutuLimitEntrustRatio.Size = new System.Drawing.Size(119, 21);
            this.tbFutuLimitEntrustRatio.TabIndex = 10;
            // 
            // GeneralSettingDailog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(538, 382);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Name = "GeneralSettingDailog";
            this.Text = "用户参数设置";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.gbBasicSetting.ResumeLayout(false);
            this.gbBasicSetting.PerformLayout();
            this.gbPriceSetting.ResumeLayout(false);
            this.gbPriceSetting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.GroupBox gbBasicSetting;
        private System.Windows.Forms.GroupBox gbPriceSetting;
        private System.Windows.Forms.ComboBox cbFutuSellPrice;
        private System.Windows.Forms.Label lblFutuSellPrice;
        private System.Windows.Forms.ComboBox cbFutuBuyPrice;
        private System.Windows.Forms.Label lblFutuBuyPrice;
        private System.Windows.Forms.ComboBox cbSpotSellPrice;
        private System.Windows.Forms.Label lblSpotSellPrice;
        private System.Windows.Forms.ComboBox cbSpotBuyPrice;
        private System.Windows.Forms.Label lblSpotBuyPrice;
        private System.Windows.Forms.Label lblFutuLimitEntrustRatio;
        private System.Windows.Forms.Label lblSpotLimitEntrustRatio;
        private System.Windows.Forms.ComboBox cbSzseEntrustPriceType;
        private System.Windows.Forms.Label lblSzseEntrustPriceType;
        private System.Windows.Forms.ComboBox cbSseEntrustPriceType;
        private System.Windows.Forms.Label lblSseEntrustPriceType;
        private System.Windows.Forms.ComboBox cbOddShareMode;
        private System.Windows.Forms.Label lblOddShareMode;
        private System.Windows.Forms.TextBox tbFutuLimitEntrustRatio;
        private System.Windows.Forms.TextBox tbSpotLimitEntrustRatio;
    }
}
