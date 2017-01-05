using DBAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccessTest
{
    [TestClass]
    public class MonitorUnitDAOTest
    {
        [TestMethod]
        public void Test_Create()
        {
            MonitorUnitDAO dao = new MonitorUnitDAO();
            MonitorUnit monitorUnit = new MonitorUnit
            {
                MonitorUnitName = "测试监控单元1",
                EAccountType = MonitorUnitAccountType.SingleAccount,
                PortfolioId = 12,
                BearContract = "IC1609",
                StockTemplateId = 650,
                Owner = 100200
            };

            int id = dao.Create(monitorUnit);
            Assert.IsNotNull(id);

            var actualItem = dao.Get(id);
            Assert.IsNotNull(actualItem);
            Assert.IsTrue(actualItem.MonitorUnitName == monitorUnit.MonitorUnitName);
        }
    }
}
