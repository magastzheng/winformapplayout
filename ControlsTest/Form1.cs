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

namespace ControlsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, System.EventArgs e)
        {
            try
            {
                var nodes = ConfigManager.Instance.GetNavbarConfig().BarDataList;
                if (nodes != null)
                {
                    foreach (var node in nodes)
                    {
                        Controls.TSNavBarItem tsNavBarItem = tsNavBarContainer1.AddBar();
                        tsNavBarItem.Title = node.Title;
                        tsNavBarItem.AddTreeNode(node.Children);
                        tsNavBarItem.TreeView.NodeCollapseImage = imageList1.Images[0];
                        tsNavBarItem.TreeView.NodeExpandedImage = imageList1.Images[1];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void TreeView_ItemClick(object sender, global::Controls.TreeViewItemArgs e)
        {
            Console.WriteLine(e.TreeNodeEvent);
        }
    }
}
