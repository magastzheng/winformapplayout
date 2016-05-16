using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }

        public string PortfolioCode { get; set; }

        public string PortfolioName { get; set; }

        public int AssetUnitId { get; set; }

        public int FundId { get; set; }

        //1 个股组合, 2 基本组合
        public int PortfolioType { get; set; }

        //1 正常，2 过期， 3 冻结
        public int PortfolioStatus { get; set; }

        //a 投机,  b 套保, c 套利
        public string FuturesInvestType { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
