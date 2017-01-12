using Model.Binding;

namespace Model.UFX
{
    public class UFXHoldingRequest
    {
        [UFXDataAttribute("account_code")]
        public string AccountCode { get; set; }

        [UFXDataAttribute("asset_no")]
        public string AssetNo { get; set; }

        [UFXDataAttribute("combi_no")]
        public string CombiNo { get; set; }

        [UFXDataAttribute("market_no")]
        public string MarketNo { get; set; }

        [UFXDataAttribute("stock_code")]
        public string StockCode { get; set; }

        [UFXDataAttribute("stockholder_id")]
        public string StockHolderId { get; set; }

        [UFXDataAttribute("hold_seat")]
        public string HoldSeat { get; set; }
    }

    public class UFXHoldingResponse
    {
        [UFXDataAttribute("account_code")]
        public string AccountCode { get; set; }

        [UFXDataAttribute("asset_no")]
        public string AssetNo { get; set; }

        [UFXDataAttribute("combi_no")]
        public string CombiNo { get; set; }

        [UFXDataAttribute("market_no")]
        public string MarketNo { get; set; }

        [UFXDataAttribute("stock_code")]
        public string StockCode { get; set; }

        [UFXDataAttribute("stockholder_id")]
        public string StockHolderId { get; set; }

        [UFXDataAttribute("hold_seat")]
        public string HoldSeat { get; set; }

        [UFXDataAttribute("invest_type")]
        public string InvestType { get; set; }

        [UFXDataAttribute("current_amount", Data.DataValueType.Int)]
        public int CurrentAmount { get; set; }

        [UFXDataAttribute("enable_amount", Data.DataValueType.Int)]
        public int EnableAmount { get; set; }

        [UFXDataAttribute("begin_cost", Data.DataValueType.Float)]
        public double BeginCost { get; set; }

        [UFXDataAttribute("current_cost", Data.DataValueType.Float)]
        public double CurrentCost { get; set; }

        [UFXDataAttribute("pre_buy_amount", Data.DataValueType.Int)]
        public int PreBuyAmount { get; set; }

        [UFXDataAttribute("pre_sell_amount", Data.DataValueType.Int)]
        public int PreSellAmount { get; set; }

        [UFXDataAttribute("pre_buy_balance", Data.DataValueType.Float)]
        public double PreBuyBalance { get; set; }

        [UFXDataAttribute("pre_sell_balance", Data.DataValueType.Float)]
        public double PreSellBalance { get; set; }

        [UFXDataAttribute("today_buy_amount", Data.DataValueType.Int)]
        public int TodayBuyAmount { get; set; }

        [UFXDataAttribute("today_sell_amount", Data.DataValueType.Int)]
        public int TodaySellAmount { get; set; }

        [UFXDataAttribute("today_buy_balance", Data.DataValueType.Float)]
        public double TodayBuyBalance { get; set; }

        [UFXDataAttribute("today_sell_balance", Data.DataValueType.Float)]
        public double TodaySellBalance { get; set; }

        [UFXDataAttribute("today_buy_fee", Data.DataValueType.Float)]
        public double TodayBuyFee { get; set; }

        [UFXDataAttribute("today_sell_fee", Data.DataValueType.Float)]
        public double TodaySellFee { get; set; }
    }

    public class UFXMultipleHoldingResponse
    {
        [UFXDataAttribute("account_code")]
        public string AccountCode { get; set; }

        [UFXDataAttribute("asset_no")]
        public string AssetNo { get; set; }

        [UFXDataAttribute("combi_no")]
        public string CombiNo { get; set; }

        [UFXDataAttribute("market_no")]
        public string MarketNo { get; set; }

        [UFXDataAttribute("stock_code")]
        public string StockCode { get; set; }

        [UFXDataAttribute("stockholder_id")]
        public string StockHolderId { get; set; }

        [UFXDataAttribute("hold_seat")]
        public string HoldSeat { get; set; }

        [UFXDataAttribute("position_flag")]
        public string PositionFlag { get; set; }

        [UFXDataAttribute("invest_type")]
        public string InvestType { get; set; }

