using Controls.Entity;
using Controls.GridView;
using Model.Binding;
using Model.Binding.BindingUtil;
using Model.config;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            this.tsDataGridView1.ComboBoxSelectionChangeCommitHandler += new ComboBoxSelectionChangeCommitHandler(GridView_ComboBoxSelectionChangeCommit);
            this.tsDataGridView1.MouseClick += new MouseEventHandler(GridView_MouseClick);
            this.contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(ContextMenu_ItemClicked);
        }

        private void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Clicked: " + e.ClickedItem.Name);

            switch (e.ClickedItem.Name)
            { 
                case "item1ToolStripMenuItem":
                    break;
                default:
                    break;
            }
        }

        private void GridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //int width = this.contextMenu.Width;
                //int height = this.contextMenu.Height;
                //int right = this.tsDataGridView1.Right;
                //int bottom = this.tsDataGridView1.Bottom;

                //int x = e.Location.X;
                //int y = e.Location.Y;

                //if (x + width > right)
                //{
                //    x = right - width;
                //}

                //if (y + height > bottom)
                //{
                //    y = bottom - height;
                //}

                //Point p = new Point(x, y);
                //this.contextMenu.Show(this.tsDataGridView1, p);
                this.contextMenu.ShowMenu(this.tsDataGridView1, e.Location);
            }
        }

        private void GridView_ComboBoxSelectionChangeCommit(ComboBox comboBox, object selectedItem, int columnIndex, int rowIndex)
        {
            if (selectedItem != null && selectedItem is ComboOptionItem)
            {
                var cbItem = selectedItem as ComboOptionItem;
                {
                    string msg = string.Format("({0},{1}) {2} - {3}", columnIndex, rowIndex, cbItem.Id, cbItem.Name);
                    MessageBox.Show(this, msg);
                }
            }
        }

        private void GridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //
            Console.WriteLine(e.ColumnIndex);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            TSGrid hsGrid = GetGridConfig();
            TSDataGridViewHelper.AddColumns(this.tsDataGridView1, hsGrid);

            Dictionary<string, string> columnMap = GridViewBindingHelper.GetPropertyBinding(typeof(TSDGVCbItem));
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
                Name = "combobox",
                Text = "ComboBox",
                ColumnType = TSGridColumnType.ComboBox,
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
