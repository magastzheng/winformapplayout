using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model.Data;
using Controls.Entity;

namespace Controls.GridView
{
    public enum UpdateDirection
    {
        Add = 1,
        Remove = 2,
    }

    public delegate void UpdateRelatedDataGrid(UpdateDirection direction, Model.Data.DataRow dataRow);

    public partial class TSDataGridView : DataGridView
    {
        public event UpdateRelatedDataGrid UpdateRelatedDataGridHandler;

        public TSDataGridView()
        {
            InitializeComponent();

            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.CellParsing += new DataGridViewCellParsingEventHandler(DataGridView_CellParsing);
            this.CellFormatting += new DataGridViewCellFormattingEventHandler(DataGridView_CellFormatting);
            //this.CellEnter += new DataGridViewCellEventHandler(DataGridView_CellEnter);
            this.CellValidating += new DataGridViewCellValidatingEventHandler(DataGridView_CellValidating);
        }

        private void SwitchSelection(DataGridViewRow row, int colIndex)
        {
            bool currentStatus = (bool)row.Cells[colIndex].EditedFormattedValue;

            if (currentStatus)
            {
                row.Cells[colIndex].Value = true;
                SetSelectionRowBackground(row, true);

                //update the related datagridview if it needs              
                if (UpdateRelatedDataGridHandler != null)
                {
                    int cbColIndex = GetCheckBoxColumnIndex();
                    Model.Data.DataRow dataRow = GetDataRow(row, 1);
                    UpdateRelatedDataGridHandler(UpdateDirection.Add, dataRow);
                }
            }
            else
            {
                row.Cells[colIndex].Value = false;
                SetSelectionRowBackground(row, false);

                if (UpdateRelatedDataGridHandler != null)
                {
                    int cbColIndex = GetCheckBoxColumnIndex();
                    Model.Data.DataRow dataRow = GetDataRow(row, 0);
                    UpdateRelatedDataGridHandler(UpdateDirection.Remove, dataRow);
                }
            }
        }

