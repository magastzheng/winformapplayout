namespace TradingSystem.View
{
    partial class SpotTemplateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpotTemplateForm));
            this.mainSplitter = new System.Windows.Forms.SplitContainer();
            this.tempGridView = new Controls.GridView.TSDataGridView();
            this.secuGridView = new Controls.GridView.TSDataGridView();
            this.spotSecuBottomPanel = new System.Windows.Forms.Panel();
            this.tbSettingWeight = new System.Windows.Forms.TextBox();
            this.lblSettingWeight = new System.Windows.Forms.Label();
            this.tbTotalCap = new System.Windows.Forms.TextBox();
            this.lblTotalCap = new System.Windows.Forms.Label();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.spotSecuTopPanel = new System.Windows.Forms.Panel();
            this.lblSpotSecu = new System.Windows.Forms.Label();
            this.tsChildTop = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbModify = new System.Windows.Forms.ToolStripButton();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsChildBottom = new System.Windows.Forms.ToolStrip();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAddStock = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbModifyStock = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDeleteStock = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCalcAmount = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitter)).BeginInit();
            this.mainSplitter.Panel1.SuspendLayout();
            this.mainSplitter.Panel2.SuspendLayout();
            this.mainSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tempGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secuGridView)).BeginInit();
            this.spotSecuBottomPanel.SuspendLayout();
            this.spotSecuTopPanel.SuspendLayout();
            this.tsChildTop.SuspendLayout();
            this.tsChildBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.tsChildTop);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.tsChildBottom);
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
            this.mainSplitter.Panel2.Controls.Add(this.spotSecuBottomPanel);
            this.mainSplitter.Panel2.Controls.Add(this.spotSecuTopPanel);
            this.mainSplitter.Size = new System.Drawing.Size(1125, 506);
            this.mainSplitter.SplitterDistance = 189;
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
            this.tempGridView.Size = new System.Drawing.Size(1125, 189);
            this.tempGridView.TabIndex = 0;
            // 
            // secuGridView
            // 
            this.secuGridView.AllowUserToAddRows = false;
            this.secuGridView.AllowUserToDeleteRows = false;
            this.secuGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.secuGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secuGridView.Location = new System.Drawing.Point(0, 23);
            this.secuGridView.Name = "secuGridView";
            this.secuGridView.RowTemplate.Height = 23;
            this.secuGridView.Size = new System.Drawing.Size(1125, 265);
            this.secuGridView.TabIndex = 0;
            // 
            // spotSecuBottomPanel
            // 
            this.spotSecuBottomPanel.Controls.Add(this.tbSettingWeight);
            this.spotSecuBottomPanel.Controls.Add(this.lblSettingWeight);
            this.spotSecuBottomPanel.Controls.Add(this.tbTotalCap);
            this.spotSecuBottomPanel.Controls.Add(this.lblTotalCap);
            this.spotSecuBottomPanel.Controls.Add(this.tbNumber);
            this.spotSecuBottomPanel.Controls.Add(this.lblNumber);
            this.spotSecuBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spotSecuBottomPanel.Location = new System.Drawing.Point(0, 288);
            this.spotSecuBottomPanel.Name = "spotSecuBottomPanel";
            this.spotSecuBottomPanel.Size = new System.Drawing.Size(1125, 25);
            this.spotSecuBottomPanel.TabIndex = 2;
            // 
            // tbSettingWeight
            // 
            this.tbSettingWeight.Enabled = false;
            this.tbSettingWeight.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSettingWeight.Location = new System.Drawing.Point(486, 4);
            this.tbSettingWeight.Name = "tbSettingWeight";
            this.tbSettingWeight.Size = new System.Drawing.Size(65, 21);
            this.tbSettingWeight.TabIndex = 5;
            this.tbSettingWeight.Text = "0";
            // 
            // lblSettingWeight
            // 
            this.lblSettingWeight.AutoSize = true;
            this.lblSettingWeight.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSettingWeight.Location = new System.Drawing.Point(418, 7);
            this.lblSettingWeight.Name = "lblSettingWeight";
            this.lblSettingWeight.Size = new System.Drawing.Size(70, 12);
            this.lblSettingWeight.TabIndex = 4;
            this.lblSettingWeight.Text = "设置比例：";
            // 
            // tbTotalCap
            // 
            this.tbTotalCap.Enabled = false;
            this.tbTotalCap.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTotalCap.Location = new System.Drawing.Point(238, 4);
            this.tbTotalCap.Name = "tbTotalCap";
            this.tbTotalCap.Size = new System.Drawing.Size(142, 21);
            this.tbTotalCap.TabIndex = 3;
            this.tbTotalCap.Text = "0";
            // 
            // lblTotalCap
            // 
            this.lblTotalCap.AutoSize = true;
            this.lblTotalCap.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalCap.Location = new System.Drawing.Point(180, 8);
            this.lblTotalCap.Name = "lblTotalCap";
            this.lblTotalCap.Size = new System.Drawing.Size(57, 12);
            this.lblTotalCap.TabIndex = 2;
            this.lblTotalCap.Text = "总市值：";
            // 
            // tbNumber
            // 
            this.tbNumber.Enabled = false;
            this.tbNumber.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbNumber.Location = new System.Drawing.Point(71, 4);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(65, 21);
            this.tbNumber.TabIndex = 1;
            this.tbNumber.Text = "0";
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumber.Location = new System.Drawing.Point(27, 7);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(44, 12);
            this.lblNumber.TabIndex = 0;
            this.lblNumber.Text = "个数：";
            // 
            // spotSecuTopPanel
            // 
            this.spotSecuTopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spotSecuTopPanel.Controls.Add(this.lblSpotSecu);
            this.spotSecuTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.spotSecuTopPanel.Location = new System.Drawing.Point(0, 0);
            this.spotSecuTopPanel.Name = "spotSecuTopPanel";
            this.spotSecuTopPanel.Size = new System.Drawing.Size(1125, 23);
            this.spotSecuTopPanel.TabIndex = 0;
            // 
            // lblSpotSecu
            // 
            this.lblSpotSecu.AutoSize = true;
            this.lblSpotSecu.Location = new System.Drawing.Point(3, 3);
            this.lblSpotSecu.Name = "lblSpotSecu";
            this.lblSpotSecu.Size = new System.Drawing.Size(77, 12);
            this.lblSpotSecu.TabIndex = 0;
            this.lblSpotSecu.Text = "现货模板构建";
            // 
            // tsChildTop
            // 
            this.tsChildTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsChildTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbModify,
            this.tsbCopy,
            this.tsbDelete});
            this.tsChildTop.Location = new System.Drawing.Point(0, 0);
            this.tsChildTop.Name = "tsChildTop";
            this.tsChildTop.Size = new System.Drawing.Size(1125, 32);
            this.tsChildTop.TabIndex = 1;
            this.tsChildTop.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.Image = global::TradingSystem.Properties.Resources.add256;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(52, 29);
            this.tsbAdd.Text = "添加";
            // 
            // tsbModify
            // 
            this.tsbModify.Image = global::TradingSystem.Properties.Resources.edit;
            this.tsbModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModify.Name = "tsbModify";
            this.tsbModify.Size = new System.Drawing.Size(52, 29);
            this.tsbModify.Text = "修改";
            // 
            // tsbCopy
            // 
            this.tsbCopy.Image = global::TradingSystem.Properties.Resources.copy256;
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(52, 29);
            this.tsbCopy.Text = "复制";
            // 
            // tsbDelete
            // 
            this.tsbDelete.Image = global::TradingSystem.Properties.Resources.delete256;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(52, 29);
            this.tsbDelete.Text = "注销";
            // 
            // tsChildBottom
            // 
            this.tsChildBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsChildBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbImport,
            this.toolStripSeparator1,
            this.tsbAddStock,
            this.toolStripSeparator2,
            this.tsbModifyStock,
            this.toolStripSeparator3,
            this.tsbDeleteStock,
            this.toolStripSeparator4,
            this.tsbCalcAmount,
            this.toolStripSeparator5,
            this.tsbSave,
            this.toolStripSeparator6,
            this.tsbExport});
            this.tsChildBottom.Location = new System.Drawing.Point(0, 0);
            this.tsChildBottom.Name = "tsChildBottom";
            this.tsChildBottom.Size = new System.Drawing.Size(1125, 32);
            this.tsChildBottom.TabIndex = 1;
            this.tsChildBottom.Text = "toolStrip1";
            // 
            // tsbImport
            // 
            this.tsbImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbImport.Image")));
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(65, 29);
            this.tsbImport.Text = "Excel导入";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbAddStock
            // 
            this.tsbAddStock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAddStock.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddStock.Image")));
            this.tsbAddStock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddStock.Name = "tsbAddStock";
            this.tsbAddStock.Size = new System.Drawing.Size(36, 29);
            this.tsbAddStock.Text = "添加";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbModifyStock
            // 
            this.tsbModifyStock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbModifyStock.Image = ((System.Drawing.Image)(resources.GetObject("tsbModifyStock.Image")));
            this.tsbModifyStock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModifyStock.Name = "tsbModifyStock";
            this.tsbModifyStock.Size = new System.Drawing.Size(36, 29);
            this.tsbModifyStock.Text = "修改";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbDeleteStock
            // 
            this.tsbDeleteStock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDeleteStock.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeleteStock.Image")));
            this.tsbDeleteStock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteStock.Name = "tsbDeleteStock";
            this.tsbDeleteStock.Size = new System.Drawing.Size(36, 29);
            this.tsbDeleteStock.Text = "删除";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbCalcAmount
            // 
            this.tsbCalcAmount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCalcAmount.Image = ((System.Drawing.Image)(resources.GetObject("tsbCalcAmount.Image")));
            this.tsbCalcAmount.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCalcAmount.Name = "tsbCalcAmount";
            this.tsbCalcAmount.Size = new System.Drawing.Size(60, 29);
            this.tsbCalcAmount.Text = "重算数量";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(36, 29);
            this.tsbSave.Text = "保存";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbExport
            // 
            this.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(36, 29);
            this.tsbExport.Text = "导出";
            // 
            // SpotTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Name = "SpotTemplateForm";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.mainSplitter.Panel1.ResumeLayout(false);
            this.mainSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitter)).EndInit();
            this.mainSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tempGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secuGridView)).EndInit();
            this.spotSecuBottomPanel.ResumeLayout(false);
            this.spotSecuBottomPanel.PerformLayout();
            this.spotSecuTopPanel.ResumeLayout(false);
            this.spotSecuTopPanel.PerformLayout();
            this.tsChildTop.ResumeLayout(false);
            this.tsChildTop.PerformLayout();
            this.tsChildBottom.ResumeLayout(false);
            this.tsChildBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitter;
        private System.Windows.Forms.Panel spotSecuTopPanel;
        private System.Windows.Forms.Label lblSpotSecu;
        private System.Windows.Forms.ToolStrip tsChildTop;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbModify;
        private System.Windows.Forms.ToolStripButton tsbCopy;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStrip tsChildBottom;
        private System.Windows.Forms.ToolStripButton tsbImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbAddStock;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbModifyStock;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbDeleteStock;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbCalcAmount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private Controls.GridView.TSDataGridView tempGridView;
        private System.Windows.Forms.Panel spotSecuBottomPanel;
        private System.Windows.Forms.TextBox tbSettingWeight;
        private System.Windows.Forms.Label lblSettingWeight;
        private System.Windows.Forms.TextBox tbTotalCap;
        private System.Windows.Forms.Label lblTotalCap;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Label lblNumber;
        private Controls.GridView.TSDataGridView secuGridView;
    }
}
