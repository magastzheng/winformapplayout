using Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingSystem.View
{
    public partial class MainForm : Form
    {
        private GridConfig _gridConfig;
        private Dictionary<string, Forms.BaseForm> _childFormMap = new Dictionary<string, Forms.BaseForm>();

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(GridConfig gridConfig)
            :this()
        {
            _gridConfig = gridConfig;
        }

        #region event handler
        private void MainForm_Load(object sender, System.EventArgs e)
        {
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
            }
        }

        private void TreeView_ItemClick(object sender, global::Controls.TreeViewItemArgs e)
        {
            if (e == null || e.TreeNodeEvent == null || e.TreeNodeEvent.Name == null)
            {
                return;
            }

            Forms.BaseForm form = null;
            string key = e.TreeNodeEvent.Name;
            switch (e.TreeNodeEvent.Name)
            {
                case "open":
                    {
                        if (!_childFormMap.ContainsKey(key))
                        {
                            form = new TradingForm();
                            //form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                            //form.TopLevel = false;
                            //form.BackColor = Color.BlueViolet;
                            //form.Dock = DockStyle.Fill;
                            _childFormMap[key] = form;
                        }
                        else
                        {
                            form = _childFormMap[key];
                        }
                    }
                    break;
                case "close":
                    break;
                case "commandmanager":
                    break;
                case "currenttemplate":
                    {
                        if (!_childFormMap.ContainsKey(key))
                        {
                            form = new StockTemplateForm(_gridConfig);
                            form.BackColor = Color.DarkGray;
                            //form.Dock = DockStyle.Fill;
                            _childFormMap[key] = form;
                        }
                        else
                        {
                            form = _childFormMap[key];
                        }
                    }
                    break;
                case "historytemplate":
                    break;
                default:
                    break;
            }

            if (form != null)
            {
                _panelMain.Controls.Clear();
                _panelMain.Controls.Add(form);
                form.Show();
            }
        }
        #endregion
    }
}
