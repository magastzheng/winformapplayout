using Model.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class ExcelUtil
    {
        public static void CreateExcel()
        {
            //create the workbook
            HSSFWorkbook wk = new HSSFWorkbook();

            //create the sheet
            ISheet sheet = wk.CreateSheet("mysheet");

            //create a row
            IRow row = sheet.CreateRow(1);
            for (int i = 0; i < 20; i++)
            {
                //create the cell in row
                ICell cell = row.CreateCell(i);
                //add data into the cell
                cell.SetCellValue(i);
            }

            using (FileStream fs = File.OpenWrite(@"d:/myxls.xls"))
            {
                wk.Write(fs);
            }
        }

        public static void ReadExcel(string fileName, string sheetName)
        {
            ISheet sheet;
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                HSSFWorkbook wk = new HSSFWorkbook(fs);
                //sheet = wk.GetSheet(sheetName);
                ReadSheet(wk, sheetName);
            }
            catch
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                XSSFWorkbook wk = new XSSFWorkbook(fs);
                //sheet = wk.GetSheet(sheetName);
                ReadSheet(wk, sheetName);
            }
            finally
            {
                fs.Close();
                fs.Dispose();
            }
        }

        public static void ReadExcel()
        {
            StringBuilder sb = new StringBuilder();
            using (FileStream fs = File.OpenRead(@"d:/myxls.xls"))
            {
                HSSFWorkbook wk = new HSSFWorkbook(fs);
                for (int i = 0; i < wk.NumberOfSheets; i++)
                {
                    ISheet sheet = wk.GetSheetAt(i);
                    for (int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
                    {
                        IRow row = sheet.GetRow(rowIndex);
                        if (row != null)
                        {
                            sb.Append("--------------------------------\r\n");
                            for (int colIndex = 0; colIndex <= row.LastCellNum; colIndex++)
                            {
                                ICell cell = row.GetCell(colIndex);
                                if (cell != null)
                                {
                                    sb.Append(cell.ToString());
                                }
                            }
                        }
                    }
                }
            }

            using (StreamWriter wr = new StreamWriter(new FileStream(@"d:/mytext.txt", FileMode.Append)))
            {
                wr.Write(sb.ToString());
                wr.Flush();
            }
        }

        public static void ReadSheet(IWorkbook workbook, string sheetName)
        {
            ISheet sheet = workbook.GetSheet(sheetName);
            if (sheet == null)
                return;

            StringBuilder sb = new StringBuilder();
            for (int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                IRow row = sheet.GetRow(rowIndex);
                if (row != null)
                {
                    sb.Append("--------------------------------\r\n");
                    for (int colIndex = 0; colIndex <= row.LastCellNum; colIndex++)
                    {
                        ICell cell = row.GetCell(colIndex);
                        if (cell != null)
                        {
                            sb.AppendFormat("{0}\t",cell.ToString());
                        }
                    }
                }
            }
            Console.WriteLine(sb.ToString());
        }

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

        public static IWorkbook GetWorkbook(string fileName)
        {
            IWorkbook workbook = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            catch(Exception e)
            {
                Console.WriteLine("Fail to read file: " + fileName + e.Message);
            }
            try
            {
                workbook = new HSSFWorkbook(fs);
            }
            catch
            {
                workbook = new XSSFWorkbook(fs);
            }
            finally
            {
                fs.Close();
                fs.Dispose();
            }

            return workbook;
        }

        public static ISheet GetSheet(IWorkbook workbook, string sheetName)
        {
            return workbook.GetSheet(sheetName);
        }

        public static ISheet GetSheet(IWorkbook workbook, int sheetIndex)
        {
            return workbook.GetSheetAt(sheetIndex);
        }

        public static DataTable ReadSheet(ISheet sheet, Dictionary<string, DataColumnHeader> colHeadMap)
        {
            DataTable table = new DataTable 
            {
                ColumnIndex = new Dictionary<string,int>(),
                Rows = new List<DataRow>()
            };

            Dictionary<int, string> colIndexMap = new Dictionary<int, string>();
            //第一行为行头
            IRow row = sheet.GetRow(1);
            for (int colIndex = 0; colIndex <= row.LastCellNum; colIndex++)
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

            for (int rowIndex = 2; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                row = sheet.GetRow(rowIndex);
                DataRow dataRow = new DataRow 
                {
                    Columns = new List<DataValue>()
                };

                for (int colIndex = 0; colIndex <= row.LastCellNum; colIndex++)
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
                                    int temp;
                                    if (int.TryParse(cell.StringCellValue, out temp))
                                    {
                                        dataValue.Value = temp;
                                    }
                                    else
                                    {
                                        dataValue.Value = 0;
                                    }
                                }
                                break;
                            case DataValueType.Float:
                                {
                                    double temp;
                                    if (double.TryParse(cell.StringCellValue, out temp))
                                    {
                                        dataValue.Value = temp;
                                    }
                                    else
                                    {
                                        dataValue.Value = 0f;
                                    }
                                }
                                break;
                            case DataValueType.String:
                                {
                                    dataValue.Value = cell.StringCellValue;
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
    }
}
