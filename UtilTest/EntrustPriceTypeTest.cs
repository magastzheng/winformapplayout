using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Converter;
using Model.EnumType;
using Model.UFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilTest
{
    [TestClass]
    public class EntrustPriceTypeTest
    {
        [TestMethod]
        public void TestEntrustPriceType()
        {
            string priceTypeId = "A";

            if (priceTypeId == null || string.IsNullOrEmpty(priceTypeId) || priceTypeId.Length != 1)
            {
                string msg = string.Format("The EntrustPriceType [{0}] is not supported!", priceTypeId);
                throw new NotSupportedException(msg);
            }

            var bytes = Encoding.UTF8.GetBytes(priceTypeId);
            int charCode = (int)bytes[0];
            if (!Enum.IsDefined(typeof(EntrustPriceType), charCode))
            {
                string msg = string.Format("The EntrustPriceType [{0}] is not supported!", priceTypeId);
                throw new NotSupportedException(msg);
            }

            EntrustPriceType priceType = (EntrustPriceType)Enum.ToObject(typeof(EntrustPriceType), charCode);

            Assert.AreEqual(EntrustPriceType.FifthIsLeftOffSZ, priceType);
        }

        [TestMethod]
        public void TestUFXMarketCode()
        {
            string marketCode = "2";
            var eMarket = UFXTypeConverter.GetMarketCode(marketCode);

            Assert.AreEqual(UFXMarketCode.ShenzhenStockExchange, eMarket); 
            Console.WriteLine(eMarket.ToString());

            //Type type = Type.GetType("System.String");

            //Type type = Type.GetType("Model.UFX.UFXMarketCode", true, true);
            //var val = (UFXMarketCode)Enum.Parse(type, "ChinaFinancialFuturesExchange");

            //Type type = Type.GetType("Model.PackageType", true, true);
            //var val = (PackageType)Enum.Parse(type, "Request");

            //Console.WriteLine(val);
        }

        [TestMethod]
        public void TestUFXMarketName()
        {
            var marketCode = UFXMarketCode.ShanghaiFuturesExchange;
            var marketName = UFXTypeConverter.GetMarketName(marketCode);

            Assert.AreEqual("上期所", marketName);
            Console.WriteLine(marketName);
        }
    }
}
