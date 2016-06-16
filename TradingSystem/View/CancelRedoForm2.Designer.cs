using Config;
using Controls;
namespace TradingSystem.View
{
    partial class CancelRedoForm2
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
            this.tlPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelCaption = new System.Windows.Forms.Panel();
            this.lblCaption = new System.Windows.Forms.Label();
            this.panelSetting = new System.Windows.Forms.Panel();
            this.btnSetting = new System.Windows.Forms.Button();
            this.gbpricetype = new System.Windows.Forms.GroupBox();
            this.cbSZExchangePrice = new System.Windows.Forms.ComboBox();
            this.lblSZPrice = new System.Windows.Forms.Label();
            this.cbSHExchangePrice = new System.Windows.Forms.ComboBox();
            this.lblSHPriceType = new System.Windows.Forms.Label();
            this.gbFuturesPrice = new System.Windows.Forms.GroupBox();
            this.cbFuturesSellPrice = new System.Windows.Forms.ComboBox();
            this.lblFutureSellPrice = new System.Windows.Forms.Label();
            this.cbFuturesBuyPrice = new System.Windows.Forms.ComboBox();
            this.lblFuturesBuyPrice = new System.Windows.Forms.Label();
            this.gbSpotPrice = new System.Windows.Forms.GroupBox();
            this.cbSpotSellPrice = new System.Windows.Forms.ComboBox();
            this.lblSpotSellPrice = new System.Windows.Forms.Label();
            this.cbSpotBuyPrice = new System.Windows.Forms.ComboBox();
            this.lblSpotBuyPrice = new System.Windows.Forms.Label();
            this.panelBottomButton = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnUnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.tlPanelMain.SuspendLayout();
            this.panelCaption.SuspendLayout();
            this.panelSetting.SuspendLayout();
            this.gbpricetype.SuspendLayout();
            this.gbFuturesPrice.SuspendLayout();
            this.gbSpotPrice.SuspendLayout();
            this.panelBottomButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlPanelMain
            // 
            this.tlPanelMain.ColumnCount = 1;
            this.tlPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelMain.Controls.Add(this.panelCaption, 0, 0);
            this.tlPanelMain.Controls.Add(this.panelSetting, 0, 1);
            this.tlPanelMain.Controls.Add(this.panelBottomButton, 0, 3);
            this.tlPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tlPanelMain.Name = "tlPanelMain";
            this.tlPanelMain.RowCount = 4;
            this.tlPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlPanelMain.Size = new System.Drawing.Size(1068, 575);
            this.tlPanelMain.TabIndex = 0;
            // 
            // panelCaption
            // 
            this.panelCaption.Controls.Add(this.lblCaption);
            this.panelCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCaption.Location = new System.Drawing.Point(3, 3);
            this.panelCaption.Name = "panelCaption";
            this.panelCaption.Size = new System.Drawing.Size(1062, 34);
            this.panelCaption.TabIndex = 0;
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Location = new System.Drawing.Point(4, 10);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(53, 12);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "委托撤补";
            // 
            // panelSetting
            // 
            this.panelSetting.Controls.Add(this.btnSetting);
            this.panelSetting.Controls.Add(this.gbpricetype);
            this.panelSetting.Controls.Add(this.gbFuturesPrice);
            this.panelSetting.Controls.Add(this.gbSpotPrice);
            this.panelSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSetting.Location = new System.Drawing.Point(3, 43);
            this.panelSetting.Name = "panelSetting";
            this.panelSetting.Size = new System.Drawing.Size(1062, 44);
            this.panelSetting.TabIndex = 1;
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetting.Location = new System.Drawing.Point(980, 12);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(75, 23);
            this.btnSetting.TabIndex = 3;
            this.btnSetting.Text = "设置";
            this.btnSetting.UseVisualStyleBackColor = true;
            // 
            // gbpricetype
            // 
            this.gbpricetype.Controls.Add(this.cbSZExchangePrice);
            this.gbpricetype.Controls.Add(this.lblSZPrice);
            this.gbpricetype.Controls.Add(this.cbSHExchangePrice);
            this.gbpricetype.Controls.Add(this.lblSHPriceType);
            this.gbpricetype.Location = new System.Drawing.Point(624, 3);
            this.gbpricetype.Name = "gbpricetype";
            this.gbpricetype.Size = new System.Drawing.Size(347, 44);
            this.gbpricetype.TabIndex = 2;
            this.gbpricetype.TabStop = false;
            this.gbpricetype.Text = "市价方式";
            // 
            // cbSZExchangePrice
            // 
            this.cbSZExchangePrice.FormattingEnabled = true;
            this.cbSZExchangePrice.Location = new System.Drawing.Point(225, 17);
            this.cbSZExchangePrice.Name = "cbSZExchangePrice";
            this.cbSZExchangePrice.Size = new System.Drawing.Size(115, 20);
            this.cbSZExchangePrice.TabIndex = 7;
            // 
            // lblSZPrice
            // 
            this.lblSZPrice.AutoSize = true;
            this.lblSZPrice.Location = new System.Drawing.Point(178, 20);
            this.lblSZPrice.Name = "lblSZPrice";
            this.lblSZPrice.Size = new System.Drawing.Size(41, 12);
            this.lblSZPrice.TabIndex = 6;
            this.lblSZPrice.Text = "深交所";
            // 
            // cbSHExchangePrice
            // 
            this.cbSHExchangePrice.FormattingEnabled = true;
            this.cbSHExchangePrice.Location = new System.Drawing.Point(54, 17);
            this.cbSHExchangePrice.Name = "cbSHExchangePrice";
            this.cbSHExchangePrice.Size = new System.Drawing.Size(115, 20);
            this.cbSHExchangePrice.TabIndex = 5;
            // 
            // lblSHPriceType
            // 
            this.lblSHPriceType.AutoSize = true;
            this.lblSHPriceType.Location = new System.Drawing.Point(6, 20);
            this.lblSHPriceType.Name = "lblSHPriceType";
            this.lblSHPriceType.Size = new System.Drawing.Size(41, 12);
            this.lblSHPriceType.TabIndex = 4;
            this.lblSHPriceType.Text = "上交所";
            // 
            // gbFuturesPrice
            // 
            this.gbFuturesPrice.Controls.Add(this.cbFuturesSellPrice);
            this.gbFuturesPrice.Controls.Add(this.lblFutureSellPrice);
            this.gbFuturesPrice.Controls.Add(this.cbFuturesBuyPrice);
            this.gbFuturesPrice.Controls.Add(this.lblFuturesBuyPrice);
            this.gbFuturesPrice.Location = new System.Drawing.Point(312, 0);
            this.gbFuturesPrice.Name = "gbFuturesPrice";
            this.gbFuturesPrice.Size = new System.Drawing.Size(306, 44);
            this.gbFuturesPrice.TabIndex = 1;
            this.gbFuturesPrice.TabStop = false;
            this.gbFuturesPrice.Text = "期货";
            // 
            // cbFuturesSellPrice
            // 
            this.cbFuturesSellPrice.FormattingEnabled = true;
            this.cbFuturesSellPrice.Location = new System.Drawing.Point(203, 17);
            this.cbFuturesSellPrice.Name = "cbFuturesSellPrice";
            this.cbFuturesSellPrice.Size = new System.Drawing.Size(95, 20);
            this.cbFuturesSellPrice.TabIndex = 7;
            // 
            // lblFutureSellPrice
            // 
            this.lblFutureSellPrice.AutoSize = true;
            this.lblFutureSellPrice.Location = new System.Drawing.Point(156, 21);
            this.lblFutureSellPrice.Name = "lblFutureSellPrice";
            this.lblFutureSellPrice.Size = new System.Drawing.Size(41, 12);
            this.lblFutureSellPrice.TabIndex = 6;
            this.lblFutureSellPrice.Text = "委卖价";
            // 
            // cbFuturesBuyPrice
            // 
            this.cbFuturesBuyPrice.FormattingEnabled = true;
            this.cbFuturesBuyPrice.Location = new System.Drawing.Point(54, 17);
            this.cbFuturesBuyPrice.Name = "cbFuturesBuyPrice";
            this.cbFuturesBuyPrice.Size = new System.Drawing.Size(95, 20);
            this.cbFuturesBuyPrice.TabIndex = 5;
            // 
            // lblFuturesBuyPrice
            // 
            this.lblFuturesBuyPrice.AutoSize = true;
            this.lblFuturesBuyPrice.Location = new System.Drawing.Point(6, 21);
            this.lblFuturesBuyPrice.Name = "lblFuturesBuyPrice";
            this.lblFuturesBuyPrice.Size = new System.Drawing.Size(41, 12);
            this.lblFuturesBuyPrice.TabIndex = 4;
            this.lblFuturesBuyPrice.Text = "委买价";
            // 
            // gbSpotPrice
            // 
            this.gbSpotPrice.Controls.Add(this.cbSpotSellPrice);
            this.gbSpotPrice.Controls.Add(this.lblSpotSellPrice);
            this.gbSpotPrice.Controls.Add(this.cbSpotBuyPrice);
            this.gbSpotPrice.Controls.Add(this.lblSpotBuyPrice);
            this.gbSpotPrice.Location = new System.Drawing.Point(0, 0);
            this.gbSpotPrice.Name = "gbSpotPrice";
            this.gbSpotPrice.Size = new System.Drawing.Size(306, 44);
            this.gbSpotPrice.TabIndex = 0;
            this.gbSpotPrice.TabStop = false;
            this.gbSpotPrice.Text = "现货";
            // 
            // cbSpotSellPrice
            // 
            this.cbSpotSellPrice.FormattingEnabled = true;
            this.cbSpotSellPrice.Location = new System.Drawing.Point(204, 18);
            this.cbSpotSellPrice.Name = "cbSpotSellPrice";
            this.cbSpotSellPrice.Size = new System.Drawing.Size(95, 20);
            this.cbSpotSellPrice.TabIndex = 3;
            // 
            // lblSpotSellPrice
            // 
            this.lblSpotSellPrice.AutoSize = true;
            this.lblSpotSellPrice.Location = new System.Drawing.Point(157, 21);
            this.lblSpotSellPrice.Name = "lblSpotSellPrice";
            this.lblSpotSellPrice.Size = new System.Drawing.Size(41, 12);
            this.lblSpotSellPrice.TabIndex = 2;
            this.lblSpotSellPrice.Text = "委卖价";
            // 
            // cbSpotBuyPrice
            // 
            this.cbSpotBuyPrice.DropDownWidth = 110;
            this.cbSpotBuyPrice.FormattingEnabled = true;
            this.cbSpotBuyPrice.Location = new System.Drawing.Point(54, 18);
            this.cbSpotBuyPrice.Name = "cbSpotBuyPrice";
            this.cbSpotBuyPrice.Size = new System.Drawing.Size(95, 20);
            this.cbSpotBuyPrice.TabIndex = 1;
            // 
            // lblSpotBuyPrice
            // 
            this.lblSpotBuyPrice.AutoSize = true;
            this.lblSpotBuyPrice.Location = new System.Drawing.Point(6, 21);
            this.lblSpotBuyPrice.Name = "lblSpotBuyPrice";
            this.lblSpotBuyPrice.Size = new System.Drawing.Size(41, 12);
            this.lblSpotBuyPrice.TabIndex = 0;
            this.lblSpotBuyPrice.Text = "委买价";
            // 
            // panelBottomButton
            // 
            this.panelBottomButton.Controls.Add(this.btnCancel);
            this.panelBottomButton.Controls.Add(this.btnConfirm);
            this.panelBottomButton.Controls.Add(this.btnUnSelectAll);
            this.panelBottomButton.Controls.Add(this.btnSelectAll);
            this.panelBottomButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottomButton.Location = new System.Drawing.Point(3, 538);
            this.panelBottomButton.Name = "panelBottomButton";
            this.panelBottomButton.Size = new System.Drawing.Size(1062, 34);
            this.panelBottomButton.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(978, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(897, 8);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.ButtonConfirm_Click);
            // 
            // btnUnSelectAll
            // 
            this.btnUnSelectAll.Location = new System.Drawing.Point(88, 6);
            this.btnUnSelectAll.Name = "btnUnSelectAll";
            this.btnUnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnUnSelectAll.TabIndex = 1;
            this.btnUnSelectAll.Text = "反选";
            this.btnUnSelectAll.UseVisualStyleBackColor = true;
            this.btnUnSelectAll.Click += new System.EventHandler(this.ButtonUnSelectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(6, 6);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.ButtonSelectAll_Click);
            // 
            // CancelRedoForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 575);
            this.Controls.Add(this.tlPanelMain);
            this.Name = "CancelRedoForm2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "委托撤补";
            this.Load += new System.EventHandler(this.CancelRedoForm_Load);
            this.tlPanelMain.ResumeLayout(false);
            this.panelCaption.ResumeLayout(false);
            this.panelCaption.PerformLayout();
            this.panelSetting.ResumeLayout(false);
            this.gbpricetype.ResumeLayout(false);
            this.gbpricetype.PerformLayout();
            this.gbFuturesPrice.ResumeLayout(false);
            this.gbFuturesPrice.PerformLayout();
            this.gbSpotPrice.ResumeLayout(false);
            this.gbSpotPrice.PerformLayout();
            this.panelBottomButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlPanelMain;
        private System.Windows.Forms.Panel panelCaption;
        private System.Windows.Forms.Panel panelSetting;
        private System.Windows.Forms.Panel panelBottomButton;
        //private System.Windows.Forms.DataGridView dataGridViewECA;

        //private HSGridView dataGridViewECA;

        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.GroupBox gbpricetype;
        private System.Windows.Forms.ComboBox cbSZExchangePrice;
        private System.Windows.Forms.Label lblSZPrice;
        private System.Windows.Forms.ComboBox cbSHExchangePrice;
        private System.Windows.Forms.Label lblSHPriceType;
        private System.Windows.Forms.GroupBox gbFuturesPrice;
        private System.Windows.Forms.ComboBox cbFuturesSellPrice;
        private System.Windows.Forms.Label lblFutureSellPrice;
        private System.Windows.Forms.ComboBox cbFuturesBuyPrice;
        private System.Windows.Forms.Label lblFuturesBuyPrice;
        private System.Windows.Forms.GroupBox gbSpotPrice;
        private System.Windows.Forms.ComboBox cbSpotSellPrice;
        private System.Windows.Forms.Label lblSpotSellPrice;
        private System.Windows.Forms.ComboBox cbSpotBuyPrice;
        private System.Windows.Forms.Label lblSpotBuyPrice;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnUnSelectAll;
        private System.Windows.Forms.Button btnSelectAll;
    }
}