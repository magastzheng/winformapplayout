using Controls.Entity;
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
    public partial class CheckComboBoxForm : Form
    {
        public CheckComboBoxForm()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            var items = GetItems();
            foreach (var item in items)
            {
                int index = this.checkComboBox1.Items.Add(item);
                if (index >= 0 && index <= this.checkComboBox1.Items.Count)
                {
                    this.checkComboBox1.SetItemChecked(index, item.IsCheck);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(string.Empty);
            var cbItems = this.checkComboBox1.CheckedItems;
            foreach (var item in cbItems)
            {
                var cbItem = (CheckComboBoxItem)item;
                if (cbItem != null)
                {
                    sb.Append(cbItem.Text);
                    sb.Append("\n");
                }
            }

            this.richTextBox1.Text = sb.ToString();
        }

        private List<CheckComboBoxItem> GetItems()
        {
            List<CheckComboBoxItem> items = new List<CheckComboBoxItem>();

            for (int i = 0, count = 50; i < count; i++)
            {
                string id = string.Format("Id-{0}", i);
                string text = string.Format("Text-{0}", i);
                var item = new CheckComboBoxItem
                {
                    Id = id,
                    Text = text,
                    IsCheck = false,
                };

                //if (i % 4 == 0)
                //{
                //    item.IsCheck = true;
                //}

                items.Add(item);
            }

            return items;
        }
    }
}
