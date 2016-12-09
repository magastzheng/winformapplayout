using Model.Binding;

namespace Model.UFX
{
    /// <summary>
    /// filter data message
    /// </summary>
    public class UFXFilterResponse
    {
        [UFXDataAttribute("msgtype")]
        public string MsgType { get; set; }

        [UFXDataAttribute("market_no")]
        public string MarketNo { get; set; }

        [UFXDataAttribute("operator_no")]
        public string OperatorNo { get; set; }
    }

    /// <summary>
    /// 委托下达成功后推送本条委托消息
    /// msgtype="a"
    /// </summary>
    public class UFXEntrustCompletedResponse
    {
        [UFXDataAttribute("business_date", Data.DataValueType.Int)]
        public int BusinessDate { get; set; }

        [UFXDataAttribute("business_time", Data.DataValueType.Int)]
        public int BusinessTime { get; set; }

        [UFXDataAttribute("batch_no", Data.DataValueType.Int)]
        public int BatchNo { get; set; }

        [UFXDataAttribute("entrust_no", Data.DataValueType.Int)]
        public int EntrustNo { get; set; }

        [UFXDataAttribute("report_no")]
        public string ReportNo { get; set; }

        [UFXDataAttribute("account_code")]
        public string AccountCode { get; set; }

        [UFXDataAttribute("combi_no")]
        public string CombiNo { get; set; }

        [UFXDataAttribute("instance_no")]
        public string InstanceNo { get; set; }

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

        [UFXDataAttribute("price_type")]
        public string PriceType { get; set; }

        [UFXDataAttribute("entrust_price", Data.DataValueType.Float)]
        public double EntrustPrice { get; set; }

        [UFXDataAttribute("entrust_amount", Data.DataValueType.Int)]
        public int EntrustAmount { get; set; }

        [UFXDataAttribute("invest_type")]
        public string InvestType { get; set; }

        [UFXDataAttribute("entrust_state")]
        public string EntrustState { get; set; }

        [UFXDataAttribute("entrust_status")]
        public string EntrustStatus { get; set; }

        [UFXDataAttribute("extsystem_id", Data.DataValueType.Int)]
        public int ExtSystemId { get; set; }

        [UFXDataAttribute("third_reff")]
        public string ThirdReff { get; set; }
    }

    /// <summary>
    /// 对某条委托进行撤单，撤单成功后推送该委托信息
    /// msgtype="e"
    /// </summary>
    public class UFXWithdrawCompletedResponse
    {
        [UFXDataAttribute("business_date", Data.DataValueType.Int)]
        public int BusinessDate { get; set; }

        [UFXDataAttribute("business_time", Data.DataValueType.Int)]
        public int BusinessTime { get; set; }

        [UFXDataAttribute("batch_no", Data.DataValueType.Int)]
        public int BatchNo { get; set; }

        [UFXDataAttribute("entrust_no", Data.DataValueType.Int)]
        public int EntrustNo { get; set; }

        [UFXDataAttribute("cancel_entrust_no", Data.DataValueType.Int)]
        public int CancelEntrustNo { get; set; }

        [UFXDataAttribute("account_code")]
        public string AccountCode { get; set; }

        [UFXDataAttribute("combi_no")]
        public string CombiNo { get; set; }

        [UFXDataAttribute("instance_no")]
        public string InstanceNo { get; set; }

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

        [UFXDataAttribute("entrust_amount", Data.DataValueType.Int)]
        public int EntrustAmount { get; set; }

        [UFXDataAttribute("cancel_amount", Data.DataValueType.Int)]
        public int CancelAmount { get; set; }

        [UFXDataAttribute("invest_type")]
        public string InvestType { get; set; }

        [UFXDataAttribute("entrust_state")]
        public string EntrustState { get; set; }

        [UFXDataAttribute("entrust_status")]
        public string EntrustStatus { get; set; }

        [UFXDataAttribute("extsystem_id", Data.DataValueType.Int)]
        public int ExtSystemId { get; set; }

        [UFXDataAttribute("third_reff")]
        public string ThirdReff { get; set; }
    }

    /// <summary>
    /// 委托有成交，成交处理完成后推送成交信息
    /// msgtype="g"
    /// </summary>
    public class UFXEntrustDealResponse
    {
        [UFXDataAttribute("deal_date", Data.DataValueType.Int)]
        public int DealDate { get; set; }

        [UFXDataAttribute("deal_time", Data.DataValueType.Int)]
        public int DealTime { get; set; }

        [UFXDataAttribute("deal_no")]
        public string DealNo { get; set; }

        [UFXDataAttribute("batch_no", Data.DataValueType.Int)]
        public int BatchNo { get; set; }

        [UFXDataAttribute("entrust_no", Data.DataValueType.Int)]
        public int EntrustNo { get; set; }

        [UFXDataAttribute("market_no")]
        public string MarketNo { get; set; }

        [UFXDataAttribute("stock_code")]
        public string StockCode { get; set; }

        [UFXDataAttribute("account_code")]
        public string AccountCode { get; set; }

        [UFXDataAttribute("combi_no")]
        public string CombiNo { get; set; }

        [UFXDataAttribute("instance_no")]
        public string InstanceNo { get; set; }

        [UFXDataAttribute("stockholder_id")]
        public string StockHolderId { get; set; }

        [UFXDataAttribute("report_seat")]
        public string ReportSeat { get; set; }

        [UFXDataAttribute("entrust_direction")]
        public string EntrustDirection { get; set; }

        [UFXDataAttribute("futures_direction")]
        public string FuturesDirection { get; set; }

        [UFXDataAttribute("entrust_amount", Data.DataValueType.Int)]
        public int EntrustAmount { get; set; }

        [UFXDataAttribute("entrust_state")]
        public string EntrustState { get; set; }

        [UFXDataAttribute("entrust_status")]
        public string EntrustStatus { get; set; }

        [UFXDataAttribute("deal_amount", Data.DataValueType.Int)]
        public int DealAmount { get; set; }

        [UFXDataAttribute("deal_price", Data.DataValueType.Float)]
        public double DealPrice { get; set; }

        [UFXDataAttribute("deal_balance", Data.DataValueType.Float)]
        public double DealBalance { get; set; }

        [UFXDataAttribute("deal_fee", Data.DataValueType.Float)]
        public double DealFee { get; set; }

        [UFXDataAttribute("total_deal_amount", Data.DataValueType.Int)]
        public int TotalDealAmount { get; set; }

        [UFXDataAttribute("total_deal_balance", Data.DataValueType.Float)]
        public double TotalDealBalance { get; set; }

        [UFXDataAttribute("cancel_amount", Data.DataValueType.Int)]
        public int CancelAmount { get; set; }

        [UFXDataAttribute("extsystem_id", Data.DataValueType.Int)]
        public int ExtSystemId { get; set; }

        [UFXDataAttribute("third_reff")]
        public string ThirdReff { get; set; }
    }
}
