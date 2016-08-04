using Model.Binding;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using Model.Quote;
using Model.SecurityInfo;

namespace Model.UI
{
    public class OpenPositionSecurityItem
    {
        [BindingAttribute("selection")]
        public bool Selection { get; set; }

        [BindingAttribute("monitorid")]
        public int MonitorId { get; set; }

        [BindingAttribute("monitorname")]
        public string MonitorName { get; set; }

        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("weightamount")]
        public int WeightAmount { get; set; }

        [BindingAttribute("entrustamount")]
        public int EntrustAmount { get; set; }

        [BindingAttribute("commandprice")]
        public double CommandPrice { get; set; }

        [BindingAttribute("commandmoney")]
        public double CommandMoney { get; set; }

        public EntrustDirection EDirection { get; set; }

        [BindingAttribute("entrustdirection")]
        public string EntrustDirection
        {
            get
            {
                return EnumTypeDisplayHelper.GetEntrustDirection(EDirection);
            }
        }

        [BindingAttribute("lastprice")]
        public double LastPrice { get; set; }

        [BindingAttribute("buyamount")]
        public long BuyAmount { get; set; }

        [BindingAttribute("sellamount")]
        public long SellAmount { get; set; }

        [BindingAttribute("suspensionflag")]
        public string SuspensionFlag
        {
            get { return EnumQuoteHelper.GetSuspendFlag(ESuspendFlag); }
        }

        [BindingAttribute("replacestatus")]
        public int ReplaceStatus { get; set; }

        [BindingAttribute("limitupdown")]
        public string LimitUpOrDown
        {
            get { return EnumQuoteHelper.GetLimitUpDownFlag(ELimitUpDownFlag); }
        }

        public SecurityType SecuType { get; set; }

        public SuspendFlag ESuspendFlag { get; set; }

        public LimitUpDownFlag ELimitUpDownFlag { get; set; }
    }
}
