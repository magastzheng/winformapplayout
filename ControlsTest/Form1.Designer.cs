namespace ControlsTest
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tsNavBarContainer1 = new Controls.TSNavBarContainer();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // tsNavBarContainer1
            // 
            this.tsNavBarContainer1.AutoScroll = true;
            this.tsNavBarContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(140)))), ((int)(((byte)(225)))));
            this.tsNavBarContainer1.BarMargin = 5;
            this.tsNavBarContainer1.BarSpace = 1;
            this.tsNavBarContainer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tsNavBarContainer1.Location = new System.Drawing.Point(0, 0);
            this.tsNavBarContainer1.Name = "tsNavBarContainer1";
            this.tsNavBarContainer1.SelectedIndex = -1;
            this.tsNavBarContainer1.Size = new System.Drawing.Size(180, 331);
            this.tsNavBarContainer1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "right.png");
            this.imageList1.Images.SetKeyName(1, "down.png");
            this.imageList1.Images.SetKeyName(2, "item.png");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 331);
            this.Controls.Add(this.tsNavBarContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TSNavBarContainer tsNavBarContainer1;
        private System.Windows.Forms.ImageList imageList1;
        //private Controls.TSNavBarItem tsNavBarItem1;
        //private Controls.TSNavBarItem tsNavBarItem2;
        //private Controls.TSNavBarItem tsNavBarItem3;
    }
}

