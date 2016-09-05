using Model.Binding;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class SourceHoldingItem
    {
        [BindingAttribute("selection")]
        public bool Seletion { get; set; }

        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("exchange")]
        public string Exchange { get; set; }

        [BindingAttribute("stockholderid")]
        public string StockHolderId { get; set; }

        [BindingAttribute("seatno")]
        public string SeatNo { get; set; }

        [BindingAttribute("longshortflag")]
        public string LongShortFlag { get; set; }

        [BindingAttribute("currentamount")]
        public int CurrentAmount { get; set; }

        [BindingAttribute("availabletransferedamount")]
        public int AvailableTransferedAmount { get; set; }

        [BindingAttribute("calcamount")]
        public int CalcAmount { get; set; }

        [BindingAttribute("transferedamount")]
        public int TransferedAmount { get; set; }

        [BindingAttribute("pricetype")]
        public string PriceType { get; set; }

        [BindingAttribute("transferedprice")]
        public double TransferedPrice { get; set; }

        [BindingAttribute("investmenttype")]
        public string InvestmentType { get; set; }

        public PositionType PositionType { get; set; }

        public SecurityType SecuType { get; set; }
    }
}
