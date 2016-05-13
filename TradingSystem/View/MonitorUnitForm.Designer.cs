namespace TradingSystem.View
{
    partial class MonitorUnitForm
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
            this.dataGridView = new Controls.GridView.TSDataGridView();
            this.buttonContainer = new Controls.ButtonContainer.ButtonContainer();
            this.confirmCancelContainer = new Controls.ButtonContainer.ButtonContainer();
            this.panelBottom.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.confirmCancelContainer);
            this.panelBottom.Controls.Add(this.buttonContainer);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.dataGridView);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(1125, 506);
            this.dataGridView.TabIndex = 0;
            // 
            // buttonContainer
            // 
            this.buttonContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonContainer.Location = new System.Drawing.Point(0, 0);
            this.buttonContainer.Name = "buttonContainer";
            this.buttonContainer.Size = new System.Drawing.Size(833, 32);
            this.buttonContainer.TabIndex = 0;
            // 
            // confirmCancelContainer
            // 
            this.confirmCancelContainer.Dock = System.Windows.Forms.DockStyle.Right;
            this.confirmCancelContainer.Location = new System.Drawing.Point(874, 0);
            this.confirmCancelContainer.Name = "confirmCancelContainer";
            this.confirmCancelContainer.Size = new System.Drawing.Size(251, 32);
            this.confirmCancelContainer.TabIndex = 1;
            // 
            // MonitorUnitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Name = "MonitorUnitForm";
            this.panelBottom.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.GridView.TSDataGridView dataGridView;
        private Controls.ButtonContainer.ButtonContainer buttonContainer;
        private Controls.ButtonContainer.ButtonContainer confirmCancelContainer;


    }
}
