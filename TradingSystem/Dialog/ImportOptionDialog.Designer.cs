namespace TradingSystem.Dialog
{
    partial class ImportOptionDialog
    {
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbImportType = new System.Windows.Forms.GroupBox();
            this.rbAppend = new System.Windows.Forms.RadioButton();
            this.rbReplace = new System.Windows.Forms.RadioButton();
            this.gbFormatDesc = new System.Windows.Forms.GroupBox();
            this.rtbImportDesc = new System.Windows.Forms.RichTextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbImportType.SuspendLayout();
            this.gbFormatDesc.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbImportType
            // 
            this.gbImportType.Controls.Add(this.rbAppend);
            this.gbImportType.Controls.Add(this.rbReplace);
            this.gbImportType.Location = new System.Drawing.Point(2, 27);
            this.gbImportType.Name = "gbImportType";
            this.gbImportType.Size = new System.Drawing.Size(111, 93);
            this.gbImportType.TabIndex = 0;
            this.gbImportType.TabStop = false;
            this.gbImportType.Text = "导入方式";
            // 
            // rbAppend
            // 
            this.rbAppend.AutoSize = true;
            this.rbAppend.Location = new System.Drawing.Point(10, 54);
            this.rbAppend.Name = "rbAppend";
            this.rbAppend.Size = new System.Drawing.Size(47, 16);
            this.rbAppend.TabIndex = 1;
            this.rbAppend.TabStop = true;
            this.rbAppend.Text = "追加";
            this.rbAppend.UseVisualStyleBackColor = true;
            this.rbAppend.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbReplace
            // 
            this.rbReplace.AutoSize = true;
            this.rbReplace.Location = new System.Drawing.Point(10, 20);
            this.rbReplace.Name = "rbReplace";
            this.rbReplace.Size = new System.Drawing.Size(47, 16);
            this.rbReplace.TabIndex = 0;
            this.rbReplace.TabStop = true;
            this.rbReplace.Text = "替代";
            this.rbReplace.UseVisualStyleBackColor = true;
            this.rbReplace.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // gbFormatDesc
            // 
            this.gbFormatDesc.Controls.Add(this.rtbImportDesc);
            this.gbFormatDesc.Location = new System.Drawing.Point(119, 27);
            this.gbFormatDesc.Name = "gbFormatDesc";
            this.gbFormatDesc.Size = new System.Drawing.Size(372, 415);
            this.gbFormatDesc.TabIndex = 1;
            this.gbFormatDesc.TabStop = false;
            this.gbFormatDesc.Text = "格式说明";
            // 
            // rtbImportDesc
            // 
            this.rtbImportDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbImportDesc.Location = new System.Drawing.Point(3, 17);
            this.rtbImportDesc.Name = "rtbImportDesc";
            this.rtbImportDesc.ReadOnly = true;
            this.rtbImportDesc.Size = new System.Drawing.Size(366, 395);
            this.rtbImportDesc.TabIndex = 0;
            this.rtbImportDesc.Text = "";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(177, 457);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.Button_Confirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(299, 457);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // ImportOptionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(503, 492);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.gbFormatDesc);
            this.Controls.Add(this.gbImportType);
            this.Name = "ImportOptionDialog";
            this.gbImportType.ResumeLayout(false);
            this.gbImportType.PerformLayout();
            this.gbFormatDesc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbImportType;
        private System.Windows.Forms.RadioButton rbAppend;
        private System.Windows.Forms.RadioButton rbReplace;
        private System.Windows.Forms.GroupBox gbFormatDesc;
        private System.Windows.Forms.RichTextBox rtbImportDesc;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}
