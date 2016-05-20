using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteTest
{
    [TestClass]
    public class QuoteTest
    {
        [TestMethod]
        public void Test_QuoteCenter_Query()
        {
            List<string> secuCodes = new List<string>() { "000001", "600000"};
            QuoteCenter.Instance.Query(secuCodes);

            var data = QuoteCenter.Instance.Quote;
        }

        [TestMethod]
        public void Test_QuoteCenter_QueryAll()
        {
            QuoteCenter.Instance.Query();
            var data = QuoteCenter.Instance.Quote;

            Assert.IsNotNull(data);
        }
    }
}
