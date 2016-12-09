using Model.Binding;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using Model.SecurityInfo;

namespace Model.UI
{
    public class CommandManagementDealItem
    {
        [BindingAttribute("dealno")]
        public int DealNo { get; set; }

        [BindingAttribute("fundname")]
        public string FundName { get; set; }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("entrustdirection")]
        public string EntrustDirection
        {
            get { return EnumTypeDisplayHelper.GetEntrustDirection(EDirection); }
        }

        [BindingAttribute("pricetype")]
        public string PriceType { get; set; }

        [BindingAttribute("dealprice")]
        public double DealPrice { get; set; }

        [BindingAttribute("dealamount")]
        public int DealAmount { get; set; }

        [BindingAttribute("dealmoney")]
        public double DealMoney { get; set; }

        [BindingAttribute("businesstime")]
        public string BusinessTime { get; set; }

        public SecurityType SecuType { get; set; }

        public EntrustDirection EDirection { get; set; }

        public DealStatus EDealStatus { get; set; }

        public int CommandId { get; set; }
    }
}
