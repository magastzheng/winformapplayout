namespace TradingSystem.View
{
    partial class ClosePositionForm
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
            this.tsChildMain = new System.Windows.Forms.ToolStrip();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mainSplitter = new System.Windows.Forms.SplitContainer();
            this.closeGridView = new Controls.GridView.TSDataGridView();
            this.closeBottomPanel = new System.Windows.Forms.Panel();
            this.securityGridView = new Controls.GridView.TSDataGridView();
            this.panelCmdBotton = new System.Windows.Forms.Panel();
            this.nudCopies = new System.Windows.Forms.NumericUpDown();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnChgPosition = new System.Windows.Forms.Button();
            this.btnCloseAll = new System.Windows.Forms.Button();
            this.btnCalc = new System.Windows.Forms.Button();
            this.lblCopies = new System.Windows.Forms.Label();
            this.cmdGridView = new Controls.GridView.TSDataGridView();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tsChildMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitter)).BeginInit();
            this.mainSplitter.Panel1.SuspendLayout();
            this.mainSplitter.Panel2.SuspendLayout();
            this.mainSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityGridView)).BeginInit();
            this.panelCmdBotton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCopies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.tsChildMain);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.cmdGridView);
            this.panelBottom.Controls.Add(this.panelCmdBotton);
            this.panelBottom.Location = new System.Drawing.Point(0, 378);
            this.panelBottom.Size = new System.Drawing.Size(1125, 192);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.mainSplitter);
            this.panelMain.Size = new System.Drawing.Size(1125, 346);
            // 
            // tsChildMain
            // 
            this.tsChildMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsChildMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.tsSeparator1});
            this.tsChildMain.Location = new System.Drawing.Point(0, 0);
            this.tsChildMain.Name = "tsChildMain";
            this.tsChildMain.Size = new System.Drawing.Size(1125, 32);
            this.tsChildMain.TabIndex = 0;
            this.tsChildMain.Text = "toolStrip1";
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::TradingSystem.Properties.Resources.refresh2;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(52, 29);
            this.tsbRefresh.Text = "刷新";
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(6, 32);
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
            this.mainSplitter.Panel1.Controls.Add(this.closeGridView);
            this.mainSplitter.Panel1.Controls.Add(this.closeBottomPanel);
            // 
            // mainSplitter.Panel2
            // 
            this.mainSplitter.Panel2.Controls.Add(this.securityGridView);
            this.mainSplitter.Size = new System.Drawing.Size(1125, 346);
            this.mainSplitter.SplitterDistance = 166;
            this.mainSplitter.TabIndex = 0;
            // 
            // closeGridView
            // 
            this.closeGridView.AllowUserToAddRows = false;
            this.closeGridView.AllowUserToDeleteRows = false;
            this.closeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.closeGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closeGridView.Location = new System.Drawing.Point(0, 0);
            this.closeGridView.Name = "closeGridView";
            this.closeGridView.RowTemplate.Height = 23;
            this.closeGridView.Size = new System.Drawing.Size(1125, 135);
            this.closeGridView.TabIndex = 1;
            // 
            // closeBottomPanel
            // 
            this.closeBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.closeBottomPanel.Location = new System.Drawing.Point(0, 135);
            this.closeBottomPanel.Name = "closeBottomPanel";
            this.closeBottomPanel.Size = new System.Drawing.Size(1125, 31);
            this.closeBottomPanel.TabIndex = 0;
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
            this.securityGridView.Size = new System.Drawing.Size(1125, 176);
            this.securityGridView.TabIndex = 0;
            // 
            // panelCmdBotton
            // 
            this.panelCmdBotton.Controls.Add(this.nudCopies);
            this.panelCmdBotton.Controls.Add(this.btnSubmit);
            this.panelCmdBotton.Controls.Add(this.btnChgPosition);
            this.panelCmdBotton.Controls.Add(this.btnCloseAll);
            this.panelCmdBotton.Controls.Add(this.btnCalc);
            this.panelCmdBotton.Controls.Add(this.lblCopies);
            this.panelCmdBotton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelCmdBotton.Location = new System.Drawing.Point(0, 156);
            this.panelCmdBotton.Name = "panelCmdBotton";
            this.panelCmdBotton.Size = new System.Drawing.Size(1125, 36);
            this.panelCmdBotton.TabIndex = 0;
            // 
            // nudCopies
            // 
            this.nudCopies.Location = new System.Drawing.Point(77, 8);
            this.nudCopies.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCopies.Name = "nudCopies";
            this.nudCopies.Size = new System.Drawing.Size(61, 21);
            this.nudCopies.TabIndex = 6;
            this.nudCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(402, 8);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "下达指令";
            this.btnSubmit.UseVisualStyleBackColor = true;
            // 
            // btnChgPosition
            // 
            this.btnChgPosition.Location = new System.Drawing.Point(315, 8);
            this.btnChgPosition.Name = "btnChgPosition";
            this.btnChgPosition.Size = new System.Drawing.Size(78, 23);
            this.btnChgPosition.TabIndex = 4;
            this.btnChgPosition.Text = "换仓";
            this.btnChgPosition.UseVisualStyleBackColor = true;
            // 
            // btnCloseAll
            // 
            this.btnCloseAll.Location = new System.Drawing.Point(231, 8);
            this.btnCloseAll.Name = "btnCloseAll";
            this.btnCloseAll.Size = new System.Drawing.Size(78, 23);
            this.btnCloseAll.TabIndex = 3;
            this.btnCloseAll.Text = "全平";
            this.btnCloseAll.UseVisualStyleBackColor = true;
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(144, 8);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(81, 23);
            this.btnCalc.TabIndex = 2;
            this.btnCalc.Text = "计算";
            this.btnCalc.UseVisualStyleBackColor = true;
            // 
            // lblCopies
            // 
            this.lblCopies.AutoSize = true;
            this.lblCopies.Location = new System.Drawing.Point(12, 13);
            this.lblCopies.Name = "lblCopies";
            this.lblCopies.Size = new System.Drawing.Size(59, 12);
            this.lblCopies.TabIndex = 0;
            this.lblCopies.Text = "操作份数:";
            // 
            // cmdGridView
            // 
            this.cmdGridView.AllowUserToAddRows = false;
            this.cmdGridView.AllowUserToDeleteRows = false;
            this.cmdGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cmdGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdGridView.Location = new System.Drawing.Point(0, 0);
            this.cmdGridView.Name = "cmdGridView";
            this.cmdGridView.RowTemplate.Height = 23;
            this.cmdGridView.Size = new System.Drawing.Size(1125, 156);
            this.cmdGridView.TabIndex = 1;
            // 
            // ClosePositionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Name = "ClosePositionForm";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.tsChildMain.ResumeLayout(false);
            this.tsChildMain.PerformLayout();
            this.mainSplitter.Panel1.ResumeLayout(false);
            this.mainSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitter)).EndInit();
            this.mainSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.closeGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityGridView)).EndInit();
            this.panelCmdBotton.ResumeLayout(false);
            this.panelCmdBotton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCopies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsChildMain;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripSeparator tsSeparator1;
        private System.Windows.Forms.SplitContainer mainSplitter;
        private Controls.GridView.TSDataGridView closeGridView;
        private System.Windows.Forms.Panel closeBottomPanel;
        private Controls.GridView.TSDataGridView securityGridView;
        private Controls.GridView.TSDataGridView cmdGridView;
        private System.Windows.Forms.Panel panelCmdBotton;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnChgPosition;
        private System.Windows.Forms.Button btnCloseAll;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Label lblCopies;
        private System.Windows.Forms.NumericUpDown nudCopies;
    }
}
