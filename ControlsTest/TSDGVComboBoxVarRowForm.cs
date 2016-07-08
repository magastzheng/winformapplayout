using Controls.Entity;
using Controls.GridView;
using Model.Binding;
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
            HSGrid hsGrid = GetGridConfig();
            TSDataGridViewHelper.AddColumns(this.tsDataGridView1, hsGrid);
            Dictionary<string, string> columnMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(CBRowItem));
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

        }

        private HSGrid GetGridConfig()
        {
            HSGrid hsGrid = new HSGrid
            {
                Columns = new List<HSGridColumn>()
            };

            HSGridColumn col1 = new HSGridColumn
            {
                Name = "name",
                Text = "Name",
                ColumnType = HSGridColumnType.Text,
                ValueType = Model.Data.DataValueType.String,
                Width = 60,
                Visible = 1
            };
            hsGrid.Columns.Add(col1);


            HSGridColumn col2 = new HSGridColumn
            {
                Name = "id",
                Text = "Id",
                ColumnType = HSGridColumnType.ComboBox,
                ValueType = Model.Data.DataValueType.String,
                Width = 60,
                Visible = 1
            };
            hsGrid.Columns.Add(col2);

            HSGridColumn col3 = new HSGridColumn
            {
                Name = "id_source",
                Text = "IdSource",
                ColumnType = HSGridColumnType.Text,
                ValueType = Model.Data.DataValueType.String,
                Width = 60,
                Visible = 0
            };
            hsGrid.Columns.Add(col3);

            return hsGrid;
        }

        private List<CBRowItem> GetData()
        {
            var listData = new List<CBRowItem>();
            CBRowItem item = new CBRowItem 
            {
                Name = "Test 1",
                Id = "Cb0_0",
                IdSource = GetComboBoxData(0)
            };
            listData.Add(item);

            item = new CBRowItem
            {
                Name = "Test 2",
                Id = "Cb1_2",
                IdSource = GetComboBoxData(1)
            };
            listData.Add(item);

            item = new CBRowItem
            {
                Name = "Test 3",
                Id = "Cb2_3",
                IdSource = GetComboBoxData(2)
            };
            listData.Add(item);

            return listData;
        }

        private ComboOption GetComboBoxData(int index)
        {
            string suffix = index.ToString();

            ComboOption cbOption = new ComboOption
            {
                Name = "combobox" + suffix,
                Items = new List<ComboOptionItem>()
            };

            for (int i = 0; i < 4; i++)
            {
                string id = string.Format("Cb{0}_{1}", index, i);
                string text = string.Format("{0} Text", id);
                ComboOptionItem item = new ComboOptionItem
                {
                    Id = id,
                    Name = text,
                    Order1 = i
                };
                cbOption.Items.Add(item);
            }

            return cbOption;
        }
    }

    public class CBRowItem
    {
        [BindingAttribute("name")]
        public string Name { get; set; }

        [BindingAttribute("id")]
        public string Id { get; set; }

        [BindingAttribute("id_source")]
        public ComboOption IdSource { get; set; }
    }
}
