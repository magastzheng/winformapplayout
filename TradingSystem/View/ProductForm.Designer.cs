namespace TradingSystem.View
{
    partial class ProductForm
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
            this.fundGridView = new Controls.GridView.TSDataGridView();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fundGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(1125, 10);
            // 
            // panelBottom
            // 
            this.panelBottom.Location = new System.Drawing.Point(0, 560);
            this.panelBottom.Size = new System.Drawing.Size(1125, 10);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.fundGridView);
            this.panelMain.Location = new System.Drawing.Point(0, 10);
            this.panelMain.Size = new System.Drawing.Size(1125, 550);
            // 
            // fundGridView
            // 
            this.fundGridView.AllowUserToAddRows = false;
            this.fundGridView.AllowUserToDeleteRows = false;
            this.fundGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fundGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fundGridView.Location = new System.Drawing.Point(0, 0);
            this.fundGridView.Name = "fundGridView";
            this.fundGridView.RowTemplate.Height = 23;
            this.fundGridView.Size = new System.Drawing.Size(1125, 550);
            this.fundGridView.TabIndex = 0;
            // 
            // ProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Name = "ProductForm";
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fundGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.GridView.TSDataGridView fundGridView;


    }
}
