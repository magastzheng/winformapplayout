namespace TradingSystem.View
{
    partial class StockTemplateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockTemplateForm));
            this.panelChildTop = new System.Windows.Forms.Panel();
            this.toolStripChildTop = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbModify = new System.Windows.Forms.ToolStripButton();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.panelChildBottom = new System.Windows.Forms.Panel();
            this.toolStripChildBottom = new System.Windows.Forms.ToolStrip();
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
            this.splitContainerChild = new System.Windows.Forms.SplitContainer();
            this.panelChildTop.SuspendLayout();
            this.toolStripChildTop.SuspendLayout();
            this.panelChildBottom.SuspendLayout();
            this.toolStripChildBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerChild)).BeginInit();
            this.splitContainerChild.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelChildTop
            // 
            this.panelChildTop.Controls.Add(this.toolStripChildTop);
            this.panelChildTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelChildTop.Location = new System.Drawing.Point(0, 0);
            this.panelChildTop.Name = "panelChildTop";
            this.panelChildTop.Size = new System.Drawing.Size(1094, 38);
            this.panelChildTop.TabIndex = 0;
            // 
            // toolStripChildTop
            // 
            this.toolStripChildTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripChildTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbModify,
            this.tsbCopy,
            this.tsbDelete});
            this.toolStripChildTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripChildTop.Name = "toolStripChildTop";
            this.toolStripChildTop.Size = new System.Drawing.Size(1094, 38);
            this.toolStripChildTop.TabIndex = 0;
            this.toolStripChildTop.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(52, 35);
            this.tsbAdd.Text = "添加";
            this.tsbAdd.Click += new System.EventHandler(this.Button_Add_Click);
            // 
            // tsbModify
            // 
            this.tsbModify.Image = ((System.Drawing.Image)(resources.GetObject("tsbModify.Image")));
            this.tsbModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModify.Name = "tsbModify";
            this.tsbModify.Size = new System.Drawing.Size(52, 35);
            this.tsbModify.Text = "修改";
            // 
            // tsbCopy
            // 
            this.tsbCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopy.Image")));
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(52, 35);
            this.tsbCopy.Text = "复制";
            // 
            // tsbDelete
            // 
            this.tsbDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelete.Image")));
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(52, 35);
            this.tsbDelete.Text = "注销";
            // 
            // panelChildBottom
            // 
            this.panelChildBottom.Controls.Add(this.toolStripChildBottom);
            this.panelChildBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelChildBottom.Location = new System.Drawing.Point(0, 327);
            this.panelChildBottom.Name = "panelChildBottom";
            this.panelChildBottom.Size = new System.Drawing.Size(1094, 35);
            this.panelChildBottom.TabIndex = 1;
            // 
            // toolStripChildBottom
            // 
            this.toolStripChildBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripChildBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.toolStripChildBottom.Location = new System.Drawing.Point(0, 0);
            this.toolStripChildBottom.Name = "toolStripChildBottom";
            this.toolStripChildBottom.Size = new System.Drawing.Size(1094, 35);
            this.toolStripChildBottom.TabIndex = 0;
            this.toolStripChildBottom.Text = "toolStrip1";
            // 
            // tsbImport
            // 
            this.tsbImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbImport.Image")));
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(65, 32);
            this.tsbImport.Text = "Excel导入";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // tsbAddStock
            // 
            this.tsbAddStock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAddStock.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddStock.Image")));
            this.tsbAddStock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddStock.Name = "tsbAddStock";
            this.tsbAddStock.Size = new System.Drawing.Size(36, 32);
            this.tsbAddStock.Text = "添加";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
            // 
            // tsbModifyStock
            // 
            this.tsbModifyStock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbModifyStock.Image = ((System.Drawing.Image)(resources.GetObject("tsbModifyStock.Image")));
            this.tsbModifyStock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModifyStock.Name = "tsbModifyStock";
            this.tsbModifyStock.Size = new System.Drawing.Size(36, 32);
            this.tsbModifyStock.Text = "修改";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 35);
            // 
            // tsbDeleteStock
            // 
            this.tsbDeleteStock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDeleteStock.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeleteStock.Image")));
            this.tsbDeleteStock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteStock.Name = "tsbDeleteStock";
            this.tsbDeleteStock.Size = new System.Drawing.Size(36, 32);
            this.tsbDeleteStock.Text = "删除";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 35);
            // 
            // tsbCalcAmount
            // 
            this.tsbCalcAmount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCalcAmount.Image = ((System.Drawing.Image)(resources.GetObject("tsbCalcAmount.Image")));
            this.tsbCalcAmount.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCalcAmount.Name = "tsbCalcAmount";
            this.tsbCalcAmount.Size = new System.Drawing.Size(60, 32);
            this.tsbCalcAmount.Text = "重算数量";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 35);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(36, 32);
            this.tsbSave.Text = "保存";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 35);
            // 
            // tsbExport
            // 
            this.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(36, 32);
            this.tsbExport.Text = "导出";
            // 
            // splitContainerChild
            // 
            this.splitContainerChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerChild.Location = new System.Drawing.Point(0, 38);
            this.splitContainerChild.Name = "splitContainerChild";
            this.splitContainerChild.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerChild.Panel2
            // 
            this.splitContainerChild.Panel2.BackColor = System.Drawing.Color.LightGray;
            this.splitContainerChild.Size = new System.Drawing.Size(1094, 289);
            this.splitContainerChild.SplitterDistance = 150;
            this.splitContainerChild.TabIndex = 2;
            // 
            // StockTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1094, 362);
            this.Controls.Add(this.splitContainerChild);
            this.Controls.Add(this.panelChildBottom);
            this.Controls.Add(this.panelChildTop);
            this.Name = "StockTemplateForm";
            this.Load += new System.EventHandler(this.Form_Load);
            this.panelChildTop.ResumeLayout(false);
            this.panelChildTop.PerformLayout();
            this.toolStripChildTop.ResumeLayout(false);
            this.toolStripChildTop.PerformLayout();
            this.panelChildBottom.ResumeLayout(false);
            this.panelChildBottom.PerformLayout();
            this.toolStripChildBottom.ResumeLayout(false);
            this.toolStripChildBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerChild)).EndInit();
            this.splitContainerChild.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelChildTop;
        private System.Windows.Forms.Panel panelChildBottom;
        private System.Windows.Forms.SplitContainer splitContainerChild;
        private System.Windows.Forms.ToolStrip toolStripChildTop;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbModify;
        private System.Windows.Forms.ToolStripButton tsbCopy;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStrip toolStripChildBottom;
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
    }
}
