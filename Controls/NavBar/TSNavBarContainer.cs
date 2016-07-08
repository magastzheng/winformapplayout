using System;
using System.Drawing;
using System.Windows.Forms;

namespace Controls
{
    public partial class TSNavBarContainer : UserControl
    {
        public event TreeViewItemClick LeafItemClick;

        private TSNavBarItems _navBarItems;

        public TSNavBarItems Bars
        {
            get { return _navBarItems; }
        }

        public TSNavBarItem this[int index]
        {
            get
            {
                if (index > -1)
                {
                    return this._navBarItems[index];
                }
                else
                {
                    return null;
                }
            }
        }

        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                SelectBar(value);
            }
        }

        private int _barSpace = 1;
        public int BarSpace
        {
            get { return _barSpace; }
            set { _barSpace = value; }
        }

        private int _barMargin = 5;
        public int BarMargin
        {
            get { return _barMargin; }
            set { _barMargin = value; }
        }


        public TSNavBarContainer()
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(112, 140, 225);
            this._navBarItems = new TSNavBarItems(this);
            this.AutoScroll = true;
            this.SizeChanged += new EventHandler(TSNavBarContainer_SizeChanged);
        }

        private void TSNavBarContainer_SizeChanged(object sender, EventArgs e)
        {
            SetLayout();
        }

        private int ValidTop
        {
            get { return Top + Margin.Top + Padding.Top; }
        }

        private int ValidBottom
        {
            get { return Bottom - Margin.Bottom - Padding.Bottom; }
        }

        public void SwitchBarState(int index)
        {
            for (int i = 0, count = this._navBarItems.Count; i < count; i++)
            {
                if (i != index && this._navBarItems[i].BarState == TSNavBarItemState.Expand)
                {
                    this._navBarItems[i].BarState = TSNavBarItemState.Collapse;
                }
            }
        }

        public void SetSelectedBarHeight()
        {
            int height = this.Height;

            if (_selectedIndex >= 0 && _selectedIndex < this._navBarItems.Count)
            {
                if (_selectedIndex == 0)
                {
                    if (this._navBarItems.Count == 1)
                    {
                        height = this.ValidBottom - this._navBarItems[_selectedIndex].Top - this._barSpace;
                    }
                    else
                    {
                        height = this._navBarItems[_selectedIndex + 1].Top - this._navBarItems[_selectedIndex].Top - this._barSpace;
                    }
                }
                else if (_selectedIndex == this._navBarItems.Count - 1)
                {
                    height = this.ValidBottom - this._navBarItems[_selectedIndex - 1].Bottom - this._barSpace;
                }
                else
                {
                    height = this._navBarItems[_selectedIndex + 1].Top - this._navBarItems[_selectedIndex - 1].Bottom - this._barSpace;
                }
            }

            this._navBarItems[_selectedIndex].SetHeight(height);
        }

        public void SetLayout()
        {
            //set the collapse bar height
            //set the bar width to fill the container
            for (int i = 0, count = this._navBarItems.Count; i < count; i++)
            {
                if (this._navBarItems[i].BarState == TSNavBarItemState.Collapse)
                {
                    this._navBarItems[i].SetHeight();
                }

                this._navBarItems[i].Left = this.Left + this.Margin.Left;
                this._navBarItems[i].Width = this.Width - this.Margin.Left - this.Margin.Right;
            }

            //set the bar top
            if (_selectedIndex >= 0 && _selectedIndex < this._navBarItems.Count)
            {

                //The bars at the top of selected one
                for (int i = 0; i <= _selectedIndex; i++)
                {
                    if (i == 0)
                    {
                        this._navBarItems[i].Top = 0;// this.ValidTop;
                    }
                    else
                    {
                        this._navBarItems[i].Top = this._navBarItems[i - 1].Bottom + this._barSpace;
                    }

                    this._navBarItems[i].BarIndex = i;
                }

                //The bars at the bottom of selected one
                for (int i = this._navBarItems.Count - 1; i > _selectedIndex; i--)
                {
                    this._navBarItems[i].BarIndex = i;
                    if (i == this._navBarItems.Count - 1)
                    {
                        this._navBarItems[i].Top = this.ValidBottom - this._navBarItems[i].Height - this._barSpace;
                    }
                    else
                    {
                        this._navBarItems[i].Top = this._navBarItems[i + 1].Top - this._navBarItems[i].Height - this._barSpace;
                    }
                }

                SetSelectedBarHeight();
            }
        }

        public void SetLayout(TSNavBarItem item)
        {
            this.SuspendLayout();
            this.Controls.Add(item);
            if (_navBarItems.Count == 0)
            {
                item.Top = this.Top;
            }
            else
            {
                item.Top = this[_navBarItems.Count - 1].Bottom + _barSpace;
            }

            item.Height = item.TitleHeight;
            item.Width = this.Width;
            //other process
            this.ResumeLayout();
        }

        public TSNavBarItem AddBar()
        {
            _navBarItems.Add();
            return _navBarItems[_navBarItems.Count - 1];
        }

        public void RemoveBar(TSNavBarItem item)
        {
            int index = item.BarIndex;
            _navBarItems.Remove(item);
            Controls.Remove(item);
            SetLayout();
        }

        public void RemoveBarAt(int index)
        {
            Controls.Remove(_navBarItems[index]);
            _navBarItems.RemoveAt(index);
            SetLayout();
        }

        private void SelectBar(int index)
        {
            foreach (TSNavBarItem bar in _navBarItems)
            {
                if (bar.BarIndex == index)
                    continue;
                bar.IsSelected = false;
            }
        }

        #region event handler
        public void TreeView_ItemClick(object sender, TreeViewItemArgs e)
        {
            if (LeafItemClick != null)
            {
                LeafItemClick(sender, e);
            }
        }
        #endregion
    }
}
