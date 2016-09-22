using System.ComponentModel;
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

    public partial class TSDataGridView : DataGridView
    {
        public event UpdateRelatedDataGrid UpdateRelatedDataGridHandler;
        public event ClickRowHandler MouseClickRow;
        public event ClickRowHandler DoubleClickRow;
        public event NumericUpDownValueChanged NumericUpDownValueChanged;
        public event CellEndEditHandler CellEndEditHandler;

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

            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var val = cell.Value;
            if (CellEndEditHandler != null)
            {
                CellEndEditHandler(e.RowIndex, e.ColumnIndex, dgv.Columns[e.ColumnIndex].Name);
            }
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
            //var ds = this.DataSource;
            //if (ds is TSGridViewData)
            //{

            //}
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
