using System;
using System.Drawing;
namespace TradingSystem.View
{
    partial class TemplateDialog
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTemplateNo = new System.Windows.Forms.Label();
            this.tbTemplateNo = new System.Windows.Forms.TextBox();
            this.tbTemplateName = new System.Windows.Forms.TextBox();
            this.lblTemplateName = new System.Windows.Forms.Label();
            this.tbFutureCopies = new System.Windows.Forms.TextBox();
            this.lblFutureCopies = new System.Windows.Forms.Label();
            this.tbMarketCapOpt = new System.Windows.Forms.TextBox();
            this.lblMarketCapOpt = new System.Windows.Forms.Label();
            this.lblBenchmark = new System.Windows.Forms.Label();
            this.cbBenchmark = new System.Windows.Forms.ComboBox();
            this.cbWeightType = new System.Windows.Forms.ComboBox();
            this.lblWeightType = new System.Windows.Forms.Label();
            this.cbReplaceType = new System.Windows.Forms.ComboBox();
            this.lblReplaceType = new System.Windows.Forms.Label();
            this.cbReplaceTemplate = new System.Windows.Forms.ComboBox();
            this.lblReplaceTemplate = new System.Windows.Forms.Label();
            this.cbViewUser = new System.Windows.Forms.ComboBox();
            this.lblViewUser = new System.Windows.Forms.Label();
            this.cbEditUser = new System.Windows.Forms.ComboBox();
            this.lblEditUser = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::TradingSystem.Properties.Resources.addnew;
            this.pictureBox.Location = new System.Drawing.Point(22, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(72, 59);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(100, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(109, 12);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "设置组合模板信息";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(100, 59);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(137, 12);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "设置新增的现货组合模板";
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.pictureBox);
            this.panelTop.Controls.Add(this.lblDescription);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(341, 80);
            this.panelTop.TabIndex = 3;
            // 
            // lblTemplateNo
            // 
            this.lblTemplateNo.AutoSize = true;
            this.lblTemplateNo.Location = new System.Drawing.Point(42, 90);
            this.lblTemplateNo.Name = "lblTemplateNo";
            this.lblTemplateNo.Size = new System.Drawing.Size(53, 12);
            this.lblTemplateNo.TabIndex = 4;
            this.lblTemplateNo.Text = "模板序号";
            // 
            // tbTemplateNo
            // 
            this.tbTemplateNo.Location = new System.Drawing.Point(117, 87);
            this.tbTemplateNo.Name = "tbTemplateNo";
            this.tbTemplateNo.Size = new System.Drawing.Size(190, 21);
            this.tbTemplateNo.TabIndex = 5;
            // 
            // tbTemplateName
            // 
            this.tbTemplateName.Location = new System.Drawing.Point(117, 114);
            this.tbTemplateName.Name = "tbTemplateName";
            this.tbTemplateName.Size = new System.Drawing.Size(190, 21);
            this.tbTemplateName.TabIndex = 7;
            // 
            // lblTemplateName
            // 
            this.lblTemplateName.AutoSize = true;
            this.lblTemplateName.Location = new System.Drawing.Point(41, 117);
            this.lblTemplateName.Name = "lblTemplateName";
            this.lblTemplateName.Size = new System.Drawing.Size(53, 12);
            this.lblTemplateName.TabIndex = 6;
            this.lblTemplateName.Text = "模板名称";
            // 
            // tbFutureCopies
            // 
            this.tbFutureCopies.Location = new System.Drawing.Point(117, 141);
            this.tbFutureCopies.Name = "tbFutureCopies";
            this.tbFutureCopies.Size = new System.Drawing.Size(190, 21);
            this.tbFutureCopies.TabIndex = 9;
            // 
            // lblFutureCopies
            // 
            this.lblFutureCopies.AutoSize = true;
            this.lblFutureCopies.Location = new System.Drawing.Point(41, 144);
            this.lblFutureCopies.Name = "lblFutureCopies";
            this.lblFutureCopies.Size = new System.Drawing.Size(53, 12);
            this.lblFutureCopies.TabIndex = 8;
            this.lblFutureCopies.Text = "期货张数";
            // 
            // tbMarketCapOpt
            // 
            this.tbMarketCapOpt.Location = new System.Drawing.Point(117, 168);
            this.tbMarketCapOpt.Name = "tbMarketCapOpt";
            this.tbMarketCapOpt.Size = new System.Drawing.Size(190, 21);
            this.tbMarketCapOpt.TabIndex = 11;
            // 
            // lblMarketCapOpt
            // 
            this.lblMarketCapOpt.AutoSize = true;
            this.lblMarketCapOpt.Location = new System.Drawing.Point(41, 171);
            this.lblMarketCapOpt.Name = "lblMarketCapOpt";
            this.lblMarketCapOpt.Size = new System.Drawing.Size(53, 12);
            this.lblMarketCapOpt.TabIndex = 10;
            this.lblMarketCapOpt.Text = "市值比例";
            // 
            // lblBenchmark
            // 
            this.lblBenchmark.AutoSize = true;
            this.lblBenchmark.Location = new System.Drawing.Point(41, 200);
            this.lblBenchmark.Name = "lblBenchmark";
            this.lblBenchmark.Size = new System.Drawing.Size(53, 12);
            this.lblBenchmark.TabIndex = 12;
            this.lblBenchmark.Text = "标的指数";
            // 
            // cbBenchmark
            // 
            this.cbBenchmark.FormattingEnabled = true;
            this.cbBenchmark.Location = new System.Drawing.Point(117, 198);
            this.cbBenchmark.Name = "cbBenchmark";
            this.cbBenchmark.Size = new System.Drawing.Size(190, 20);
            this.cbBenchmark.TabIndex = 13;
            // 
            // cbWeightType
            // 
            this.cbWeightType.FormattingEnabled = true;
            this.cbWeightType.Location = new System.Drawing.Point(117, 224);
            this.cbWeightType.Name = "cbWeightType";
            this.cbWeightType.Size = new System.Drawing.Size(190, 20);
            this.cbWeightType.TabIndex = 15;
            // 
            // lblWeightType
            // 
            this.lblWeightType.AutoSize = true;
            this.lblWeightType.Location = new System.Drawing.Point(41, 226);
            this.lblWeightType.Name = "lblWeightType";
            this.lblWeightType.Size = new System.Drawing.Size(53, 12);
            this.lblWeightType.TabIndex = 14;
            this.lblWeightType.Text = "权重类别";
            // 
            // cbReplaceType
            // 
            this.cbReplaceType.FormattingEnabled = true;
            this.cbReplaceType.Location = new System.Drawing.Point(117, 250);
            this.cbReplaceType.Name = "cbReplaceType";
            this.cbReplaceType.Size = new System.Drawing.Size(190, 20);
            this.cbReplaceType.TabIndex = 17;
            // 
            // lblReplaceType
            // 
            this.lblReplaceType.AutoSize = true;
            this.lblReplaceType.Location = new System.Drawing.Point(41, 252);
            this.lblReplaceType.Name = "lblReplaceType";
            this.lblReplaceType.Size = new System.Drawing.Size(53, 12);
            this.lblReplaceType.TabIndex = 16;
            this.lblReplaceType.Text = "替代类型";
            // 
            // cbReplaceTemplate
            // 
            this.cbReplaceTemplate.FormattingEnabled = true;
            this.cbReplaceTemplate.Location = new System.Drawing.Point(117, 276);
            this.cbReplaceTemplate.Name = "cbReplaceTemplate";
            this.cbReplaceTemplate.Size = new System.Drawing.Size(190, 20);
            this.cbReplaceTemplate.TabIndex = 19;
            // 
            // lblReplaceTemplate
            // 
            this.lblReplaceTemplate.AutoSize = true;
            this.lblReplaceTemplate.Location = new System.Drawing.Point(41, 278);
            this.lblReplaceTemplate.Name = "lblReplaceTemplate";
            this.lblReplaceTemplate.Size = new System.Drawing.Size(53, 12);
            this.lblReplaceTemplate.TabIndex = 18;
            this.lblReplaceTemplate.Text = "替代模板";
            this.lblReplaceTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbViewUser
            // 
            this.cbViewUser.FormattingEnabled = true;
            this.cbViewUser.Location = new System.Drawing.Point(117, 302);
            this.cbViewUser.Name = "cbViewUser";
            this.cbViewUser.Size = new System.Drawing.Size(190, 20);
            this.cbViewUser.TabIndex = 21;
            // 
            // lblViewUser
            // 
            this.lblViewUser.AutoSize = true;
            this.lblViewUser.Location = new System.Drawing.Point(30, 304);
            this.lblViewUser.Name = "lblViewUser";
            this.lblViewUser.Size = new System.Drawing.Size(65, 12);
            this.lblViewUser.TabIndex = 20;
            this.lblViewUser.Text = "可浏览用户";
            this.lblViewUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbEditUser
            // 
            this.cbEditUser.FormattingEnabled = true;
            this.cbEditUser.Location = new System.Drawing.Point(117, 328);
            this.cbEditUser.Name = "cbEditUser";
            this.cbEditUser.Size = new System.Drawing.Size(190, 20);
            this.cbEditUser.TabIndex = 23;
            // 
            // lblEditUser
            // 
            this.lblEditUser.AutoSize = true;
            this.lblEditUser.Location = new System.Drawing.Point(30, 330);
            this.lblEditUser.Name = "lblEditUser";
            this.lblEditUser.Size = new System.Drawing.Size(65, 12);
            this.lblEditUser.TabIndex = 22;
            this.lblEditUser.Text = "可编辑用户";
            this.lblEditUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::TradingSystem.Properties.Resources.save;
            this.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirm.Location = new System.Drawing.Point(84, 370);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 24;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfirm.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(178, 370);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // TemplateDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(341, 405);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.cbEditUser);
            this.Controls.Add(this.lblEditUser);
            this.Controls.Add(this.cbViewUser);
            this.Controls.Add(this.lblViewUser);
            this.Controls.Add(this.cbReplaceTemplate);
            this.Controls.Add(this.lblReplaceTemplate);
            this.Controls.Add(this.cbReplaceType);
            this.Controls.Add(this.lblReplaceType);
            this.Controls.Add(this.cbWeightType);
            this.Controls.Add(this.lblWeightType);
            this.Controls.Add(this.cbBenchmark);
            this.Controls.Add(this.lblBenchmark);
            this.Controls.Add(this.tbMarketCapOpt);
            this.Controls.Add(this.lblMarketCapOpt);
            this.Controls.Add(this.tbFutureCopies);
            this.Controls.Add(this.lblFutureCopies);
            this.Controls.Add(this.tbTemplateName);
            this.Controls.Add(this.lblTemplateName);
            this.Controls.Add(this.tbTemplateNo);
            this.Controls.Add(this.lblTemplateNo);
            this.Controls.Add(this.panelTop);
            this.Name = "TemplateDialog";
            this.Load += new EventHandler(Form_Load);
            this.LoadFormActived += new FormActiveHandler(Form_LoadActived);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTemplateNo;
        private System.Windows.Forms.TextBox tbTemplateNo;
        private System.Windows.Forms.TextBox tbTemplateName;
        private System.Windows.Forms.Label lblTemplateName;
        private System.Windows.Forms.TextBox tbFutureCopies;
        private System.Windows.Forms.Label lblFutureCopies;
        private System.Windows.Forms.TextBox tbMarketCapOpt;
        private System.Windows.Forms.Label lblMarketCapOpt;
        private System.Windows.Forms.Label lblBenchmark;
        private System.Windows.Forms.ComboBox cbBenchmark;
        private System.Windows.Forms.ComboBox cbWeightType;
        private System.Windows.Forms.Label lblWeightType;
        private System.Windows.Forms.ComboBox cbReplaceType;
        private System.Windows.Forms.Label lblReplaceType;
        private System.Windows.Forms.ComboBox cbReplaceTemplate;
        private System.Windows.Forms.Label lblReplaceTemplate;
        private System.Windows.Forms.ComboBox cbViewUser;
        private System.Windows.Forms.Label lblViewUser;
        private System.Windows.Forms.ComboBox cbEditUser;
        private System.Windows.Forms.Label lblEditUser;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}
