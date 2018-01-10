using Model.Binding;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using Model.Quote;
using Model.SecurityInfo;

namespace Model.UI
{
    public class ClosePositionSecurityItem
    {
        [BindingAttribute("selection")]
        public bool Selection { get; set; }

        [BindingAttribute("instanceid")]
        public int InstanceId { get; set; }

        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("exchange")]
        public string Exchange
        {
            get { return SecurityItemHelper.GetExchange(ExchangeCode); }
        }

        [BindingAttribute("longshort")]
        public string LongShort
        {
            get
            {
                return EnumTypeDisplayHelper.GetLongShort(SecuType);
            }
        }

        [BindingAttribute("portfolioid")]
        public int PortfolioId { get; set; }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        [BindingAttribute("holdingamount")]
        public int HoldingAmount { get; set; }

        [BindingAttribute("availableamount")]
        public int AvailableAmount { get; set; }

        [BindingAttribute("entrustamount")]
        public int EntrustAmount { get; set; }

        [BindingAttribute("targetmktcap")]
        public double TargetMktCap { get; set; }

        [BindingAttribute("commandmoney")]
        public double CommandMoney { get; set; }

        [BindingAttribute("limitupdown")]
        public string LimitUpOrDown
        {
            get { return EnumQuoteHelper.GetLimitUpDownFlag(ELimitUpDownFlag); }
        }

        [BindingAttribute("holdingweight")]
        public double HoldingWeight { get; set; }

        [BindingAttribute("targetweight")]
        public double TargetWeight { get; set; }

        [BindingAttribute("entrustdirection")]
        public string EntrustDirection 
        {
            get 
            {
                return EnumTypeDisplayHelper.GetEntrustDirection(EDirection);
            }
        }

        [BindingAttribute("commandprice")]
        public double CommandPrice { get; set; }

        [BindingAttribute("lastprice")]
        public double LastPrice { get; set; }

        public string ExchangeCode { get; set; }

        public SecurityType SecuType { get; set; }

        public PositionType PositionType { get; set; }

        public EntrustDirection EDirection { get; set; }

        public SuspendFlag ESuspendFlag { get; set; }

        public LimitUpDownFlag ELimitUpDownFlag { get; set; }
    }
}
