using Model.Binding;
using Model.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class Portfolio
    {
        [BindingAttribute("fundname")]
        public string FundName { get; set; }

        [BindingAttribute("fundcode")]
        public string FundCode { get; set; }

        [BindingAttribute("accounttype")]
        public int AccountType { get; set; }

        [BindingAttribute("capitalaccount")]
        public string CapitalAccount { get; set; }

        [BindingAttribute("assetno")]
        public string AssetNo { get; set; }

        [BindingAttribute("assetname")]
        public string AssetName { get; set; }

        [BindingAttribute("portfoliono")]
        public string PortfolioNo { get; set; }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        public int PortfolioId { get; set; }

        public PortfolioStatus PortfolioStatus { get; set; }
    }
}
