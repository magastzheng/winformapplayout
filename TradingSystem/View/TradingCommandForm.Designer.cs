using Config;
using Controls;
namespace TradingSystem.View
{
    partial class TradingCommandForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private GridConfig _gridConfig = null;
        private const string GridCMDTrading = "cmdtrading";
        private const string GridEntrustFlow = "entrustflow";
        private const string GridDealFlow = "dealflow";
        private const string GridCMDSecurity = "cmdsecurity";
        private const string GridBuySell = "buysell";

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradingCommandForm));
            this.tlPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.tsbMainRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbMainOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbMainSave = new System.Windows.Forms.ToolStripButton();
            this.tsbMainSwitchOp = new System.Windows.Forms.ToolStripButton();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.sysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabParentCmdTrading = new System.Windows.Forms.TabPage();
            this.spContainerParentCmdTrading = new System.Windows.Forms.SplitContainer();
            this.spContainerChildCmdTrading = new System.Windows.Forms.SplitContainer();
            this.tlPanelParentCommand = new System.Windows.Forms.TableLayoutPanel();
            //this.dataGridViewCmdTrading = new System.Windows.Forms.DataGridView();
            this.toolStripCmdTrading = new System.Windows.Forms.ToolStrip();
            this.tsbCmdRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbCmdUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbCmdCancelRedo = new System.Windows.Forms.ToolStripButton();
            this.tsbCmdCancelAdd = new System.Windows.Forms.ToolStripButton();
            this.panelCmdTradingBottom = new System.Windows.Forms.Panel();
            this.btnCmdUnSelectAll = new System.Windows.Forms.Button();
            this.btnCmdSelectAll = new System.Windows.Forms.Button();
            this.tabControlCmdDetail = new System.Windows.Forms.TabControl();
            this.tabChildCmdSecurity = new System.Windows.Forms.TabPage();
            this.tlPanelChildCmdSecurity = new System.Windows.Forms.TableLayoutPanel();
            //this.dataGridViewCmdSecurity = new System.Windows.Forms.DataGridView();
            this.tabChildEntrustFlow = new System.Windows.Forms.TabPage();
            this.tlPanelChildEntrustFlow = new System.Windows.Forms.TableLayoutPanel();
            this.tabChildDealFlow = new System.Windows.Forms.TabPage();
            this.tlPanelChildDealFlow = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainerChildMarket = new System.Windows.Forms.SplitContainer();
            this.splitContainerChildEntrust = new System.Windows.Forms.SplitContainer();
            this.tlPanelCalcEntrust = new System.Windows.Forms.TableLayoutPanel();
            this.buysellPanel = new System.Windows.Forms.Panel();
            this.btnEntrust = new System.Windows.Forms.Button();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.comboBoxFutureSell = new System.Windows.Forms.ComboBox();
            this.comboBoxFutureBuy = new System.Windows.Forms.ComboBox();
            this.comboBoxSpotSell = new System.Windows.Forms.ComboBox();
            this.comboBoxSpotBuy = new System.Windows.Forms.ComboBox();
            this.lblFuturesSellPrice = new System.Windows.Forms.Label();
            this.lblFuturesBuyPrice = new System.Windows.Forms.Label();
            this.lblSpotSellPrice = new System.Windows.Forms.Label();
            this.lblSpotBuyPrice = new System.Windows.Forms.Label();
            //this.dataGridViewBuySell = new System.Windows.Forms.DataGridView();
            this.panelEntrust = new System.Windows.Forms.Panel();
            this.btnEntrusting = new System.Windows.Forms.Button();
            this.btnCalc = new System.Windows.Forms.Button();
            this.tabParentEntrustFlow = new System.Windows.Forms.TabPage();
            this.tlPanelParentEntrustFlow = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripEntrustFlow = new System.Windows.Forms.ToolStrip();
            this.tsbEFRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            //this.dataGridViewEntrustFlow = new System.Windows.Forms.DataGridView();
            this.panelParentEntrustFlow = new System.Windows.Forms.Panel();
            this.btnCancelAdd = new System.Windows.Forms.Button();
            this.btnCancelSelect = new System.Windows.Forms.Button();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.tabParentDealFlow = new System.Windows.Forms.TabPage();
            this.tlPanelParentDealFlow = new System.Windows.Forms.TableLayoutPanel();
            //this.dataGridViewDealFlow = new System.Windows.Forms.DataGridView();
            this.toolStripDealFlow = new System.Windows.Forms.ToolStrip();
            this.tsbDFRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.panelAddCopies = new System.Windows.Forms.Panel();
            this.labelAddCopies = new System.Windows.Forms.Label();
            this.txtBoxAddCopies = new System.Windows.Forms.TextBox();
            this.lblCopyUnit = new System.Windows.Forms.Label();
            this.panelChildCmdSecurity = new System.Windows.Forms.Panel();
            this.btnCmdSecuritySelectAll = new System.Windows.Forms.Button();
            this.btnCmdSecurityUnSelectAll = new System.Windows.Forms.Button();

            //
            //GridView
            //
            this.dataGridViewCmdTrading = new HSGridView(_gridConfig.GetGid(GridCMDTrading));
            this.dataGridViewCmdSecurity = new HSGridView(_gridConfig.GetGid(GridCMDSecurity));
            this.dataGridViewBuySell = new HSGridView(_gridConfig.GetGid(GridBuySell));
            this.dataGridViewEntrustFlow = new HSGridView(_gridConfig.GetGid(GridEntrustFlow));
            this.dataGridViewDealFlow = new HSGridView(_gridConfig.GetGid(GridDealFlow));

            this.tlPanelMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabParentCmdTrading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spContainerParentCmdTrading)).BeginInit();
            this.spContainerParentCmdTrading.Panel1.SuspendLayout();
            this.spContainerParentCmdTrading.Panel2.SuspendLayout();
            this.spContainerParentCmdTrading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spContainerChildCmdTrading)).BeginInit();
            this.spContainerChildCmdTrading.Panel1.SuspendLayout();
            this.spContainerChildCmdTrading.Panel2.SuspendLayout();
            this.spContainerChildCmdTrading.SuspendLayout();
            this.tlPanelParentCommand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCmdTrading)).BeginInit();
            this.toolStripCmdTrading.SuspendLayout();
            this.panelCmdTradingBottom.SuspendLayout();
            this.tabControlCmdDetail.SuspendLayout();
            this.tabChildCmdSecurity.SuspendLayout();
            this.tlPanelChildCmdSecurity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCmdSecurity)).BeginInit();
            this.tabChildEntrustFlow.SuspendLayout();
            this.tabChildDealFlow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerChildMarket)).BeginInit();
            this.splitContainerChildMarket.Panel2.SuspendLayout();
            this.splitContainerChildMarket.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerChildEntrust)).BeginInit();
            this.splitContainerChildEntrust.Panel2.SuspendLayout();
            this.splitContainerChildEntrust.SuspendLayout();
            this.tlPanelCalcEntrust.SuspendLayout();
            this.buysellPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBuySell)).BeginInit();
            this.panelEntrust.SuspendLayout();
            this.tabParentEntrustFlow.SuspendLayout();
            this.tlPanelParentEntrustFlow.SuspendLayout();
            this.toolStripEntrustFlow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntrustFlow)).BeginInit();
            this.panelParentEntrustFlow.SuspendLayout();
            this.tabParentDealFlow.SuspendLayout();
            this.tlPanelParentDealFlow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDealFlow)).BeginInit();
            this.toolStripDealFlow.SuspendLayout();
            this.panelAddCopies.SuspendLayout();
            this.panelChildCmdSecurity.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlPanelMain
            // 
            this.tlPanelMain.ColumnCount = 1;
            this.tlPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelMain.Controls.Add(this.toolStripMain, 0, 1);
            this.tlPanelMain.Controls.Add(this.menuStripMain, 0, 0);
            this.tlPanelMain.Controls.Add(this.tabControlMain, 0, 2);
            this.tlPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tlPanelMain.Name = "tlPanelMain";
            this.tlPanelMain.RowCount = 4;
            this.tlPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tlPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlPanelMain.Size = new System.Drawing.Size(1033, 536);
            this.tlPanelMain.TabIndex = 0;
            // 
            // toolStripMain
            // 
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMainRefresh,
            this.tsbMainOpen,
            this.tsbMainSave,
            this.tsbMainSwitchOp});
            this.toolStripMain.Location = new System.Drawing.Point(0, 26);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(1033, 26);
            this.toolStripMain.TabIndex = 3;
            this.toolStripMain.Text = "toolStripMain";
            // 
            // tsbMainRefresh
            // 
            this.tsbMainRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbMainRefresh.Image")));
            this.tsbMainRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMainRefresh.Name = "tsbMainRefresh";
            this.tsbMainRefresh.Size = new System.Drawing.Size(52, 23);
            this.tsbMainRefresh.Text = "刷新";
            this.tsbMainRefresh.Click += new System.EventHandler(this.MainRefresh_Click);
            // 
            // tsbMainOpen
            // 
            this.tsbMainOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbMainOpen.Image")));
            this.tsbMainOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMainOpen.Name = "tsbMainOpen";
            this.tsbMainOpen.Size = new System.Drawing.Size(52, 23);
            this.tsbMainOpen.Text = "打开";
            // 
            // tsbMainSave
            // 
            this.tsbMainSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbMainSave.Image")));
            this.tsbMainSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMainSave.Name = "tsbMainSave";
            this.tsbMainSave.Size = new System.Drawing.Size(52, 23);
            this.tsbMainSave.Text = "保存";
            // 
            // tsbMainSwitchOp
            // 
            this.tsbMainSwitchOp.Image = ((System.Drawing.Image)(resources.GetObject("tsbMainSwitchOp.Image")));
            this.tsbMainSwitchOp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMainSwitchOp.Name = "tsbMainSwitchOp";
            this.tsbMainSwitchOp.Size = new System.Drawing.Size(88, 23);
            this.tsbMainSwitchOp.Text = "更换操作员";
            // 
            // menuStripMain
            // 
            this.menuStripMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sysToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1033, 26);
            this.menuStripMain.TabIndex = 2;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // sysToolStripMenuItem
            // 
            this.sysToolStripMenuItem.Name = "sysToolStripMenuItem";
            this.sysToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.sysToolStripMenuItem.Text = "系统";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.viewToolStripMenuItem.Text = "视图";
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.toolToolStripMenuItem.Text = "工具";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.helpToolStripMenuItem.Text = "帮助";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabParentCmdTrading);
            this.tabControlMain.Controls.Add(this.tabParentEntrustFlow);
            this.tabControlMain.Controls.Add(this.tabParentDealFlow);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(3, 55);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1027, 449);
            this.tabControlMain.TabIndex = 0;
            this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.TabControlMain_SelectedIndexChanged);
            // 
            // tabParentCmdTrading
            // 
            this.tabParentCmdTrading.Controls.Add(this.spContainerParentCmdTrading);
            this.tabParentCmdTrading.Location = new System.Drawing.Point(4, 22);
            this.tabParentCmdTrading.Name = "tabParentCmdTrading";
            this.tabParentCmdTrading.Padding = new System.Windows.Forms.Padding(3);
            this.tabParentCmdTrading.Size = new System.Drawing.Size(1019, 423);
            this.tabParentCmdTrading.TabIndex = 0;
            this.tabParentCmdTrading.Text = "指令交易";
            this.tabParentCmdTrading.UseVisualStyleBackColor = true;
            // 
            // spContainerParentCmdTrading
            // 
            this.spContainerParentCmdTrading.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spContainerParentCmdTrading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spContainerParentCmdTrading.Location = new System.Drawing.Point(3, 3);
            this.spContainerParentCmdTrading.Name = "spContainerParentCmdTrading";
            // 
            // spContainerParentCmdTrading.Panel1
            // 
            this.spContainerParentCmdTrading.Panel1.Controls.Add(this.spContainerChildCmdTrading);
            // 
            // spContainerParentCmdTrading.Panel2
            // 
            this.spContainerParentCmdTrading.Panel2.Controls.Add(this.splitContainerChildMarket);
            this.spContainerParentCmdTrading.Size = new System.Drawing.Size(1013, 417);
            this.spContainerParentCmdTrading.SplitterDistance = 778;
            this.spContainerParentCmdTrading.TabIndex = 0;
            // 
            // spContainerChildCmdTrading
            // 
            this.spContainerChildCmdTrading.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spContainerChildCmdTrading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spContainerChildCmdTrading.Location = new System.Drawing.Point(0, 0);
            this.spContainerChildCmdTrading.Name = "spContainerChildCmdTrading";
            this.spContainerChildCmdTrading.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spContainerChildCmdTrading.Panel1
            // 
            this.spContainerChildCmdTrading.Panel1.Controls.Add(this.tlPanelParentCommand);
            // 
            // spContainerChildCmdTrading.Panel2
            // 
            this.spContainerChildCmdTrading.Panel2.Controls.Add(this.tabControlCmdDetail);
            this.spContainerChildCmdTrading.Size = new System.Drawing.Size(778, 417);
            this.spContainerChildCmdTrading.SplitterDistance = 267;
            this.spContainerChildCmdTrading.TabIndex = 0;
            // 
            // tlPanelParentCommand
            // 
            this.tlPanelParentCommand.ColumnCount = 1;
            this.tlPanelParentCommand.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelParentCommand.Controls.Add(this.dataGridViewCmdTrading, 0, 1);
            this.tlPanelParentCommand.Controls.Add(this.toolStripCmdTrading, 0, 0);
            this.tlPanelParentCommand.Controls.Add(this.panelCmdTradingBottom, 0, 2);
            this.tlPanelParentCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPanelParentCommand.Location = new System.Drawing.Point(0, 0);
            this.tlPanelParentCommand.Name = "tlPanelParentCommand";
            this.tlPanelParentCommand.RowCount = 3;
            this.tlPanelParentCommand.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlPanelParentCommand.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelParentCommand.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlPanelParentCommand.Size = new System.Drawing.Size(774, 263);
            this.tlPanelParentCommand.TabIndex = 0;
            // 
            // dataGridViewCmdTrading
            // 
            this.dataGridViewCmdTrading.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCmdTrading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCmdTrading.Location = new System.Drawing.Point(3, 33);
            this.dataGridViewCmdTrading.Name = "dataGridViewCmdTrading";
            this.dataGridViewCmdTrading.RowTemplate.Height = 23;
            this.dataGridViewCmdTrading.Size = new System.Drawing.Size(768, 197);
            this.dataGridViewCmdTrading.TabIndex = 0;
            // 
            // toolStripCmdTrading
            // 
            this.toolStripCmdTrading.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCmdRefresh,
            this.tsbCmdUndo,
            this.tsbCmdCancelRedo,
            this.tsbCmdCancelAdd});
            this.toolStripCmdTrading.Location = new System.Drawing.Point(0, 0);
            this.toolStripCmdTrading.Name = "toolStripCmdTrading";
            this.toolStripCmdTrading.Size = new System.Drawing.Size(774, 25);
            this.toolStripCmdTrading.TabIndex = 1;
            this.toolStripCmdTrading.Text = "toolStripCmdTrading";
            // 
            // tsbCmdRefresh
            // 
            this.tsbCmdRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbCmdRefresh.Image")));
            this.tsbCmdRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCmdRefresh.Name = "tsbCmdRefresh";
            this.tsbCmdRefresh.Size = new System.Drawing.Size(52, 22);
            this.tsbCmdRefresh.Text = "刷新";
            this.tsbCmdRefresh.Click += new System.EventHandler(this.ToolStripButton_CmdRefresh_Click);
            // 
            // tsbCmdUndo
            // 
            this.tsbCmdUndo.Image = ((System.Drawing.Image)(resources.GetObject("tsbCmdUndo.Image")));
            this.tsbCmdUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCmdUndo.Name = "tsbCmdUndo";
            this.tsbCmdUndo.Size = new System.Drawing.Size(52, 22);
            this.tsbCmdUndo.Text = "撤单";
            this.tsbCmdUndo.Click += new System.EventHandler(this.ToolStripButton_CmdUndo_Click);
            // 
            // tsbCmdCancelRedo
            // 
            this.tsbCmdCancelRedo.Image = ((System.Drawing.Image)(resources.GetObject("tsbCmdCancelRedo.Image")));
            this.tsbCmdCancelRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCmdCancelRedo.Name = "tsbCmdCancelRedo";
            this.tsbCmdCancelRedo.Size = new System.Drawing.Size(52, 22);
            this.tsbCmdCancelRedo.Text = "撤补";
            this.tsbCmdCancelRedo.Click += new System.EventHandler(this.ToolStripButton_CmdCancelRedo_Click);
            // 
            // tsbCmdCancelAdd
            // 
            this.tsbCmdCancelAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbCmdCancelAdd.Image")));
            this.tsbCmdCancelAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCmdCancelAdd.Name = "tsbCmdCancelAdd";
            this.tsbCmdCancelAdd.Size = new System.Drawing.Size(76, 22);
            this.tsbCmdCancelAdd.Text = "撤销追加";
            // 
            // panelCmdTradingBottom
            // 
            this.panelCmdTradingBottom.Controls.Add(this.btnCmdUnSelectAll);
            this.panelCmdTradingBottom.Controls.Add(this.btnCmdSelectAll);
            this.panelCmdTradingBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCmdTradingBottom.Location = new System.Drawing.Point(3, 236);
            this.panelCmdTradingBottom.Name = "panelCmdTradingBottom";
            this.panelCmdTradingBottom.Size = new System.Drawing.Size(768, 24);
            this.panelCmdTradingBottom.TabIndex = 2;
            // 
            // btnCmdUnSelectAll
            // 
            this.btnCmdUnSelectAll.Location = new System.Drawing.Point(99, 2);
            this.btnCmdUnSelectAll.Name = "btnCmdUnSelectAll";
            this.btnCmdUnSelectAll.Size = new System.Drawing.Size(75, 21);
            this.btnCmdUnSelectAll.TabIndex = 1;
            this.btnCmdUnSelectAll.Text = "反选";
            this.btnCmdUnSelectAll.UseVisualStyleBackColor = true;
            // 
            // btnCmdSelectAll
            // 
            this.btnCmdSelectAll.Location = new System.Drawing.Point(7, 2);
            this.btnCmdSelectAll.Name = "btnCmdSelectAll";
            this.btnCmdSelectAll.Size = new System.Drawing.Size(75, 21);
            this.btnCmdSelectAll.TabIndex = 0;
            this.btnCmdSelectAll.Text = "全选";
            this.btnCmdSelectAll.UseVisualStyleBackColor = true;
            // 
            // tabControlCmdDetail
            // 
            this.tabControlCmdDetail.Controls.Add(this.tabChildCmdSecurity);
            this.tabControlCmdDetail.Controls.Add(this.tabChildEntrustFlow);
            this.tabControlCmdDetail.Controls.Add(this.tabChildDealFlow);
            this.tabControlCmdDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlCmdDetail.Location = new System.Drawing.Point(0, 0);
            this.tabControlCmdDetail.Name = "tabControlCmdDetail";
            this.tabControlCmdDetail.SelectedIndex = 0;
            this.tabControlCmdDetail.Size = new System.Drawing.Size(774, 142);
            this.tabControlCmdDetail.TabIndex = 0;
            this.tabControlCmdDetail.SelectedIndexChanged += new System.EventHandler(this.TabControlCmdDetail_SelectedIndexChanged);
            // 
            // tabChildCmdSecurity
            // 
            this.tabChildCmdSecurity.Controls.Add(this.tlPanelChildCmdSecurity);
            this.tabChildCmdSecurity.Location = new System.Drawing.Point(4, 22);
            this.tabChildCmdSecurity.Name = "tabChildCmdSecurity";
            this.tabChildCmdSecurity.Padding = new System.Windows.Forms.Padding(3);
            this.tabChildCmdSecurity.Size = new System.Drawing.Size(766, 116);
            this.tabChildCmdSecurity.TabIndex = 0;
            this.tabChildCmdSecurity.Text = "指令证券";
            this.tabChildCmdSecurity.UseVisualStyleBackColor = true;
            // 
            // tlPanelChildCmdSecurity
            // 
            this.tlPanelChildCmdSecurity.ColumnCount = 1;
            this.tlPanelChildCmdSecurity.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelChildCmdSecurity.Controls.Add(this.dataGridViewCmdSecurity, 0, 0);
            this.tlPanelChildCmdSecurity.Controls.Add(this.panelChildCmdSecurity, 0, 1);
            this.tlPanelChildCmdSecurity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPanelChildCmdSecurity.Location = new System.Drawing.Point(3, 3);
            this.tlPanelChildCmdSecurity.Name = "tlPanelChildCmdSecurity";
            this.tlPanelChildCmdSecurity.RowCount = 2;
            this.tlPanelChildCmdSecurity.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelChildCmdSecurity.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlPanelChildCmdSecurity.Size = new System.Drawing.Size(760, 110);
            this.tlPanelChildCmdSecurity.TabIndex = 0;
            // 
            // dataGridViewCmdSecurity
            // 
            this.dataGridViewCmdSecurity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCmdSecurity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCmdSecurity.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewCmdSecurity.Name = "dataGridViewCmdSecurity";
            this.dataGridViewCmdSecurity.RowTemplate.Height = 23;
            this.dataGridViewCmdSecurity.Size = new System.Drawing.Size(754, 74);
            this.dataGridViewCmdSecurity.TabIndex = 0;
            // 
            // tabChildEntrustFlow
            // 
            this.tabChildEntrustFlow.Controls.Add(this.tlPanelChildEntrustFlow);
            this.tabChildEntrustFlow.Location = new System.Drawing.Point(4, 22);
            this.tabChildEntrustFlow.Name = "tabChildEntrustFlow";
            this.tabChildEntrustFlow.Padding = new System.Windows.Forms.Padding(3);
            this.tabChildEntrustFlow.Size = new System.Drawing.Size(766, 116);
            this.tabChildEntrustFlow.TabIndex = 1;
            this.tabChildEntrustFlow.Text = "委托流水";
            this.tabChildEntrustFlow.UseVisualStyleBackColor = true;
            // 
            // tlPanelChildEntrustFlow
            // 
            this.tlPanelChildEntrustFlow.ColumnCount = 1;
            this.tlPanelChildEntrustFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelChildEntrustFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPanelChildEntrustFlow.Location = new System.Drawing.Point(3, 3);
            this.tlPanelChildEntrustFlow.Name = "tlPanelChildEntrustFlow";
            this.tlPanelChildEntrustFlow.RowCount = 2;
            this.tlPanelChildEntrustFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelChildEntrustFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlPanelChildEntrustFlow.Size = new System.Drawing.Size(760, 110);
            this.tlPanelChildEntrustFlow.TabIndex = 0;
            // 
            // tabChildDealFlow
            // 
            this.tabChildDealFlow.Controls.Add(this.tlPanelChildDealFlow);
            this.tabChildDealFlow.Location = new System.Drawing.Point(4, 22);
            this.tabChildDealFlow.Name = "tabChildDealFlow";
            this.tabChildDealFlow.Padding = new System.Windows.Forms.Padding(3);
            this.tabChildDealFlow.Size = new System.Drawing.Size(766, 116);
            this.tabChildDealFlow.TabIndex = 2;
            this.tabChildDealFlow.Text = "成交流水";
            this.tabChildDealFlow.UseVisualStyleBackColor = true;
            // 
            // tlPanelChildDealFlow
            // 
            this.tlPanelChildDealFlow.ColumnCount = 1;
            this.tlPanelChildDealFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelChildDealFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPanelChildDealFlow.Location = new System.Drawing.Point(3, 3);
            this.tlPanelChildDealFlow.Name = "tlPanelChildDealFlow";
            this.tlPanelChildDealFlow.RowCount = 2;
            this.tlPanelChildDealFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelChildDealFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlPanelChildDealFlow.Size = new System.Drawing.Size(760, 110);
            this.tlPanelChildDealFlow.TabIndex = 0;
            // 
            // splitContainerChildMarket
            // 
            this.splitContainerChildMarket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerChildMarket.Location = new System.Drawing.Point(0, 0);
            this.splitContainerChildMarket.Name = "splitContainerChildMarket";
            this.splitContainerChildMarket.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerChildMarket.Panel2
            // 
            this.splitContainerChildMarket.Panel2.Controls.Add(this.splitContainerChildEntrust);
            this.splitContainerChildMarket.Size = new System.Drawing.Size(227, 413);
            this.splitContainerChildMarket.SplitterDistance = 57;
            this.splitContainerChildMarket.TabIndex = 0;
            // 
            // splitContainerChildEntrust
            // 
            this.splitContainerChildEntrust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerChildEntrust.Location = new System.Drawing.Point(0, 0);
            this.splitContainerChildEntrust.Name = "splitContainerChildEntrust";
            this.splitContainerChildEntrust.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerChildEntrust.Panel2
            // 
            this.splitContainerChildEntrust.Panel2.Controls.Add(this.tlPanelCalcEntrust);
            this.splitContainerChildEntrust.Size = new System.Drawing.Size(227, 352);
            this.splitContainerChildEntrust.SplitterDistance = 58;
            this.splitContainerChildEntrust.TabIndex = 0;
            // 
            // tlPanelCalcEntrust
            // 
            this.tlPanelCalcEntrust.ColumnCount = 1;
            this.tlPanelCalcEntrust.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelCalcEntrust.Controls.Add(this.buysellPanel, 0, 0);
            this.tlPanelCalcEntrust.Controls.Add(this.dataGridViewBuySell, 0, 1);
            this.tlPanelCalcEntrust.Controls.Add(this.panelEntrust, 0, 3);
            this.tlPanelCalcEntrust.Controls.Add(this.panelAddCopies, 0, 2);
            this.tlPanelCalcEntrust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPanelCalcEntrust.Location = new System.Drawing.Point(0, 0);
            this.tlPanelCalcEntrust.Name = "tlPanelCalcEntrust";
            this.tlPanelCalcEntrust.RowCount = 4;
            this.tlPanelCalcEntrust.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlPanelCalcEntrust.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelCalcEntrust.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlPanelCalcEntrust.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlPanelCalcEntrust.Size = new System.Drawing.Size(227, 290);
            this.tlPanelCalcEntrust.TabIndex = 0;
            // 
            // buysellPanel
            // 
            this.buysellPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buysellPanel.Controls.Add(this.btnEntrust);
            this.buysellPanel.Controls.Add(this.btnCalculate);
            this.buysellPanel.Controls.Add(this.comboBoxFutureSell);
            this.buysellPanel.Controls.Add(this.comboBoxFutureBuy);
            this.buysellPanel.Controls.Add(this.comboBoxSpotSell);
            this.buysellPanel.Controls.Add(this.comboBoxSpotBuy);
            this.buysellPanel.Controls.Add(this.lblFuturesSellPrice);
            this.buysellPanel.Controls.Add(this.lblFuturesBuyPrice);
            this.buysellPanel.Controls.Add(this.lblSpotSellPrice);
            this.buysellPanel.Controls.Add(this.lblSpotBuyPrice);
            this.buysellPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buysellPanel.Location = new System.Drawing.Point(3, 3);
            this.buysellPanel.Name = "buysellPanel";
            this.buysellPanel.Size = new System.Drawing.Size(221, 114);
            this.buysellPanel.TabIndex = 3;
            // 
            // btnEntrust
            // 
            this.btnEntrust.Location = new System.Drawing.Point(148, 287);
            this.btnEntrust.Name = "btnEntrust";
            this.btnEntrust.Size = new System.Drawing.Size(75, 23);
            this.btnEntrust.TabIndex = 11;
            this.btnEntrust.Text = "委托";
            this.btnEntrust.UseVisualStyleBackColor = true;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(25, 287);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 10;
            this.btnCalculate.Text = "计算";
            this.btnCalculate.UseVisualStyleBackColor = true;
            // 
            // comboBoxFutureSell
            // 
            this.comboBoxFutureSell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFutureSell.FormattingEnabled = true;
            this.comboBoxFutureSell.Location = new System.Drawing.Point(102, 93);
            this.comboBoxFutureSell.Name = "comboBoxFutureSell";
            this.comboBoxFutureSell.Size = new System.Drawing.Size(121, 20);
            this.comboBoxFutureSell.TabIndex = 8;
            // 
            // comboBoxFutureBuy
            // 
            this.comboBoxFutureBuy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFutureBuy.FormattingEnabled = true;
            this.comboBoxFutureBuy.Location = new System.Drawing.Point(102, 67);
            this.comboBoxFutureBuy.Name = "comboBoxFutureBuy";
            this.comboBoxFutureBuy.Size = new System.Drawing.Size(121, 20);
            this.comboBoxFutureBuy.TabIndex = 7;
            // 
            // comboBoxSpotSell
            // 
            this.comboBoxSpotSell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSpotSell.FormattingEnabled = true;
            this.comboBoxSpotSell.Location = new System.Drawing.Point(102, 41);
            this.comboBoxSpotSell.Name = "comboBoxSpotSell";
            this.comboBoxSpotSell.Size = new System.Drawing.Size(121, 20);
            this.comboBoxSpotSell.TabIndex = 6;
            // 
            // comboBoxSpotBuy
            // 
            this.comboBoxSpotBuy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSpotBuy.FormattingEnabled = true;
            this.comboBoxSpotBuy.Location = new System.Drawing.Point(102, 12);
            this.comboBoxSpotBuy.Name = "comboBoxSpotBuy";
            this.comboBoxSpotBuy.Size = new System.Drawing.Size(121, 20);
            this.comboBoxSpotBuy.TabIndex = 5;
            // 
            // lblFuturesSellPrice
            // 
            this.lblFuturesSellPrice.AutoSize = true;
            this.lblFuturesSellPrice.Location = new System.Drawing.Point(4, 93);
            this.lblFuturesSellPrice.Name = "lblFuturesSellPrice";
            this.lblFuturesSellPrice.Size = new System.Drawing.Size(65, 12);
            this.lblFuturesSellPrice.TabIndex = 3;
            this.lblFuturesSellPrice.Text = "期货委卖价";
            // 
            // lblFuturesBuyPrice
            // 
            this.lblFuturesBuyPrice.AutoSize = true;
            this.lblFuturesBuyPrice.Location = new System.Drawing.Point(4, 68);
            this.lblFuturesBuyPrice.Name = "lblFuturesBuyPrice";
            this.lblFuturesBuyPrice.Size = new System.Drawing.Size(65, 12);
            this.lblFuturesBuyPrice.TabIndex = 2;
            this.lblFuturesBuyPrice.Text = "期货委买价";
            // 
            // lblSpotSellPrice
            // 
            this.lblSpotSellPrice.AutoSize = true;
            this.lblSpotSellPrice.Location = new System.Drawing.Point(4, 41);
            this.lblSpotSellPrice.Name = "lblSpotSellPrice";
            this.lblSpotSellPrice.Size = new System.Drawing.Size(65, 12);
            this.lblSpotSellPrice.TabIndex = 1;
            this.lblSpotSellPrice.Text = "现货委卖价";
            // 
            // lblSpotBuyPrice
            // 
            this.lblSpotBuyPrice.AutoSize = true;
            this.lblSpotBuyPrice.Location = new System.Drawing.Point(4, 14);
            this.lblSpotBuyPrice.Name = "lblSpotBuyPrice";
            this.lblSpotBuyPrice.Size = new System.Drawing.Size(65, 12);
            this.lblSpotBuyPrice.TabIndex = 0;
            this.lblSpotBuyPrice.Text = "现货委买价";
            // 
            // dataGridViewBuySell
            // 
            this.dataGridViewBuySell.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBuySell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewBuySell.Location = new System.Drawing.Point(3, 123);
            this.dataGridViewBuySell.Name = "dataGridViewBuySell";
            this.dataGridViewBuySell.RowTemplate.Height = 23;
            this.dataGridViewBuySell.Size = new System.Drawing.Size(221, 84);
            this.dataGridViewBuySell.TabIndex = 12;
            this.dataGridViewBuySell.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewBuySell_CellMouseClick);
            this.dataGridViewBuySell.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DataGridViewBuySell_EditingControlShowing);
            // 
            // panelEntrust
            // 
            this.panelEntrust.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelEntrust.Controls.Add(this.btnEntrusting);
            this.panelEntrust.Controls.Add(this.btnCalc);
            this.panelEntrust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEntrust.Location = new System.Drawing.Point(3, 253);
            this.panelEntrust.Name = "panelEntrust";
            this.panelEntrust.Size = new System.Drawing.Size(221, 34);
            this.panelEntrust.TabIndex = 11;
            // 
            // btnEntrusting
            // 
            this.btnEntrusting.Location = new System.Drawing.Point(128, 6);
            this.btnEntrusting.Name = "btnEntrusting";
            this.btnEntrusting.Size = new System.Drawing.Size(75, 23);
            this.btnEntrusting.TabIndex = 1;
            this.btnEntrusting.Text = "委托";
            this.btnEntrusting.UseVisualStyleBackColor = true;
            this.btnEntrusting.Click += new System.EventHandler(this.ButtonEntrusting_Click);
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(33, 6);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(75, 23);
            this.btnCalc.TabIndex = 0;
            this.btnCalc.Text = "计算";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.ButtonCalc_Click);
            // 
            // tabParentEntrustFlow
            // 
            this.tabParentEntrustFlow.Controls.Add(this.tlPanelParentEntrustFlow);
            this.tabParentEntrustFlow.Location = new System.Drawing.Point(4, 22);
            this.tabParentEntrustFlow.Name = "tabParentEntrustFlow";
            this.tabParentEntrustFlow.Padding = new System.Windows.Forms.Padding(3);
            this.tabParentEntrustFlow.Size = new System.Drawing.Size(1019, 423);
            this.tabParentEntrustFlow.TabIndex = 1;
            this.tabParentEntrustFlow.Text = "委托流水";
            this.tabParentEntrustFlow.UseVisualStyleBackColor = true;
            // 
            // tlPanelParentEntrustFlow
            // 
            this.tlPanelParentEntrustFlow.ColumnCount = 1;
            this.tlPanelParentEntrustFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelParentEntrustFlow.Controls.Add(this.toolStripEntrustFlow, 0, 0);
            this.tlPanelParentEntrustFlow.Controls.Add(this.dataGridViewEntrustFlow, 0, 1);
            this.tlPanelParentEntrustFlow.Controls.Add(this.panelParentEntrustFlow, 0, 2);
            this.tlPanelParentEntrustFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPanelParentEntrustFlow.Location = new System.Drawing.Point(3, 3);
            this.tlPanelParentEntrustFlow.Name = "tlPanelParentEntrustFlow";
            this.tlPanelParentEntrustFlow.RowCount = 3;
            this.tlPanelParentEntrustFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlPanelParentEntrustFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelParentEntrustFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlPanelParentEntrustFlow.Size = new System.Drawing.Size(1013, 417);
            this.tlPanelParentEntrustFlow.TabIndex = 0;
            // 
            // toolStripEntrustFlow
            // 
            this.toolStripEntrustFlow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbEFRefresh,
            this.toolStripButton9});
            this.toolStripEntrustFlow.Location = new System.Drawing.Point(0, 0);
            this.toolStripEntrustFlow.Name = "toolStripEntrustFlow";
            this.toolStripEntrustFlow.Size = new System.Drawing.Size(1013, 25);
            this.toolStripEntrustFlow.TabIndex = 1;
            this.toolStripEntrustFlow.Text = "toolStripEntrustFlow";
            // 
            // tsbEFRefresh
            // 
            this.tsbEFRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbEFRefresh.Image")));
            this.tsbEFRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEFRefresh.Name = "tsbEFRefresh";
            this.tsbEFRefresh.Size = new System.Drawing.Size(52, 22);
            this.tsbEFRefresh.Text = "刷新";
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton9.Image")));
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton9.Text = "toolStripButton9";
            // 
            // dataGridViewEntrustFlow
            // 
            this.dataGridViewEntrustFlow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEntrustFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEntrustFlow.Location = new System.Drawing.Point(3, 33);
            this.dataGridViewEntrustFlow.Name = "dataGridViewEntrustFlow";
            this.dataGridViewEntrustFlow.RowTemplate.Height = 23;
            this.dataGridViewEntrustFlow.Size = new System.Drawing.Size(1007, 351);
            this.dataGridViewEntrustFlow.TabIndex = 0;
            // 
            // panelParentEntrustFlow
            // 
            this.panelParentEntrustFlow.Controls.Add(this.btnCancelAdd);
            this.panelParentEntrustFlow.Controls.Add(this.btnCancelSelect);
            this.panelParentEntrustFlow.Controls.Add(this.btnUnselectAll);
            this.panelParentEntrustFlow.Controls.Add(this.btnSelectAll);
            this.panelParentEntrustFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelParentEntrustFlow.Location = new System.Drawing.Point(3, 390);
            this.panelParentEntrustFlow.Name = "panelParentEntrustFlow";
            this.panelParentEntrustFlow.Size = new System.Drawing.Size(1007, 24);
            this.panelParentEntrustFlow.TabIndex = 2;
            // 
            // btnCancelAdd
            // 
            this.btnCancelAdd.Location = new System.Drawing.Point(248, 0);
            this.btnCancelAdd.Name = "btnCancelAdd";
            this.btnCancelAdd.Size = new System.Drawing.Size(75, 23);
            this.btnCancelAdd.TabIndex = 3;
            this.btnCancelAdd.Text = "撤补";
            this.btnCancelAdd.UseVisualStyleBackColor = true;
            // 
            // btnCancelSelect
            // 
            this.btnCancelSelect.Location = new System.Drawing.Point(166, 0);
            this.btnCancelSelect.Name = "btnCancelSelect";
            this.btnCancelSelect.Size = new System.Drawing.Size(75, 23);
            this.btnCancelSelect.TabIndex = 2;
            this.btnCancelSelect.Text = "撤单";
            this.btnCancelSelect.UseVisualStyleBackColor = true;
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.Location = new System.Drawing.Point(84, 0);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(75, 23);
            this.btnUnselectAll.TabIndex = 1;
            this.btnUnselectAll.Text = "反选";
            this.btnUnselectAll.UseVisualStyleBackColor = true;
            this.btnUnselectAll.Click += new System.EventHandler(this.ButtonUnSelectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(3, 0);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.ButtonSelectAll_Click);
            // 
            // tabParentDealFlow
            // 
            this.tabParentDealFlow.Controls.Add(this.tlPanelParentDealFlow);
            this.tabParentDealFlow.Location = new System.Drawing.Point(4, 22);
            this.tabParentDealFlow.Name = "tabParentDealFlow";
            this.tabParentDealFlow.Padding = new System.Windows.Forms.Padding(3);
            this.tabParentDealFlow.Size = new System.Drawing.Size(1019, 423);
            this.tabParentDealFlow.TabIndex = 2;
            this.tabParentDealFlow.Text = "成交流水";
            this.tabParentDealFlow.UseVisualStyleBackColor = true;
            // 
            // tlPanelParentDealFlow
            // 
            this.tlPanelParentDealFlow.ColumnCount = 1;
            this.tlPanelParentDealFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelParentDealFlow.Controls.Add(this.dataGridViewDealFlow, 0, 1);
            this.tlPanelParentDealFlow.Controls.Add(this.toolStripDealFlow, 0, 0);
            this.tlPanelParentDealFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPanelParentDealFlow.Location = new System.Drawing.Point(3, 3);
            this.tlPanelParentDealFlow.Name = "tlPanelParentDealFlow";
            this.tlPanelParentDealFlow.RowCount = 3;
            this.tlPanelParentDealFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlPanelParentDealFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPanelParentDealFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlPanelParentDealFlow.Size = new System.Drawing.Size(1013, 417);
            this.tlPanelParentDealFlow.TabIndex = 0;
            // 
            // dataGridViewDealFlow
            // 
            this.dataGridViewDealFlow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDealFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDealFlow.Location = new System.Drawing.Point(3, 33);
            this.dataGridViewDealFlow.Name = "dataGridViewDealFlow";
            this.dataGridViewDealFlow.RowTemplate.Height = 23;
            this.dataGridViewDealFlow.Size = new System.Drawing.Size(1007, 351);
            this.dataGridViewDealFlow.TabIndex = 0;
            // 
            // toolStripDealFlow
            // 
            this.toolStripDealFlow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDFRefresh,
            this.toolStripButton11});
            this.toolStripDealFlow.Location = new System.Drawing.Point(0, 0);
            this.toolStripDealFlow.Name = "toolStripDealFlow";
            this.toolStripDealFlow.Size = new System.Drawing.Size(1013, 25);
            this.toolStripDealFlow.TabIndex = 1;
            this.toolStripDealFlow.Text = "toolStripDealFlow";
            // 
            // tsbDFRefresh
            // 
            this.tsbDFRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbDFRefresh.Image")));
            this.tsbDFRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDFRefresh.Name = "tsbDFRefresh";
            this.tsbDFRefresh.Size = new System.Drawing.Size(52, 22);
            this.tsbDFRefresh.Text = "刷新";
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton11.Image")));
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton11.Text = "toolStripButton11";
            // 
            // panelAddCopies
            // 
            this.panelAddCopies.Controls.Add(this.lblCopyUnit);
            this.panelAddCopies.Controls.Add(this.txtBoxAddCopies);
            this.panelAddCopies.Controls.Add(this.labelAddCopies);
            this.panelAddCopies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAddCopies.Location = new System.Drawing.Point(3, 213);
            this.panelAddCopies.Name = "panelAddCopies";
            this.panelAddCopies.Size = new System.Drawing.Size(221, 34);
            this.panelAddCopies.TabIndex = 13;
            // 
            // labelAddCopies
            // 
            this.labelAddCopies.AutoSize = true;
            this.labelAddCopies.Location = new System.Drawing.Point(4, 7);
            this.labelAddCopies.Name = "labelAddCopies";
            this.labelAddCopies.Size = new System.Drawing.Size(53, 12);
            this.labelAddCopies.TabIndex = 0;
            this.labelAddCopies.Text = "追加份数";
            // 
            // txtBoxAddCopies
            // 
            this.txtBoxAddCopies.Location = new System.Drawing.Point(64, 5);
            this.txtBoxAddCopies.Name = "txtBoxAddCopies";
            this.txtBoxAddCopies.Size = new System.Drawing.Size(100, 21);
            this.txtBoxAddCopies.TabIndex = 1;
            this.txtBoxAddCopies.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
            // 
            // lblCopyUnit
            // 
            this.lblCopyUnit.AutoSize = true;
            this.lblCopyUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCopyUnit.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCopyUnit.Location = new System.Drawing.Point(171, 6);
            this.lblCopyUnit.Name = "lblCopyUnit";
            this.lblCopyUnit.Padding = new System.Windows.Forms.Padding(3);
            this.lblCopyUnit.Size = new System.Drawing.Size(37, 20);
            this.lblCopyUnit.TabIndex = 2;
            this.lblCopyUnit.Text = "套手";
            // 
            // panelChildCmdSecurity
            // 
            this.panelChildCmdSecurity.Controls.Add(this.btnCmdSecurityUnSelectAll);
            this.panelChildCmdSecurity.Controls.Add(this.btnCmdSecuritySelectAll);
            this.panelChildCmdSecurity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildCmdSecurity.Location = new System.Drawing.Point(3, 83);
            this.panelChildCmdSecurity.Name = "panelChildCmdSecurity";
            this.panelChildCmdSecurity.Size = new System.Drawing.Size(754, 24);
            this.panelChildCmdSecurity.TabIndex = 1;
            // 
            // btnCmdSecuritySelectAll
            // 
            this.btnCmdSecuritySelectAll.Location = new System.Drawing.Point(4, 0);
            this.btnCmdSecuritySelectAll.Name = "btnCmdSecuritySelectAll";
            this.btnCmdSecuritySelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnCmdSecuritySelectAll.TabIndex = 0;
            this.btnCmdSecuritySelectAll.Text = "全选";
            this.btnCmdSecuritySelectAll.UseVisualStyleBackColor = true;
            // 
            // btnCmdSecurityUnSelectAll
            // 
            this.btnCmdSecurityUnSelectAll.Location = new System.Drawing.Point(92, 0);
            this.btnCmdSecurityUnSelectAll.Name = "btnCmdSecurityUnSelectAll";
            this.btnCmdSecurityUnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnCmdSecurityUnSelectAll.TabIndex = 1;
            this.btnCmdSecurityUnSelectAll.Text = "反选";
            this.btnCmdSecurityUnSelectAll.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 536);
            this.Controls.Add(this.tlPanelMain);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "策略交易";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tlPanelMain.ResumeLayout(false);
            this.tlPanelMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabParentCmdTrading.ResumeLayout(false);
            this.spContainerParentCmdTrading.Panel1.ResumeLayout(false);
            this.spContainerParentCmdTrading.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spContainerParentCmdTrading)).EndInit();
            this.spContainerParentCmdTrading.ResumeLayout(false);
            this.spContainerChildCmdTrading.Panel1.ResumeLayout(false);
            this.spContainerChildCmdTrading.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spContainerChildCmdTrading)).EndInit();
            this.spContainerChildCmdTrading.ResumeLayout(false);
            this.tlPanelParentCommand.ResumeLayout(false);
            this.tlPanelParentCommand.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCmdTrading)).EndInit();
            this.toolStripCmdTrading.ResumeLayout(false);
            this.toolStripCmdTrading.PerformLayout();
            this.panelCmdTradingBottom.ResumeLayout(false);
            this.tabControlCmdDetail.ResumeLayout(false);
            this.tabChildCmdSecurity.ResumeLayout(false);
            this.tlPanelChildCmdSecurity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCmdSecurity)).EndInit();
            this.tabChildEntrustFlow.ResumeLayout(false);
            this.tabChildDealFlow.ResumeLayout(false);
            this.splitContainerChildMarket.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerChildMarket)).EndInit();
            this.splitContainerChildMarket.ResumeLayout(false);
            this.splitContainerChildEntrust.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerChildEntrust)).EndInit();
            this.splitContainerChildEntrust.ResumeLayout(false);
            this.tlPanelCalcEntrust.ResumeLayout(false);
            this.buysellPanel.ResumeLayout(false);
            this.buysellPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBuySell)).EndInit();
            this.panelEntrust.ResumeLayout(false);
            this.tabParentEntrustFlow.ResumeLayout(false);
            this.tlPanelParentEntrustFlow.ResumeLayout(false);
            this.tlPanelParentEntrustFlow.PerformLayout();
            this.toolStripEntrustFlow.ResumeLayout(false);
            this.toolStripEntrustFlow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntrustFlow)).EndInit();
            this.panelParentEntrustFlow.ResumeLayout(false);
            this.tabParentDealFlow.ResumeLayout(false);
            this.tlPanelParentDealFlow.ResumeLayout(false);
            this.tlPanelParentDealFlow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDealFlow)).EndInit();
            this.toolStripDealFlow.ResumeLayout(false);
            this.toolStripDealFlow.PerformLayout();
            this.panelAddCopies.ResumeLayout(false);
            this.panelAddCopies.PerformLayout();
            this.panelChildCmdSecurity.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlPanelMain;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabParentCmdTrading;
        private System.Windows.Forms.SplitContainer spContainerParentCmdTrading;
        private System.Windows.Forms.TabPage tabParentEntrustFlow;
        private System.Windows.Forms.TabPage tabParentDealFlow;
        private System.Windows.Forms.SplitContainer spContainerChildCmdTrading;
        private System.Windows.Forms.SplitContainer splitContainerChildMarket;
        private System.Windows.Forms.SplitContainer splitContainerChildEntrust;
        private System.Windows.Forms.TableLayoutPanel tlPanelParentCommand;
        private System.Windows.Forms.TabControl tabControlCmdDetail;
        private System.Windows.Forms.TabPage tabChildCmdSecurity;
        private System.Windows.Forms.TableLayoutPanel tlPanelChildCmdSecurity;
        private System.Windows.Forms.TabPage tabChildEntrustFlow;
        private System.Windows.Forms.TabPage tabChildDealFlow;
        private System.Windows.Forms.TableLayoutPanel tlPanelParentEntrustFlow;
        private System.Windows.Forms.TableLayoutPanel tlPanelParentDealFlow;
        private System.Windows.Forms.TableLayoutPanel tlPanelCalcEntrust;
        private System.Windows.Forms.Panel buysellPanel;
        private System.Windows.Forms.Button btnEntrust;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.ComboBox comboBoxFutureSell;
        private System.Windows.Forms.ComboBox comboBoxFutureBuy;
        private System.Windows.Forms.ComboBox comboBoxSpotSell;
        private System.Windows.Forms.ComboBox comboBoxSpotBuy;
        private System.Windows.Forms.Label lblFuturesSellPrice;
        private System.Windows.Forms.Label lblFuturesBuyPrice;
        private System.Windows.Forms.Label lblSpotSellPrice;
        private System.Windows.Forms.Label lblSpotBuyPrice;
        private System.Windows.Forms.Panel panelEntrust;
        private System.Windows.Forms.Button btnEntrusting;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem sysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton tsbMainRefresh;
        private System.Windows.Forms.ToolStripButton tsbMainOpen;
        private System.Windows.Forms.ToolStripButton tsbMainSave;
        private System.Windows.Forms.ToolStripButton tsbMainSwitchOp;

        private HSGridView dataGridViewCmdTrading;
        private HSGridView dataGridViewCmdSecurity;
        private HSGridView dataGridViewBuySell;
        private HSGridView dataGridViewEntrustFlow;
        private HSGridView dataGridViewDealFlow;

        //private System.Windows.Forms.DataGridView dataGridViewCmdTrading;
        //private System.Windows.Forms.DataGridView dataGridViewCmdSecurity;
        //private System.Windows.Forms.DataGridView dataGridViewBuySell;
        //private System.Windows.Forms.DataGridView dataGridViewEntrustFlow;
        //private System.Windows.Forms.DataGridView dataGridViewDealFlow;

        private System.Windows.Forms.ToolStrip toolStripCmdTrading;
        private System.Windows.Forms.ToolStripButton tsbCmdRefresh;
        private System.Windows.Forms.ToolStripButton tsbCmdUndo;
        private System.Windows.Forms.ToolStripButton tsbCmdCancelRedo;
        private System.Windows.Forms.ToolStrip toolStripEntrustFlow;
        private System.Windows.Forms.ToolStripButton tsbEFRefresh;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStrip toolStripDealFlow;
        private System.Windows.Forms.ToolStripButton tsbDFRefresh;
        private System.Windows.Forms.ToolStripButton toolStripButton11;
        private System.Windows.Forms.Panel panelParentEntrustFlow;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnCancelAdd;
        private System.Windows.Forms.Button btnCancelSelect;
        private System.Windows.Forms.TableLayoutPanel tlPanelChildEntrustFlow;
        private System.Windows.Forms.TableLayoutPanel tlPanelChildDealFlow;
        private System.Windows.Forms.ToolStripButton tsbCmdCancelAdd;
        private System.Windows.Forms.Panel panelCmdTradingBottom;
        private System.Windows.Forms.Button btnCmdUnSelectAll;
        private System.Windows.Forms.Button btnCmdSelectAll;
        private System.Windows.Forms.Panel panelAddCopies;
        private System.Windows.Forms.Label lblCopyUnit;
        private System.Windows.Forms.TextBox txtBoxAddCopies;
        private System.Windows.Forms.Label labelAddCopies;
        private System.Windows.Forms.Panel panelChildCmdSecurity;
        private System.Windows.Forms.Button btnCmdSecurityUnSelectAll;
        private System.Windows.Forms.Button btnCmdSecuritySelectAll;
    }
}