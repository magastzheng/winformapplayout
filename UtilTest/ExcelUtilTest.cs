using Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace UtilTest
{
    [TestClass]
    public class ExcelUtilTest
    {
        [TestMethod]
        public void TestExcelRead()
        {
            string fileName = @"d:/temp/HK-WIND西药.xlsx";
            string sheetName = @"file";
            //ExcelUtil.ReadExcel(fileName, sheetName);
            Console.WriteLine("test");
        }

        [TestMethod]
        public void TestExcelRead2()
        {
            //string fileName = @"d:/temp/20160222.xls";
            string fileName = @"d:/temp/20160222saveas.xls";
            string sheetName = @"file";
            //ExcelUtil.ReadExcel(fileName, sheetName);
            Console.WriteLine("test");
        }

        [TestMethod]
        public void TestExcelRead3()
        {
            var sheetConfig = ConfigManager.Instance.GetImportConfig().GetSheet("stocktemplate");
            var cellRanges = ConfigManager.Instance.GetImportConfig().GetColumnHeader(sheetConfig.Columns);
            Dictionary<string, DataColumnHeader> colHeadMap = new Dictionary<string, DataColumnHeader>();
            foreach (var column in cellRanges)
            {
                colHeadMap.Add(column.Name, column);
            }

            string fileName = @"d:/temp/20160222saveas.xls";
            string sheetName = @"20160222";
            var table = ExcelUtil.GetSheetData(fileName, sheetName, colHeadMap); 
            Console.WriteLine("test");

            string newFileName = @"d:/temp/20160222_newcopy.xls";
            ExcelUtil.CreateExcel(newFileName, table);
            Console.WriteLine("test2");
        }

        [TestMethod]
        public void TestExcelRead_cellRange()
        {
            var sheetConfig = ConfigManager.Instance.GetImportConfig().GetSheet("stocktemplate");
            var cellRanges = ConfigManager.Instance.GetImportConfig().GetColumnHeader(sheetConfig.Columns);
            Dictionary<string, DataColumnHeader> colHeadMap = new Dictionary<string, DataColumnHeader>();
            foreach (var column in cellRanges)
            {
                colHeadMap.Add(column.Name, column);
            }

            string fileName = @"d:/temp/20160222saveas.xls";
            string sheetName = @"20160222";
            var table = ExcelUtil.GetSheetData(fileName, sheetName, colHeadMap);
            Console.WriteLine("test");

            string newFileName = @"d:/temp/20160222_newcopy_cellRange.xls";
            ExcelUtil.CreateExcel(newFileName, table, sheetConfig.Columns);
            Console.WriteLine("test2");
        }

        [TestMethod]
        public void TestExcel_2007()
        {
            string fileName = @"d:/temp/对冲0812全0819-20160905-resave.xlsx";
            string sheetName = @"对冲0812全0819-20160905";

            Dictionary<string, DataColumnHeader> colHeadMap = new Dictionary<string,DataColumnHeader>();
            ExcelUtil.GetSheetData(fileName, 0, colHeadMap);

            Console.WriteLine(fileName);
        }

        [TestMethod]
        public void TestStringParse()
        {
            string istr = "1,700";
            int temp = 0;
            NumberStyles styles = NumberStyles.Integer | NumberStyles.AllowThousands;

            if (int.TryParse(istr, styles, CultureInfo.InvariantCulture, out temp))
            {
                Console.WriteLine(temp);
            }

            string dstr = "4,212,456.12";
            double dTemp = 0.0f;
            styles = NumberStyles.Float | NumberStyles.AllowThousands;
            if (double.TryParse(dstr, styles, CultureInfo.InvariantCulture, out dTemp))
            {
                Console.WriteLine(dTemp);
            }
        }

        [TestMethod]
        public void Test_Excel_WithoutMergeCell()
        {
            string fileName = @"D:\temp\测试上交所成交模板--test.xls";

            Dictionary<string, DataColumnHeader> colHeadMap = new Dictionary<string, DataColumnHeader>();
            ExcelUtil.GetSheetData(fileName, 0, colHeadMap);
        }

        [TestMethod]
        public void Test_Excel_MergeCell()
        {
            string fileName = @"D:\temp\spottemplate\对冲0930全1021-20161103-saveas.xlsx";

            Dictionary<string, DataColumnHeader> colHeadMap = new Dictionary<string,DataColumnHeader>();
            ExcelUtil.GetSheetData(fileName, 0, colHeadMap);
        }
    }
}
