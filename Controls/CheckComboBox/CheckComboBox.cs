using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Controls.CheckComboBox
{
    public partial class CheckComboBox : ComboBox
    {
        //===============internal Dropdown===================
        internal class Dropdown : Form
        {
            internal class CCBoxEventArgs : EventArgs
            {
                private bool _assignValues;
                public bool AssignValues
                {
                    get { return _assignValues; }
                    set { _assignValues = value; }
                }

                private EventArgs _e;
                public EventArgs EventArgs
                {
                    get { return _e; }
                    set { _e = value; }
                }

                public CCBoxEventArgs(EventArgs e, bool assignValues)
                    : base()
                {
                    this._e = e;
                    this._assignValues = assignValues;
                }
            }

            internal class CustomCheckedListBox : CheckedListBox
            {
                private int _curSelIndex = -1;

                public CustomCheckedListBox()
                    : base()
                {
                    this.SelectionMode = System.Windows.Forms.SelectionMode.One;
                    this.HorizontalScrollbar = true;
                }

                protected override void OnKeyDown(KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        //selection
                        ((Dropdown)Parent).OnDeactivate(new CCBoxEventArgs(null, true));
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Escape)
                    {
                        //cancel selection
                        ((Dropdown)Parent).OnDeactivate(new CCBoxEventArgs(null, false));
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Delete)
                    {
                        //delete unchecks all, [Shift+Delete] checks all.
                        for (int i = 0, count = Items.Count; i < count; i++)
                        {
                            SetItemChecked(i, e.Shift);
                        }
                        e.Handled = true;
                    }

                    base.OnKeyDown(e);
                }

                protected override void OnMouseMove(MouseEventArgs e)
                {
                    base.OnMouseMove(e);
                    int index = IndexFromPoint(e.Location);
                    if ((index >= 0) && (index != _curSelIndex))
                    {
                        _curSelIndex = index;
                        SetSelected(index, true);
                    }
                }
            }
        
            //==============Data==============

            private CheckComboBox _ccbParent;
            private string _oldStrValue = string.Empty;
            public bool ValueChanged
            {
                get
                {
                    string newStrValue = _ccbParent.Text;
                    if ((_oldStrValue.Length > 0) && (newStrValue.Length > 0))
                    {
                        return (_oldStrValue.CompareTo(newStrValue) != 0);
                    }
                    else
                    {
                        return (_oldStrValue.Length != newStrValue.Length);
                    }
                }
            }

            bool[] _checkedStateArr;

            //Whether the dropdown is closed.
            private bool _dropdownClosed = true;

            private CustomCheckedListBox _cclb;
            public CustomCheckedListBox List
            {
                get { return _cclb; }
                set { _cclb = value; }
            }

            public Dropdown(CheckComboBox ccbParent)
            {
                this._ccbParent = ccbParent;
                InitializeComponent();
                this.ShowInTaskbar = false;

                this._cclb.ItemCheck += new ItemCheckEventHandler(this.cclb_ItemCheck);
            }

            private void InitializeComponent()
            {
                this._cclb = new CustomCheckedListBox();
                this.SuspendLayout();

                //cclb
                this._cclb.BorderStyle = BorderStyle.None;
                this._cclb.Dock = DockStyle.Fill;
                this._cclb.FormattingEnabled = true;
                this._cclb.Location = new Point(0, 0);
                this._cclb.Name = "cclb";
                this._cclb.Size = new Size(47, 15);
                this._cclb.TabIndex = 0;

                //Dropdown
                this.AutoScaleDimensions = new SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.SystemColors.Menu;
                this.ClientSize = new Size(47, 16);
                this.ControlBox = false;
                this.Controls.Add(this._cclb);
                this.ForeColor = System.Drawing.SystemColors.ControlText;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                this.MinimizeBox = false;
                this.Name = "ccbParent";
                this.StartPosition = FormStartPosition.Manual;
                this.ResumeLayout();
            }

            public string GetCheckedItemsStringValue()
            {
                StringBuilder sb = new StringBuilder("");
                for (int i = 0, count = this._cclb.CheckedItems.Count; i < count; i++)
                {
                    sb.Append(this._cclb.GetItemText(this._cclb.CheckedItems[i])).Append(this._ccbParent.ValueSeparator);
                }
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - this._ccbParent.ValueSeparator.Length, this._ccbParent.ValueSeparator.Length);
                }

                return sb.ToString();
            }

            public void CloseDropdown(bool enactChanges)
            {
                if (this._dropdownClosed)
                {
                    return;
                }

                if (enactChanges)
                {
                    this._ccbParent.SelectedIndex = -1;
                    this._ccbParent.Text = GetCheckedItemsStringValue();
                }
                else
                {
                    for (int i = 0, count = this._cclb.Items.Count; i < count; i++)
                    {
                        this._cclb.SetItemChecked(i, this._checkedStateArr[i]);
                    }
                }

                //Set the flag to prevent OnDeactivate() calling the method once again
                //after hiding this window.
                this._dropdownClosed = true;
                this._ccbParent.Focus();
                this.Hide();
                this._ccbParent.OnDropDownClosed(new CCBoxEventArgs(null, false));
            }

            protected override void OnActivated(EventArgs e)
            {
                Debug.WriteLine("OnActivated....");
                base.OnActivated(e);
                this._dropdownClosed = false;
                this._oldStrValue = this._ccbParent.Text;
                this._checkedStateArr = new bool[this._cclb.Items.Count];
                for (int i = 0, count = this._cclb.Items.Count; i < count; i++)
                {
                    this._checkedStateArr[i] = this._cclb.GetItemChecked(i);
                }
            }

            protected override void OnDeactivate(EventArgs e)
            {
                Debug.WriteLine("OnDeactivate...");
                base.OnDeactivate(e);

                CCBoxEventArgs ce = e as CCBoxEventArgs;
                if (ce != null)
                {
                    CloseDropdown(ce.AssignValues);
                }
                else
                {
                    //call from framework.
                    CloseDropdown(true);
                }
            }

            private void cclb_ItemCheck(object sender, ItemCheckEventArgs e)
            {
                if (_ccbParent.ItemCheck != null)
                {
                    _ccbParent.ItemCheck(sender, e);
                }
            }
        }

        private Dropdown _dropdown;
        private string _valueSeparator;
        public string ValueSeparator
        {
            get { return _valueSeparator; }
            set { _valueSeparator = value; }
        }

        public bool CheckOnClick
        {
            get { return _dropdown.List.CheckOnClick; }
            set { _dropdown.List.CheckOnClick = value; }
        }

        public new string DisplayMember
        {
            get { return _dropdown.List.DisplayMember; }
            set { _dropdown.List.DisplayMember = value; }
        }

        public new CheckedListBox.ObjectCollection Items
        {
            get { return _dropdown.List.Items; }
        }

        public CheckedListBox.CheckedItemCollection CheckedItems
        {
            get { return _dropdown.List.CheckedItems; }
        }

        public CheckedListBox.CheckedIndexCollection CheckedIndices
        {
            get { return _dropdown.List.CheckedIndices; }
        }

        public bool ValueChanged
        {
            get { return _dropdown.ValueChanged; }
        }

        public event ItemCheckEventHandler ItemCheck;

        public CheckComboBox()
            :base()
        {
            InitializeComponent();

            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this._valueSeparator = ",";
            this.DropDownHeight = 1;
            this.DropDownStyle = ComboBoxStyle.DropDown;
            this._dropdown = new Dropdown(this);
            this.CheckOnClick = true;
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);
            DoDropDown();
        }

        private void DoDropDown()
        {
            if (!_dropdown.Visible)
            {
                Rectangle rect = RectangleToScreen(this.ClientRectangle);
                _dropdown.Location = new Point(rect.X, rect.Y + this.Size.Height);
                int count = _dropdown.List.Items.Count;
                if (count > this.MaxDropDownItems)
                {
                    count = this.MaxDropDownItems;
                }
                else if (count == 0)
                {
                    count = 1;
                }

                _dropdown.Size = new Size(this.Size.Width, (_dropdown.List.ItemHeight) * count + 2);
                _dropdown.Show(this);
            }
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            if (e is Dropdown.CCBoxEventArgs)
            {
                base.OnDropDownClosed(e);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                OnDropDown(null);
            }

            e.Handled = !e.Alt && !(e.KeyCode == Keys.Tab)
                &&!((e.KeyCode == Keys.Left)||(e.KeyCode == Keys.Right) || (e.KeyCode == Keys.Home) || (e.KeyCode == Keys.End));

            base.OnKeyDown(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            e.Handled = true;
            base.OnKeyPress(e);
        }

        public bool GetItemChecked(int index)
        {
            if (index < 0 || index > Items.Count)
            {
                throw new ArgumentOutOfRangeException("index", "value out of range");
            }
            else
            {
                return _dropdown.List.GetItemChecked(index);
            }
        }

        public void SetItemChecked(int index, bool isChecked)
        {
            if (index < 0 || index > Items.Count)
            {
                throw new ArgumentOutOfRangeException("index", "value out of range");
            }
            else
            {
                _dropdown.List.SetItemChecked(index, isChecked);

                this.Text = _dropdown.GetCheckedItemsStringValue();
            }
        }

        public CheckState GetItemCheckState(int index)
        {
            if (index < 0 || index > Items.Count)
            {
                throw new ArgumentOutOfRangeException("index", "value out of range");
            }
            else
            {
                return _dropdown.List.GetItemCheckState(index);
            }
        }

        public void SetItemCheckState(int index, CheckState state)
        {
            if (index < 0 || index > Items.Count)
            {
                throw new ArgumentOutOfRangeException("index", "value out of range");
            }
            else
            {
                _dropdown.List.SetItemCheckState(index, state);

                this.Text = _dropdown.GetCheckedItemsStringValue();
            }
        }
    }
}
