using Model.Binding;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using Model.Quote;
using Model.SecurityInfo;

namespace Model.UI
{
    public class ModifySecurityItem
    {
        [BindingAttribute("selection")]
        public bool Selection { get; set; }

        [BindingAttribute("fund")]
        public string Fund { get; set; }

        [BindingAttribute("portfolio")]
        public string Portfolio { get; set; }

        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("longshort")]
        public string LongShort
        { 
            get { return PositionTypeHelper.GetDisplayText(PositionType); }
        }

        [BindingAttribute("exchange")]
        public string Exchange
        {
            get { return SecurityItemHelper.GetExchange(ExchangeCode); }
        }

        [BindingAttribute("origincommandamount")]
        public int OriginCommandAmount { get; set; }

        [BindingAttribute("entrustamount")]
        public int EntrustAmount { get; set; }

        [BindingAttribute("availableamount")]
        public int AvailableAmount { get; set; }

        [BindingAttribute("newcommandamount")]
        public int NewCommandAmount { get; set; }

        [BindingAttribute("entrustdirection")]
        public string EntrustDirection
        {
            //get
            //{
            //    return EnumTypeDisplayHelper.GetEntrustDirection(EDirection);
            //}
            get;
            set;
        }

        [BindingAttribute("origincommandprice")]
        public double OriginCommandPrice { get; set; }

        [BindingAttribute("newcommandprice")]
        public double NewCommandPrice { get; set; }

        [BindingAttribute("lastprice")]
        public double LastPrice { get; set; }

        [BindingAttribute("suspendflag")]
        public string SuspendFlag
        {
            get { return EnumQuoteHelper.GetSuspendFlag(ESuspendFlag); }
        }

        [BindingAttribute("origincommandmoney")]
        public double OriginCommandMoney { get; set; }

        [BindingAttribute("newcommandmoney")]
        public double NewCommandMoney { get; set; }

        [BindingAttribute("pricetype")]
        public string PriceType
        {
            get { return EnumTypeDisplayHelper.GetPriceType(EPriceType); }
        }

        [BindingAttribute("investmenttype")]
        public string InvestmentType { get; set; }

        [BindingAttribute("limitupdown")]
        public string LimitUpDown
        {
            get { return EnumQuoteHelper.GetLimitUpDownFlag(ELimitUpDownFlag); }
        }

        public string ExchangeCode { get; set; }

        public SecurityType SecuType { get; set; }

        public EntrustDirection EDirection { get; set; }

        public PositionType PositionType { get; set; }

        public LimitUpDownFlag ELimitUpDownFlag { get; set; }

        public PriceType EPriceType { get; set; }

        public SuspendFlag ESuspendFlag { get; set; }
    }
}
