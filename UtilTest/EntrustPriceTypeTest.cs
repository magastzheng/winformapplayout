using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.EnumType;
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
    }
}
