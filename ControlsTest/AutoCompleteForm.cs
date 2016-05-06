using Controls;
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
    public partial class AutoCompleteForm : Form
    {
        public AutoCompleteForm()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, System.EventArgs e)
        {
            this.autoComplete1.SetDropdownList(this.listBox1);
            IList<AutoItem> dataSource = LoadData();
            this.autoComplete1.AutoDataSource = dataSource;
        }

        public static IList<AutoItem> LoadData()
        {
            IList<AutoItem> dataSource = new List<AutoItem>();
            dataSource.Add(new AutoItem { Id = "000001", Name = "中国平安" });
            dataSource.Add(new AutoItem { Id = "000002", Name = "万科A" });
            dataSource.Add(new AutoItem { Id = "300001", Name = "特锐德" });
            dataSource.Add(new AutoItem { Id = "300101", Name = "振芯科技" });
            dataSource.Add(new AutoItem { Id = "300205", Name = "天喻信息" });
            dataSource.Add(new AutoItem { Id = "600004", Name = "白云机场" });
            dataSource.Add(new AutoItem { Id = "601238", Name = "广汽集团" });
            //dataSource.Add(new AutoItem { Id = "", Name = "" });
            //dataSource.Add(new AutoItem { Id = "", Name = "" });
            //dataSource.Add(new AutoItem { Id = "", Name = "" });
            //dataSource.Add(new AutoItem { Id = "", Name = "" });
            //dataSource.Add(new AutoItem { Id = "", Name = "" });

            return dataSource;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var autoItem = this.autoComplete1.GetCurrentItem();
            this.textBox1.Text = autoItem.Id + " " + autoItem.Name;
        }

    }
}
