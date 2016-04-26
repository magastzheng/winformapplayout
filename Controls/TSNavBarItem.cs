using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model.UI;

namespace Controls
{
    public partial class TSNavBarItem : UserControl
    {
        private List<TSNavNodeData> _data;
        private TSNavBarContainer _owner;

        private int _barIndex;
        private TSNavBarItemState _barState = TSNavBarItemState.Expand;
        //private string _title;
        private int _titleHeight = 40;
        private int _barSpace = 5;
        private int _barMargin = 5;
        private bool _isSelected = false;
        private Color _titleStartColor = Color.White;
        private Color _titleEndColor = Color.FromArgb(199, 211, 247);

        public int BarIndex
        {
            get { return _barIndex; }
            set { _barIndex = value; }
        }

        public TSNavBarItemState BarState
        {
            get { return _barState; }
            set
            {
                _barState = value;
                //SetBarState(value);
            }
        }

        public void SetHeight()
        {
            this.Height = _titleHeight;
            this._button.Height = _titleHeight;
        }

        public void SetHeight(int height)
        {
            this.Height = height;
            this._button.Height = _titleHeight;
            this._tsTreeView.Top = this._button.Top + this._button.Height;
            this._tsTreeView.Height = height - this._button.Height;
        }

        private void SetBarState(TSNavBarItemState state)
        {
            //if (state == NavBarItemState.Collapse)
            //{
            //    this.Height = _button.Height;
            //}
            //else
            //{ 
            //    //TODO: calc the bar height
            //    //_owner.SetExpandHeight();
            //}
            _owner.SwitchBarState(_barIndex);
            _owner.SetLayout();
        }

        public List<TSNavNodeData> Data
        {
            get { return _data; }
        }

        public string Title
        {
            get { return _button.Text; }
            set { _button.Text = value; }
        }


        public int TitleHeight
        {
            get { return _titleHeight; }
            set { _titleHeight = value; }
        }

        public int BarSpace
        {
            get { return _barSpace; }
            set { _barSpace = value; }
        }

        public int BarMargin
        {
            get { return _barMargin; }
            set { _barMargin = value; }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                Invalidate();
            }
        }

        public Color TitleStartColor
        {
            get { return _titleStartColor; }
            set { _titleStartColor = value; }
        }

        public Color TitleEndColor
        {
            get { return _titleEndColor; }
            set { _titleEndColor = value; }
        }

        public TSTreeView TreeView
        {
            get { return _tsTreeView; }
        }

        public TSNavBarItem()
        {
            InitializeComponent();

            //this._title = "NavBarItem";
            this.BackColor = Color.FromArgb(214, 233, 247);
            this._button.Height = _titleHeight;
            this._button.Click += new EventHandler(
                    delegate(object sender, EventArgs e)
                    {
                        IsSelected = true;
                        _owner.SelectedIndex = _barIndex;

                        if (_barState == TSNavBarItemState.Collapse)
                            BarState = TSNavBarItemState.Expand;
                        else
                            BarState = TSNavBarItemState.Collapse;

                        SetBarState(_barState);
                    }
                );
            this._tsTreeView.Top = this.Top + this._button.Height;
        }

        public TSNavBarItem(TSNavBarContainer owner)
            : this()
        {
            _owner = owner;
        }

        public void SetTreeViewHeihgt()
        { 
            
        }

        public void AddTreeNode(List<TSNavNodeData> nodes)
        {
            _data = nodes;
            _tsTreeView.BeginUpdate();

            _tsTreeView.Top = _button.Bottom + _barSpace;
            AddChildrenTreeNode(null, nodes);

            _tsTreeView.EndUpdate();
        }

        public void AddChildrenTreeNode(TreeNode parent, List<TSNavNodeData> nodes)
        {
           
            foreach (var node in nodes)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Name = node.Id;
                treeNode.Text = node.Title;
                if (parent == null)
                {
                    _tsTreeView.Nodes.Add(treeNode);
                }
                else
                {
                    parent.Nodes.Add(treeNode);
                }

                if (node.Children.Count > 0)
                {
                    AddChildrenTreeNode(treeNode, node.Children);
                }
            }
        }

        public void AddTreeNode(List<TreeNode> nodes)
        {
            _tsTreeView.BeginUpdate();
            _tsTreeView.Top = _button.Bottom + _barSpace;
            foreach (var node in nodes)
            {
                _tsTreeView.Nodes.Add(node);
            }
            _tsTreeView.EndUpdate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            IsSelected = true;
            _owner.SelectedIndex = _barIndex;
        }

        private void TreeView_ItemClick(object sender, TreeViewItemArgs e)
        {
            _owner.TreeView_ItemClick(sender, e);
        }
    }
}
