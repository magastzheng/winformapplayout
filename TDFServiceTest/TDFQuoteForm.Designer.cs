namespace TDFServiceTest
{
    partial class TDFQuoteForm
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
            this.rtbQuote = new System.Windows.Forms.RichTextBox();
            this.tbSecuCodes = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbQuote
            // 
            this.rtbQuote.Location = new System.Drawing.Point(109, 37);
            this.rtbQuote.Name = "rtbQuote";
            this.rtbQuote.Size = new System.Drawing.Size(364, 184);
            this.rtbQuote.TabIndex = 0;
            this.rtbQuote.Text = "";
            // 
            // tbSecuCodes
            // 
            this.tbSecuCodes.Location = new System.Drawing.Point(191, 242);
            this.tbSecuCodes.Name = "tbSecuCodes";
            this.tbSecuCodes.Size = new System.Drawing.Size(282, 21);
            this.tbSecuCodes.TabIndex = 1;
            this.tbSecuCodes.Text = "000002.SZ; 600001.SH; IC1611.CF";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(107, 245);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(53, 12);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "股票代码";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(191, 290);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TDFQuoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 412);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tbSecuCodes);
            this.Controls.Add(this.rtbQuote);
            this.Name = "TDFQuoteForm";
            this.Text = "TDFQuoteForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbQuote;
        private System.Windows.Forms.TextBox tbSecuCodes;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button button1;
    }
}