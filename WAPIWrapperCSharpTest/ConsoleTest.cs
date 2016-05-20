using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WAPIWrapperCSharp;

namespace WAPIWrapperCSharpTest
{
    static class ConsoleTest
    {
        [STAThread]
        static void Main()
        {
            List<string> windSecCodes = new List<string>() { "000001.SZ, 600000.SH" };
            List<string> fields = new List<string>() { "rt_last", "rt_amt", "rt_trade_status", "rt_high_limit", "rt_low_limit", "rt_upward_vol", "rt_downward_vol", "rt_ask1", "rt_ask2", "rt_ask3", "rt_ask4", "rt_ask5", "rt_ask6", "rt_ask7", "rt_ask8", "rt_ask9", "rt_ask10", "rt_bid1", "rt_bid2", "rt_bid3", "rt_bid4", "rt_bid5", "rt_bid6", "rt_bid7", "rt_bid8", "rt_bid9", "rt_bid10" };
            Dictionary<string, string> options = new Dictionary<string, string>();

            int errCode = -1;
            ulong reqId = WindAPIWrap.Instance.RequestData(ref errCode, windSecCodes, fields, options, true, (code, data) => {
                Console.WriteLine("RequestID {0}", code);
                //string output = string.Empty;
                //for
                int codeLen = data.codeList.Length;
                int fieldLen = data.fieldList.Length;

                StringBuilder sb = new StringBuilder();
                sb.Append("Name\t");
                foreach(var field in data.fieldList)
                {
                    sb.AppendFormat("{0}\t", field);
                }
                sb.Append("\n");
                for (int i = 0; i < codeLen; i++)
                {
                    
                    sb.AppendFormat("{0}\t", data.codeList[i]);
                    for (int j = i * fieldLen; j < (i + 1) * fieldLen; j++)
                    {
                        sb.AppendFormat("{0}\t", ((double[])data.data)[j]);
                    }
                    sb.Append("\n");
                }

                Console.WriteLine(sb.ToString());
            });

            Stopwatch sw = new Stopwatch();
            sw.Start();
            //AutoResetEvent ev = new AutoResetEvent(false);
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    if (sw.ElapsedMilliseconds > 20000)
                    {
                        //ev.Set();
                        break;
                    }
                }
            });
            t.Start();

            //ev.WaitOne(-1);
            t.Join();
        }
    }
}
