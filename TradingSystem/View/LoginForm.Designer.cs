namespace TradingSystem.View
{
    partial class LoginForm
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
            this.lblOperatorNo = new System.Windows.Forms.Label();
            this.lblOperatorPwd = new System.Windows.Forms.Label();
            this.cmbOperatorNo = new System.Windows.Forms.ComboBox();
            this.tbOperatorPwd = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblOperatorNo
            // 
            this.lblOperatorNo.AutoSize = true;
            this.lblOperatorNo.Location = new System.Drawing.Point(116, 77);
            this.lblOperatorNo.Name = "lblOperatorNo";
            this.lblOperatorNo.Size = new System.Drawing.Size(65, 12);
            this.lblOperatorNo.TabIndex = 0;
            this.lblOperatorNo.Text = "操作员编号";
            // 
            // lblOperatorPwd
            // 
            this.lblOperatorPwd.AutoSize = true;
            this.lblOperatorPwd.Location = new System.Drawing.Point(116, 111);
            this.lblOperatorPwd.Name = "lblOperatorPwd";
            this.lblOperatorPwd.Size = new System.Drawing.Size(65, 12);
            this.lblOperatorPwd.TabIndex = 1;
            this.lblOperatorPwd.Text = "操作员密码";
            // 
            // cmbOperatorNo
            // 
            this.cmbOperatorNo.FormattingEnabled = true;
            this.cmbOperatorNo.Location = new System.Drawing.Point(200, 77);
            this.cmbOperatorNo.Name = "cmbOperatorNo";
            this.cmbOperatorNo.Size = new System.Drawing.Size(121, 20);
            this.cmbOperatorNo.TabIndex = 2;
            // 
            // tbOperatorPwd
            // 
            this.tbOperatorPwd.Location = new System.Drawing.Point(200, 111);
            this.tbOperatorPwd.Name = "tbOperatorPwd";
            this.tbOperatorPwd.Size = new System.Drawing.Size(121, 21);
            this.tbOperatorPwd.TabIndex = 3;
            this.tbOperatorPwd.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(116, 190);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(ButtonLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(246, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(ButtonCancel_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 278);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbOperatorPwd);
            this.Controls.Add(this.cmbOperatorNo);
            this.Controls.Add(this.lblOperatorPwd);
            this.Controls.Add(this.lblOperatorNo);
            this.Name = "LoginForm";
            this.Text = "登录";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(LoginForm_FormClosing);

            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOperatorNo;
        private System.Windows.Forms.Label lblOperatorPwd;
        private System.Windows.Forms.ComboBox cmbOperatorNo;
        private System.Windows.Forms.TextBox tbOperatorPwd;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancel;
    }
}