using Model.Binding;

namespace Model.UI
{
    public class CommandManagementEntrustItem
    {
        [BindingAttribute("entrustno")]
        public int EntrustNo { get; set; }

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

        [BindingAttribute("entrustprice")]
        public double EntrustPrice { get; set; }

        [BindingAttribute("effectivedate")]
        public string EffectiveDate { get; set; }

        [BindingAttribute("entrustamount")]
        public int EntrustAmount { get; set; }

        [BindingAttribute("entruststatus")]
        public string EntrustStatus { get; set; }

        [BindingAttribute("todaydealamount")]
        public int TodayDealAmount { get; set; }

        [BindingAttribute("todaydealmoney")]
        public double TodayDealMoney { get; set; }

        [BindingAttribute("entrusttime")]
        public string EntrustTime { get; set; }

        [BindingAttribute("dealpercent")]
        public double DealPercent { get; set; }
    }
}
