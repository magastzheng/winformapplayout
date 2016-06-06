using Controls.Entity;
using Controls.GridView;
using Model;
using Model.Binding;
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
    public partial class TSDGVComboBox : Form
    {
        private SortableBindingList<TSDGVCbItem> _dataSource = new SortableBindingList<TSDGVCbItem>(new List<TSDGVCbItem>());
        public TSDGVComboBox()
        {
            InitializeComponent();

            this.Load += new EventHandler(Form_Load);
            this.tsDataGridView1.DataError += new DataGridViewDataErrorEventHandler(GridView_DataError);
        }

        private void GridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //
            Console.WriteLine(e.ColumnIndex);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            HSGrid hsGrid = GetGridConfig();
            TSDataGridViewHelper.AddColumns(this.tsDataGridView1, hsGrid);

            Dictionary<string, string> columnMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(TSDGVCbItem));
            TSDataGridViewHelper.SetDataBinding(this.tsDataGridView1, columnMap);
            //List<ComboOptionItem> cbItems = new List<ComboOptionItem>() { "Cb1", "Cb2", "Cb3", "Cb4" };
            ComboOption cbOption = GetComboBoxData();
            TSDataGridViewHelper.SetDataBinding(this.tsDataGridView1, "combobox", cbOption);
            this.tsDataGridView1.DataSource = _dataSource;

            //Load Data
            var listData = GetData();
            foreach (var item in listData)
            {
                _dataSource.Add(item);
            }
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
                Name = "combobox",
                Text = "ComboBox",
                ColumnType = HSGridColumnType.ComboBox,
                ValueType = Model.Data.DataValueType.String,
                Width = 60,
                Visible = 1
            };
            hsGrid.Columns.Add(col2);

            return hsGrid;
        }

        private List<TSDGVCbItem> GetData()
        {
            List<TSDGVCbItem> listData = new List<TSDGVCbItem>();

            TSDGVCbItem item1 = new TSDGVCbItem
            {
                Name = "Test1",
                ComboBox = "Cb1"
                //ComboBox = new List<string>() {"Cb1_1", "Cb1_2", "Cb1_3" }
            };

            listData.Add(item1);

            TSDGVCbItem item2 = new TSDGVCbItem
            {
                Name = "Test2",
                ComboBox = "Cb2"
                //ComboBox = new List<string>() { "Cb2_1", "Cb2_2", "Cb2_3" }
            };

            listData.Add(item2);

            TSDGVCbItem item3 = new TSDGVCbItem
            {
                Name = "Test1",
                ComboBox = "Cb3"
                //ComboBox = new List<string>() { "Cb3_1", "Cb3_2", "Cb3_3" }
            };

            listData.Add(item3);

            return listData;
        }

        private ComboOption GetComboBoxData()
        {
            ComboOption cbOption = new ComboOption 
            {
                Name = "combobox",
                Items = new List<ComboOptionItem>()
            };

            ComboOptionItem item = new ComboOptionItem 
            {
                Id = "Cb1",
                Name = "Cb1 Text",
                Order1 = 1
            };
            cbOption.Items.Add(item);

            item = new ComboOptionItem
            {
                Id = "Cb2",
                Name = "Cb2 Text",
                Order1 = 2
            };
            cbOption.Items.Add(item);

            item = new ComboOptionItem
            {
                Id = "Cb3",
                Name = "Cb3 Text",
                Order1 = 3
            };
            cbOption.Items.Add(item);

            item = new ComboOptionItem
            {
                Id = "Cb4",
                Name = "Cb4 Text",
                Order1 = 4
            };
            cbOption.Items.Add(item);

            return cbOption;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public class TSDGVCbItem
    {
        [BindingAttribute("name")]
        public string Name { get; set; }

        [BindingAttribute("combobox")]
        public string ComboBox { get; set; }
        //public List<string> ComboBox { get; set; }
    }
}
