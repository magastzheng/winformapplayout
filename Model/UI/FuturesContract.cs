using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class FuturesContract
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public double PriceLimits { get; set; }
        public double Deposit { get; set; }
        public DateTime FirstTradingDay { get; set; }
        public DateTime LastTradingDay { get; set; }
        public DateTime LastDeliveryDay { get; set; }
    }
}
