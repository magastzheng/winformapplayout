using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Controls.Entity;

namespace Controls
{
    //public delegate void ChangeTitle(string title);

    public partial class AutoComplete : UserControl
    {
        //public event ChangeTitle 
        //private AutoItem _currentItem;
        //public AutoItem CurrentItem { get { return _currentItem; } }

        private AutoItemManager _dataManager;
        public IList<AutoItem> AutoDataSource
        {
            set
            {
                if(_dataManager == null)
                {
                    _dataManager = new AutoItemManager();
                }
                _dataManager.DataSource = value;
            }
        }

        //public string Title 
        //{
        //    set 
        //    {
        //        this.lblTitle.Text = value;
        //    }
        //}

        private ListBox lbDropdown;

        public AutoComplete()
        {
            InitializeComponent();
        }

        public void SetDropdownList(ListBox listBox)
        {
            lbDropdown = listBox;
            lbDropdown.Visible = false;
            lbDropdown.DrawMode = DrawMode.OwnerDrawFixed;
            lbDropdown.Click += new EventHandler(ListBox_Click);
            lbDropdown.MouseMove += new MouseEventHandler(ListBox_MouseMove);
            lbDropdown.DrawItem += new DrawItemEventHandler(ListBox_DrawItem);
        }

        public AutoItem GetCurrentItem()
        {
            AutoItem autoItem = new AutoItem();
            autoItem.Id = this.tbInputId.Text.Trim();
            autoItem.Name = this.tbName.Text.Trim();

            return autoItem;
        }

        public void SetCurrentItem(AutoItem autoItem)
        {
            this.tbInputId.Text = autoItem.Id;
            this.tbName.Text = autoItem.Name;
        }
        #region event handler

        /// <summary>
        /// 输入文本框，键盘按下松开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;

            //清除tbName
            tbName.Text = string.Empty;

            ListBox listBox = this.lbDropdown;
            //上下左右键
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
            {
                if (listBox.SelectedIndex > 0)
                {
                    listBox.SelectedIndex--;
                }
            }
            else if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (listBox.SelectedIndex < listBox.Items.Count - 1)
                {
                    listBox.SelectedIndex++;
                }
            }
            //回车
            else if (e.KeyCode == Keys.Enter)
            {
                AutoItem item = listBox.SelectedItem as AutoItem;
                textBox.Text = item.Id;
                this.tbName.Text = item.Name;
                listBox.Visible = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    IList<AutoItem> dataSource = _dataManager.GetMatch(textBox.Text.Trim());
                    if (dataSource.Count > 0)
                    {
                        listBox.DataSource = dataSource;
                        listBox.DisplayMember = "Id";
                        listBox.ValueMember = "Id";
                        listBox.Visible = true;
                    }
                    else
                    {
                        listBox.Visible = false;
                    }
                }
                else
                {
                    listBox.Visible = false;
                }
            }

            //光标定位到文本框最后
            textBox.Select(textBox.Text.Length, 1);
        }

        private void TextBox_Enter(object sender, System.EventArgs e)
        {
            lbDropdown.Visible = false;
        }

        private void ListBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            listBox.SelectedIndex = listBox.IndexFromPoint(e.Location);
        }

        /// <summary>
        /// listbox各项绘制，为了可以调整Item的高度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            AutoItem item = (sender as ListBox).Items[e.Index] as AutoItem;
            string text = string.Format("{0}  {1}", item.Id, item.Name);
            e.Graphics.DrawString(text, e.Font, new SolidBrush(e.ForeColor), e.Bounds);

        }

        /// <summary>
        /// listbox的点击事件，为了给textbox赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_Click(object sender, System.EventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox == null)
                return;
            AutoItem item = listBox.SelectedItem as AutoItem;
            this.tbInputId.Text = item.Id;
            this.tbName.Text = item.Name;
            listBox.Visible = false;

            //光标点位到最后
            this.tbInputId.Select(tbInputId.Text.Length, 1);
        }

        #endregion

        #region


        #endregion
    }
}
