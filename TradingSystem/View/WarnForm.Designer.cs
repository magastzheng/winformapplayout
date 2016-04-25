namespace TradingSystem.View
{
    partial class WarnForm
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
            this.tlPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblCaption = new System.Windows.Forms.Label();
            this.rtBoxMessage = new System.Windows.Forms.RichTextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.tlPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlPanelMain
            // 
            this.tlPanelMain.ColumnCount = 1;
            this.tlPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlPanelMain.Controls.Add(this.lblCaption, 0, 0);
            this.tlPanelMain.Controls.Add(this.rtBoxMessage, 0, 1);
            this.tlPanelMain.Controls.Add(this.btnConfirm, 0, 2);
            this.tlPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tlPanelMain.Name = "tlPanelMain";
            this.tlPanelMain.RowCount = 3;
            this.tlPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlPanelMain.Size = new System.Drawing.Size(284, 262);
            this.tlPanelMain.TabIndex = 0;
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCaption.Location = new System.Drawing.Point(3, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(278, 30);
            this.lblCaption.TabIndex = 0;
            // 
            // rtBoxMessage
            // 
            this.rtBoxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtBoxMessage.Location = new System.Drawing.Point(3, 33);
            this.rtBoxMessage.Name = "rtBoxMessage";
            this.rtBoxMessage.ReadOnly = true;
            this.rtBoxMessage.Size = new System.Drawing.Size(278, 176);
            this.rtBoxMessage.TabIndex = 1;
            this.rtBoxMessage.Text = "";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnConfirm.Location = new System.Drawing.Point(104, 225);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(ButtonConfirm_Click);
            // 
            // WarnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tlPanelMain);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "WarnForm";
            this.Text = "警告";
            this.tlPanelMain.ResumeLayout(false);
            this.tlPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlPanelMain;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.RichTextBox rtBoxMessage;
        private System.Windows.Forms.Button btnConfirm;

    }
}