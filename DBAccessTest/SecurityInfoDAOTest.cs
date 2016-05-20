using DBAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccessTest
{
    [TestClass]
    public class SecurityInfoDAOTest
    {
        [TestMethod]
        public void Test_SecurityInfoDAO_GetAll()
        {
            var dbdao = new SecurityInfoDAO();
            List<SecurityItem> items = dbdao.Get("", -1);

            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
        }

        [TestMethod]
        public void Test_SecurityInfoDAO_GetSingle()
        {
            var dbdao = new SecurityInfoDAO();
            List<SecurityItem> items = dbdao.Get("000001", 2);

            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count == 1);
        }
    }
}
