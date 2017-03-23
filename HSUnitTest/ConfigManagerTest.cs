using Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.UFX;
using System;

namespace HSUnitTest
{
    [TestClass]
    public class ConfigManagerTest
    {
        [TestMethod]
        public void TestConfigManager()
        {
            var functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(UFXFunctionCode.HeartBeat);
            Console.WriteLine(functionItem.Code);
        }
    }
}
