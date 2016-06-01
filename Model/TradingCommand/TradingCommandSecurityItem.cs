using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TradingCommand
{
    public class TradingCommandSecurityItem
    {
        public int CommandId { get; set; }

        public string SecuCode { get; set; }

        public SecurityType SecuType { get; set; }

        public int WeightAmount { get; set; }

        public int CommandAmount { get; set; }

        public int EntrustedAmount { get; set; }

        public double CommandPrice { get; set; }

        public EntrustStatus EntrustStatus { get; set; }
    }
}