        private int GetCheckBoxColumnIndex()
        {
            int index = -1;
            for (int i = 0, count = this.Columns.Count; i < count; i++)
            {
                DataGridViewColumn column = this.Columns[i];
                if (column is DataGridViewCheckBoxColumn)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        private Model.Data.DataRow GetDataRow(DataGridViewRow row, int defSelection)
        {
            Model.Data.DataRow dataRow = new Model.Data.DataRow();
            dataRow.Columns = new List<DataValue>();

            var ds = this.DataSource;
            if (ds is TSGridViewData)
            {
                var gridData = ds as TSGridViewData;

                for (int i = 0, count = gridData.Columns.Count; i < count; i++)
                {
                    HSGridColumn column = gridData.Columns[i];
                    if (column.ColumnType == HSGridColumnType.None)
                        continue;

                    DataValue dataValue = new DataValue();
                    dataValue.Type = column.ValueType;
                    if (column.ColumnType == HSGridColumnType.CheckBox)
                    {
                        dataValue.Value = defSelection;
                    }
                    else
                    {
                        dataValue.Value = row.Cells[i].Value;
                    }
                    dataRow.Columns.Add(dataValue);
                }
            }

            return dataRow;
        }

        private Model.Data.DataTable GetSelectionRows()
        {
            Model.Data.DataTable dataTable = new Model.Data.DataTable();
            dataTable.ColumnIndex = new Dictionary<string, int>();
            dataTable.Rows = new List<Model.Data.DataRow>();

            int cbColIndex = GetCheckBoxColumnIndex();
            if (cbColIndex < 0)
                return dataTable;

            var ds = this.DataSource;
            if (ds is TSGridViewData)
            {
                var gridData = ds as TSGridViewData;
                dataTable.ColumnIndex = gridData.DataTable.ColumnIndex;

                int validColCount = gridData.Columns.Count - 1;
                foreach (DataGridViewRow row in this.Rows)
                {
                    bool isChecked = (bool)row.Cells[cbColIndex].EditedFormattedValue;
                    if (!isChecked)
                        continue;

                    Model.Data.DataRow dataRow = GetDataRow(row, 1);
                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }

        #region common operation of controls

        private void SetSelectionRowBackground(int rowIndex, bool isSelected)
        {
            if (isSelected)
            {
                this.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Blue;
            }
            else
            {
                this.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void SetSelectionRowBackground(DataGridViewRow row, bool isSelected)
        {
            if (isSelected)
            {
                row.DefaultCellStyle.BackColor = Color.Blue;
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
        }
        #endregion

        #region event handler
        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            var ds = this.DataSource;
            if (ds is TSGridViewData)
            {
                TSGridViewData gridData = ds as TSGridViewData;
                if (e.ColumnIndex >= 0 && e.ColumnIndex < gridData.Columns.Count)
                {
                    var column = gridData.Columns[e.ColumnIndex];
                    DataValueType colValType = column.ValueType;
                    switch (colValType)
                    {
                        case DataValueType.Int:
                            {
                                int temp = 0;
                                if (!int.TryParse(e.FormattedValue.ToString(), out temp) || temp < 0)
                                {
                                    e.Cancel = true;
                                    Rows[e.RowIndex].ErrorText = "请输入数字";
                                    //TODO: popup the error message
                                    return;
                                }
                                else
                                {
                                    e.Cancel = false;
                                    Rows[e.RowIndex].ErrorText = string.Empty;
                                    return;
                                }
                            }
                            break;
                        case DataValueType.Float:
                            {
                                double temp = 0.0f;
                                if (!double.TryParse(e.FormattedValue.ToString(), out temp) || temp < 0.0001)
                                {
                                    e.Cancel = true;
                                    Rows[e.RowIndex].ErrorText = "请输入数字";
                                    return;
                                }
                                else
                                {
                                    e.Cancel = false;
                                    Rows[e.RowIndex].ErrorText = string.Empty;
                                    return;
                                }
                            }
                            break;
                        case DataValueType.String:
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        ////private void DataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        ////{
        ////    if (e.ColumnIndex < 0 || e.RowIndex < 0)
        ////        return;
        ////    var ds = this.DataSource;
        ////    if (ds is TSGridViewData)
        ////    { 
                
        ////    }
        ////}

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is DataValue)
            {
                DataValue dataValue = e.Value as DataValue;
                switch(dataValue.Type)
                {
                    case DataValueType.Int:
                        e.Value = dataValue.GetInt();
                        break;
                    case DataValueType.Float:
                        e.Value = dataValue.GetDouble();
                        break;
                    case DataValueType.String:
                        e.Value = dataValue.GetStr();
                        break;
                    default:
                        e.Value = dataValue.GetStr();
                        break;
                }
            }
        }

        private void DataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            var ds = this.DataSource;
            if (ds is TSGridViewData)
            {
                TSGridViewData gridData = ds as TSGridViewData;
                if (e.ColumnIndex >= 0 && e.ColumnIndex < gridData.Columns.Count)
                {
                    var column = gridData.Columns[e.ColumnIndex];
                    DataValue dataValue = new DataValue();
                    dataValue.Type = column.ValueType;
                    switch (column.ValueType)
                    {
                        case DataValueType.Int:
                            {
                                int temp = 0;
                                if (int.TryParse((string)e.Value, out temp))
                                {
                                    dataValue.Value = temp;
                                    e.Value = dataValue;
                                    e.ParsingApplied = true;
                                }
                                else
                                {
                                    //dataValue.Value = temp;
                                    e.ParsingApplied = false;
                                }
                            }
                            break;
                        case DataValueType.Float:
                            {
                                double temp = 0.0f;
                                if (double.TryParse((string)e.Value, out temp))
                                {
                                    dataValue.Value = temp;
                                    e.Value = dataValue;
                                    e.ParsingApplied = true;
                                }
                                else
                                {
                                    //dataValue.Value = temp;
                                    e.ParsingApplied = false;
                                }
                            }
                            break;
                        case DataValueType.String:
                            {
                                dataValue.Value = e.Value;
                                e.Value = dataValue;
                                e.ParsingApplied = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        #endregion
    }
}
