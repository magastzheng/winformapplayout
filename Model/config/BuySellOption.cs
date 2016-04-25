using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BuySellOption
    {
        public List<ComboOptionItem> SpotBuy { get; set; }
        public List<ComboOptionItem> SpotSell { get; set; }
        public List<ComboOptionItem> FutureBuy { get; set; }
        public List<ComboOptionItem> FutureSell { get; set; }
    }
}
