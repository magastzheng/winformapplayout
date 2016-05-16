namespace TradingSystem.View
{
    partial class OpenPositionForm
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
            this.childSplitter = new System.Windows.Forms.SplitContainer();
            this.monitorGridView = new Controls.GridView.TSDataGridView();
            this.securityGridView = new Controls.GridView.TSDataGridView();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.childSplitter)).BeginInit();
            this.childSplitter.Panel1.SuspendLayout();
            this.childSplitter.Panel2.SuspendLayout();
            this.childSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monitorGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.childSplitter);
            // 
            // childSplitter
            // 
            this.childSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.childSplitter.Location = new System.Drawing.Point(0, 0);
            this.childSplitter.Name = "childSplitter";
            this.childSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // childSplitter.Panel1
            // 
            this.childSplitter.Panel1.Controls.Add(this.monitorGridView);
            // 
            // childSplitter.Panel2
            // 
            this.childSplitter.Panel2.Controls.Add(this.securityGridView);
            this.childSplitter.Size = new System.Drawing.Size(1125, 506);
            this.childSplitter.SplitterDistance = 133;
            this.childSplitter.TabIndex = 0;
            // 
            // monitorGridView
            // 
            this.monitorGridView.AllowUserToAddRows = false;
            this.monitorGridView.AllowUserToDeleteRows = false;
            this.monitorGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.monitorGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorGridView.Location = new System.Drawing.Point(0, 0);
            this.monitorGridView.Name = "monitorGridView";
            this.monitorGridView.RowTemplate.Height = 23;
            this.monitorGridView.Size = new System.Drawing.Size(1125, 133);
            this.monitorGridView.TabIndex = 0;
            // 
            // securityGridView
            // 
            this.securityGridView.AllowUserToAddRows = false;
            this.securityGridView.AllowUserToDeleteRows = false;
            this.securityGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.securityGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.securityGridView.Location = new System.Drawing.Point(0, 0);
            this.securityGridView.Name = "securityGridView";
            this.securityGridView.RowTemplate.Height = 23;
            this.securityGridView.Size = new System.Drawing.Size(1125, 369);
            this.securityGridView.TabIndex = 0;
            // 
            // OpenPositionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Name = "OpenPositionForm";
            this.panelMain.ResumeLayout(false);
            this.childSplitter.Panel1.ResumeLayout(false);
            this.childSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.childSplitter)).EndInit();
            this.childSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.monitorGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer childSplitter;
        private Controls.GridView.TSDataGridView monitorGridView;
        private Controls.GridView.TSDataGridView securityGridView;

    }
}
