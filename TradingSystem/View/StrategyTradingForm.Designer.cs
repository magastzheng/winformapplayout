namespace TradingSystem.View
{
    partial class StrategyTradingForm
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
            //if (disposing && (components != null))
            //{
            //    components.Dispose();
            //}
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tsChildTop = new System.Windows.Forms.ToolStrip();
            this.bottomContainer = new Controls.ButtonContainer.ButtonContainer();
            this.childSplitter = new System.Windows.Forms.SplitContainer();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.childSplitter)).BeginInit();
            this.childSplitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.tsChildTop);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.bottomContainer);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.childSplitter);
            // 
            // tsChildTop
            // 
            this.tsChildTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsChildTop.Location = new System.Drawing.Point(0, 0);
            this.tsChildTop.Name = "tsChildTop";
            this.tsChildTop.Size = new System.Drawing.Size(1125, 32);
            this.tsChildTop.TabIndex = 0;
            this.tsChildTop.Text = "toolStrip1";
            // 
            // bottomContainer
            // 
            this.bottomContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.bottomContainer.Location = new System.Drawing.Point(0, 0);
            this.bottomContainer.Name = "bottomContainer";
            this.bottomContainer.Size = new System.Drawing.Size(804, 32);
            this.bottomContainer.TabIndex = 0;
            // 
            // childSplitter
            // 
            this.childSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.childSplitter.Location = new System.Drawing.Point(0, 0);
            this.childSplitter.Name = "childSplitter";
            this.childSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.childSplitter.Size = new System.Drawing.Size(1125, 506);
            this.childSplitter.SplitterDistance = 174;
            this.childSplitter.TabIndex = 0;
            // 
            // StrategyTradingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Name = "StrategyTradingForm";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.childSplitter)).EndInit();
            this.childSplitter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsChildTop;
        private Controls.ButtonContainer.ButtonContainer bottomContainer;
        private System.Windows.Forms.SplitContainer childSplitter;
    }
}
