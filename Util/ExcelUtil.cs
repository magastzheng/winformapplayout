using log4net;
using Model.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Util
{
    public class ExcelUtil
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region public method to write sheet data

        public static void CreateExcel(string fileName, DataTable dataTable)
        {
            //create the workbook
            HSSFWorkbook wk = new HSSFWorkbook();

            //create the sheet
            ISheet sheet = wk.CreateSheet("mysheet");

            //create a row to write header
            IRow rowHead = sheet.CreateRow(0);
            foreach(var kv in dataTable.ColumnIndex)
            {
                //create the cell in row
                ICell cell = rowHead.CreateCell(kv.Value);
                //add data into the cell
                cell.SetCellValue(kv.Key);
            }

            for (int rowIndex = 0, rowSize = dataTable.Rows.Count; rowIndex < rowSize; rowIndex++)
            { 
                DataRow dataRow = dataTable.Rows[rowIndex];
                IRow row = sheet.CreateRow(rowIndex + 1);

                for (int colIndex = 0, colSize = dataRow.Columns.Count; colIndex < colSize; colIndex++)
                {
                    DataValue dataValue = dataRow.Columns[colIndex];
                    ICell cell = row.CreateCell(colIndex);
                    switch(dataValue.Type)
                    {
                        case DataValueType.Int:
                            cell.SetCellValue(dataValue.GetInt());
                            break;
                        case DataValueType.Float:
                            cell.SetCellValue(dataValue.GetDouble());
                            break;
                        case DataValueType.String:
                            cell.SetCellValue(dataValue.GetStr());
                            break;
                        default:
                            cell.SetCellValue(dataValue.GetStr());
                            break;
                    }
                    
                }
            }

            try
            {
                using (FileStream fs = File.OpenWrite(fileName))
                {
                    wk.Write(fs);
                }
            }
            catch (Exception e)
            {
                string msg = string.Format("Fail to write the data into excel: {0}, message: {1}", fileName, e.Message);
                logger.Error(msg);
                throw;
            }
        }

        public static void CreateExcel(string fileName, DataTable dataTable, List<DataHeader> cellRange)
        {
            //create the workbook
            HSSFWorkbook wk = new HSSFWorkbook();

            //create the sheet
            ISheet sheet = wk.CreateSheet("mysheet");

            int startRow = 0;
            //create the cell ranges
            if (cellRange != null && cellRange.Count > 0)
            {
                IRow rowRange = sheet.CreateRow(startRow);
                int startCol = 0;
                int endCol = 0;
                foreach (var range in cellRange)
                {
                    ICell cell = rowRange.CreateCell(startCol);
                    cell.SetCellValue(range.Name);

                    if (range.Children.Count > 0)
                    {
                        endCol = startCol + range.Children.Count - 1;
                    }
                    else
                    {
                        endCol = startCol;
                    }

                    sheet.AddMergedRegion(new CellRangeAddress(startRow, startRow, startCol, endCol));

                    startCol = endCol + 1;
                }

                startRow += 1;
            }

            //create a row to write header
            IRow rowHead = sheet.CreateRow(startRow);
            foreach (var kv in dataTable.ColumnIndex)
            {
                //create the cell in row
                ICell cell = rowHead.CreateCell(kv.Value);
                //add data into the cell
                cell.SetCellValue(kv.Key);
            }

            startRow += 1;

            for (int rowIndex = 0, rowSize = dataTable.Rows.Count; rowIndex < rowSize; rowIndex++)
            {
                DataRow dataRow = dataTable.Rows[rowIndex];
                IRow row = sheet.CreateRow(startRow + rowIndex);

                for (int colIndex = 0, colSize = dataRow.Columns.Count; colIndex < colSize; colIndex++)
                {
                    DataValue dataValue = dataRow.Columns[colIndex];
                    ICell cell = row.CreateCell(colIndex);
                    switch (dataValue.Type)
                    {
                        case DataValueType.Int:
                            cell.SetCellValue(dataValue.GetInt());
                            break;
                        case DataValueType.Float:
                            cell.SetCellValue(dataValue.GetDouble());
                            break;
                        case DataValueType.String:
                            cell.SetCellValue(dataValue.GetStr());
                            break;
                        default:
                            cell.SetCellValue(dataValue.GetStr());
                            break;
                    }

                }
            }

            try
            {
                using (FileStream fs = File.OpenWrite(fileName))
                {
                    wk.Write(fs);
                }
            }
            catch (Exception e)
            {
                string msg = string.Format("Fail to write the data into excel: {0}, message: {1}", fileName, e.Message);
                logger.Error(msg);
                throw;
            }
        }

        #endregion

        #region public method to read sheet data

        public static DataTable GetSheetData(string fileName, string sheetName, Dictionary<string, DataColumnHeader> colHeadMap)
        {
            DataTable table = new DataTable();
            var wb = GetWorkbook(fileName);
            if (wb == null)
                return table;
            var sheet = GetSheet(wb, sheetName);
            if (sheet == null)
                return table;
            table = ReadSheet(sheet, colHeadMap);
            return table;
        }

        public static DataTable GetSheetData(string fileName, int sheetIndex, Dictionary<string, DataColumnHeader> colHeadMap)
        {
            DataTable table = new DataTable();
            var wb = GetWorkbook(fileName);
            if (wb == null)
                return table;
            var sheet = GetSheet(wb, sheetIndex);
            if (sheet == null)
                return table;
            table = ReadSheet(sheet, colHeadMap);
            return table;
        }

        #endregion

        #region private method to support read SheetData

        private static IWorkbook GetWorkbook(string fileName)
        {
            IWorkbook workbook = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            catch (IOException e)
            {
                string msg = string.Format("Fail to read the file: {0}, message: {1}", fileName, e.Message);
                logger.Error(msg);
            }

            try
            {
                workbook = WorkbookFactory.Create(fs);
            }
            catch
            {
                string msg = string.Format("Fail to read file :{0}", fileName);
                logger.Error(msg);
            }
            finally
            {
                fs.Close();
                fs.Dispose();
            }

            return workbook;
        }

        private static ISheet GetSheet(IWorkbook workbook, string sheetName)
        {
            return workbook.GetSheet(sheetName);
        }

        private static ISheet GetSheet(IWorkbook workbook, int sheetIndex)
        {
            return workbook.GetSheetAt(sheetIndex);
        }

        private static List<DataCellRange> GetSheetMergeCellRange(ISheet sheet)
        {
            List<DataCellRange> ranges = new List<DataCellRange>();
            
            int regionsCount = sheet.NumMergedRegions;
            for (int i = 0; i < regionsCount; i++)
            {
                CellRangeAddress range = sheet.GetMergedRegion(i);
                if(sheet.IsMergedRegion(range))
                {
                    int firstCol = range.FirstColumn;
                    int lastCol = range.LastColumn;
                    int firstRow = range.FirstRow;
                    int lastRow = range.LastRow;
                    int num = range.NumberOfCells;

                    var row = sheet.GetRow(firstRow);
                    var cell = row.GetCell(firstCol);

                    DataCellRange cellRange = new DataCellRange 
                    {
                        Name = cell.StringCellValue,
                        FirstRow = firstRow,
                        LastRow = lastRow,
                        FirstColumn = firstCol,
                        LastColumn = lastCol,
                    };

                    ranges.Add(cellRange);
                }
            }

            return ranges;
        }

        private static DataTable ReadSheet(ISheet sheet, Dictionary<string, DataColumnHeader> colHeadMap)
        {
            var cellRanges = GetSheetMergeCellRange(sheet);

            DataTable table = new DataTable 
            {
                ColumnIndex = new Dictionary<string,int>(),
                Rows = new List<DataRow>()
            };

            Dictionary<int, string> colIndexMap = new Dictionary<int, string>();

            int startRow = 0;
            if (cellRanges.Count > 0)
            {
                startRow = 1;
            }

            //第一行为行头
            IRow row = sheet.GetRow(startRow);
            short minCol = row.FirstCellNum;
            short maxCol = row.LastCellNum;
            for (int colIndex = minCol; colIndex < maxCol; colIndex++)
            {
                ICell cell = row.GetCell(colIndex);
                if (cell != null)
                {
                    string name = cell.StringCellValue;
                    if (!table.ColumnIndex.ContainsKey(name))
                    {
                        table.ColumnIndex.Add(name, colIndex);
                    }
                    if (!colIndexMap.ContainsKey(colIndex))
                    {
                        colIndexMap.Add(colIndex, name);
                    }
                }
            }

            for (int rowIndex = startRow + 1; rowIndex < sheet.LastRowNum; rowIndex++)
            {
                row = sheet.GetRow(rowIndex);
                if (row == null)
                    break;

                DataRow dataRow = new DataRow 
                {
                    Columns = new List<DataValue>()
                };

                minCol = row.FirstCellNum;
                maxCol = row.LastCellNum;
                for (int colIndex = minCol; colIndex < maxCol; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (cell != null)
                    { 
                        string colName = string.Empty;
                        DataColumnHeader colHeader = null;
                        if(colIndexMap.ContainsKey(colIndex))
                        {
                            colName = colIndexMap[colIndex];
                        }
                        if (!string.IsNullOrEmpty(colName) && colHeadMap.ContainsKey(colName))
                        {
                            colHeader = colHeadMap[colName];
                        }

                        DataValue dataValue = new DataValue();
                        if (colHeader != null)
                        {
                            dataValue.Type = colHeader.Type;
                        }
                        else
                        {
                            dataValue.Type = DataValueType.String;
                        }

                        switch (dataValue.Type)
                        {
                            case DataValueType.Int:
                                {
                                    NumberStyles styles = NumberStyles.Integer | NumberStyles.AllowThousands;
                                    if (cell.CellType == CellType.Numeric)
                                    {
                                        dataValue.Value = (int)cell.NumericCellValue;
                                    }
                                    else if (cell.CellType == CellType.String)
                                    {
                                        int temp;
                                        if (int.TryParse(cell.StringCellValue, styles, CultureInfo.InvariantCulture, out temp))
                                        {
                                            dataValue.Value = temp;
                                        }
                                        else
                                        {
                                            dataValue.Value = 0;
                                        }
                                    }
                                    else
                                    {
                                        dataValue.Value = 0;
                                    }
                                }
                                break;
                            case DataValueType.Float:
                                {
                                    NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands;
                                    if (cell.CellType == CellType.Numeric)
                                    {
                                        dataValue.Value = (double)cell.NumericCellValue;
                                    }
                                    else if (cell.CellType == CellType.String)
                                    {
                                        double temp;
                                        if (double.TryParse(cell.StringCellValue, styles, CultureInfo.InvariantCulture, out temp))
                                        {
                                            dataValue.Value = temp;
                                        }
                                        else
                                        {
                                            dataValue.Value = 0f;
                                        }
                                    }
                                    else
                                    {
                                        dataValue.Value = 0f;
                                    }
                                }
                                break;
                            case DataValueType.String:
                                {
                                    if (cell.CellType == CellType.Numeric)
                                    {
                                        dataValue.Value = string.Format("{0}", cell.NumericCellValue);
                                    }
                                    else if (cell.CellType == CellType.String)
                                    {
                                        dataValue.Value = cell.StringCellValue;
                                    }
                                    else 
                                    {
                                        dataValue.Value = string.Empty;
                                    }
                                }
                                break;
                        }

                        dataRow.Columns.Add(dataValue);
                    }
                }//end of columns
                table.Rows.Add(dataRow);
            }//end of rows

            return table;
        }

        #endregion
    }
}
