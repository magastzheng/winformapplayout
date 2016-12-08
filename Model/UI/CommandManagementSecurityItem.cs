using Model.Binding;

namespace Model.UI
{
    public class CommandManagementSecurityItem
    {
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

        [BindingAttribute("commandamount")]
        public int CommandAmount { get; set; }

        [BindingAttribute("commandmoney")]
        public double CommandMoney { get; set; }

        [BindingAttribute("entruststatus")]
        public string EntrustStatus { get; set; }

        [BindingAttribute("dealstatus")]
        public string DealStatus { get; set; }

        [BindingAttribute("todayentrustamount")]
        public int TodayEntrustAmount { get; set; }

        [BindingAttribute("todaydealamount")]
        public int TodayDealAmount { get; set; }

        [BindingAttribute("totalentrustamount")]
        public int TotalEntrustAmount { get; set; }

        [BindingAttribute("totaldealamount")]
        public int TotalDealAmount { get; set; }

        [BindingAttribute("totaldealmoney")]
        public double TotalDealMoney { get; set; }

        [BindingAttribute("todaydealmoney")]
        public double TodayDealMoney { get; set; }

        [BindingAttribute("totalentrustmoney")]
        public double TotalEntrustMoney { get; set; }

        [BindingAttribute("todayentrustmoney")]
        public double TodayEntrustMoney { get; set; }

        [BindingAttribute("averageprice")]
        public double AveragePrice { get; set; }

        [BindingAttribute("unentrustamount")]
        public int UnentrustAmount { get; set; }

        [BindingAttribute("undealamount")]
        public int UndealAmount { get; set; }
    }
}
