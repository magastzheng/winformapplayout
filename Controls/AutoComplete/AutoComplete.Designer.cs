namespace Controls
{
    partial class AutoComplete
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbInputId = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbInputId
            // 
            this.tbInputId.Location = new System.Drawing.Point(8, 6);
            this.tbInputId.Name = "tbInputId";
            this.tbInputId.Size = new System.Drawing.Size(87, 21);
            this.tbInputId.TabIndex = 1;
            this.tbInputId.Enter += new System.EventHandler(this.TextBox_Enter);
            this.tbInputId.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyUp);
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tbName.Location = new System.Drawing.Point(101, 6);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(78, 21);
            this.tbName.TabIndex = 2;
            // 
            // AutoComplete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.tbInputId);
            this.Name = "AutoComplete";
            this.Size = new System.Drawing.Size(188, 31);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox tbInputId;
        private System.Windows.Forms.TextBox tbName;
    }
}
