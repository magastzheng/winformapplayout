namespace TradingSystem.View
{
    partial class StrategyTradingForm_old
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
            this.tsChildTop = new System.Windows.Forms.ToolStrip();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbCancelAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbCanelAppend = new System.Windows.Forms.ToolStripButton();
            this.bottomContainer = new Controls.ButtonContainer.ButtonContainer();
            this.lrSplitter = new System.Windows.Forms.SplitContainer();
            this.tbSplitter = new System.Windows.Forms.SplitContainer();
            this.cmdGridView = new Controls.GridView.TSDataGridView();
            this.cmdButtonContainer = new Controls.ButtonContainer.ButtonContainer();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tsChildTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lrSplitter)).BeginInit();
            this.lrSplitter.Panel1.SuspendLayout();
            this.lrSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSplitter)).BeginInit();
            this.tbSplitter.Panel1.SuspendLayout();
            this.tbSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmdGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.tsChildTop);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.bottomContainer);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lrSplitter);
            // 
            // tsChildTop
            // 
            this.tsChildTop.Dock = System.Windows.Forms.DockStyle.None;
            this.tsChildTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.tsSeparator1,
            this.tsbUndo,
            this.tsbCancelAdd,
            this.tsbCanelAppend});
            this.tsChildTop.Location = new System.Drawing.Point(0, 4);
            this.tsChildTop.Name = "tsChildTop";
            this.tsChildTop.Size = new System.Drawing.Size(281, 25);
            this.tsChildTop.TabIndex = 0;
            this.tsChildTop.Text = "toolStrip1";
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::TradingSystem.Properties.Resources.refresh2;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(52, 22);
            this.tsbRefresh.Text = "刷新";
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbUndo
            // 
            this.tsbUndo.Image = global::TradingSystem.Properties.Resources.undo;
            this.tsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Size = new System.Drawing.Size(52, 22);
            this.tsbUndo.Text = "撤单";
            // 
            // tsbCancelAdd
            // 
            this.tsbCancelAdd.Image = global::TradingSystem.Properties.Resources.canceladd;
            this.tsbCancelAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancelAdd.Name = "tsbCancelAdd";
            this.tsbCancelAdd.Size = new System.Drawing.Size(52, 22);
            this.tsbCancelAdd.Text = "撤补";
            // 
            // tsbCanelAppend
            // 
            this.tsbCanelAppend.Image = global::TradingSystem.Properties.Resources.cancelredo;
            this.tsbCanelAppend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCanelAppend.Name = "tsbCanelAppend";
            this.tsbCanelAppend.Size = new System.Drawing.Size(76, 22);
            this.tsbCanelAppend.Text = "撤销追加";
            // 
            // bottomContainer
            // 
            this.bottomContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.bottomContainer.Location = new System.Drawing.Point(0, 0);
            this.bottomContainer.Name = "bottomContainer";
            this.bottomContainer.Size = new System.Drawing.Size(804, 32);
            this.bottomContainer.TabIndex = 0;
            // 
            // lrSplitter
            // 
            this.lrSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lrSplitter.Location = new System.Drawing.Point(0, 0);
            this.lrSplitter.Name = "lrSplitter";
            // 
            // lrSplitter.Panel1
            // 
            this.lrSplitter.Panel1.Controls.Add(this.tbSplitter);
            this.lrSplitter.Size = new System.Drawing.Size(1125, 506);
            this.lrSplitter.SplitterDistance = 936;
            this.lrSplitter.TabIndex = 0;
            // 
            // tbSplitter
            // 
            this.tbSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSplitter.Location = new System.Drawing.Point(0, 0);
            this.tbSplitter.Name = "tbSplitter";
            this.tbSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // tbSplitter.Panel1
            // 
            this.tbSplitter.Panel1.Controls.Add(this.cmdGridView);
            this.tbSplitter.Panel1.Controls.Add(this.cmdButtonContainer);
            this.tbSplitter.Size = new System.Drawing.Size(936, 506);
            this.tbSplitter.SplitterDistance = 155;
            this.tbSplitter.TabIndex = 0;
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
            this.cmdGridView.Size = new System.Drawing.Size(936, 132);
            this.cmdGridView.TabIndex = 1;
            // 
            // cmdButtonContainer
            // 
            this.cmdButtonContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmdButtonContainer.Location = new System.Drawing.Point(0, 132);
            this.cmdButtonContainer.Name = "cmdButtonContainer";
            this.cmdButtonContainer.Size = new System.Drawing.Size(936, 23);
            this.cmdButtonContainer.TabIndex = 0;
            // 
            // StrategyTradingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1125, 570);
            this.Name = "StrategyTradingForm";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.tsChildTop.ResumeLayout(false);
            this.tsChildTop.PerformLayout();
            this.lrSplitter.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lrSplitter)).EndInit();
            this.lrSplitter.ResumeLayout(false);
            this.tbSplitter.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbSplitter)).EndInit();
            this.tbSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmdGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsChildTop;
        private Controls.ButtonContainer.ButtonContainer bottomContainer;
        private System.Windows.Forms.SplitContainer lrSplitter;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripSeparator tsSeparator1;
        private System.Windows.Forms.ToolStripButton tsbUndo;
        private System.Windows.Forms.ToolStripButton tsbCancelAdd;
        private System.Windows.Forms.ToolStripButton tsbCanelAppend;
        private System.Windows.Forms.SplitContainer tbSplitter;
        private Controls.GridView.TSDataGridView cmdGridView;
        private Controls.ButtonContainer.ButtonContainer cmdButtonContainer;
    }
}
