using BLL.Manager;
using BLL.Permission;
using Config;
using Controls.Entity;
using System.Collections.Generic;
using System.Windows.Forms;
using TradingSystem.Controller;
using TradingSystem.Dialog;
using UFX;

namespace TradingSystem.View
{
    public partial class MainForm : Form
    {
        private GridConfig _gridConfig;
        private T2SDKWrap _t2SDKWrap;
        private PermissionManager _permissionManager = new PermissionManager();

        //private Dictionary<string, Forms.BaseForm> _childFormMap = new Dictionary<string, Forms.BaseForm>();

        public MainForm()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.MainForm_Load);
            _systemMenuItem.Click += new System.EventHandler(MenuItem_Click_System);
            _viewMenuItem.Click += new System.EventHandler(MenuItem_Click_View);
            _toolMenuItem.Click += new System.EventHandler(MenuItem_Click_Tool);
            _helpMenuItem.Click += new System.EventHandler(MenuItem_Click_Help);

            _tbOpen.Click += new System.EventHandler(ToolStripButton_Click_Open);
            _tbSave.Click += new System.EventHandler(ToolStripButton_Click_Save);
            _tbRefresh.Click += new System.EventHandler(ToolStripButton_Click_Refresh);
        }

        public MainForm(GridConfig gridConfig, T2SDKWrap t2SDKWrap)
            :this()
        {
            _gridConfig = gridConfig;
            _t2SDKWrap = t2SDKWrap;
        }

        #region event handler
        private void MainForm_Load(object sender, System.EventArgs e)
        {
            //Set the user name into the statuslabel
            var user = LoginManager.Instance.GetUser();
            if (user != null)
            {
                this.tsslUser.Text = user.Name;
            }
            int userId = LoginManager.Instance.GetUserId();

            //load the navbar in the left panel
            var nodes = ConfigManager.Instance.GetNavbarConfig().BarDataList;
            var validNodes = GetValidNavItems(userId, nodes);
            if (validNodes != null)
            {
                foreach (var node in validNodes)
                {
                    if (_permissionManager.HasFeaturePermission(userId, node.Id, Model.Permission.PermissionMask.View))
                    {
                        Controls.TSNavBarItem tsNavBarItem = _navBarContainer.AddBar();
                        tsNavBarItem.Title = node.Title;
                        //tsNavBarItem.Dock = DockStyle.Top;
                        tsNavBarItem.AddTreeNode(node.Children);
                        tsNavBarItem.TreeView.NodeCollapseImage = _imageList.Images[0];
                        tsNavBarItem.TreeView.NodeExpandedImage = _imageList.Images[1];
                        tsNavBarItem.TreeView.NodeImage = _imageList.Images[2];
                    }
                }

                //Set the default expand
                const int index = 0;
                string featureId = string.Empty;
                if (validNodes.Count > 0 && validNodes[index].Children != null && validNodes[index].Children.Count > 0)
                {
                    featureId = validNodes[index].Children[0].Id;

                    _navBarContainer.ExpandDefaultBar(index);
                    FormManager.Instance.ActiveForm(this, _panelMain, featureId, _gridConfig, UFXBLLManager.Instance);
                }
            }
        }

        private void TreeView_ItemClick(object sender, global::Controls.TreeViewItemArgs e)
        {
            if (e == null || e.TreeNodeEvent == null || e.TreeNodeEvent.Name == null)
            {
                return;
            }

            Forms.BaseForm form = FormManager.Instance.ActiveForm(this, _panelMain, e.TreeNodeEvent.Name, _gridConfig, UFXBLLManager.Instance);

            //TODO:
        }
        #endregion

        #region left panel nav items

        private List<TSNavNodeData> GetValidNavItems(int userId, List<TSNavNodeData> allNavItems)
        {
            var navItems = new List<TSNavNodeData>();
            foreach (var navItem in allNavItems)
            {
                if (_permissionManager.HasFeaturePermission(userId, navItem.Id, Model.Permission.PermissionMask.View))
                {
                    var newNavItem = new TSNavNodeData 
                    {
                        Id = navItem.Id,
                        IsExpansed = navItem.IsExpansed,
                        Title = navItem.Title,
                        ParentId = navItem.ParentId,
                        Children = new List<TSNavNodeData>()
                    };

                    if (navItem.Children != null && navItem.Children.Count > 0)
                    {
                        var children = GetValidNavItems(userId, navItem.Children);
                        if (children.Count > 0)
                        {
                            newNavItem.Children.AddRange(children);
                        }
                    }

                    navItems.Add(newNavItem);
                }
            }

            return navItems;
        }

        #endregion

        #region MenuItem click event handler

        private void MenuItem_Click_System(object sender, System.EventArgs e)
        {
            MessageDialog.Info(this, "菜单未完成");
        }

        private void MenuItem_Click_View(object sender, System.EventArgs e)
        {
            MessageDialog.Info(this, "菜单未完成");
        }

        private void MenuItem_Click_Tool(object sender, System.EventArgs e)
        {
            MessageDialog.Info(this, "菜单未完成");
        }

        private void MenuItem_Click_Help(object sender, System.EventArgs e)
        {
            MessageDialog.Info(this, "菜单未完成");
        }

        #endregion

        #region ToolStripButton click event handler

        private void ToolStripButton_Click_Open(object sender, System.EventArgs e)
        {
            MessageDialog.Info(this, "菜单未完成");
        }

        private void ToolStripButton_Click_Save(object sender, System.EventArgs e)
        {
            //MessageDialog.Info(this, "菜单未完成");
            GeneralSettingDailog dialog = new GeneralSettingDailog();
            dialog.OnLoadControl(null, null);
            dialog.OnLoadData(null, null);
            dialog.ShowDialog();
            if (dialog.DialogResult == System.Windows.Forms.DialogResult.OK)
            {

            }
            else
            { 
                
            }
        }

        private void ToolStripButton_Click_Refresh(object sender, System.EventArgs e)
        {
            MessageDialog.Info(this, "菜单未完成");
        }

        #endregion

        #region message notify

        //TODO

        #endregion
    }
}
