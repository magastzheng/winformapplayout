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
            Dictionary<string, DataColumnHeader> colHeadMap = new Dictionary<string, DataColumnHeader>();
            foreach (var column in sheetConfig.Columns)
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
    }
}
