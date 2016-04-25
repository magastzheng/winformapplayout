﻿using Model.Data;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controls
{
    public enum UpdateDirection
    {
        Add = 1,
        Remove = 2,
    }
    public delegate void UpdateRelatedDataGrid(UpdateDirection direction, DataRow dataRow);

    [System.ComponentModel.DesignerCategory("code"),
    Designer(typeof(System.Windows.Forms.Design.ControlDesigner)),
    ComplexBindingProperties(),
    Docking(DockingBehavior.Ask)]
    public partial class HSGridView : DataGridView
    {
        private HSGrid _hsGrid = null;
        private Dictionary<string, int> _columnNameIndex = new Dictionary<string, int>();
        private List<HSGridColumn> _columns = null;
        public List<HSGridColumn> GridColumns { get { return _columns; } }
        
        //选中父表中行之后，子表需要添加相应行
        private UpdateRelatedDataGrid _updateRelatedDataGrid;
        public UpdateRelatedDataGrid UpdateRelatedDataGrid
        {
            set { _updateRelatedDataGrid = value; }
            get { return _updateRelatedDataGrid; }
        }

        public HSGridView(HSGrid hsGrid)
        {
            InitializeComponent();

            this._hsGrid = hsGrid;
            this._columns = hsGrid.Columns;
            for (int i = 0, count = _columns.Count; i < count; i++)
            {
                _columnNameIndex[_columns[i].Name] = i;
            }

            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            AddColumns();

            this.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellContentClick);
        }

        public void FillData(DataSet dataSet, Dictionary<string, string> colDataMap)
        {
            if (dataSet == null || dataSet.Rows == null || colDataMap == null)
                return;

            for(int r = 0, count = dataSet.Rows.Count; r < count; r++)
            {
                DataRow dataRow = dataSet.Rows[r];

                int rowIndex = this.Rows.Add();
                DataGridViewRow row = this.Rows[rowIndex];
                bool isSelected = false;
                foreach(HSGridColumn col in _columns)
                {

                    //switch(col.ValueType)
                    //{
                    //    case DataValueType.Int:
                    //        break;
                    //    case DataValueType.Float:
                    //        break;
                    //    case DataValueType.String:
                    //        break;
                    //    default:
                    //        break;
                    //}

                    switch (col.ColumnType)
                    {
                        case HSGridColumnType.CheckBox:
                            {
                                if (colDataMap.ContainsKey(col.Name) && dataRow.Columns.ContainsKey(colDataMap[col.Name]))
                                {
                                    string dataKey = colDataMap[col.Name];
                                    int targetValue = dataRow.Columns[dataKey].GetInt();
                                    isSelected = targetValue > 0 ? true : false;
                                    row.Cells[col.Name].Value = isSelected;
                                    //FillDataCell(ref row, col, dataRow.Columns[dataKey]);
                                }
                            }
                            break;
                        case HSGridColumnType.Text:
                            {
                                if (colDataMap.ContainsKey(col.Name) && dataRow.Columns.ContainsKey(colDataMap[col.Name]))
                                {
                                    string dataKey = colDataMap[col.Name];
                                    FillDataCell(ref row, col, dataRow.Columns[dataKey]);
                                }
                            }
                            break;
                        case HSGridColumnType.Image:
                            {
                                if (colDataMap.ContainsKey(col.Name) && dataRow.Columns.ContainsKey(colDataMap[col.Name]))
                                {
                                    string dataKey = colDataMap[col.Name];
                                    string imgPath = dataRow.Columns[dataKey].GetStr();
                                    Image plusImg = Image.FromFile(imgPath);
                                    Bitmap plusBt = new Bitmap(plusImg, new Size(20, 20));
                                    row.Cells[col.Name].Value = plusBt;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }

                SetSelectionRowBackground(rowIndex, isSelected);
            }
        }

        public void SelectAll(bool isSelected)
        {
            foreach (DataGridViewRow row in this.Rows)
            {
                int cbColIndex = GetCheckBoxColumnIndex();
                if (cbColIndex < 0)
                    return;

                row.Cells[cbColIndex].Value = isSelected;
                SetSelectionRowBackground(row, isSelected);
            }
        }

        private void FillDataCell(ref DataGridViewRow row, HSGridColumn column, DataValue dataValue)
        {
            switch (column.ValueType)
            {
                case DataValueType.Int:
                    {
                        row.Cells[column.Name].Value = dataValue.GetInt();
                    }
                    break;
                case DataValueType.Float:
                    {
                        row.Cells[column.Name].Value = dataValue.GetDouble();
                    }
                    break;
                case DataValueType.Char:
                case DataValueType.String:
                    {
                        row.Cells[column.Name].Value = dataValue.GetStr();
                    }
                    break;
                default:
                    {
                        row.Cells[column.Name].Value = dataValue.GetStr();
                    }
                    break;
            }
        }

        public void DeleteData(string targetColName, DataValue targetColValue)
        {
            int index = _columnNameIndex[targetColName];
            var column = _columns[index];
            if(column ==  null)
                return;

            for (int i = this.Rows.Count - 1; i >= 0; i--)
            {
                string curValue = Rows[i].Cells[targetColName].Value.ToString();
                bool needDelete = false;
                switch (column.ValueType)
                { 
                    case DataValueType.Int:
                        {
                            int targetValue = targetColValue.GetInt();
                            int temp = int.Parse(curValue);
                            needDelete = targetValue == temp ? true : false;
                        }
                        break;
                    case DataValueType.Char:
                    case DataValueType.String:
                        {
                            string targetValue = targetColValue.GetStr();
                            needDelete = targetValue == curValue ? true : false;
                        }
                        break;
                    default:
                        break;
                }

                if (needDelete)
                {
                    this.Rows.RemoveAt(i);
                }
            }
        }

        private void AddColumns()
        { 
            DataGridViewColumn[] gridColumns = new DataGridViewColumn[_columns.Count];
            for(int i = 0, count = _columns.Count; i < count; i++)
            {
                HSGridColumn col = _columns[i];
         
                DataGridViewColumn column = null;
                switch (col.ColumnType)
                {
                    case HSGridColumnType.CheckBox:
                        {
                            column = new DataGridViewCheckBoxColumn();
                        }
                        break;
                    case HSGridColumnType.Image:
                        {
                            column = new DataGridViewImageColumn();
                        }
                        break;
                    case HSGridColumnType.Text:
                        {
                            column = new DataGridViewTextBoxColumn();
                        }
                        break;
                    default:
                        {
                            column = new DataGridViewTextBoxColumn();
                        }
                        break;
                }

                column.HeaderText = col.Text;
                column.Name = col.Name;
                column.Width = col.Width;
                column.ReadOnly = (col.ReadOnly != 0) ? true : false;
                gridColumns[i] = column;
            }

            this.Columns.AddRange(gridColumns);
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv == null || e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            int cbColIndex = GetCheckBoxColumnIndex();
            if (cbColIndex < 0)
                return;
            DataGridViewRow row = dgv.Rows[e.RowIndex];

            if (e.ColumnIndex == cbColIndex)
            {
                SwitchSelection(row, e.ColumnIndex);
            }
        }

        private void SwitchSelection(DataGridViewRow row, int colIndex)
        {
            bool currentStatus = (bool)row.Cells[colIndex].EditedFormattedValue;

            if (currentStatus)
            {
                row.Cells[colIndex].Value = true;
                SetSelectionRowBackground(row, true);

                //update the related datagridview if it needs              
                if (_updateRelatedDataGrid != null)
                {
                    int cbColIndex = GetCheckBoxColumnIndex();
                    DataRow dataRow = GetDataRow(row, 1);
                    _updateRelatedDataGrid(UpdateDirection.Add, dataRow);
                }
            }
            else
            {
                row.Cells[colIndex].Value = false;
                SetSelectionRowBackground(row, false);

                if (_updateRelatedDataGrid != null)
                {
                    int cbColIndex = GetCheckBoxColumnIndex();
                    DataRow dataRow = GetDataRow(row, 0);
                    _updateRelatedDataGrid(UpdateDirection.Remove, dataRow);
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

        private DataRow GetDataRow(DataGridViewRow row, int defSelection)
        {
            DataRow dataRow = new DataRow();
            dataRow.Columns = new Dictionary<string, DataValue>();
            for(int i = 0, count = this._columns.Count; i < count; i++)
            {
                HSGridColumn column = this._columns[i];
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
                dataRow.Columns[column.Name] = dataValue;
            }

            return dataRow;
        }

        private DataSet GetSelectionRows()
        {
            DataSet dataSet = new DataSet();
            dataSet.Rows = new List<DataRow>();

            int cbColIndex = GetCheckBoxColumnIndex();
            if (cbColIndex < 0)
                return dataSet;

            int validColCount = this._columns.Count - 1;
            foreach (DataGridViewRow row in this.Rows)
            {
                bool isChecked = (bool)row.Cells[cbColIndex].EditedFormattedValue;
                if (!isChecked)
                    continue;

                DataRow dataRow = GetDataRow(row, 1);
                dataSet.Rows.Add(dataRow);
            }

            return dataSet;
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
    }
}
