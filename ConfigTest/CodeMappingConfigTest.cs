using Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTest
{
    [TestClass]
    public class CodeMappingConfigTest
    {
        [TestMethod]
        public void Test_CodeMapping_SecuCode2WindCode()
        {
            var config = new CodeMappingConfig();
            var item = config.GetItemBySecuCode("000001");

            Assert.IsNotNull(item);
            Assert.AreEqual("999999", item.WindCode);
        }

        [TestMethod]
        public void Test_CodeMapping_WindCode2SecuCode()
        {
            var config = new CodeMappingConfig();
            var item = config.GetItemByWindCode("999999");

            Assert.IsNotNull(item);
            Assert.AreEqual("000001", item.SecuCode);
        }
    }
}
