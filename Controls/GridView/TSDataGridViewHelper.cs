using Controls.Entity;
using Model.config;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Controls.GridView
{
    public class TSDataGridViewHelper
    {
        public static void AddColumns(TSDataGridView dgv, TSGrid hsGrid)
        {
            if (dgv == null)
            {
                throw new ArgumentNullException("dgv");
            }
            if (hsGrid == null)
            {
                throw new ArgumentNullException("hsGrid");
            }

            List<TSGridColumn> columns = hsGrid.Columns;
            DataGridViewColumn[] gridColumns = new DataGridViewColumn[columns.Count];
            for (int i = 0, count = columns.Count; i < count; i++)
            {
                TSGridColumn col = columns[i];

                DataGridViewColumn column = null;
                switch (col.ColumnType)
                {
                    case TSGridColumnType.CheckBox:
                        {
                            column = new DataGridViewCheckBoxColumn();
                        }
                        break;
                    case TSGridColumnType.Image:
                        {
                            column = new DataGridViewImageColumn();
                            column.Tag = col.DefaultValue;
                        }
                        break;
                    case TSGridColumnType.Text:
                        {
                            column = new DataGridViewTextBoxColumn();
                        }
                        break;
                    case TSGridColumnType.ComboBox:
                        {
                            column = new DataGridViewComboBoxColumn();
                        }
                        break;
                    case TSGridColumnType.NumericUpDown:
                        {
                            var numericCell = new DataGridViewNumericUpDownColumn();
                            numericCell.Minimum = 0;
                            numericCell.Increment = 1;
                            numericCell.Maximum = 100;

                            column = numericCell;
                        }
                        break;
                    default:
                        {
                            column = new DataGridViewTextBoxColumn();
                        }
                        break;
                }

                if (col.ValueType == DataValueType.Float)
                {
                    column.DefaultCellStyle.Format = "##0.000";
                }

                column.HeaderText = col.Text;
                column.Name = col.Name;
                column.Width = col.Width;
                column.ReadOnly = (col.ReadOnly != 0) ? true : false;
                column.Visible = (col.Visible != 0) ? true : false;
                gridColumns[i] = column;

                if (hsGrid.Background == 1 && column.ReadOnly)
                {
                    column.DefaultCellStyle.BackColor = Color.FromArgb(244, 243, 234);
                }
            }

            dgv.Columns.AddRange(gridColumns);
        }

        #region set databinding

        public static void SetDataBinding(TSDataGridView dgv, Dictionary<string, string> colDataMap)
        {
            if (dgv == null)
            {
                throw new ArgumentNullException("dgv");
            }

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (colDataMap.ContainsKey(column.Name))
                {
                    column.DataPropertyName = colDataMap[column.Name];
                }
            }
        }

        public static void SetDataBinding(TSDataGridView dgv, string cbName, ComboOption cbOption)
        {
            if (dgv == null)
            {
                throw new ArgumentNullException("dgv");
            }

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Name.Equals(cbName) && column is DataGridViewComboBoxColumn)
                {
                    var cbColumn = column as DataGridViewComboBoxColumn;
                    cbColumn.DataSource = cbOption.Items;
                    cbColumn.ValueMember = "Id";
                    cbColumn.DisplayMember = "Text";

                    break;
                }
            }
        }

        #endregion

        #region select

        /// <summary>
        /// 返回选中行按升序排序的行号列表
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static List<int> GetSelectRowIndex(TSDataGridView dgv)
        {
            List<int> selectedIndex = new List<int>();

            int cbColIndex = GetCheckBoxColumnIndex(dgv);
            if (cbColIndex < 0)
                return selectedIndex;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                bool isChecked = (bool)row.Cells[cbColIndex].EditedFormattedValue;
                if (!isChecked)
                    continue;

                selectedIndex.Add(row.Index);
            }

            selectedIndex.Sort();
            return selectedIndex;
        }

        #endregion

        #region image

        public static void CellFormatting(TSDataGridView dgv, DataGridViewCellFormattingEventArgs e)
        {
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            if(columnIndex < 0 || columnIndex > dgv.Columns.Count || rowIndex < 0 || rowIndex > dgv.Rows.Count)
                return;

            DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
            if (column != null)
            {
                if (column is DataGridViewImageColumn)
                {
                    //e.Value = Image.FromFile((string)column.Tag);
                    Image image = Image.FromFile((string)column.Tag);
                    Bitmap bitmap = new Bitmap(image, new Size(20, 20));
                    e.Value = bitmap;
                }
                else if (dgv.Columns["limitupdown"] != null && columnIndex == dgv.Columns["limitupdown"].Index)
                {
                    var row = dgv.Rows[rowIndex];
                    var cell = row.Cells[columnIndex];
                    if (cell.Value.ToString() == "涨停")
                    { 
                        e.CellStyle.ForeColor = ControlConstVariable.LimitForeColor;
                        e.CellStyle.BackColor = ControlConstVariable.LimitUpColor;
                    }
                    else if (cell.Value.ToString() == "跌停")
                    {
                        e.CellStyle.ForeColor = ControlConstVariable.LimitForeColor;
                        e.CellStyle.BackColor = ControlConstVariable.LimitDownColor;
                    }
                }
                else if (dgv.Columns["suspensionflag"] != null && columnIndex == dgv.Columns["suspensionflag"].Index)
                { 
                    var row = dgv.Rows[rowIndex];
                    var cell = row.Cells[columnIndex];
                    if (cell.Value.ToString().Contains("停牌"))
                    {
                        row.DefaultCellStyle.ForeColor = ControlConstVariable.LimitSuspensionColor;
                    }
                }
                //else if (column is DataGridViewComboBoxColumn)
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
                //                cbCell.Items.AddRange(cbOption.Items);
                //                cbCell.DisplayMember = "Name";
                //                cbCell.ValueMember = "Id";
                //            }
                //        }
                //    }
                //}
            }
        }

        #endregion

        #region edit

        #region entrust copies input

        //private void CellParsing(TSDataGridView dgv, DataGridViewCellParsingEventArgs e)
        //{
        //    if (dgv != null && dgv.Columns[e.ColumnIndex].Name == "copies")
        //    {
        //        if (e.DesiredType == typeof(int))
        //        {
        //            e.ParsingApplied = true;
        //        }
        //        else
        //        {
        //            Console.WriteLine("请输入数字！");
        //        }
        //    }
        //}

        public static void NumericCheck(object sender, KeyPressEventArgs e)
        {
            DataGridViewTextBoxEditingControl s = sender as DataGridViewTextBoxEditingControl;

            if (s != null && (e.KeyChar == '.' || e.KeyChar == ','))
            {
                e.KeyChar = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                e.Handled = s.Text.Contains(e.KeyChar);
            }
            else
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        public static void CopiesCheck(object sender, KeyPressEventArgs e)
        {
            DataGridViewTextBoxEditingControl s = sender as DataGridViewTextBoxEditingControl;
            if (s == null)
                return;

            if (char.IsNumber(e.KeyChar))
            {
                int preValue = 0;
                if (!string.IsNullOrEmpty(s.Text))
                {
                    preValue = int.Parse(s.Text);
                }
                int curValue = int.Parse(e.KeyChar.ToString());
                if (preValue * 10 + curValue <= MAX_ENTRUST_AMOUNT)
                {
                    //让操作失效
                    e.Handled = false;
                }
                else
                {
                    //input beyond the range
                    e.Handled = true;
                }
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                //让操作失效
                e.Handled = false;
            }
            else
            {
                Console.WriteLine("输入无效值！");
                //取消事件响应
                e.Handled = true;
            }
        }

        private static void EditingControlShowing(TSDataGridView dgv, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgv == null)
                return;

            if (dgv.CurrentCell.ColumnIndex == dgv.Columns["copies"].Index)
            {
                DataGridViewTextBoxEditingControl dgvTxt = e.Control as DataGridViewTextBoxEditingControl;
                dgvTxt.SelectAll();
                dgvTxt.KeyPress += new KeyPressEventHandler(Cells_KeyPress);
            }
        }

        private static void Cells_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEx(e, (DataGridViewTextBoxEditingControl)sender);
        }

        private static void KeyPressEx(KeyPressEventArgs e, DataGridViewTextBoxEditingControl dgvTxt)
        {
            if (char.IsNumber(e.KeyChar))
            {
                int preValue = 0;
                if (!string.IsNullOrEmpty(dgvTxt.Text))
                {
                    preValue = int.Parse(dgvTxt.Text);
                }
                int curValue = int.Parse(e.KeyChar.ToString());
                if (preValue * 10 + curValue <= MAX_ENTRUST_AMOUNT)
                {
                    //让操作失效
                    e.Handled = false;
                }
                else
                {
                    //input beyond the range
                    e.Handled = true;
                }
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                //让操作失效
                e.Handled = false;
            }
            else
            {
                Console.WriteLine("输入无效值！");
                //取消事件响应
                e.Handled = true;
            }
        }
        #endregion

        #region 增加和减少

        //public static void CellMouseClick(TSDataGridView dgv, DataGridViewCellMouseEventArgs e)
        //{
        //    if (dgv == null || e.ColumnIndex < 0 || e.RowIndex < 0)
        //        return;

        //    if (dgv.Columns["copies"] != null)
        //    {
        //        int copiesIndex = dgv.Columns["copies"].Index;
        //        DataGridViewRow row = dgv.Rows[e.RowIndex];
        //        switch (dgv.Columns[e.ColumnIndex].Name)
        //        {
        //            case "plus":
        //                {
        //                    int oldValue = int.Parse(row.Cells["copies"].Value.ToString());
        //                    if (oldValue < MAX_ENTRUST_AMOUNT)
        //                    {
        //                        row.Cells["copies"].Value = oldValue + 1;
        //                        if (dgv.ClickRow != null)
        //                        { 
                                    
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //invalid input
        //                    }
        //                }
        //                break;
        //            case "minus":
        //                {
        //                    int oldValue = int.Parse(row.Cells["copies"].Value.ToString());
        //                    if (oldValue > 1)
        //                    {
        //                        row.Cells["copies"].Value = oldValue - 1;
        //                    }
        //                    else
        //                    {
        //                        //invalid input
        //                    }
        //                }
        //                break;
        //        }
        //    }
        //}

        #endregion

        #endregion

        //#region set data
        //public static void SetData(TSDataGridView dgv, HSGrid hsGrid, DataTable dataTable)
        //{
        //    foreach (DataRow dataRow in dataTable.Rows)
        //    {
        //        SetRow(dgv, hsGrid, dataRow, dataTable.ColumnIndex);
        //    }
        //}

        //public static void SetRow(TSDataGridView dgv, HSGrid hsGrid, Model.Data.DataRow dataRow, Dictionary<string, int> colIndexMap)
        //{
        //    int rowIndex = dgv.Rows.Add();
        //    DataGridViewRow row = dgv.Rows[rowIndex];
        //    SetRow(dgv, hsGrid, row, dataRow, colIndexMap);
        //}

        //private static void SetRow(TSDataGridView dgv, HSGrid hsGrid, DataGridViewRow row, Model.Data.DataRow dataRow, Dictionary<string, int> colIndexMap)
        //{
        //    bool isSelected = false;
        //    List<HSGridColumn> columns = hsGrid.Columns;

        //    foreach (HSGridColumn col in columns)
        //    {
        //        int index = -1;
        //        if (colIndexMap.ContainsKey(col.Name))
        //        {
        //            index = colIndexMap[col.Name];
        //        }
        //        if (index < 0 || index >= dataRow.Columns.Count)
        //            continue;

        //        Model.Data.DataValue dataValue = dataRow.Columns[index];

        //        switch (col.ColumnType)
        //        {
        //            case HSGridColumnType.CheckBox:
        //                {
        //                    int targetValue = dataValue.GetInt();
        //                    isSelected = targetValue > 0 ? true : false;
        //                    row.Cells[col.Name].Value = isSelected;
        //                }
        //                break;
        //            case HSGridColumnType.Text:
        //                {
        //                    SetDataCell(ref row, col, dataValue);
        //                }
        //                break;
        //            case HSGridColumnType.Image:
        //                {
        //                    string imgPath = dataValue.GetStr();
        //                    Image plusImg = Image.FromFile(imgPath);
        //                    Bitmap plusBt = new Bitmap(plusImg, new Size(20, 20));
        //                    row.Cells[col.Name].Value = plusBt;
        //                }
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

        //private static void SetDataCell(ref DataGridViewRow row, HSGridColumn column, Model.Data.DataValue dataValue)
        //{
        //    switch (column.ValueType)
        //    {
        //        case DataValueType.Int:
        //            {
        //                row.Cells[column.Name].Value = dataValue.GetInt();
        //            }
        //            break;
        //        case DataValueType.Float:
        //            {
        //                row.Cells[column.Name].Value = dataValue.GetDouble();
        //            }
        //            break;
        //        case DataValueType.Char:
        //        case DataValueType.String:
        //            {
        //                row.Cells[column.Name].Value = dataValue.GetStr();
        //            }
        //            break;
        //        default:
        //            {
        //                row.Cells[column.Name].Value = dataValue.GetStr();
        //            }
        //            break;
        //    }
        //}

        //#endregion

        //#region change the row

        //public void AddRow(TSDataGridView dgv, HSGrid hsGrid, DataTable dataTable, Model.Data.DataRow dataRow, Dictionary<string, int> colIndexMap)
        //{
        //    if (dataTable == null)
        //    {
        //        dataTable = new DataTable
        //        {
        //            Rows = new List<DataRow>(),
        //            ColumnIndex = new Dictionary<string, int>()
        //        };

        //        dataTable.Rows.Add(dataRow);
        //    }
        //    else
        //    {
        //        dataTable.Rows.Add(dataRow);
        //    }

        //    SetRow(dgv, hsGrid, dataRow, dataTable.ColumnIndex);
        //}

        //public static bool UpdateRow(TSDataGridView dgv, HSGrid hsGrid, DataTable dataTable, int rowIndex, DataRow dataRow)
        //{
        //    if (rowIndex < 0 || rowIndex >= dataTable.Rows.Count)
        //        return false;
        //    if (rowIndex >= dgv.Rows.Count)
        //        return false;

        //    dataTable.Rows[rowIndex] = dataRow;

        //    DataGridViewRow row = dgv.Rows[rowIndex];
        //    SetRow(dgv, hsGrid, row, dataRow, dataTable.ColumnIndex);

        //    return true;
        //}

        //private bool DeleteRow(TSDataGridView dgv, DataTable dataTable, int rowIndex)
        //{
        //    if (rowIndex < 0 || rowIndex >= dataTable.Rows.Count)
        //    {
        //        return false;
        //    }

        //    if (rowIndex >= dgv.Rows.Count)
        //    {
        //        return false;
        //    }

        //    dataTable.Rows.RemoveAt(rowIndex);
        //    dgv.Rows.RemoveAt(rowIndex);

        //    return true;
        //}

        //#endregion

        #region select row

        //public static Model.Data.DataRow GetDataRow(HSGrid hsGrid, DataGridViewRow row, int defSelection)
        //{
        //    DataRow dataRow = new DataRow();
        //    dataRow.Columns = new List<DataValue>();
        //    var columns = hsGrid.Columns;

        //    for (int i = 0, count = columns.Count; i < count; i++)
        //    {
        //        HSGridColumn column = columns[i];
        //        if (column.ColumnType == HSGridColumnType.None)
        //            continue;

        //        DataValue dataValue = new DataValue();
        //        dataValue.Type = column.ValueType;
        //        if (column.ColumnType == HSGridColumnType.CheckBox)
        //        {
        //            dataValue.Value = defSelection;
        //        }
        //        else
        //        {
        //            dataValue.Value = row.Cells[i].Value;
        //        }
        //        dataRow.Columns.Add(dataValue);
        //    }

        //    return dataRow;
        //}

        //public static Model.Data.DataTable GetSelectionRows(TSDataGridView dgv, HSGrid hsGrid)
        //{
        //    Model.Data.DataTable dataTable = new Model.Data.DataTable();
        //    dataTable.ColumnIndex = new Dictionary<string, int>();
        //    dataTable.Rows = new List<DataRow>();

        //    int cbColIndex = GetCheckBoxColumnIndex(dgv);
        //    if (cbColIndex < 0)
        //        return dataTable;

        //    int validColCount = hsGrid.Columns.Count - 1;
        //    foreach (DataGridViewRow row in dgv.Rows)
        //    {
        //        bool isChecked = (bool)row.Cells[cbColIndex].EditedFormattedValue;
        //        if (!isChecked)
        //            continue;

        //        Model.Data.DataRow dataRow = GetDataRow(hsGrid, row, 1);
        //        dataTable.Rows.Add(dataRow);
        //    }

        //    //TODO:add the column index
        //    foreach (DataGridViewColumn column in dgv.Columns)
        //    { 
                
        //    }

        //    return dataTable;
        //}

        private static int GetCheckBoxColumnIndex(TSDataGridView dgv)
        {
            int index = -1;
            for (int i = 0, count = dgv.Columns.Count; i < count; i++)
            {
                DataGridViewColumn column = dgv.Columns[i];
                if (column is DataGridViewCheckBoxColumn)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        #endregion

        public static int MAX_ENTRUST_AMOUNT { get { return 10; } }
    }
}
