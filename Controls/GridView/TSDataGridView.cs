﻿using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Controls.GridView
{
    public enum UpdateDirection
    {
        Select = 1,
        UnSelect = 2,
        Increase = 3,
        Decrease = 4,
        Update = 5,
        NumericChanged = 6,
    }

    public delegate void UpdateRelatedDataGrid(UpdateDirection direction, int rowIndex, int columnIndex);
    public delegate void ClickRowHandler(object sender, int rowIndex);
    public delegate void NumericUpDownValueChanged(int newValue, int rowIndex, int columnIndex);

    public partial class TSDataGridView : DataGridView
    {
        public event UpdateRelatedDataGrid UpdateRelatedDataGridHandler;
        public event ClickRowHandler MouseClickRow;
        public event ClickRowHandler DoubleClickRow;
        public event NumericUpDownValueChanged NumericUpDownValueChanged;

        public KeyPressEventHandler CopiesCheckHandler = new KeyPressEventHandler(CopiesCheck);

        private static void CopiesCheck(object sender, KeyPressEventArgs e)
        {
            TSDataGridViewHelper.CopiesCheck(sender, e);
        }

        public TSDataGridView()
        {
            InitializeComponent();

            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            //设置AutoGenerateColumns以阻止数据绑定时自动添加新列 
            this.AutoGenerateColumns = false;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            //this.CellParsing += new DataGridViewCellParsingEventHandler(DataGridView_CellParsing);
            this.CellFormatting += new DataGridViewCellFormattingEventHandler(DataGridView_CellFormatting);
            this.CellEnter += new DataGridViewCellEventHandler(DataGridView_CellEnter);
            //this.CellValidating += new DataGridViewCellValidatingEventHandler(DataGridView_CellValidating);

            //this.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(DataGridView_ColumnHeaderMouseClick);
            this.CellMouseClick += new DataGridViewCellMouseEventHandler(DataGridView_CellMouseClick);
            this.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(DataGridView_EditingControlShowing);
            this.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellContentClick);
            this.CellDoubleClick += new DataGridViewCellEventHandler(DataGridView_CellDoubleClick);
            this.CellEndEdit += new DataGridViewCellEventHandler(DataGridView_CellEndEdit);

            //this.CurrentCellChanged += new EventHandler(DataGridView_CurrentCellChanged);
        }

        public void NotifyNumericUpDownValueChanged(decimal newValue)
        {
            if (NumericUpDownValueChanged != null)
            {
                NumericUpDownValueChanged((int)newValue, CurrentCell.RowIndex, CurrentCell.ColumnIndex);
            }
        }

        //private void DataGridView_CurrentCellChanged(object sender, EventArgs e)
        //{
        //    Console.WriteLine("test");
        //}

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            TSDataGridView dgv = sender as TSDataGridView;
            if (dgv == null)
                return;

            //DataGridViewColumn column = dgv.Columns[e.ColumnIndex];

            //if (column is DataGridViewComboBoxColumn)
            //{
            //    string cbSource = string.Format("{0}_{1}", column.Name, "source");
            //    DataGridViewColumn srccolumn = dgv.Columns[cbSource];
            //    if(srccolumn != null)
            //    {
            //        var cellSource = dgv.Rows[e.RowIndex].Cells[srccolumn.Name];
            //        ComboOption cbOption = (ComboOption)cellSource.Value;
            //        if (cbOption != null)
            //        {
            //            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //            var cbCell = (DataGridViewComboBoxCell)cell;
            //            if (cbCell != null)
            //            {
            //                //cbCell.DataSource = cbOption.Items;
            //                //cbCell.DisplayMember = "Name";
            //                //cbCell.ValueMember = "Id";
            //                cbCell.Items.Clear();
            //                cbCell.Items.AddRange(cbOption.Items);
            //                cbCell.DisplayMember = "Name";
            //                cbCell.ValueMember = "Id";
            //            }
            //        }
            //    }
            //}
        }

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv == null || e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            if (DoubleClickRow != null)
            {
                DoubleClickRow(this, e.RowIndex);
            }
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

        private void DataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TSDataGridView dgv = sender as TSDataGridView;
            int columnIndex = dgv.CurrentCell.ColumnIndex;
            if (Columns["copies"] != null && columnIndex == Columns["copies"].Index)
            {
                e.Control.KeyPress -= CopiesCheck;
                e.Control.KeyPress += CopiesCheck;
            }
        }

        private void DataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (sender == null || e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            TSDataGridView dgv = sender as TSDataGridView;
            if (dgv == null)
                return;
            
            if (MouseClickRow != null)
            {
                MouseClickRow(this, e.RowIndex);
            }

            //if (dgv.Columns["copies"] != null)
            //{
            //    int copiesIndex = dgv.Columns["copies"].Index;
            //    DataGridViewRow row = dgv.Rows[e.RowIndex];
            //    switch (dgv.Columns[e.ColumnIndex].Name)
            //    {
            //        case "plus":
            //            {
            //                int oldValue = int.Parse(row.Cells["copies"].Value.ToString());
            //                if (oldValue < TSDataGridViewHelper.MAX_ENTRUST_AMOUNT)
            //                {
            //                    row.Cells["copies"].Value = oldValue + 1;
            //                    if (dgv.UpdateRelatedDataGridHandler != null)
            //                    {
            //                        dgv.UpdateRelatedDataGridHandler(UpdateDirection.Increase, e.RowIndex, e.ColumnIndex);
            //                    }
            //                }
            //                else
            //                {
            //                    //invalid input
            //                }
            //            }
            //            break;
            //        case "minus":
            //            {
            //                int oldValue = int.Parse(row.Cells["copies"].Value.ToString());
            //                if (oldValue > 1)
            //                {
            //                    row.Cells["copies"].Value = oldValue - 1;
            //                    if (dgv.UpdateRelatedDataGridHandler != null)
            //                    { 
            //                        dgv.UpdateRelatedDataGridHandler(UpdateDirection.Decrease, e.RowIndex, e.ColumnIndex);
            //                    }
            //                }
            //                else
            //                {
            //                    //invalid input
            //                }
            //            }
            //            break;
            //    }
            //}
        }

        public override void Sort(DataGridViewColumn dataGridViewColumn, ListSortDirection direction)
        {
            base.Sort(dataGridViewColumn, direction);
        }

        //private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
            
        //}

        private void SwitchSelection(DataGridViewRow row, int colIndex)
        {
            bool currentStatus = (bool)row.Cells[colIndex].EditedFormattedValue;

            if (currentStatus)
            {
                row.Cells[colIndex].Value = true;
                //SetSelectionRowBackground(row, true);

                //update the related datagridview if it needs              
                if (UpdateRelatedDataGridHandler != null)
                {
                    UpdateRelatedDataGridHandler(UpdateDirection.Select, row.Index, colIndex);
                }
            }
            else
            {
                row.Cells[colIndex].Value = false;
                //SetSelectionRowBackground(row, false);

                if (UpdateRelatedDataGridHandler != null)
                {
                    UpdateRelatedDataGridHandler(UpdateDirection.UnSelect, row.Index, colIndex);
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

        //private Model.Data.DataRow GetDataRow(DataGridViewRow row, int defSelection)
        //{
        //    Model.Data.DataRow dataRow = new Model.Data.DataRow();
        //    dataRow.Columns = new List<DataValue>();

        //    var ds = this.DataSource;
        //    if (ds is TSGridViewData)
        //    {
        //        var gridData = ds as TSGridViewData;

        //        for (int i = 0, count = gridData.Columns.Count; i < count; i++)
        //        {
        //            HSGridColumn column = gridData.Columns[i];
        //            if (column.ColumnType == HSGridColumnType.None)
        //                continue;

        //            DataValue dataValue = new DataValue();
        //            dataValue.Type = column.ValueType;
        //            if (column.ColumnType == HSGridColumnType.CheckBox)
        //            {
        //                dataValue.Value = defSelection;
        //            }
        //            else
        //            {
        //                dataValue.Value = row.Cells[i].Value;
        //            }
        //            dataRow.Columns.Add(dataValue);
        //        }
        //    }

        //    return dataRow;
        //}

        //private Model.Data.DataTable GetSelectionRows()
        //{
        //    Model.Data.DataTable dataTable = new Model.Data.DataTable();
        //    dataTable.ColumnIndex = new Dictionary<string, int>();
        //    dataTable.Rows = new List<Model.Data.DataRow>();

        //    int cbColIndex = GetCheckBoxColumnIndex();
        //    if (cbColIndex < 0)
        //        return dataTable;

        //    var ds = this.DataSource;
        //    if (ds is TSGridViewData)
        //    {
        //        var gridData = ds as TSGridViewData;
        //        dataTable.ColumnIndex = gridData.DataTable.ColumnIndex;

        //        int validColCount = gridData.Columns.Count - 1;
        //        foreach (DataGridViewRow row in this.Rows)
        //        {
        //            bool isChecked = (bool)row.Cells[cbColIndex].EditedFormattedValue;
        //            if (!isChecked)
        //                continue;

        //            Model.Data.DataRow dataRow = GetDataRow(row, 1);
        //            dataTable.Rows.Add(dataRow);
        //        }
        //    }

        //    return dataTable;
        //}

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

        //#region event handler
        //private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        //{
        //    if (e.ColumnIndex < 0 || e.RowIndex < 0)
        //        return;
        //    var ds = this.DataSource;
        //    if (ds is TSGridViewData)
        //    {
        //        TSGridViewData gridData = ds as TSGridViewData;
        //        if (e.ColumnIndex >= 0 && e.ColumnIndex < gridData.Columns.Count)
        //        {
        //            var column = gridData.Columns[e.ColumnIndex];
        //            DataValueType colValType = column.ValueType;
        //            switch (colValType)
        //            {
        //                case DataValueType.Int:
        //                    {
        //                        int temp = 0;
        //                        if (!int.TryParse(e.FormattedValue.ToString(), out temp) || temp < 0)
        //                        {
        //                            e.Cancel = true;
        //                            Rows[e.RowIndex].ErrorText = "请输入数字";
        //                            //TODO: popup the error message
        //                            return;
        //                        }
        //                        else
        //                        {
        //                            e.Cancel = false;
        //                            Rows[e.RowIndex].ErrorText = string.Empty;
        //                            return;
        //                        }
        //                    }
        //                    break;
        //                case DataValueType.Float:
        //                    {
        //                        double temp = 0.0f;
        //                        if (!double.TryParse(e.FormattedValue.ToString(), out temp) || temp < 0.0001)
        //                        {
        //                            e.Cancel = true;
        //                            Rows[e.RowIndex].ErrorText = "请输入数字";
        //                            return;
        //                        }
        //                        else
        //                        {
        //                            e.Cancel = false;
        //                            Rows[e.RowIndex].ErrorText = string.Empty;
        //                            return;
        //                        }
        //                    }
        //                    break;
        //                case DataValueType.String:
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //}

        private void DataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            if (Columns["copies"] != null)
            {
                if (e.ColumnIndex == Columns["copies"].Index)
                {
                    if (UpdateRelatedDataGridHandler != null)
                    {
                        UpdateRelatedDataGridHandler(UpdateDirection.Update, e.RowIndex, e.ColumnIndex);
                    }
                }
            }
            //var ds = this.DataSource;
            //if (ds is TSGridViewData)
            //{

            //}
        }

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            TSDataGridViewHelper.CellFormatting(this, e);
        }

        //private void DataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        //{
        //    var ds = this.DataSource;
        //    if (ds is TSGridViewData)
        //    {
        //        TSGridViewData gridData = ds as TSGridViewData;
        //        if (e.ColumnIndex >= 0 && e.ColumnIndex < gridData.Columns.Count)
        //        {
        //            var column = gridData.Columns[e.ColumnIndex];
        //            DataValue dataValue = new DataValue();
        //            dataValue.Type = column.ValueType;
        //            switch (column.ValueType)
        //            {
        //                case DataValueType.Int:
        //                    {
        //                        int temp = 0;
        //                        if (int.TryParse((string)e.Value, out temp))
        //                        {
        //                            dataValue.Value = temp;
        //                            e.Value = dataValue;
        //                            e.ParsingApplied = true;
        //                        }
        //                        else
        //                        {
        //                            //dataValue.Value = temp;
        //                            e.ParsingApplied = false;
        //                        }
        //                    }
        //                    break;
        //                case DataValueType.Float:
        //                    {
        //                        double temp = 0.0f;
        //                        if (double.TryParse((string)e.Value, out temp))
        //                        {
        //                            dataValue.Value = temp;
        //                            e.Value = dataValue;
        //                            e.ParsingApplied = true;
        //                        }
        //                        else
        //                        {
        //                            //dataValue.Value = temp;
        //                            e.ParsingApplied = false;
        //                        }
        //                    }
        //                    break;
        //                case DataValueType.String:
        //                    {
        //                        dataValue.Value = e.Value;
        //                        e.Value = dataValue;
        //                        e.ParsingApplied = true;
        //                    }
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //}
        //#endregion

        #region select/unselect

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

        #endregion

        #region get current Row and Column index

        public int GetCurrentRowIndex()
        {
            if (CurrentRow != null)
            {
                return CurrentRow.Index;
            }
            else
            {
                return -1;
            }
        }

        public int GetCurrentColumnIndex()
        {
            if (CurrentCell != null)
            {
                return CurrentCell.ColumnIndex;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }
}
