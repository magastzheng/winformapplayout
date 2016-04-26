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
    public class NavbarConfigTest
    {
        [TestMethod]
        public void GetNavbarConfigTest()
        {
            var config = ConfigManager.Instance.GetNavbarConfig();
            Console.WriteLine("test");
        }
    }
}
