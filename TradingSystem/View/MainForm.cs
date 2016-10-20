using BLL;
using BLL.UFX;
using Config;
using System.Windows.Forms;
using TradingSystem.Controller;

namespace TradingSystem.View
{
    public partial class MainForm : Form
    {
        private GridConfig _gridConfig;
        private T2SDKWrap _t2SDKWrap;

        //private Dictionary<string, Forms.BaseForm> _childFormMap = new Dictionary<string, Forms.BaseForm>();

        public MainForm()
        {
            InitializeComponent();
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

            //load the navbar in the left panel
            var nodes = ConfigManager.Instance.GetNavbarConfig().BarDataList;
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    Controls.TSNavBarItem tsNavBarItem = _navBarContainer.AddBar();
                    tsNavBarItem.Title = node.Title;
                    //tsNavBarItem.Dock = DockStyle.Top;
                    tsNavBarItem.AddTreeNode(node.Children);
                    tsNavBarItem.TreeView.NodeCollapseImage = _imageList.Images[0];
                    tsNavBarItem.TreeView.NodeExpandedImage = _imageList.Images[1];
                }

                //_navBarContainer.SwitchBarState(1);
                _navBarContainer.ExpandDefaultBar(1);
                FormManager.Instance.ActiveForm(this, _panelMain, "open", _gridConfig, BLLManager.Instance);
            }
        }

        private void TreeView_ItemClick(object sender, global::Controls.TreeViewItemArgs e)
        {
            if (e == null || e.TreeNodeEvent == null || e.TreeNodeEvent.Name == null)
            {
                return;
            }

            Forms.BaseForm form = FormManager.Instance.ActiveForm(this, _panelMain, e.TreeNodeEvent.Name, _gridConfig, BLLManager.Instance);

            //TODO:
        }
        #endregion
    }
}
