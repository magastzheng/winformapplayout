using Controls.Entity;
using Controls.NavBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlsTest
{
    public partial class TreeViewForm : Form
    {
        public TreeViewForm()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, System.EventArgs e)
        {
            List<TSNavNodeData> nodeDatas = GetData();
            TreeViewHelper.AddTreeViewNode(this.tsTreeView1, null, nodeDatas);

            TreeViewHelper.AddTreeViewNode(this.treeView1, null, nodeDatas);
            //this.tsTreeView1.Width = 250;
        }

        private List<TSNavNodeData> GetData()
        {
            TSNavNodeData tsNodeData = new TSNavNodeData
            {
                Id = "fund",
                Title = "海蓝宝银",
                IsExpansed = false,
                Children = new List<TSNavNodeData>()
            };

            TSNavNodeData tsItem1 = new TSNavNodeData
            {
                Id = "fund-asset-1",
                Title = "默认资产单元1 - 我要变得更厉害",
                IsExpansed = false,
            };

            tsNodeData.Children.Add(tsItem1);
            TSNavNodeData tsItem2 = new TSNavNodeData
            {
                Id = "fund-asset-2",
                Title = "默认资产单元2",
                IsExpansed = false,
            };
            tsNodeData.Children.Add(tsItem2);

            TSNavNodeData tsItem3 = new TSNavNodeData
            {
                Id = "fund-asset-3",
                Title = "默认资产单元3",
                IsExpansed = false,
            };
            tsNodeData.Children.Add(tsItem3);

            //Add second items
            TSNavNodeData tsNodeData2 = new TSNavNodeData
            {
                Id = "fund2",
                Title = "宝银量化2",
                IsExpansed = false,
                Children = new List<TSNavNodeData>()
            };

            TSNavNodeData tsItem21 = new TSNavNodeData
            {
                Id = "fund2-asset-1",
                Title = "默认资产单元1",
                IsExpansed = false,
            };
            tsNodeData2.Children.Add(tsItem21);

            TSNavNodeData tsItem22 = new TSNavNodeData
            {
                Id = "fund2-asset-2",
                Title = "默认资产单元2",
                IsExpansed = false,
            };
            tsNodeData2.Children.Add(tsItem22);

            TSNavNodeData tsItem23 = new TSNavNodeData
            {
                Id = "fund2-asset-3",
                Title = "默认资产单元3",
                IsExpansed = false,
            };
            tsNodeData2.Children.Add(tsItem23);

            //Add third items
            TSNavNodeData tsNodeData3 = new TSNavNodeData
            {
                Id = "fund3",
                Title = "宝银量化3",
                IsExpansed = false,
                Children = new List<TSNavNodeData>()
            };

            TSNavNodeData tsItem31 = new TSNavNodeData
            {
                Id = "fund3-asset-1",
                Title = "默认资产单元1",
                IsExpansed = false,
            };
            tsNodeData3.Children.Add(tsItem31);

            TSNavNodeData tsItem32 = new TSNavNodeData
            {
                Id = "fund3-asset-2",
                Title = "默认资产单元2",
                IsExpansed = false,
            };
            tsNodeData3.Children.Add(tsItem32);

            TSNavNodeData tsItem33 = new TSNavNodeData
            {
                Id = "fund3-asset-3",
                Title = "默认资产单元3",
                IsExpansed = false,
            };
            tsNodeData3.Children.Add(tsItem33);

            List<TSNavNodeData> nodeDatas = new List<TSNavNodeData>() { tsNodeData, tsNodeData2, tsNodeData3};

            return nodeDatas;
        }
    }
}
