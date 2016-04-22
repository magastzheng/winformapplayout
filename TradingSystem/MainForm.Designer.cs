namespace TradingSystem
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelTopContainer = new System.Windows.Forms.Panel();
            this.panelBottomContainer = new System.Windows.Forms.Panel();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelMainCaption = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.systemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbOpen = new System.Windows.Forms.ToolStripButton();
            this.tbSave = new System.Windows.Forms.ToolStripButton();
            this.tbRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.panelTopContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTopContainer
            // 
            this.panelTopContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTopContainer.Controls.Add(this.toolStrip1);
            this.panelTopContainer.Controls.Add(this.menuStripMain);
            this.panelTopContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopContainer.Location = new System.Drawing.Point(0, 0);
            this.panelTopContainer.Name = "panelTopContainer";
            this.panelTopContainer.Size = new System.Drawing.Size(897, 50);
            this.panelTopContainer.TabIndex = 0;
            // 
            // panelBottomContainer
            // 
            this.panelBottomContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelBottomContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomContainer.Location = new System.Drawing.Point(0, 508);
            this.panelBottomContainer.Name = "panelBottomContainer";
            this.panelBottomContainer.Size = new System.Drawing.Size(897, 35);
            this.panelBottomContainer.TabIndex = 1;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 50);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelMain);
            this.splitContainerMain.Panel2.Controls.Add(this.panelMainCaption);
            this.splitContainerMain.Size = new System.Drawing.Size(897, 458);
            this.splitContainerMain.SplitterDistance = 180;
            this.splitContainerMain.TabIndex = 2;
            // 
            // panelMainCaption
            // 
            this.panelMainCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMainCaption.Location = new System.Drawing.Point(0, 0);
            this.panelMainCaption.Name = "panelMainCaption";
            this.panelMainCaption.Size = new System.Drawing.Size(713, 10);
            this.panelMainCaption.TabIndex = 0;
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 10);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(713, 448);
            this.panelMain.TabIndex = 1;
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemMenuItem,
            this.viewMenuItem,
            this.toolMenuItem,
            this.helpMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(893, 25);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // systemMenuItem
            // 
            this.systemMenuItem.Name = "systemMenuItem";
            this.systemMenuItem.Size = new System.Drawing.Size(44, 21);
            this.systemMenuItem.Text = "系统";
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.Name = "viewMenuItem";
            this.viewMenuItem.Size = new System.Drawing.Size(44, 21);
            this.viewMenuItem.Text = "视图";
            // 
            // toolMenuItem
            // 
            this.toolMenuItem.Name = "toolMenuItem";
            this.toolMenuItem.Size = new System.Drawing.Size(44, 21);
            this.toolMenuItem.Text = "工具";
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 21);
            this.helpMenuItem.Text = "帮助";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbOpen,
            this.tbSave,
            this.toolStripSeparator2,
            this.tbRefresh,
            this.toolStripButton1,
            this.toolStripSeparator3,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(893, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbOpen
            // 
            this.tbOpen.Image = global::TradingSystem.Properties.Resources.open;
            this.tbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbOpen.Name = "tbOpen";
            this.tbOpen.Size = new System.Drawing.Size(52, 22);
            this.tbOpen.Text = "打开";
            // 
            // tbSave
            // 
            this.tbSave.Image = global::TradingSystem.Properties.Resources.save;
            this.tbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbSave.Name = "tbSave";
            this.tbSave.Size = new System.Drawing.Size(52, 22);
            this.tbSave.Text = "保存";
            // 
            // tbRefresh
            // 
            this.tbRefresh.Image = global::TradingSystem.Properties.Resources.refresh;
            this.tbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbRefresh.Name = "tbRefresh";
            this.tbRefresh.Size = new System.Drawing.Size(52, 22);
            this.tbRefresh.Text = "刷新";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 543);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.panelBottomContainer);
            this.Controls.Add(this.panelTopContainer);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "MainForm";
            this.Text = "交易系统";
            this.panelTopContainer.ResumeLayout(false);
            this.panelTopContainer.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTopContainer;
        private System.Windows.Forms.Panel panelBottomContainer;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelMainCaption;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbOpen;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem systemMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripButton tbSave;
        private System.Windows.Forms.ToolStripButton tbRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

