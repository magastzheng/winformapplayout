using Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSUnitTest
{
    [TestClass]
    public class IniFileTest
    {
        [TestMethod]
        public void TestReadFile()
        {
            IniFile iniFile = new IniFile(@"D:\t2sdk.ini");
            string retStr = iniFile.IniReadValue("t2sdk", "license_file");
        }
    }
}
