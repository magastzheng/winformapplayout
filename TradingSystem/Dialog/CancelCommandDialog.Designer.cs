namespace TradingSystem.Dialog
{
    partial class CancelCommandDialog
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
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbCause = new System.Windows.Forms.TextBox();
            this.lblCancelCause = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(157, 73);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(293, 73);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tbCause
            // 
            this.tbCause.Location = new System.Drawing.Point(12, 40);
            this.tbCause.Name = "tbCause";
            this.tbCause.Size = new System.Drawing.Size(483, 21);
            this.tbCause.TabIndex = 2;
            // 
            // lblCancelCause
            // 
            this.lblCancelCause.AutoSize = true;
            this.lblCancelCause.Location = new System.Drawing.Point(12, 14);
            this.lblCancelCause.Name = "lblCancelCause";
            this.lblCancelCause.Size = new System.Drawing.Size(59, 12);
            this.lblCancelCause.TabIndex = 3;
            this.lblCancelCause.Text = "撤销原因:";
            // 
            // CancelCommandDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(507, 107);
            this.Controls.Add(this.lblCancelCause);
            this.Controls.Add(this.tbCause);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Name = "CancelCommandDialog";
            this.Text = "确定撤销指令吗?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbCause;
        private System.Windows.Forms.Label lblCancelCause;
    }
}
