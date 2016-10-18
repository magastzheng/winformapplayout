using Model.Binding;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using Model.SecurityInfo;
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
        public string Exchange
        {
            get { return SecurityItemHelper.GetExchange(ExchangeCode); }
        }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        [BindingAttribute("stockholderid")]
        public string StockHolderId { get; set; }

        [BindingAttribute("seatno")]
        public string SeatNo { get; set; }

        [BindingAttribute("longshortflag")]
        public string LongShortFlag
        {
            get { return PositionTypeHelper.GetDisplayText(PositionType); }
        }

        [BindingAttribute("currentamount")]
        public int CurrentAmount { get; set; }

        [BindingAttribute("investmenttype")]
        public string InvestmentType { get; set; }

        public PositionType PositionType { get; set; }

        public SecurityType SecuType { get; set; }

        public string ExchangeCode { get; set; }

        public string PortfolioCode { get; set; }
    }
}
