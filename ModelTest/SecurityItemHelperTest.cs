using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTest
{
    [TestClass]
    public class SecurityItemHelperTest
    {
        [TestMethod]
        public void Test_GetExchange()
        {
            var actual = SecurityItemHelper.GetExchange(Exchange.SHSE);

            Assert.AreEqual("上交所", actual);

            actual = SecurityItemHelper.GetExchange(Exchange.SZSE);

            Assert.AreEqual("深交所", actual);

            actual = SecurityItemHelper.GetExchange(Exchange.CFFEX);

            Assert.AreEqual("中金所", actual);
        }

        [TestMethod]
        public void Test_GetSecurityType()
        {
            var actual = SecurityItemHelper.GetSecurityType("000001", Exchange.SHSE);
            Assert.AreEqual(SecurityType.Index, actual);

            actual = SecurityItemHelper.GetSecurityType("000001", Exchange.SZSE);
            Assert.AreEqual(SecurityType.Stock, actual);

            actual = SecurityItemHelper.GetSecurityType("002415", Exchange.SZSE);
            Assert.AreEqual(SecurityType.Stock, actual);

            actual = SecurityItemHelper.GetSecurityType("300001", Exchange.SZSE);
            Assert.AreEqual(SecurityType.Stock, actual);

            actual = SecurityItemHelper.GetSecurityType("600000", Exchange.SHSE);
            Assert.AreEqual(SecurityType.Stock, actual);

            actual = SecurityItemHelper.GetSecurityType("399001", Exchange.SZSE);
            Assert.AreEqual(SecurityType.Index, actual);

            actual = SecurityItemHelper.GetSecurityType("399998", Exchange.SZSE);
            Assert.AreEqual(SecurityType.Index, actual);

            actual = SecurityItemHelper.GetSecurityType("399999", Exchange.SZSE);
            Assert.AreEqual(SecurityType.All, actual);

            actual = SecurityItemHelper.GetSecurityType("930901", Exchange.SHSE);
            Assert.AreEqual(SecurityType.Index, actual);

            actual = SecurityItemHelper.GetSecurityType("950110", Exchange.SHSE);
            Assert.AreEqual(SecurityType.Index, actual);

            actual = SecurityItemHelper.GetSecurityType("IF1612", Exchange.CFFEX);
            Assert.AreEqual(SecurityType.Futures, actual);

            actual = SecurityItemHelper.GetSecurityType("IH1612", Exchange.CFFEX);
            Assert.AreEqual(SecurityType.Futures, actual);

            actual = SecurityItemHelper.GetSecurityType("IC1612", Exchange.CFFEX);
            Assert.AreEqual(SecurityType.Futures, actual);
        }

        [TestMethod]
        public void Test_GetExchangeCode()
        {
            var actual = SecurityItemHelper.GetExchangeCode("000001", SecurityType.Stock);
            Assert.AreEqual(Exchange.SZSE, actual);

            actual = SecurityItemHelper.GetExchangeCode("002001", SecurityType.Stock);
            Assert.AreEqual(Exchange.SZSE, actual);

            actual = SecurityItemHelper.GetExchangeCode("300024", SecurityType.Stock);
            Assert.AreEqual(Exchange.SZSE, actual);

            actual = SecurityItemHelper.GetExchangeCode("600110", SecurityType.Stock);
            Assert.AreEqual(Exchange.SHSE, actual);

            actual = SecurityItemHelper.GetExchangeCode("000001", SecurityType.Index);
            Assert.AreEqual(Exchange.SHSE, actual);

            actual = SecurityItemHelper.GetExchangeCode("399001", SecurityType.Index);
            Assert.AreEqual(Exchange.SZSE, actual);

            actual = SecurityItemHelper.GetExchangeCode("930901", SecurityType.Index);
            Assert.AreEqual(Exchange.SHSE, actual);

            actual = SecurityItemHelper.GetExchangeCode("950110", SecurityType.Index);
            Assert.AreEqual(Exchange.SHSE, actual);

            actual = SecurityItemHelper.GetExchangeCode("IF1612", SecurityType.Futures);
            Assert.AreEqual(Exchange.CFFEX, actual);

            actual = SecurityItemHelper.GetExchangeCode("IC1612", SecurityType.Futures);
            Assert.AreEqual(Exchange.CFFEX, actual);

            actual = SecurityItemHelper.GetExchangeCode("IH1612", SecurityType.Futures);
            Assert.AreEqual(Exchange.CFFEX, actual);
        }
    }
}
