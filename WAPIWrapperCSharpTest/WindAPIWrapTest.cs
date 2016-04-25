using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WAPIWrapperCSharp;

namespace WAPIWrapperCSharpTest
{
    [TestClass]
    public class WindAPIWrapTest
    {
        [TestMethod]
        public void TestGetData()
        {
            List<string> windSecCodes = new List<string>() { "000001.SZ, 600000.SH"};
            List<string> fields = new List<string>() { "rt_last", "rt_amt", "rt_ask1", "rt_ask2", "rt_ask3", "rt_ask4", "rt_ask5", "rt_ask6", "rt_ask7", "rt_ask8", "rt_ask9", "rt_ask10", "rt_bid1", "rt_bid2", "rt_bid3", "rt_bid4", "rt_bid5", "rt_bid6", "rt_bid7", "rt_bid8", "rt_bid9", "rt_bid10" };
            Dictionary<string, string> options = new Dictionary<string, string>();

            int errCode = -1;
            ulong reqId = WindAPIWrap.Instance.RequestData(ref errCode, windSecCodes, fields, options, true, null);
            while (true)
            {
                Thread.Sleep(10000);
            }
        }
    }
}
