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
    public class NavbarConfigTest
    {
        //[TestMethod]
        //public void TestGetNavbarConfig()
        //{
        //    var config = ConfigManager.Instance.GetNavbarConfig();
        //    Console.WriteLine("test");
        //}

        [TestMethod]
        public void TestGetNavbarConfig()
        {
            try
            {
                var config = new NavbarConfig();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("test");
        }
    }
}
