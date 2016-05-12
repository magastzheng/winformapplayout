namespace ControlsTest
{
    partial class ButtonContainerForm
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
            this.buttonContainer1 = new Controls.ButtonContainer.ButtonContainer();
            this.SuspendLayout();
            // 
            // buttonContainer1
            // 
            this.buttonContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonContainer1.Location = new System.Drawing.Point(0, 0);
            this.buttonContainer1.Name = "buttonContainer1";
            this.buttonContainer1.Size = new System.Drawing.Size(284, 262);
            this.buttonContainer1.TabIndex = 0;
            // 
            // ButtonContainerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.buttonContainer1);
            this.Name = "ButtonContainerForm";
            this.Text = "ButtonContainerForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ButtonContainer.ButtonContainer buttonContainer1;
    }
}