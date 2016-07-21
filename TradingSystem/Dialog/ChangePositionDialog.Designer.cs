namespace TradingSystem.Dialog
{
    partial class ChangePositionDialog
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
            this.lblCaptin = new System.Windows.Forms.Label();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.gbChangIn = new System.Windows.Forms.GroupBox();
            this.lbSecuIn = new System.Windows.Forms.ListBox();
            this.tbInAmount = new System.Windows.Forms.TextBox();
            this.tbInPrice = new System.Windows.Forms.TextBox();
            this.cbInDirection = new System.Windows.Forms.ComboBox();
            this.cbStopFlag = new System.Windows.Forms.ComboBox();
            this.acSecuIn = new Controls.AutoComplete();
            this.lblInAmount = new System.Windows.Forms.Label();
            this.lblInPrice = new System.Windows.Forms.Label();
            this.lblInDirection = new System.Windows.Forms.Label();
            this.lblStopFlag = new System.Windows.Forms.Label();
            this.lblSecuCode2 = new System.Windows.Forms.Label();
            this.gbChangeOut = new System.Windows.Forms.GroupBox();
            this.lbSecuOut = new System.Windows.Forms.ListBox();
            this.tbOutAmount = new System.Windows.Forms.TextBox();
            this.tbOutPrice = new System.Windows.Forms.TextBox();
            this.cbOutDirection = new System.Windows.Forms.ComboBox();
            this.cbLongShort = new System.Windows.Forms.ComboBox();
            this.acSecuOut = new Controls.AutoComplete();
            this.lblOutAmount = new System.Windows.Forms.Label();
            this.lblOutPrice = new System.Windows.Forms.Label();
            this.lblOutDirection = new System.Windows.Forms.Label();
            this.lblLongShort = new System.Windows.Forms.Label();
            this.lblSecuCode1 = new System.Windows.Forms.Label();
            this.topPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.gbChangIn.SuspendLayout();
            this.gbChangeOut.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topPanel.Controls.Add(this.lblCaptin);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(643, 40);
            this.topPanel.TabIndex = 0;
            // 
            // lblCaptin
            // 
            this.lblCaptin.AutoSize = true;
            this.lblCaptin.Location = new System.Drawing.Point(209, 12);
            this.lblCaptin.Name = "lblCaptin";
            this.lblCaptin.Size = new System.Drawing.Size(53, 12);
            this.lblCaptin.TabIndex = 0;
            this.lblCaptin.Text = "套利换仓";
            // 
            // bottomPanel
            // 
            this.bottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottomPanel.Controls.Add(this.btnCancel);
            this.bottomPanel.Controls.Add(this.btnConfirm);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 353);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(643, 38);
            this.bottomPanel.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(545, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(444, 6);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.gbChangIn);
            this.panelMain.Controls.Add(this.gbChangeOut);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 40);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(643, 313);
            this.panelMain.TabIndex = 2;
            // 
            // gbChangIn
            // 
            this.gbChangIn.Controls.Add(this.lbSecuIn);
            this.gbChangIn.Controls.Add(this.tbInAmount);
            this.gbChangIn.Controls.Add(this.tbInPrice);
            this.gbChangIn.Controls.Add(this.cbInDirection);
            this.gbChangIn.Controls.Add(this.cbStopFlag);
            this.gbChangIn.Controls.Add(this.acSecuIn);
            this.gbChangIn.Controls.Add(this.lblInAmount);
            this.gbChangIn.Controls.Add(this.lblInPrice);
            this.gbChangIn.Controls.Add(this.lblInDirection);
            this.gbChangIn.Controls.Add(this.lblStopFlag);
            this.gbChangIn.Controls.Add(this.lblSecuCode2);
            this.gbChangIn.Location = new System.Drawing.Point(317, 19);
            this.gbChangIn.Name = "gbChangIn";
            this.gbChangIn.Size = new System.Drawing.Size(304, 270);
            this.gbChangIn.TabIndex = 1;
            this.gbChangIn.TabStop = false;
            this.gbChangIn.Text = "调入";
            // 
            // lbSecuIn
            // 
            this.lbSecuIn.FormattingEnabled = true;
            this.lbSecuIn.ItemHeight = 12;
            this.lbSecuIn.Location = new System.Drawing.Point(103, 62);
            this.lbSecuIn.Name = "lbSecuIn";
            this.lbSecuIn.Size = new System.Drawing.Size(151, 88);
            this.lbSecuIn.TabIndex = 20;
            this.lbSecuIn.Visible = false;
            // 
            // tbInAmount
            // 
            this.tbInAmount.Location = new System.Drawing.Point(103, 219);
            this.tbInAmount.Name = "tbInAmount";
            this.tbInAmount.Size = new System.Drawing.Size(168, 21);
            this.tbInAmount.TabIndex = 19;
            this.tbInAmount.Text = "0";
            // 
            // tbInPrice
            // 
            this.tbInPrice.Location = new System.Drawing.Point(103, 174);
            this.tbInPrice.Name = "tbInPrice";
            this.tbInPrice.Size = new System.Drawing.Size(168, 21);
            this.tbInPrice.TabIndex = 18;
            this.tbInPrice.Text = "0";
            // 
            // cbInDirection
            // 
            this.cbInDirection.Enabled = false;
            this.cbInDirection.FormattingEnabled = true;
            this.cbInDirection.Location = new System.Drawing.Point(103, 130);
            this.cbInDirection.Name = "cbInDirection";
            this.cbInDirection.Size = new System.Drawing.Size(168, 20);
            this.cbInDirection.TabIndex = 17;
            // 
            // cbStopFlag
            // 
            this.cbStopFlag.Enabled = false;
            this.cbStopFlag.FormattingEnabled = true;
            this.cbStopFlag.Location = new System.Drawing.Point(103, 86);
            this.cbStopFlag.Name = "cbStopFlag";
            this.cbStopFlag.Size = new System.Drawing.Size(168, 20);
            this.cbStopFlag.TabIndex = 16;
            // 
            // acSecuIn
            // 
            this.acSecuIn.Location = new System.Drawing.Point(94, 31);
            this.acSecuIn.Name = "acSecuIn";
            this.acSecuIn.Size = new System.Drawing.Size(188, 31);
            this.acSecuIn.TabIndex = 15;
            // 
            // lblInAmount
            // 
            this.lblInAmount.AutoSize = true;
            this.lblInAmount.Location = new System.Drawing.Point(30, 225);
            this.lblInAmount.Name = "lblInAmount";
            this.lblInAmount.Size = new System.Drawing.Size(65, 12);
            this.lblInAmount.TabIndex = 14;
            this.lblInAmount.Text = "调入数量：";
            // 
            // lblInPrice
            // 
            this.lblInPrice.AutoSize = true;
            this.lblInPrice.Location = new System.Drawing.Point(30, 179);
            this.lblInPrice.Name = "lblInPrice";
            this.lblInPrice.Size = new System.Drawing.Size(65, 12);
            this.lblInPrice.TabIndex = 13;
            this.lblInPrice.Text = "调入价格：";
            // 
            // lblInDirection
            // 
            this.lblInDirection.AutoSize = true;
            this.lblInDirection.Location = new System.Drawing.Point(30, 133);
            this.lblInDirection.Name = "lblInDirection";
            this.lblInDirection.Size = new System.Drawing.Size(65, 12);
            this.lblInDirection.TabIndex = 12;
            this.lblInDirection.Text = "调入方向：";
            // 
            // lblStopFlag
            // 
            this.lblStopFlag.AutoSize = true;
            this.lblStopFlag.Location = new System.Drawing.Point(30, 87);
            this.lblStopFlag.Name = "lblStopFlag";
            this.lblStopFlag.Size = new System.Drawing.Size(65, 12);
            this.lblStopFlag.TabIndex = 11;
            this.lblStopFlag.Text = "停牌标志：";
            // 
            // lblSecuCode2
            // 
            this.lblSecuCode2.AutoSize = true;
            this.lblSecuCode2.Location = new System.Drawing.Point(30, 41);
            this.lblSecuCode2.Name = "lblSecuCode2";
            this.lblSecuCode2.Size = new System.Drawing.Size(65, 12);
            this.lblSecuCode2.TabIndex = 10;
            this.lblSecuCode2.Text = "证券代码：";
            // 
            // gbChangeOut
            // 
            this.gbChangeOut.Controls.Add(this.lbSecuOut);
            this.gbChangeOut.Controls.Add(this.tbOutAmount);
            this.gbChangeOut.Controls.Add(this.tbOutPrice);
            this.gbChangeOut.Controls.Add(this.cbOutDirection);
            this.gbChangeOut.Controls.Add(this.cbLongShort);
            this.gbChangeOut.Controls.Add(this.acSecuOut);
            this.gbChangeOut.Controls.Add(this.lblOutAmount);
            this.gbChangeOut.Controls.Add(this.lblOutPrice);
            this.gbChangeOut.Controls.Add(this.lblOutDirection);
            this.gbChangeOut.Controls.Add(this.lblLongShort);
            this.gbChangeOut.Controls.Add(this.lblSecuCode1);
            this.gbChangeOut.Location = new System.Drawing.Point(13, 19);
            this.gbChangeOut.Name = "gbChangeOut";
            this.gbChangeOut.Size = new System.Drawing.Size(298, 270);
            this.gbChangeOut.TabIndex = 0;
            this.gbChangeOut.TabStop = false;
            this.gbChangeOut.Text = "调出";
            // 
            // lbSecuOut
            // 
            this.lbSecuOut.FormattingEnabled = true;
            this.lbSecuOut.ItemHeight = 12;
            this.lbSecuOut.Location = new System.Drawing.Point(99, 59);
            this.lbSecuOut.Name = "lbSecuOut";
            this.lbSecuOut.Size = new System.Drawing.Size(151, 88);
            this.lbSecuOut.TabIndex = 10;
            this.lbSecuOut.Visible = false;
            // 
            // tbOutAmount
            // 
            this.tbOutAmount.Location = new System.Drawing.Point(99, 216);
            this.tbOutAmount.Name = "tbOutAmount";
            this.tbOutAmount.Size = new System.Drawing.Size(168, 21);
            this.tbOutAmount.TabIndex = 9;
            this.tbOutAmount.Text = "0";
            // 
            // tbOutPrice
            // 
            this.tbOutPrice.Location = new System.Drawing.Point(99, 171);
            this.tbOutPrice.Name = "tbOutPrice";
            this.tbOutPrice.Size = new System.Drawing.Size(168, 21);
            this.tbOutPrice.TabIndex = 8;
            this.tbOutPrice.Text = "0";
            // 
            // cbOutDirection
            // 
            this.cbOutDirection.Enabled = false;
            this.cbOutDirection.FormattingEnabled = true;
            this.cbOutDirection.Location = new System.Drawing.Point(99, 127);
            this.cbOutDirection.Name = "cbOutDirection";
            this.cbOutDirection.Size = new System.Drawing.Size(168, 20);
            this.cbOutDirection.TabIndex = 7;
            // 
            // cbLongShort
            // 
            this.cbLongShort.Enabled = false;
            this.cbLongShort.FormattingEnabled = true;
            this.cbLongShort.Location = new System.Drawing.Point(99, 83);
            this.cbLongShort.Name = "cbLongShort";
            this.cbLongShort.Size = new System.Drawing.Size(168, 20);
            this.cbLongShort.TabIndex = 6;
            // 
            // acSecuOut
            // 
            this.acSecuOut.Enabled = false;
            this.acSecuOut.Location = new System.Drawing.Point(90, 28);
            this.acSecuOut.Name = "acSecuOut";
            this.acSecuOut.Size = new System.Drawing.Size(188, 31);
            this.acSecuOut.TabIndex = 5;
            // 
            // lblOutAmount
            // 
            this.lblOutAmount.AutoSize = true;
            this.lblOutAmount.Location = new System.Drawing.Point(26, 222);
            this.lblOutAmount.Name = "lblOutAmount";
            this.lblOutAmount.Size = new System.Drawing.Size(65, 12);
            this.lblOutAmount.TabIndex = 4;
            this.lblOutAmount.Text = "调出数量：";
            // 
            // lblOutPrice
            // 
            this.lblOutPrice.AutoSize = true;
            this.lblOutPrice.Location = new System.Drawing.Point(26, 176);
            this.lblOutPrice.Name = "lblOutPrice";
            this.lblOutPrice.Size = new System.Drawing.Size(65, 12);
            this.lblOutPrice.TabIndex = 3;
            this.lblOutPrice.Text = "调出价格：";
            // 
            // lblOutDirection
            // 
            this.lblOutDirection.AutoSize = true;
            this.lblOutDirection.Location = new System.Drawing.Point(26, 130);
            this.lblOutDirection.Name = "lblOutDirection";
            this.lblOutDirection.Size = new System.Drawing.Size(65, 12);
            this.lblOutDirection.TabIndex = 2;
            this.lblOutDirection.Text = "调出方向：";
            // 
            // lblLongShort
            // 
            this.lblLongShort.AutoSize = true;
            this.lblLongShort.Location = new System.Drawing.Point(26, 84);
            this.lblLongShort.Name = "lblLongShort";
            this.lblLongShort.Size = new System.Drawing.Size(65, 12);
            this.lblLongShort.TabIndex = 1;
            this.lblLongShort.Text = "多空标志：";
            // 
            // lblSecuCode1
            // 
            this.lblSecuCode1.AutoSize = true;
            this.lblSecuCode1.Location = new System.Drawing.Point(26, 38);
            this.lblSecuCode1.Name = "lblSecuCode1";
            this.lblSecuCode1.Size = new System.Drawing.Size(65, 12);
            this.lblSecuCode1.TabIndex = 0;
            this.lblSecuCode1.Text = "证券代码：";
            // 
            // ChangePositionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(643, 391);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Name = "ChangePositionDialog";
            this.Text = "换仓对话框";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.gbChangIn.ResumeLayout(false);
            this.gbChangIn.PerformLayout();
            this.gbChangeOut.ResumeLayout(false);
            this.gbChangeOut.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label lblCaptin;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox gbChangIn;
        private System.Windows.Forms.GroupBox gbChangeOut;
        private System.Windows.Forms.TextBox tbInAmount;
        private System.Windows.Forms.TextBox tbInPrice;
        private System.Windows.Forms.ComboBox cbInDirection;
        private System.Windows.Forms.ComboBox cbStopFlag;
        private Controls.AutoComplete acSecuIn;
        private System.Windows.Forms.Label lblInAmount;
        private System.Windows.Forms.Label lblInPrice;
        private System.Windows.Forms.Label lblInDirection;
        private System.Windows.Forms.Label lblStopFlag;
        private System.Windows.Forms.Label lblSecuCode2;
        private System.Windows.Forms.TextBox tbOutAmount;
        private System.Windows.Forms.TextBox tbOutPrice;
        private System.Windows.Forms.ComboBox cbOutDirection;
        private System.Windows.Forms.ComboBox cbLongShort;
        private Controls.AutoComplete acSecuOut;
        private System.Windows.Forms.Label lblOutAmount;
        private System.Windows.Forms.Label lblOutPrice;
        private System.Windows.Forms.Label lblOutDirection;
        private System.Windows.Forms.Label lblLongShort;
        private System.Windows.Forms.Label lblSecuCode1;
        private System.Windows.Forms.ListBox lbSecuIn;
        private System.Windows.Forms.ListBox lbSecuOut;
    }
}
