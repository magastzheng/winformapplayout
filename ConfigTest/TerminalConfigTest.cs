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
    public class TerminalConfigTest
    {
        [TestMethod]
        public void TestGetMACAddress()
        {
            var _systemConfig = new SystemConfig();
            var config = new TerminalConfig(_systemConfig);
            var macAddress = config.MacAddress;
        }

        [TestMethod]
        public void TestGetIPAddress()
        {
            var terminalConfig = ConfigManager.Instance.GetTerminalConfig();
            var ip = terminalConfig.IPAddress;
        }
    }
}
