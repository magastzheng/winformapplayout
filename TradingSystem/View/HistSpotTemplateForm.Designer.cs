namespace TradingSystem.View
{
    partial class HistSpotTemplateForm
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
            this.mainSplitter = new System.Windows.Forms.SplitContainer();
            this.tempGridView = new Controls.GridView.TSDataGridView();
            this.secuGridView = new Controls.GridView.TSDataGridView();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitter)).BeginInit();
            this.mainSplitter.Panel1.SuspendLayout();
            this.mainSplitter.Panel2.SuspendLayout();
            this.mainSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tempGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secuGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.mainSplitter);
            // 
            // mainSplitter
            // 
            this.mainSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitter.Location = new System.Drawing.Point(0, 0);
            this.mainSplitter.Name = "mainSplitter";
            this.mainSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSplitter.Panel1
            // 
            this.mainSplitter.Panel1.Controls.Add(this.tempGridView);
            // 
            // mainSplitter.Panel2
            // 
            this.mainSplitter.Panel2.Controls.Add(this.secuGridView);
            this.mainSplitter.Size = new System.Drawing.Size(1125, 506);
            this.mainSplitter.SplitterDistance = 149;
            this.mainSplitter.TabIndex = 0;
            // 
            // tempGridView
            // 
            this.tempGridView.AllowUserToAddRows = false;
            this.tempGridView.AllowUserToDeleteRows = false;
            this.tempGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tempGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tempGridView.Location = new System.Drawing.Point(0, 0);
            this.tempGridView.Name = "tempGridView";
            this.tempGridView.RowTemplate.Height = 23;
            this.tempGridView.Size = new System.Drawing.Size(1125, 149);
            this.tempGridView.TabIndex = 0;
            // 
            // secuGridView
            // 
            this.secuGridView.AllowUserToAddRows = false;
            this.secuGridView.AllowUserToDeleteRows = false;
            this.secuGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.secuGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secuGridView.Location = new System.Drawing.Point(0, 0);
            this.secuGridView.Name = "secuGridView";
            this.secuGridView.RowTemplate.Height = 23;
            this.secuGridView.Size = new System.Drawing.Size(1125, 353);
            this.secuGridView.TabIndex = 0;
            // 
            // HistSpotTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Name = "HistSpotTemplateForm";
            this.panelMain.ResumeLayout(false);
            this.mainSplitter.Panel1.ResumeLayout(false);
            this.mainSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitter)).EndInit();
            this.mainSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tempGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secuGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitter;
        private Controls.GridView.TSDataGridView tempGridView;
        private Controls.GridView.TSDataGridView secuGridView;
    }
}
