using Controls.Entity;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Controls.NavBar
{
    public static class TreeViewHelper
    {
        public static void AddTreeViewNode(TreeView treeView, TreeNode parent, List<TSNavNodeData> nodeDatas)
        {
            treeView.BeginUpdate();

            foreach (TSNavNodeData nodeData in nodeDatas)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Name = nodeData.Id;
                treeNode.Text = nodeData.Title;
                treeNode.EnsureVisible();
                //treeView.Scrollable = true;

                if (parent == null)
                {
                    treeView.Nodes.Add(treeNode);
                }
                else
                {
                    parent.Nodes.Add(treeNode);
                }

                if (nodeData.Children != null && nodeData.Children.Count > 0)
                {
                    AddTreeViewNode(treeView, treeNode, nodeData.Children);
                }
            }

            treeView.EndUpdate();
        }
    }
}
