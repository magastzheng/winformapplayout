using Model.config;
using System;
using System.ComponentModel;
using System.Diagnostics;
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
    public delegate void CellEndEditHandler(int rowIndex, int columnIndex, string columnName); 
    public delegate void ComboBoxSelectionChangeCommitHandler(ComboBox comboBox, object selectedItem, int rowIndex, int columnIndex);

    public partial class TSDataGridView : DataGridView
    {
        public event UpdateRelatedDataGrid UpdateRelatedDataGridHandler;
        public event ClickRowHandler MouseClickRow;
        public event NumericUpDownValueChanged NumericUpDownValueChanged;
        public event CellEndEditHandler CellEndEditHandler;
        public event ComboBoxSelectionChangeCommitHandler ComboBoxSelectionChangeCommitHandler; 

        public KeyPressEventHandler CopiesCheckHandler = new KeyPressEventHandler(CopiesCheck);

        private DataGridViewComboBoxEditingControl _dgvComboBox = null;

        //Use the store the multiple selected drag to check/uncheck
        private int startRow = -1;
        private bool isChecked = false;
        private bool isShiftSelected = false;

        private static void CopiesCheck(object sender, KeyPressEventArgs e)
        {
            TSDataGridViewHelper.CopiesCheck(sender, e);
        }

        public TSDataGridView()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.RowTemplate.HeaderCell = new DataGridViewRowHeaderCellEx();

            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            //设置AutoGenerateColumns以阻止数据绑定时自动添加新列 
            this.AutoGenerateColumns = false;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.CellFormatting += new DataGridViewCellFormattingEventHandler(DataGridView_CellFormatting);
            this.CellEnter += new DataGridViewCellEventHandler(DataGridView_CellEnter);

            this.CellMouseClick += new DataGridViewCellMouseEventHandler(DataGridView_CellMouseClick);
            this.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(DataGridView_EditingControlShowing);
            this.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellContentClick);
            //this.CellDoubleClick += new DataGridViewCellEventHandler(DataGridView_CellDoubleClick);
            this.CellEndEdit += new DataGridViewCellEventHandler(DataGridView_CellEndEdit);

            this.CellMouseDown += new DataGridViewCellMouseEventHandler(DataGridView_CellMouseDown);
        }

        private void DataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            TSDataGridView dgv = sender as TSDataGridView;
            if (dgv == null)
                return;

            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            if (columnIndex < 0 || columnIndex > dgv.Columns.Count || rowIndex < 0 || rowIndex > dgv.Rows.Count)
                return;

            //string msg = string.Format("CellMouseDown - row: {0}, column: {1}", rowIndex, columnIndex);
            //Trace.WriteLine(msg);

            int cbIndex = GetCheckBoxColumnIndex();
            if (cbIndex < 0)
                return;

            if (e.Button == System.Windows.Forms.MouseButtons.Left && Control.ModifierKeys == Keys.Shift)
            {
                isShiftSelected = true;
                if (cbIndex == columnIndex)
                {
                    startRow = e.RowIndex;
                    dgv.CellMouseUp += new DataGridViewCellMouseEventHandler(DataGridView_CellMouseUp);
                    var cell = dgv.Rows[rowIndex].Cells[columnIndex];
                    if (Convert.ToBoolean(cell.EditedFormattedValue) == false)
                    {
                        isChecked = false;
                    }
                    else
                    {
                        isChecked = true;
                    }
                }
            }
        }

        /// <summary>
        /// TODO: the last row is not check/uncheck directly. It needs to move focus to other cell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            TSDataGridView dgv = sender as TSDataGridView;
            if (dgv == null)
                return;
            
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            if (columnIndex < 0 || columnIndex > dgv.Columns.Count || rowIndex < 0 || rowIndex > dgv.Rows.Count)
                return;

            //string msg = string.Format("CellMouseUp - row: {0}, column: {1}", rowIndex, columnIndex);
            //Trace.WriteLine(msg);

            dgv.CellMouseUp -= DataGridView_CellMouseUp;

            int cbIndex = GetCheckBoxColumnIndex();
            if (cbIndex < 0)
                return;

            if (startRow < 0 || startRow > dgv.Rows.Count)
                return;

            if (!isShiftSelected)
                return;

            bool result = isChecked ? false : true;
            int endRow = rowIndex;
            if (endRow < 0 || endRow > dgv.Rows.Count)
                return;

            if (startRow == endRow)
                return;

            int start, end;
            start = startRow < endRow ? startRow : endRow;
            end = startRow > endRow ? startRow : endRow;

            for (int i = start; i <= end; i++)
            {
                dgv.BeginEdit(false);
                var row = dgv.Rows[i];
                var cell = row.Cells[cbIndex];
                cell.Value = result;
                //dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dgv.EndEdit();
                SwitchSelection(row, columnIndex);
            }

            isShiftSelected = false;

            startRow = -1;

            //dgv.EndEdit();
            
        }

        public void NotifyNumericUpDownValueChanged(decimal newValue)
        {
            if (NumericUpDownValueChanged != null)
            {
                NumericUpDownValueChanged((int)newValue, CurrentCell.RowIndex, CurrentCell.ColumnIndex);
            }
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            TSDataGridView dgv = sender as TSDataGridView;
            if (dgv == null)
                return;

            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var val = cell.Value;
            if (CellEndEditHandler != null)
            {
                CellEndEditHandler(e.RowIndex, e.ColumnIndex, dgv.Columns[e.ColumnIndex].Name);
            }

            if (_dgvComboBox != null)
            {
                _dgvComboBox.SelectionChangeCommitted -= new System.EventHandler(DataGridViewComboBox_SelectionChangeCommitted);
                _dgvComboBox = null;
            }

            DataGridViewColumn column = dgv.Columns[e.ColumnIndex];

            if (column is DataGridViewComboBoxColumn)
            {
                string cbSource = string.Format("{0}_{1}", column.Name, "source");
                DataGridViewColumn srccolumn = dgv.Columns[cbSource];
                if (srccolumn != null)
                {
                    var cellSource = dgv.Rows[e.RowIndex].Cells[srccolumn.Name];
                    ComboOption cbOption = (ComboOption)cellSource.Value;
                    if (cbOption != null)
                    {
                        var theCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        var cbCell = (DataGridViewComboBoxCell)theCell;
                        if (cbCell != null)
                        {
                            //cbCell.DataSource = cbOption.Items;
                            //cbCell.DisplayMember = "Name";
                            //cbCell.ValueMember = "Id";
                            cbCell.Items.Clear();
                            cbCell.Items.AddRange(cbOption.Items);
                            cbCell.DisplayMember = "Name";
                            cbCell.ValueMember = "Id";
                        }
                    }
                }
            }
        }

        //private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    DataGridView dgv = (DataGridView)sender;
        //    if (dgv == null || e.ColumnIndex < 0 || e.RowIndex < 0)
        //        return;

        //    //if (DoubleClickRow != null)
        //    //{
        //    //    DoubleClickRow(this, e.RowIndex);
        //    //}
        //}

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
            else if (e.Control is DataGridViewComboBoxEditingControl 
                && !Columns[columnIndex].ReadOnly
                && CurrentCell.RowIndex != -1
                )
            {
                _dgvComboBox = (DataGridViewComboBoxEditingControl)e.Control;

                //add event handler for ComboBox selection 
                _dgvComboBox.SelectionChangeCommitted += new System.EventHandler(DataGridViewComboBox_SelectionChangeCommitted);
            }
        }

        private void DataGridViewComboBox_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            //handle the business
            //how to get the ColumnIndex, RowIndex??
            if (ComboBoxSelectionChangeCommitHandler != null)
            { 
                ComboBox comboBox = (ComboBox)sender;
                if(comboBox != null)
                {
                    int columnIndex = CurrentCell.ColumnIndex;
                    int rowIndex = CurrentCell.RowIndex;

                    ComboBoxSelectionChangeCommitHandler(comboBox, comboBox.SelectedItem, columnIndex, rowIndex);
                }
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
        }

        public override void Sort(DataGridViewColumn dataGridViewColumn, ListSortDirection direction)
        {
            base.Sort(dataGridViewColumn, direction);
        }

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
        }

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            TSDataGridViewHelper.CellFormatting(this, e);
        }

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

        #region

        public void SetFocus(int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= this.Rows.Count || columnIndex < 0 || columnIndex >= this.Columns.Count)
                return;

            this.CurrentCell = this.Rows[rowIndex].Cells[columnIndex];
            BeginEdit(true);
        }

        public void SetFocus(int rowIndex, string columnName)
        {
            if(rowIndex < 0 || rowIndex >= this.Rows.Count)
                return;

            var cell = this.Rows[rowIndex].Cells[columnName];
            if (cell == null)
            {
                return;
            }

            this.CurrentCell = cell;
            BeginEdit(true);
        }
        #endregion
    }
}
