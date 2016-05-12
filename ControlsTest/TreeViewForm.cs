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
                Title = "默认资产单元1",
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

            List<TSNavNodeData> nodeDatas = new List<TSNavNodeData>(){tsNodeData};
            TreeViewHelper.AddTreeViewNode(this.tsTreeView1, null, nodeDatas);

            TreeViewHelper.AddTreeViewNode(this.treeView1, null, nodeDatas);
            this.tsTreeView1.Width = 250;
        }
    }
}
