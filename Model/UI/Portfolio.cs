using Model.Binding;
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

        //public int PortfolioId { get; set; }

        //[BindingAttribute("")]
        //public string PortfolioCode { get; set; }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        //public int AssetUnitId { get; set; }

        //public int FundId { get; set; }

        ////1 个股组合, 2 基本组合
        //public int PortfolioType { get; set; }

        ////1 正常，2 过期， 3 冻结
        //public int PortfolioStatus { get; set; }

        ////a 投机,  b 套保, c 套利
        //public string FuturesInvestType { get; set; }

        //public DateTime? CreatedDate { get; set; }

        //public DateTime? ModifiedDate { get; set; }
    }
}
