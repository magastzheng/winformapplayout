namespace TradingSystem.View
{
    partial class GeneralForm
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
            this.panelChildTop = new System.Windows.Forms.Panel();
            this.tsChildTop = new System.Windows.Forms.ToolStrip();
            this.panelChildBottom = new System.Windows.Forms.Panel();
            this.panelChildMain = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.panelChildTop.SuspendLayout();
            this.panelChildMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelChildTop
            // 
            this.panelChildTop.Controls.Add(this.tsChildTop);
            this.panelChildTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelChildTop.Location = new System.Drawing.Point(0, 0);
            this.panelChildTop.Name = "panelChildTop";
            this.panelChildTop.Size = new System.Drawing.Size(1125, 26);
            this.panelChildTop.TabIndex = 0;
            // 
            // tsChildTop
            // 
            this.tsChildTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsChildTop.Location = new System.Drawing.Point(0, 0);
            this.tsChildTop.Name = "tsChildTop";
            this.tsChildTop.Size = new System.Drawing.Size(1125, 26);
            this.tsChildTop.TabIndex = 0;
            this.tsChildTop.Text = "toolStrip1";
            // 
            // panelChildBottom
            // 
            this.panelChildBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelChildBottom.Location = new System.Drawing.Point(0, 546);
            this.panelChildBottom.Name = "panelChildBottom";
            this.panelChildBottom.Size = new System.Drawing.Size(1125, 24);
            this.panelChildBottom.TabIndex = 1;
            // 
            // panelChildMain
            // 
            this.panelChildMain.Controls.Add(this.dataGridView);
            this.panelChildMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildMain.Location = new System.Drawing.Point(0, 26);
            this.panelChildMain.Name = "panelChildMain";
            this.panelChildMain.Size = new System.Drawing.Size(1125, 520);
            this.panelChildMain.TabIndex = 2;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(1125, 520);
            this.dataGridView.TabIndex = 0;
            // 
            // GeneralForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Controls.Add(this.panelChildMain);
            this.Controls.Add(this.panelChildBottom);
            this.Controls.Add(this.panelChildTop);
            this.Name = "GeneralForm";
            this.panelChildTop.ResumeLayout(false);
            this.panelChildTop.PerformLayout();
            this.panelChildMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelChildTop;
        private System.Windows.Forms.Panel panelChildBottom;
        private System.Windows.Forms.Panel panelChildMain;
        private System.Windows.Forms.ToolStrip tsChildTop;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}
