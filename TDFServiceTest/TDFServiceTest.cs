using Config;
using Quote.TDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TDFServiceTest
{
    class TDFServiceTest
    {
        static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            var buttonConfig = ConfigManager.Instance.GetButtonConfig();

            TDFQuote quote = new TDFQuote();
            quote.Start();

            while (true) 
            {
                autoResetEvent.WaitOne();
            }
        }
    }
}
