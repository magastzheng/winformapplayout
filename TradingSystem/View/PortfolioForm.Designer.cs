namespace TradingSystem.View
{
    partial class PortfolioForm
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
            this.panelChildTop = new System.Windows.Forms.Panel();
            this.panelChildBottom = new System.Windows.Forms.Panel();
            this.panelChildMain = new System.Windows.Forms.Panel();
            this.gridView = new Controls.GridView.TSDataGridView();
            this.panelChildMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelChildTop
            // 
            this.panelChildTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelChildTop.Location = new System.Drawing.Point(0, 0);
            this.panelChildTop.Name = "panelChildTop";
            this.panelChildTop.Size = new System.Drawing.Size(1125, 10);
            this.panelChildTop.TabIndex = 0;
            // 
            // panelChildBottom
            // 
            this.panelChildBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelChildBottom.Location = new System.Drawing.Point(0, 560);
            this.panelChildBottom.Name = "panelChildBottom";
            this.panelChildBottom.Size = new System.Drawing.Size(1125, 10);
            this.panelChildBottom.TabIndex = 1;
            // 
            // panelChildMain
            // 
            this.panelChildMain.Controls.Add(this.gridView);
            this.panelChildMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildMain.Location = new System.Drawing.Point(0, 10);
            this.panelChildMain.Name = "panelChildMain";
            this.panelChildMain.Size = new System.Drawing.Size(1125, 550);
            this.panelChildMain.TabIndex = 2;
            // 
            // gridView
            // 
            this.gridView.AllowUserToAddRows = false;
            this.gridView.AllowUserToDeleteRows = false;
            this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridView.Location = new System.Drawing.Point(0, 0);
            this.gridView.Name = "gridView";
            this.gridView.RowTemplate.Height = 23;
            this.gridView.Size = new System.Drawing.Size(1125, 550);
            this.gridView.TabIndex = 0;
            // 
            // PortfolioForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Controls.Add(this.panelChildMain);
            this.Controls.Add(this.panelChildBottom);
            this.Controls.Add(this.panelChildTop);
            this.Name = "PortfolioForm";
            this.panelChildMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelChildTop;
        private System.Windows.Forms.Panel panelChildBottom;
        private System.Windows.Forms.Panel panelChildMain;
        private Controls.GridView.TSDataGridView gridView;
    }
}
