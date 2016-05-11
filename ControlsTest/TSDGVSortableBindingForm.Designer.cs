namespace ControlsTest
{
    partial class TSDGVSortableBindingForm
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
            this.tsDataGridView1 = new Controls.GridView.TSDataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tsDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tsDataGridView1
            // 
            this.tsDataGridView1.AllowUserToAddRows = false;
            this.tsDataGridView1.AllowUserToDeleteRows = false;
            this.tsDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tsDataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tsDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.tsDataGridView1.Name = "tsDataGridView1";
            this.tsDataGridView1.RowTemplate.Height = 23;
            this.tsDataGridView1.Size = new System.Drawing.Size(804, 337);
            this.tsDataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(349, 358);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TSDGVSortableBindingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 393);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tsDataGridView1);
            this.Name = "TSDGVSortableBindingForm";
            this.Text = "TSDGVSortableBindingForm";
            ((System.ComponentModel.ISupportInitialize)(this.tsDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.GridView.TSDataGridView tsDataGridView1;
        private System.Windows.Forms.Button button1;
    }
}