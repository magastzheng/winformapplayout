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
    public class TDFAPIConfigTest
    {
        [TestMethod]
        public void Test_GetSetting()
        {
            var config = new TDFAPIConfig();
            var setting = config.GetSetting();

            Assert.IsNotNull(setting);
        }
    }
}
