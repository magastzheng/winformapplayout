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
            this.btnBottomContainer = new Controls.ButtonContainer.ButtonContainer();
            this.secuContextMenu = new Controls.ContextMenu.ContextMenu();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unSelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelSelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelStopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelLimitUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelLimitDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelBottom.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.childSplitter)).BeginInit();
            this.childSplitter.Panel1.SuspendLayout();
            this.childSplitter.Panel2.SuspendLayout();
            this.childSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monitorGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityGridView)).BeginInit();
            this.secuContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnBottomContainer);
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
            // btnBottomContainer
            // 
            this.btnBottomContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBottomContainer.Location = new System.Drawing.Point(0, 0);
            this.btnBottomContainer.Name = "btnBottomContainer";
            this.btnBottomContainer.Size = new System.Drawing.Size(804, 32);
            this.btnBottomContainer.TabIndex = 0;
            // 
            // secuContextMenu
            // 
            this.secuContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.unSelectToolStripMenuItem,
            this.cancelSelectToolStripMenuItem,
            this.cancelStopToolStripMenuItem,
            this.cancelLimitUpToolStripMenuItem,
            this.cancelLimitDownToolStripMenuItem});
            this.secuContextMenu.Name = "secuContextMenu";
            this.secuContextMenu.Size = new System.Drawing.Size(153, 158);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.selectAllToolStripMenuItem.Text = "全选";
            // 
            // unSelectToolStripMenuItem
            // 
            this.unSelectToolStripMenuItem.Name = "unSelectToolStripMenuItem";
            this.unSelectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.unSelectToolStripMenuItem.Text = "反选";
            // 
            // cancelSelectToolStripMenuItem
            // 
            this.cancelSelectToolStripMenuItem.Name = "cancelSelectToolStripMenuItem";
            this.cancelSelectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cancelSelectToolStripMenuItem.Text = "取消所有选中";
            // 
            // cancelStopToolStripMenuItem
            // 
            this.cancelStopToolStripMenuItem.Name = "cancelStopToolStripMenuItem";
            this.cancelStopToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cancelStopToolStripMenuItem.Text = "取消停牌股";
            // 
            // cancelLimitUpToolStripMenuItem
            // 
            this.cancelLimitUpToolStripMenuItem.Name = "cancelLimitUpToolStripMenuItem";
            this.cancelLimitUpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cancelLimitUpToolStripMenuItem.Text = "取消涨停股";
            // 
            // cancelLimitDownToolStripMenuItem
            // 
            this.cancelLimitDownToolStripMenuItem.Name = "cancelLimitDownToolStripMenuItem";
            this.cancelLimitDownToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cancelLimitDownToolStripMenuItem.Text = "取消跌停股";
            // 
            // OpenPositionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Name = "OpenPositionForm";
            this.Controls.SetChildIndex(this.panelTop, 0);
            this.Controls.SetChildIndex(this.panelBottom, 0);
            this.Controls.SetChildIndex(this.panelMain, 0);
            this.panelBottom.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.childSplitter.Panel1.ResumeLayout(false);
            this.childSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.childSplitter)).EndInit();
            this.childSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.monitorGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityGridView)).EndInit();
            this.secuContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer childSplitter;
        private Controls.GridView.TSDataGridView monitorGridView;
        private Controls.GridView.TSDataGridView securityGridView;
        private Controls.ButtonContainer.ButtonContainer btnBottomContainer;
        private Controls.ContextMenu.ContextMenu secuContextMenu;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unSelectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelSelectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelStopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelLimitUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelLimitDownToolStripMenuItem;

    }
}
