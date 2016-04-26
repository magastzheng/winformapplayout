using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controls
{
    public class TSTreeView: TreeView
    {
        private Font _nodeFont;
        private Brush _backgroundBrush;
        private Pen _backgroundPen;
        private Image _nodeExpandedImage;
        private Image _nodeCollapseImage;
        private Size _nodeImageSize;
        private int _nodeOffset;

        public Font NodeFont
        {
            get { return _nodeFont; }
            set { _nodeFont = value; }
        }

        public Brush BackgroundBrush
        {
            get { return _backgroundBrush; }
            set { _backgroundBrush = value; }
        }

        public Pen BackgroundPen
        {
            get { return _backgroundPen; }
            set { _backgroundPen = value; }
        }

        public Image NodeExpandedImage
        {
            get { return _nodeExpandedImage; }
            set { _nodeExpandedImage = value; }
        }

        public Image NodeCollapseImage
        {
            get { return _nodeCollapseImage; }
            set { _nodeCollapseImage = value; }
        }

        public Size NodeImageSize
        {
            get { return _nodeImageSize; }
            set { _nodeImageSize = value; }
        }

        public int NodeOffset
        {
            get { return _nodeOffset; }
            set { _nodeOffset = value; }
        }

        public TSTreeView()
        {
            DrawMode = TreeViewDrawMode.OwnerDrawAll;
            this.ShowPlusMinus = false;
            this.ShowLines = false;
            this.CheckBoxes = false;
            this.ItemHeight = 24;

            _backgroundBrush = new SolidBrush(Color.FromArgb(90, Color.FromArgb(205, 226, 252)));
            _backgroundPen = new Pen(Color.FromArgb(130, 249, 252), 1);
            _nodeFont = new Font("微软雅黑", 12, FontStyle.Regular);

            _nodeExpandedImage = null;
            _nodeCollapseImage = null;
            _nodeImageSize = new Size(18, 18);

            _nodeOffset = 5;

            this.DrawNode += new DrawTreeNodeEventHandler(TreeView_DrawNode);
            this.AfterSelect += new TreeViewEventHandler(TreeView_AfterSelect);
            this.AfterLabelEdit += new NodeLabelEditEventHandler(TreeView_AfterLabelEdit);
            this.MouseDown += new MouseEventHandler(TreeView_MouseDown);
            //this.MouseDoubleClick += new MouseEventHandler(TreeView_MouseDoubleClick);
            this.MouseClick += new MouseEventHandler(TreeView_MouseClick);

        }

        private void TreeView_MouseClick(object sender, MouseEventArgs e)
        {
            TreeNode node = GetNodeAt(e.X, e.Y);
            if (node != null && NodeBounds(node).Contains(e.X, e.Y))
            {
                this.SelectedNode = node;
                if (node.Nodes.Count != 0)
                {
                    if (node.IsExpanded)
                    {
                        node.Collapse();
                    }
                    else
                    {
                        node.Expand();
                    }
                }
            }
        }

        private void TreeView_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode node = GetNodeAt(e.X, e.Y);
            if (node != null && NodeBounds(node).Contains(e.X, e.Y))
            {
                this.SelectedNode = node;
            }
        }

        private void TreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void TreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if(sender == null || e == null || e.Node == null)
                return;
            TreeNode node = e.Node;
            Graphics g = e.Graphics;

            //设置Image绘制矩形
            Point pt = new Point(node.Bounds.X + _nodeOffset, node.Bounds.Y);
            Rectangle rect = new Rectangle(pt, _nodeImageSize);


            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                //绘制TreeNode选择后的背景框
                g.FillRectangle(_backgroundBrush, 2, node.Bounds.Y, Width, node.Bounds.Height);

                //绘制TreeNode选择后的边框线条
                g.DrawRectangle(_backgroundPen, 1, node.Bounds.Y, Width, node.Bounds.Height);
            }

            //绘制节点图片
            if (_nodeExpandedImage != null && _nodeCollapseImage != null)
            {
                if (node.Nodes.Count != 0)
                {
                    if (node.IsExpanded == true)
                    {
                        g.DrawImage(_nodeExpandedImage, rect);
                    }
                    else
                    {
                        g.DrawImage(_nodeCollapseImage, rect);
                    }
                }

                rect.X += 15;
            }

            //绘制节点自身图片
            if (node.SelectedImageIndex != -1 && this.ImageList != null)
            {
                rect.X += 5;
                g.DrawImage(ImageList.Images[node.SelectedImageIndex], rect);
            }

            //绘制节点的文本
            rect.X += 20;
            rect.Y += 1;
            rect.Width = Width - rect.X;
            g.DrawString(node.Text, _nodeFont, Brushes.Black, rect);
        }

        private Rectangle NodeBounds(TreeNode node)
        {
            Rectangle bounds = node.Bounds;
            //if (node.Tag != null)
            //{
            //    Graphics g = this.CreateGraphics();
            //    int tagWidth = (int)g.MeasureString(node.Tag.ToString(), _nodeFont).Width + 6;
            //    bounds.Offset(tagWidth / 2, 0);
            //    bounds = Rectangle.Inflate(bounds, tagWidth / 2, 0);
            //    g.Dispose();
            //}

            bounds.Width = this.Width;

            return bounds;
        }
    }
}
