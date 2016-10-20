namespace TradingSystem.View
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelTopContainer = new System.Windows.Forms.Panel();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._menuStripMain = new System.Windows.Forms.MenuStrip();
            this._systemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._toolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelBottomContainer = new System.Windows.Forms.Panel();
            this._splitContainerMain = new System.Windows.Forms.SplitContainer();
            this._panelMain = new System.Windows.Forms.Panel();
            this._panelMainCaption = new System.Windows.Forms.Panel();
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslCompany = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslMiddle = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslIcon = new System.Windows.Forms.ToolStripStatusLabel();
            this._tbOpen = new System.Windows.Forms.ToolStripButton();
            this._tbSave = new System.Windows.Forms.ToolStripButton();
            this._tbRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this._navBarContainer = new Controls.TSNavBarContainer();
            this.panelTopContainer.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this._menuStripMain.SuspendLayout();
            this.panelBottomContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerMain)).BeginInit();
            this._splitContainerMain.Panel1.SuspendLayout();
            this._splitContainerMain.Panel2.SuspendLayout();
            this._splitContainerMain.SuspendLayout();
            this._statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTopContainer
            // 
            this.panelTopContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTopContainer.Controls.Add(this._toolStrip);
            this.panelTopContainer.Controls.Add(this._menuStripMain);
            this.panelTopContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopContainer.Location = new System.Drawing.Point(0, 0);
            this.panelTopContainer.Name = "panelTopContainer";
            this.panelTopContainer.Size = new System.Drawing.Size(1212, 50);
            this.panelTopContainer.TabIndex = 0;
            // 
            // _toolStrip
            // 
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tbOpen,
            this._tbSave,
            this.toolStripSeparator2,
            this._tbRefresh,
            this.toolStripButton1,
            this.toolStripSeparator3,
            this.toolStripSeparator1});
            this._toolStrip.Location = new System.Drawing.Point(0, 25);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(1208, 25);
            this._toolStrip.TabIndex = 1;
            this._toolStrip.Text = "_toolStrip";
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _menuStripMain
            // 
            this._menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._systemMenuItem,
            this._viewMenuItem,
            this._toolMenuItem,
            this._helpMenuItem});
            this._menuStripMain.Location = new System.Drawing.Point(0, 0);
            this._menuStripMain.Name = "_menuStripMain";
            this._menuStripMain.Size = new System.Drawing.Size(1208, 25);
            this._menuStripMain.TabIndex = 0;
            this._menuStripMain.Text = "menuStrip1";
            // 
            // _systemMenuItem
            // 
            this._systemMenuItem.Name = "_systemMenuItem";
            this._systemMenuItem.Size = new System.Drawing.Size(44, 21);
            this._systemMenuItem.Text = "系统";
            // 
            // _viewMenuItem
            // 
            this._viewMenuItem.Name = "_viewMenuItem";
            this._viewMenuItem.Size = new System.Drawing.Size(44, 21);
            this._viewMenuItem.Text = "视图";
            // 
            // _toolMenuItem
            // 
            this._toolMenuItem.Name = "_toolMenuItem";
            this._toolMenuItem.Size = new System.Drawing.Size(44, 21);
            this._toolMenuItem.Text = "工具";
            // 
            // _helpMenuItem
            // 
            this._helpMenuItem.Name = "_helpMenuItem";
            this._helpMenuItem.Size = new System.Drawing.Size(44, 21);
            this._helpMenuItem.Text = "帮助";
            // 
            // panelBottomContainer
            // 
            this.panelBottomContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelBottomContainer.Controls.Add(this._statusStrip);
            this.panelBottomContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomContainer.Location = new System.Drawing.Point(0, 603);
            this.panelBottomContainer.Name = "panelBottomContainer";
            this.panelBottomContainer.Size = new System.Drawing.Size(1212, 29);
            this.panelBottomContainer.TabIndex = 1;
            // 
            // _splitContainerMain
            // 
            this._splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainerMain.Location = new System.Drawing.Point(0, 50);
            this._splitContainerMain.Name = "_splitContainerMain";
            // 
            // _splitContainerMain.Panel1
            // 
            this._splitContainerMain.Panel1.Controls.Add(this._navBarContainer);
            // 
            // _splitContainerMain.Panel2
            // 
            this._splitContainerMain.Panel2.Controls.Add(this._panelMain);
            this._splitContainerMain.Panel2.Controls.Add(this._panelMainCaption);
            this._splitContainerMain.Size = new System.Drawing.Size(1212, 553);
            this._splitContainerMain.SplitterDistance = 169;
            this._splitContainerMain.TabIndex = 2;
            // 
            // _panelMain
            // 
            this._panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelMain.Location = new System.Drawing.Point(0, 10);
            this._panelMain.Name = "_panelMain";
            this._panelMain.Size = new System.Drawing.Size(1039, 543);
            this._panelMain.TabIndex = 1;
            // 
            // _panelMainCaption
            // 
            this._panelMainCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelMainCaption.Location = new System.Drawing.Point(0, 0);
            this._panelMainCaption.Name = "_panelMainCaption";
            this._panelMainCaption.Size = new System.Drawing.Size(1039, 10);
            this._panelMainCaption.TabIndex = 0;
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList.Images.SetKeyName(0, "right.png");
            this._imageList.Images.SetKeyName(1, "down.png");
            this._imageList.Images.SetKeyName(2, "item.png");
            // 
            // _statusStrip
            // 
            this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslIcon,
            this.tsslCompany,
            this.tsslMiddle,
            this.tsslUser});
            this._statusStrip.Location = new System.Drawing.Point(0, -1);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.Size = new System.Drawing.Size(1208, 26);
            this._statusStrip.TabIndex = 0;
            this._statusStrip.Text = "statusStrip1";
            // 
            // tsslCompany
            // 
            this.tsslCompany.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslCompany.Name = "tsslCompany";
            this.tsslCompany.Size = new System.Drawing.Size(60, 21);
            this.tsslCompany.Text = "海通资管";
            // 
            // tsslMiddle
            // 
            this.tsslMiddle.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslMiddle.Name = "tsslMiddle";
            this.tsslMiddle.Size = new System.Drawing.Size(1053, 21);
            this.tsslMiddle.Spring = true;
            this.tsslMiddle.Text = "  ";
            // 
            // tsslUser
            // 
            this.tsslUser.AutoSize = false;
            this.tsslUser.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslUser.Name = "tsslUser";
            this.tsslUser.Size = new System.Drawing.Size(60, 21);
            this.tsslUser.Text = "用户";
            this.tsslUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslIcon
            // 
            this.tsslIcon.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslIcon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsslIcon.Image = global::TradingSystem.Properties.Resources.htsamc_logo;
            this.tsslIcon.Name = "tsslIcon";
            this.tsslIcon.Size = new System.Drawing.Size(20, 21);
            this.tsslIcon.Text = "toolStripStatusLabel1";
            // 
            // _tbOpen
            // 
            this._tbOpen.Image = global::TradingSystem.Properties.Resources.open;
            this._tbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tbOpen.Name = "_tbOpen";
            this._tbOpen.Size = new System.Drawing.Size(52, 22);
            this._tbOpen.Text = "打开";
            // 
            // _tbSave
            // 
            this._tbSave.Image = global::TradingSystem.Properties.Resources.save;
            this._tbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tbSave.Name = "_tbSave";
            this._tbSave.Size = new System.Drawing.Size(52, 22);
            this._tbSave.Text = "保存";
            // 
            // _tbRefresh
            // 
            this._tbRefresh.Image = global::TradingSystem.Properties.Resources.refresh;
            this._tbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tbRefresh.Name = "_tbRefresh";
            this._tbRefresh.Size = new System.Drawing.Size(52, 22);
            this._tbRefresh.Text = "刷新";
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
            // _navBarContainer
            // 
            this._navBarContainer.AutoScroll = true;
            this._navBarContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._navBarContainer.BarMargin = 5;
            this._navBarContainer.BarSpace = 1;
            this._navBarContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._navBarContainer.Location = new System.Drawing.Point(0, 0);
            this._navBarContainer.Name = "_navBarContainer";
            this._navBarContainer.SelectedIndex = -1;
            this._navBarContainer.Size = new System.Drawing.Size(169, 553);
            this._navBarContainer.TabIndex = 0;
            this._navBarContainer.LeafItemClick += new Controls.TreeViewItemClick(this.TreeView_ItemClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 632);
            this.Controls.Add(this._splitContainerMain);
            this.Controls.Add(this.panelBottomContainer);
            this.Controls.Add(this.panelTopContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this._menuStripMain;
            this.Name = "MainForm";
            this.Text = "交易系统";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelTopContainer.ResumeLayout(false);
            this.panelTopContainer.PerformLayout();
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this._menuStripMain.ResumeLayout(false);
            this._menuStripMain.PerformLayout();
            this.panelBottomContainer.ResumeLayout(false);
            this.panelBottomContainer.PerformLayout();
            this._splitContainerMain.Panel1.ResumeLayout(false);
            this._splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerMain)).EndInit();
            this._splitContainerMain.ResumeLayout(false);
            this._statusStrip.ResumeLayout(false);
            this._statusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTopContainer;
        private System.Windows.Forms.Panel panelBottomContainer;
        private System.Windows.Forms.SplitContainer _splitContainerMain;
        private System.Windows.Forms.Panel _panelMain;
        private System.Windows.Forms.Panel _panelMainCaption;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripButton _tbOpen;
        private System.Windows.Forms.MenuStrip _menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem _systemMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _toolMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _helpMenuItem;
        private System.Windows.Forms.ToolStripButton _tbSave;
        private System.Windows.Forms.ToolStripButton _tbRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Controls.TSNavBarContainer _navBarContainer;
        private System.Windows.Forms.ImageList _imageList;
        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsslCompany;
        private System.Windows.Forms.ToolStripStatusLabel tsslMiddle;
        private System.Windows.Forms.ToolStripStatusLabel tsslUser;
        private System.Windows.Forms.ToolStripStatusLabel tsslIcon;
    }
}

