using Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UFX
{
    public class UFXQueryMoneyRequest
    {
        [UFXDataAttribute("account_code")]
        public string AccountCode { get; set; }

        [UFXDataAttribute("asset_no")]
        public string AssetNo { get; set; }

        [UFXDataAttribute("combi_no")]
        public string CombiNo { get; set; }
    }

    public class UFXQueryMoneyResponse
    {
        [UFXDataAttribute("account_code")]
        public string AccountCode { get; set; }

        [UFXDataAttribute("asset_no")]
        public string AssetNo { get; set; }

        [UFXDataAttribute("enable_balance_t0", Data.DataValueType.Float)]
        public double EnableBalanceT0 { get; set; }

        [UFXDataAttribute("enable_balance_t1", Data.DataValueType.Float)]
        public double EnableBalanceT1 { get; set; }

        [UFXDataAttribute("current_balance", Data.DataValueType.Float)]
        public double CurrentBalance { get; set; }
    }
}