        [UFXDataAttribute("current_amount", Data.DataValueType.Int)]
        public int CurrentAmount { get; set; }

        [UFXDataAttribute("enable_amount", Data.DataValueType.Int)]
        public int EnableAmount { get; set; }

        [UFXDataAttribute("current_cost", Data.DataValueType.Float)]
        public double CurrentCost { get; set; }
    }

    public class UFXFutureHoldingRequest : UFXHoldingRequest
    {
        [UFXDataAttribute("invest_type")]
        public string InvestType { get; set; }

        [UFXDataAttribute("position_flag")]
        public string PositionFlag { get; set; }
    }

    public class UFXFutureHoldingResponse : UFXHoldingResponse
    {
        [UFXDataAttribute("today_amount", Data.DataValueType.Int)]
        public int TodayAmount { get; set; }

        [UFXDataAttribute("lastday_amount", Data.DataValueType.Int)]
        public int LastDayAmount { get; set; }

        [UFXDataAttribute("today_enable_amount", Data.DataValueType.Int)]
        public int TodayEnableAmount { get; set; }

        [UFXDataAttribute("lastday_enable_amount", Data.DataValueType.Int)]
        public int LastDayEnableAmount { get; set; }

        [UFXDataAttribute("current_cost_price", Data.DataValueType.Int)]
        public double CurrentCostPrice { get; set; }
    }

    public class UFXFutureHoldingDetailRequest
    {
        [UFXDataAttribute("account_code")]
        public string AccountCode { get; set; }

        [UFXDataAttribute("asset_no")]
        public string AssetNo { get; set; }

        [UFXDataAttribute("combi_no")]
        public string CombiNo { get; set; }

        [UFXDataAttribute("market_no")]
        public string MarketNo { get; set; }

        [UFXDataAttribute("stock_code")]
        public string StockCode { get; set; }

        [UFXDataAttribute("stockholder_id")]
        public string StockHolderId { get; set; }

        [UFXDataAttribute("invest_type")]
        public string InvestType { get; set; }

        [UFXDataAttribute("position_flag")]
        public string PositionFlag { get; set; }
    }

    public class UFXFutureHoldingDetailResponse
    {
        [UFXDataAttribute("account_code")]
        public string AccountCode { get; set; }

        [UFXDataAttribute("asset_no")]
        public string AssetNo { get; set; }

        [UFXDataAttribute("combi_no")]
        public string CombiNo { get; set; }

        [UFXDataAttribute("market_no")]
        public string MarketNo { get; set; }

        [UFXDataAttribute("stock_code")]
        public string StockCode { get; set; }

        [UFXDataAttribute("stockholder_id")]
        public string StockHolderId { get; set; }

        [UFXDataAttribute("position_flag")]
        public string PositionFlag { get; set; }

        [UFXDataAttribute("invest_type")]
        public string InvestType { get; set; }

        [UFXDataAttribute("stock_open_date", Data.DataValueType.Int)]
        public int StockOpenDate { get; set; }

        [UFXDataAttribute("deal_no")]
        public string DealNo { get; set; }

        [UFXDataAttribute("open_amount", Data.DataValueType.Int)]
        public int OpenAmount { get; set; }

        [UFXDataAttribute("current_amount", Data.DataValueType.Int)]
        public int CurrentAmount { get; set; }

        [UFXDataAttribute("drop_amount", Data.DataValueType.Int)]
        public int DropAmount { get; set; }

        [UFXDataAttribute("occupy_deposit_balance", Data.DataValueType.Float)]
        public double OccupyDepositBalance { get; set; }

        [UFXDataAttribute("open_price", Data.DataValueType.Float)]
        public double OpenPrice { get; set; }

        [UFXDataAttribute("drop_income", Data.DataValueType.Float)]
        public double DropIncome { get; set; }

        [UFXDataAttribute("total_fee", Data.DataValueType.Float)]
        public double TotalFee { get; set; }

        [UFXDataAttribute("pre_settlement_price", Data.DataValueType.Float)]
        public double PreSettlementPrice { get; set; }

        [UFXDataAttribute("multiple", Data.DataValueType.Int)]
        public int Multiple { get; set; }
    }
}
