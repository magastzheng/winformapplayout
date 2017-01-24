using Controls.Entity;
using Controls.GridView;
using ControlsTest.Entity;
using Model.Binding;
using Model.Binding.BindingUtil;
using Model.config;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ControlsTest
{
    public partial class TSDGVComboBoxVarRowForm : Form
    {
        private SortableBindingList<CBRowItem> _dataSource = new SortableBindingList<CBRowItem>(new List<CBRowItem>());

        public TSDGVComboBoxVarRowForm()
        {
            InitializeComponent();

            this.Load += new EventHandler(Form_Load);

            this.tsDataGridView1.DataError += new DataGridViewDataErrorEventHandler(GridView_DataError);
        }

        private void GridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Console.WriteLine(e.ColumnIndex);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            TSGrid hsGrid = CBRowItemHelper.GetGridConfig();
            TSDataGridViewHelper.AddColumns(this.tsDataGridView1, hsGrid);
            Dictionary<string, string> columnMap = GridViewBindingHelper.GetPropertyBinding(typeof(CBRowItem));
            TSDataGridViewHelper.SetDataBinding(this.tsDataGridView1, columnMap);
            this.tsDataGridView1.DataSource = _dataSource;

            //Load Data
            var listData = CBRowItemHelper.GetData();
            foreach (var item in listData)
            {
                _dataSource.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}
