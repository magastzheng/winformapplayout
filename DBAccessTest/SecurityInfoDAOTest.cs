using DBAccess.SecurityInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.SecurityInfo;
using System.Collections.Generic;

namespace DBAccessTest
{
    [TestClass]
    public class SecurityInfoDAOTest
    {
        [TestMethod]
        public void Test_SecurityInfoDAO_GetAll()
        {
            var dbdao = new SecurityInfoDAO();
            List<SecurityItem> items = dbdao.Get("", SecurityType.All);

            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
        }

        [TestMethod]
        public void Test_SecurityInfoDAO_GetSingle()
        {
            var dbdao = new SecurityInfoDAO();
            List<SecurityItem> items = dbdao.Get("000001", SecurityType.Stock);

            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count == 1);
        }
    }
}
