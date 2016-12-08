using Model.Binding;

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
        public string EntrustDirection { get; set; }

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
    }
}
