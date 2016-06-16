namespace TradingSystem.Dialog
{
    partial class CancelRedoDialog
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelPrice = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblCaption = new System.Windows.Forms.Label();
            this.gbSpotPrice = new System.Windows.Forms.GroupBox();
            this.cbSpotSellPrice = new System.Windows.Forms.ComboBox();
            this.lblSpotSellPrice = new System.Windows.Forms.Label();
            this.cbSpotBuyPrice = new System.Windows.Forms.ComboBox();
            this.lblSpotBuyPrice = new System.Windows.Forms.Label();
            this.gbFuturesPrice = new System.Windows.Forms.GroupBox();
            this.cbFuturesSellPrice = new System.Windows.Forms.ComboBox();
            this.lblFutureSellPrice = new System.Windows.Forms.Label();
            this.cbFuturesBuyPrice = new System.Windows.Forms.ComboBox();
            this.lblFuturesBuyPrice = new System.Windows.Forms.Label();
            this.gbpricetype = new System.Windows.Forms.GroupBox();
            this.cbSZExchangePrice = new System.Windows.Forms.ComboBox();
            this.lblSZPrice = new System.Windows.Forms.Label();
            this.cbSHExchangePrice = new System.Windows.Forms.ComboBox();
            this.lblSHPriceType = new System.Windows.Forms.Label();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnUnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.secuGridView = new Controls.GridView.TSDataGridView();
            this.panelTop.SuspendLayout();
            this.panelPrice.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.gbSpotPrice.SuspendLayout();
            this.gbFuturesPrice.SuspendLayout();
            this.gbpricetype.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secuGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTop.Controls.Add(this.lblCaption);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1125, 41);
            this.panelTop.TabIndex = 0;
            // 
            // panelPrice
            // 
            this.panelPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPrice.Controls.Add(this.btnSetting);
            this.panelPrice.Controls.Add(this.gbpricetype);
            this.panelPrice.Controls.Add(this.gbFuturesPrice);
            this.panelPrice.Controls.Add(this.gbSpotPrice);
            this.panelPrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPrice.Location = new System.Drawing.Point(0, 41);
            this.panelPrice.Name = "panelPrice";
            this.panelPrice.Size = new System.Drawing.Size(1125, 59);
            this.panelPrice.TabIndex = 1;
            // 
            // panelBottom
            // 
            this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnConfirm);
            this.panelBottom.Controls.Add(this.btnUnSelectAll);
            this.panelBottom.Controls.Add(this.btnSelectAll);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 533);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1125, 37);
            this.panelBottom.TabIndex = 2;
            // 
            // panelMain
            // 
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMain.Controls.Add(this.secuGridView);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 100);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1125, 433);
            this.panelMain.TabIndex = 3;
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Location = new System.Drawing.Point(35, 13);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(53, 12);
            this.lblCaption.TabIndex = 1;
            this.lblCaption.Text = "委托撤补";
            // 
            // gbSpotPrice
            // 
            this.gbSpotPrice.Controls.Add(this.cbSpotSellPrice);
            this.gbSpotPrice.Controls.Add(this.lblSpotSellPrice);
            this.gbSpotPrice.Controls.Add(this.cbSpotBuyPrice);
            this.gbSpotPrice.Controls.Add(this.lblSpotBuyPrice);
            this.gbSpotPrice.Location = new System.Drawing.Point(11, 4);
            this.gbSpotPrice.Name = "gbSpotPrice";
            this.gbSpotPrice.Size = new System.Drawing.Size(306, 44);
            this.gbSpotPrice.TabIndex = 1;
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
            // gbFuturesPrice
            // 
            this.gbFuturesPrice.Controls.Add(this.cbFuturesSellPrice);
            this.gbFuturesPrice.Controls.Add(this.lblFutureSellPrice);
            this.gbFuturesPrice.Controls.Add(this.cbFuturesBuyPrice);
            this.gbFuturesPrice.Controls.Add(this.lblFuturesBuyPrice);
            this.gbFuturesPrice.Location = new System.Drawing.Point(323, 4);
            this.gbFuturesPrice.Name = "gbFuturesPrice";
            this.gbFuturesPrice.Size = new System.Drawing.Size(306, 44);
            this.gbFuturesPrice.TabIndex = 2;
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
            // gbpricetype
            // 
            this.gbpricetype.Controls.Add(this.cbSZExchangePrice);
            this.gbpricetype.Controls.Add(this.lblSZPrice);
            this.gbpricetype.Controls.Add(this.cbSHExchangePrice);
            this.gbpricetype.Controls.Add(this.lblSHPriceType);
            this.gbpricetype.Location = new System.Drawing.Point(635, 4);
            this.gbpricetype.Name = "gbpricetype";
            this.gbpricetype.Size = new System.Drawing.Size(347, 44);
            this.gbpricetype.TabIndex = 3;
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
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetting.Location = new System.Drawing.Point(1019, 18);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(75, 23);
            this.btnSetting.TabIndex = 4;
            this.btnSetting.Text = "设置";
            this.btnSetting.UseVisualStyleBackColor = true;
            // 
            // btnUnSelectAll
            // 
            this.btnUnSelectAll.Location = new System.Drawing.Point(93, 5);
            this.btnUnSelectAll.Name = "btnUnSelectAll";
            this.btnUnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnUnSelectAll.TabIndex = 3;
            this.btnUnSelectAll.Text = "反选";
            this.btnUnSelectAll.UseVisualStyleBackColor = true;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(11, 5);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(1019, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(938, 3);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            // 
            // secuGridView
            // 
            this.secuGridView.AllowUserToAddRows = false;
            this.secuGridView.AllowUserToDeleteRows = false;
            this.secuGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.secuGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secuGridView.Location = new System.Drawing.Point(0, 0);
            this.secuGridView.Name = "secuGridView";
            this.secuGridView.RowTemplate.Height = 23;
            this.secuGridView.Size = new System.Drawing.Size(1123, 431);
            this.secuGridView.TabIndex = 0;
            // 
            // CancelRedoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelPrice);
            this.Controls.Add(this.panelTop);
            this.Name = "CancelRedoForm";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelPrice.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.gbSpotPrice.ResumeLayout(false);
            this.gbSpotPrice.PerformLayout();
            this.gbFuturesPrice.ResumeLayout(false);
            this.gbFuturesPrice.PerformLayout();
            this.gbpricetype.ResumeLayout(false);
            this.gbpricetype.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secuGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelPrice;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.GroupBox gbSpotPrice;
        private System.Windows.Forms.ComboBox cbSpotSellPrice;
        private System.Windows.Forms.Label lblSpotSellPrice;
        private System.Windows.Forms.ComboBox cbSpotBuyPrice;
        private System.Windows.Forms.Label lblSpotBuyPrice;
        private System.Windows.Forms.GroupBox gbFuturesPrice;
        private System.Windows.Forms.ComboBox cbFuturesSellPrice;
        private System.Windows.Forms.Label lblFutureSellPrice;
        private System.Windows.Forms.ComboBox cbFuturesBuyPrice;
        private System.Windows.Forms.Label lblFuturesBuyPrice;
        private System.Windows.Forms.GroupBox gbpricetype;
        private System.Windows.Forms.ComboBox cbSZExchangePrice;
        private System.Windows.Forms.Label lblSZPrice;
        private System.Windows.Forms.ComboBox cbSHExchangePrice;
        private System.Windows.Forms.Label lblSHPriceType;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Button btnUnSelectAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private Controls.GridView.TSDataGridView secuGridView;
    }
}
