using Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSUnitTest
{
    [TestClass]
    public class ConfigManagerTest
    {
        [TestMethod]
        public void TestConfigManager()
        {
            var functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QueryDealInstance);
            Console.WriteLine(functionItem.Code);
        }
    }
}
