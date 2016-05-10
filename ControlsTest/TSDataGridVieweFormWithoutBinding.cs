using Config;
using Controls.Entity;
using Controls.GridView;
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
    public partial class TSDataGridVieweFormWithoutBinding : Form
    {
        public TSDataGridVieweFormWithoutBinding()
        {
            InitializeComponent();

            this.Load += new EventHandler(Form_Load);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            GridConfig gridConfig = ConfigManager.Instance.GetGridConfig();
            HSGrid hsGrid = gridConfig.GetGid("templatestock");
            TSDataGridViewHelper.AddColumns(this.tsDataGridView1, hsGrid);
            var dataTable = GenerateData(hsGrid);

            TSDataGridViewHelper.SetData(this.tsDataGridView1, hsGrid, dataTable);
        }

        private Model.Data.DataTable GenerateData(HSGrid hsGrid)
        {
            Model.Data.DataTable dataTable = new Model.Data.DataTable();
            dataTable.Rows = new List<Model.Data.DataRow>();
            dataTable.ColumnIndex = new Dictionary<string, int>();
            Random rand = new Random();
            for (int i = 0, count = 15; i < count; i++)
            {
                Model.Data.DataRow dataRow = new Model.Data.DataRow();
                dataRow.Columns = new List<Model.Data.DataValue>();

                for(int j = 0; j < hsGrid.Columns.Count; j++)
                //foreach (var column in hsGrid.Columns)
                {
                    var column = hsGrid.Columns[j];
                    Model.Data.DataValue dataValue = new Model.Data.DataValue();
                    dataValue.Type = column.ValueType;
                    switch (column.ValueType)
                    {
                        case Model.Data.DataValueType.Int:
                            {
                                dataValue.Value = rand.Next(500);
                            }
                            break;
                        case Model.Data.DataValueType.Float:
                            {
                                dataValue.Value = 100 * rand.NextDouble();
                            }
                            break;
                        case Model.Data.DataValueType.String:
                            {
                                dataValue.Value = string.Format("STR-{0}", rand.Next(120));
                            }
                            break;
                        default:
                            break;
                    }

                    if (i == 0)
                    {
                        if (!dataTable.ColumnIndex.ContainsKey(column.Name))
                        {
                            dataTable.ColumnIndex.Add(column.Name, j);
                        }
                    }

                    dataRow.Columns.Add(dataValue);
                }

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }
    }
}
