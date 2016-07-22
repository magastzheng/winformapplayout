using Model.Binding;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
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
        public int BuyAmount { get; set; }

        [BindingAttribute("sellamount")]
        public int SellAmount { get; set; }

        [BindingAttribute("suspensionflag")]
        public int SuspensionFlag { get; set; }

        [BindingAttribute("replacestatus")]
        public int ReplaceStatus { get; set; }

        [BindingAttribute("limitmove")]
        public int LimitMove { get; set; }

        public SecurityType SecuType { get; set; }
    }
}
