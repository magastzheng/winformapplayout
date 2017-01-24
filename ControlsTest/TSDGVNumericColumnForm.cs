using Controls.Entity;
using Controls.GridView;
using Model.Binding;
using Model.Binding.BindingUtil;
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
       
    public partial class TSDGVNumericColumnForm : Form
    {
        private SortableBindingList<NumericItem> _dataSource = new SortableBindingList<NumericItem>(new List<NumericItem>());
     
        public TSDGVNumericColumnForm()
        {
            InitializeComponent();

            this.Load += new EventHandler(Form_Load);

            this.tsDataGridView1.DataError += new DataGridViewDataErrorEventHandler(GridView_DataError);
            this.button1.Click += new EventHandler(button1_Click);
        }

        private void GridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Console.WriteLine(e.ColumnIndex);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            TSGrid hsGrid = GetGridConfig();
            TSDataGridViewHelper.AddColumns(this.tsDataGridView1, hsGrid);
            Dictionary<string, string> columnMap = GridViewBindingHelper.GetPropertyBinding(typeof(NumericItem));
            TSDataGridViewHelper.SetDataBinding(this.tsDataGridView1, columnMap);
            this.tsDataGridView1.DataSource = _dataSource;

            //Load Data
            var listData = GetData();
            foreach (var item in listData)
            {
                _dataSource.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Hah");
            _dataSource[0].Age = 27;

            this.tsDataGridView1.Invalidate();
        }

        private TSGrid GetGridConfig()
        {
            TSGrid hsGrid = new TSGrid
            {
                Columns = new List<TSGridColumn>()
            };

            TSGridColumn col1 = new TSGridColumn
            {
                Name = "name",
                Text = "Name",
                ColumnType = TSGridColumnType.Text,
                ValueType = Model.Data.DataValueType.String,
                Width = 60,
                Visible = 1
            };
            hsGrid.Columns.Add(col1);


            TSGridColumn col2 = new TSGridColumn
            {
                Name = "id",
                Text = "Id",
                ColumnType = TSGridColumnType.Text,
                ValueType = Model.Data.DataValueType.String,
                Width = 60,
                Visible = 1
            };
            hsGrid.Columns.Add(col2);

            TSGridColumn col3 = new TSGridColumn
            {
                Name = "age",
                Text = "Age",
                ColumnType = TSGridColumnType.NumericUpDown,
                ValueType = Model.Data.DataValueType.Int,
                Width = 60,
                Visible = 1
            };
            hsGrid.Columns.Add(col3);

            return hsGrid;
        }

        public List<NumericItem> GetData()
        {
            List<NumericItem> items = new List<NumericItem>();
            var item1 = new NumericItem 
            {
                Name = "Magast",
                Id = "magast",
                Age = 31
            };

            items.Add(item1);

            var item2 = new NumericItem
            {
                Name = "Peony",
                Id = "peony",
                Age = 29
            };

            items.Add(item2);

            var item3 = new NumericItem
            {
                Name = "Jack",
                Id = "jack",
                Age = 35
            };

            items.Add(item3);

            return items;
        }
    }

    public class NumericItem
    {
        [BindingAttribute("name")]
        public string Name { get; set; }

        [BindingAttribute("id")]
        public string Id { get; set; }

        [BindingAttribute("age")]
        public int Age { get; set; }
    }
}
