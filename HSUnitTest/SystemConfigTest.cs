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
    public class SystemConfigTest
    {
        [TestMethod]
        public void TestSystemConfig()
        {
            SystemConfig config = new SystemConfig();
            var opstation = config.GetSystemStr("opstation");
            Assert.AreEqual("localhost", opstation);

            var ip = config.GetServerStr("ip");
            Assert.AreEqual("10.10.10.19", ip);

            var port = config.GetServerInt("port");
            Assert.AreEqual(5123, port);
        }
    }
}
