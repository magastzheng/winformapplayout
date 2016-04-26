﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controls
{
    public class TreeViewItemArgs : MouseEventArgs
    {
        public readonly TreeNode TreeNodeEvent;
        public TreeViewItemArgs(MouseEventArgs e, TreeNode node)
            : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
            TreeNodeEvent = node;
        }
    }

    public delegate void TreeViewItemClick(object sender, TreeViewItemArgs e);
}
