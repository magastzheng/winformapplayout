using Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTest
{
    [TestClass]
    public class ImportConfigTest
    {
        [TestMethod]
        public void TestImportSheet()
        {
            ImportConfig config = new ImportConfig();
            ImportSheet sheet = config.GetSheet("stocktemplate");
            Assert.IsNotNull(sheet);
        }
    }
}
