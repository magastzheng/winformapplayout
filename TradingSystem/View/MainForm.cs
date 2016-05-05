using Config;
using DBAccess;
using Forms;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util;

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
            Type formType = null;
            bool hasGrid = false;
            string json = string.Empty;
            switch (e.TreeNodeEvent.Name)
            {
                case "open":
                    {
                        if (!_childFormMap.ContainsKey(key))
                        {
                            //form = new TradingForm();
                            //_childFormMap[key] = form;
                            formType = typeof(TradingForm);
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
                            formType = typeof(StockTemplateForm);
                            hasGrid = true;
                            //StockTemplateDAO _dbdao = new StockTemplateDAO();
                            //var items = _dbdao.GetTemplate(-1);
                            //json = JsonUtil.SerializeObject(items);
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

            if (formType != null && form == null)
            {
                if (hasGrid)
                {
                    form = FormManager.LoadForm(this, formType, new object[] { _gridConfig }, json);
                }
                else
                {
                    form = FormManager.LoadForm(this, formType, null, json);
                }
                _childFormMap[key] = form;
            }

            if (form != null)
            {
                ILoadFormActived formActived = form as ILoadFormActived;
                if (formActived != null)
                {
                    //TODO: add the step to load data and refresh the child form
                    formActived.OnLoadFormActived("");
                }

                form.MdiParent = this;
                form.Parent = _panelMain;
                form.Dock = DockStyle.Fill;
                form.BringToFront();
                form.Show();
            }
            else
            { 
                //default fomr
            }
        }
        #endregion
    }
}
