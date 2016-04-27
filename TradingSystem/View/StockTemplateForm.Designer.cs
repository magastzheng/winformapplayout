namespace TradingSystem.View
{
    partial class StockTemplateForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        //private System.ComponentModel.IContainer components = null;

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
            this.panelChildTop = new System.Windows.Forms.Panel();
            this.toolStripChildTop = new System.Windows.Forms.ToolStrip();
            this.panelChildBottom = new System.Windows.Forms.Panel();
            this.splitContainerChild = new System.Windows.Forms.SplitContainer();
            this.panelChildTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerChild)).BeginInit();
            this.splitContainerChild.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelChildTop
            // 
            this.panelChildTop.Controls.Add(this.toolStripChildTop);
            this.panelChildTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelChildTop.Location = new System.Drawing.Point(0, 0);
            this.panelChildTop.Name = "panelChildTop";
            this.panelChildTop.Size = new System.Drawing.Size(1125, 38);
            this.panelChildTop.TabIndex = 0;
            // 
            // toolStripChildTop
            // 
            this.toolStripChildTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripChildTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripChildTop.Name = "toolStripChildTop";
            this.toolStripChildTop.Size = new System.Drawing.Size(1125, 38);
            this.toolStripChildTop.TabIndex = 0;
            this.toolStripChildTop.Text = "toolStrip1";
            // 
            // panelChildBottom
            // 
            this.panelChildBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelChildBottom.Location = new System.Drawing.Point(0, 452);
            this.panelChildBottom.Name = "panelChildBottom";
            this.panelChildBottom.Size = new System.Drawing.Size(1125, 35);
            this.panelChildBottom.TabIndex = 1;
            // 
            // splitContainerChild
            // 
            this.splitContainerChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerChild.Location = new System.Drawing.Point(0, 38);
            this.splitContainerChild.Name = "splitContainerChild";
            this.splitContainerChild.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainerChild.Size = new System.Drawing.Size(1125, 414);
            this.splitContainerChild.SplitterDistance = 217;
            this.splitContainerChild.TabIndex = 2;
            // 
            // StockTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 487);
            this.Controls.Add(this.splitContainerChild);
            this.Controls.Add(this.panelChildBottom);
            this.Controls.Add(this.panelChildTop);
            this.Name = "StockTemplateForm";
            this.panelChildTop.ResumeLayout(false);
            this.panelChildTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerChild)).EndInit();
            this.splitContainerChild.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelChildTop;
        private System.Windows.Forms.Panel panelChildBottom;
        private System.Windows.Forms.SplitContainer splitContainerChild;
        private System.Windows.Forms.ToolStrip toolStripChildTop;
    }
}
