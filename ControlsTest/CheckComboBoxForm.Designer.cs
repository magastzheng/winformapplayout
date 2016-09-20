namespace ControlsTest
{
    partial class CheckComboBoxForm
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
            this.checkComboBox1 = new Controls.CheckComboBox.CheckComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // checkComboBox1
            // 
            this.checkComboBox1.CheckOnClick = true;
            this.checkComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.checkComboBox1.DropDownHeight = 1;
            this.checkComboBox1.FormattingEnabled = true;
            this.checkComboBox1.IntegralHeight = false;
            this.checkComboBox1.Location = new System.Drawing.Point(25, 24);
            this.checkComboBox1.Name = "checkComboBox1";
            this.checkComboBox1.Size = new System.Drawing.Size(121, 22);
            this.checkComboBox1.TabIndex = 0;
            this.checkComboBox1.ValueSeparator = ",";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(127, 152);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(182, 145);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // CheckComboBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 309);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkComboBox1);
            this.Name = "CheckComboBoxForm";
            this.Text = "CheckComboBoxForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CheckComboBox.CheckComboBox checkComboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}