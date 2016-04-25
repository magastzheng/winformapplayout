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
    public class GridConfigTest
    {
        [TestMethod]
        public void TestGridConfig()
        {
            //var grid = ConfigManager.Instance.GetGridConfig().GetGid("cmdtrading");
            //Console.WriteLine("test");

            GridConfig gridConfig = new GridConfig();
            var grid = gridConfig.GetGid("cmdtrading");
        }
    }
}
