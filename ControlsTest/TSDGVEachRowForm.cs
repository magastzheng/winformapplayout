using Controls.Entity;
using Controls.GridView;
using ControlsTest.Entity;
using Model.config;
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
    public partial class TSDGVEachRowForm : Form
    {
        public TSDGVEachRowForm()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);
            this.tsDataGridView1.DataError += new DataGridViewDataErrorEventHandler(GridView_DataError);
            this.tsDataGridView1.ComboBoxSelectionChangeCommitHandler += new ComboBoxSelectionChangeCommitHandler(GridView_ComboBoxSelectionChangeCommit);
            //this.tsDataGridView1.MouseClick += new MouseEventHandler(GridView_MouseClick);
            this.gridviewContextMenu.Click += new EventHandler(ContextMenuItemClick);
        }

        //private void GridView_MouseClick(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == System.Windows.Forms.MouseButtons.Right)
        //    {
        //        this.gridviewContextMenu.Show(this.tsDataGridView1, e.Location);
        //    }
        //}

        private void ContextMenuItemClick(object sender, EventArgs e)
        {
            MessageBox.Show(this, e.ToString());
        }

        private void GridView_ComboBoxSelectionChangeCommit(ComboBox comboBox, object selectedItem, int rowIndex, int columnIndex)
        {
            if (selectedItem != null && selectedItem is ComboOptionItem)
            {
                var cbItem = selectedItem as ComboOptionItem;
                Console.WriteLine(cbItem.Id, cbItem.Name);
            }
        }

        private void GridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Console.WriteLine(e.ColumnIndex);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            TSGrid hsGrid = CBRowItemHelper.GetGridConfig();
            TSDataGridViewHelper.AddColumns(this.tsDataGridView1, hsGrid);

            var listData = CBRowItemHelper.GetData();
            for (int i = 0; i < listData.Count; i++)
            {
                int rowIndex = this.tsDataGridView1.Rows.Add();
                var rowData = listData[i];
                foreach (var column in hsGrid.Columns)
                {
                    var row = this.tsDataGridView1.Rows[rowIndex];
                    var cell = row.Cells[column.Name];
                    switch (column.Name)
                    {
                        case "name":
                            {
                                cell.Value = rowData.Name;
                            }
                            break;
                        case "id":
                            {
                                var cbCell = (DataGridViewComboBoxCell)cell;
                                cbCell.DisplayMember = "Text";
                                cbCell.ValueMember = "Id";
                                foreach (var item in rowData.IdSource.Items)
                                {
                                    cbCell.Items.Add(item);
                                }
                                cbCell.Value = cbCell.Items[0];
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
