using Model.Binding;

namespace Model.UFX
{
    public class UFXEntrustRequest
    {
        //public string UserToken { get; set; }
        [UFXDataAttribute("batch_no", Data.DataValueType.Int)]
        public int BatchNo { get; set; }

        [UFXDataAttribute("account_code")]
        public string AccountCode { get; set; }

        [UFXDataAttribute("asset_no")]
        public string AssetNo { get; set; }

        [UFXDataAttribute("combi_no")]
        public string CombiNo { get; set; }

        [UFXDataAttribute("stockholder_id")]
        public string StockHolderId { get; set; }

        [UFXDataAttribute("report_seat")]
        public string ReportSeat { get; set; }

        [UFXDataAttribute("market_no")]
        public string MarketNo { get; set; }

        [UFXDataAttribute("stock_code")]
        public string StockCode { get; set; }

        [UFXDataAttribute("entrust_direction")]
        public string EntrustDirection { get; set; }

        [UFXDataAttribute("price_type")]
        public string PriceType { get; set; }

        [UFXDataAttribute("entrust_price", Data.DataValueType.Float)]
        public double EntrustPrice { get; set; }

        [UFXDataAttribute("entrust_amount", Data.DataValueType.Int)]
        public int EntrustAmount { get; set; }

        [UFXDataAttribute("extsystem_id", Data.DataValueType.Int)]
        public int ExtSystemId { get; set; }

        [UFXDataAttribute("third_reff")]
        public string ThirdReff { get; set; }
    }

    public class UFXEntrustResponse
    {
        [UFXDataAttribute("batch_no", Data.DataValueType.Int)]
        public int BatchNo { get; set; }

        [UFXDataAttribute("entrust_no", Data.DataValueType.Int)]
        public int EntrustNo { get; set; }

        [UFXDataAttribute("extsystem_id", Data.DataValueType.Int)]
        public int ExtSystemId { get; set; }
    }

    public class UFXWithdrawRequest
    {
        //public string UserToken { get; set; }
        [UFXDataAttribute("entrust_no", Data.DataValueType.Int)]
        public int EntrustNo { get; set; }
    }

    public class UFXBasketEntrustRequest
    {
        //public string UserToken { get; set;}
        [UFXDataAttribute("account_code")]
        public string AccountCode { get; set; }

        [UFXDataAttribute("asset_no")]
        public string AssetNo { get; set; }

        [UFXDataAttribute("combi_no")]
        public string CombiNo { get; set; }

        [UFXDataAttribute("stockholder_id")]
        public string StockHolderId { get; set; }

        [UFXDataAttribute("report_seat")]
        public string ReportSeat { get; set; }

        [UFXDataAttribute("market_no")]
        public string MarketNo { get; set; }

        [UFXDataAttribute("stock_code")]
        public string StockCode { get; set; }

        [UFXDataAttribute("entrust_direction")]
        public string EntrustDirection { get; set; }

        [UFXDataAttribute("futures_direction")]
        public string FuturesDirection { get; set; }

        [UFXDataAttribute("covered_flag", Data.DataValueType.Int)]
        public int CoveredFlag { get; set; }

        [UFXDataAttribute("price_type")]
        public string PriceType { get; set; }

        [UFXDataAttribute("entrust_price", Data.DataValueType.Float)]
        public double EntrustPrice { get; set; }

        [UFXDataAttribute("entrust_amount", Data.DataValueType.Int)]
        public int EntrustAmount { get; set; }

        [UFXDataAttribute("limit_entrust_ratio", Data.DataValueType.Float)]
        public double LimitEntrustRatio { get; set; }

        [UFXDataAttribute("ftr_limit_entrust_ratio", Data.DataValueType.Float)]
        public double FutuLimitEntrustRatio { get; set; }

        [UFXDataAttribute("opt_limit_entrust_ratio", Data.DataValueType.Float)]
        public double OptLimitEntrustRatio { get; set; }

        [UFXDataAttribute("extsystem_id", Data.DataValueType.Int)]
        public int ExtSystemId { get; set; }

        [UFXDataAttribute("third_reff")]
        public string ThirdReff { get; set; }
    }

    public class UFXBasketEntrustResponse
    {
        [UFXDataAttribute("batch_no", Data.DataValueType.Int)]
        public int BatchNo { get; set; }

        [UFXDataAttribute("entrust_no", Data.DataValueType.Int)]
        public int EntrustNo { get; set; }

        [UFXDataAttribute("request_order", Data.DataValueType.Int)]
        public int RequestOrder { get; set; }

        [UFXDataAttribute("extsystem_id", Data.DataValueType.Int)]
        public int ExtSystemId { get; set; }

        [UFXDataAttribute("entrust_fail_code", Data.DataValueType.Int)]
        public int EntrustFailCode { get; set; }

        [UFXDataAttribute("fail_cause")]
        public string FailCause { get; set; }

        [UFXDataAttribute("risk_serial_no", Data.DataValueType.Int)]
        public int RiskSerialNo { get; set; }

        [UFXDataAttribute("market_no")]
        public string MarketNo { get; set; }

        [UFXDataAttribute("stock_code")]
        public string StockCode { get; set; }

        [UFXDataAttribute("entrust_direction")]
        public string EntrustDirection { get; set; }

        [UFXDataAttribute("futures_direction")]
        public string FuturesDirection { get; set; }

        [UFXDataAttribute("risk_no", Data.DataValueType.Int)]
        public int RiskNo { get; set; }

        [UFXDataAttribute("risk_type", Data.DataValueType.Int)]
        public int RiskType { get; set; }

        [UFXDataAttribute("risk_summary")]
        public string RiskSummary { get; set; }

        [UFXDataAttribute("risk_operation")]
        public string RiskOperation { get; set; }

        [UFXDataAttribute("remark_short")]
        public string RemarkShort { get; set; }

        [UFXDataAttribute("risk_threshold_value", Data.DataValueType.Float)]
        public double RiskThresholdValue { get; set; }

        [UFXDataAttribute("risk_value", Data.DataValueType.Float)]
        public double RiskValue { get; set; }
    }

    public class UFXBasketWithdrawRequest
    {
        //public string UserToken { get; set; }

        [UFXDataAttribute("batch_no", Data.DataValueType.Int)]
        public int BatchNo { get; set; }
    }

    public class UFXBasketWithdrawResponse
    {
        [UFXDataAttribute("entrust_no", Data.DataValueType.Int)]
        public int EntrustNo { get; set; }

        [UFXDataAttribute("market_no")]
        public string MarketNo { get; set; }

        [UFXDataAttribute("stock_code")]
        public string StockCode { get; set; }

        [UFXDataAttribute("sucess_flag", Data.DataValueType.Int)]
        public int SuccessFlag { get; set; }

        [UFXDataAttribute("fail_cause")]
        public string FailCause { get; set; }
    }
}
