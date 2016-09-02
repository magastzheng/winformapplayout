using Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class DestinationHoldingItem
    {
        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("exchange")]
        public string Exchange { get; set; }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        [BindingAttribute("stockholderid")]
        public string StockHolderId { get; set; }

        [BindingAttribute("seatno")]
        public string SeatNo { get; set; }

        [BindingAttribute("longshortflag")]
        public string LongShortFlag { get; set; }

        [BindingAttribute("currentamount")]
        public int CurrentAmount { get; set; }

        [BindingAttribute("investmenttype")]
        public string InvestmentType { get; set; }
    }
}
