namespace TradingSystem.Dialog
{
    partial class PortfolioSecurityDialog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.lblSecuCode = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblSettingWeight = new System.Windows.Forms.Label();
            this.tbAmount = new System.Windows.Forms.TextBox();
            this.rdbAmount = new System.Windows.Forms.RadioButton();
            this.lblShare = new System.Windows.Forms.Label();
            this.tbSettingWeight = new System.Windows.Forms.TextBox();
            this.lblPercent = new System.Windows.Forms.Label();
            this.rdbPercent = new System.Windows.Forms.RadioButton();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbDropdown = new System.Windows.Forms.ListBox();
            this.acSecurity = new Controls.AutoComplete();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lblDescription);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(341, 99);
            this.panel1.TabIndex = 0;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(112, 56);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(125, 12);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "设置组合模板中的证券";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(111, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(77, 12);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "设置证券信息";
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(105, 95);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // lblSecuCode
            // 
            this.lblSecuCode.AutoSize = true;
            this.lblSecuCode.Location = new System.Drawing.Point(32, 122);
            this.lblSecuCode.Name = "lblSecuCode";
            this.lblSecuCode.Size = new System.Drawing.Size(53, 12);
            this.lblSecuCode.TabIndex = 1;
            this.lblSecuCode.Text = "证券代码";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(32, 157);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(29, 12);
            this.lblAmount.TabIndex = 2;
            this.lblAmount.Text = "数量";
            // 
            // lblSettingWeight
            // 
            this.lblSettingWeight.AutoSize = true;
            this.lblSettingWeight.Location = new System.Drawing.Point(32, 187);
            this.lblSettingWeight.Name = "lblSettingWeight";
            this.lblSettingWeight.Size = new System.Drawing.Size(53, 12);
            this.lblSettingWeight.TabIndex = 3;
            this.lblSettingWeight.Text = "设置比例";
            // 
            // tbAmount
            // 
            this.tbAmount.Location = new System.Drawing.Point(116, 153);
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.Size = new System.Drawing.Size(132, 21);
            this.tbAmount.TabIndex = 4;
            // 
            // rdbAmount
            // 
            this.rdbAmount.AutoSize = true;
            this.rdbAmount.Checked = true;
            this.rdbAmount.Location = new System.Drawing.Point(277, 156);
            this.rdbAmount.Name = "rdbAmount";
            this.rdbAmount.Size = new System.Drawing.Size(14, 13);
            this.rdbAmount.TabIndex = 5;
            this.rdbAmount.TabStop = true;
            this.rdbAmount.UseVisualStyleBackColor = true;
            // 
            // lblShare
            // 
            this.lblShare.AutoSize = true;
            this.lblShare.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblShare.Location = new System.Drawing.Point(254, 157);
            this.lblShare.Name = "lblShare";
            this.lblShare.Size = new System.Drawing.Size(17, 12);
            this.lblShare.TabIndex = 6;
            this.lblShare.Text = "股";
            // 
            // tbSettingWeight
            // 
            this.tbSettingWeight.Enabled = false;
            this.tbSettingWeight.Location = new System.Drawing.Point(115, 184);
            this.tbSettingWeight.Name = "tbSettingWeight";
            this.tbSettingWeight.Size = new System.Drawing.Size(133, 21);
            this.tbSettingWeight.TabIndex = 7;
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblPercent.Location = new System.Drawing.Point(254, 193);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(11, 12);
            this.lblPercent.TabIndex = 8;
            this.lblPercent.Text = "%";
            // 
            // rdbPercent
            // 
            this.rdbPercent.AutoSize = true;
            this.rdbPercent.Location = new System.Drawing.Point(277, 192);
            this.rdbPercent.Name = "rdbPercent";
            this.rdbPercent.Size = new System.Drawing.Size(14, 13);
            this.rdbPercent.TabIndex = 9;
            this.rdbPercent.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(59, 352);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 10;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(190, 352);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lbDropdown
            // 
            this.lbDropdown.FormattingEnabled = true;
            this.lbDropdown.ItemHeight = 12;
            this.lbDropdown.Location = new System.Drawing.Point(120, 140);
            this.lbDropdown.Name = "lbDropdown";
            this.lbDropdown.Size = new System.Drawing.Size(129, 136);
            this.lbDropdown.TabIndex = 14;
            this.lbDropdown.Visible = false;
            // 
            // acSecurity
            // 
            this.acSecurity.Location = new System.Drawing.Point(108, 112);
            this.acSecurity.Name = "acSecurity";
            this.acSecurity.Size = new System.Drawing.Size(188, 31);
            this.acSecurity.TabIndex = 15;
            // 
            // PortfolioSecurityDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(341, 391);
            this.Controls.Add(this.acSecurity);
            this.Controls.Add(this.lbDropdown);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.rdbPercent);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.tbSettingWeight);
            this.Controls.Add(this.lblShare);
            this.Controls.Add(this.rdbAmount);
            this.Controls.Add(this.tbAmount);
            this.Controls.Add(this.lblSettingWeight);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblSecuCode);
            this.Controls.Add(this.panel1);
            this.Name = "PortfolioSecurityDialog";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblSecuCode;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblSettingWeight;
        private System.Windows.Forms.TextBox tbAmount;
        private System.Windows.Forms.RadioButton rdbAmount;
        private System.Windows.Forms.Label lblShare;
        private System.Windows.Forms.TextBox tbSettingWeight;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.RadioButton rdbPercent;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lbDropdown;
        private Controls.AutoComplete acSecurity;
    }
}
