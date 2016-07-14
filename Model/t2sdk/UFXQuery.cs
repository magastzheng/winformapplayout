using Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.t2sdk
{
    public class UFXQueryEntrustRequest
    {
        //public string UserToken { get; set; }

        [UFXDataAttribute("batch_no", Data.DataValueType.Int)]
        public int BatchNo { get; set; }

        [UFXDataAttribute("entrust_no", Data.DataValueType.Int)]
        public int EntrustNo { get; set; }

        [UFXDataAttribute("account_code")]
        public string AccountCode { get; set; }

        [UFXDataAttribute("asset_no")]
        public string AssetNo { get; set; }

        [UFXDataAttribute("combi_no")]
        public string CombiNo { get; set; }

        [UFXDataAttribute("stockholder_id")]
        public string StockHolderId { get; set; }

        [UFXDataAttribute("market_no")]
        public string MarketNo { get; set; }

        [UFXDataAttribute("stock_code")]
        public string StockCode { get; set; }

        [UFXDataAttribute("entrust_direction")]
        public string EntrustDirection { get; set; }

        [UFXDataAttribute("entrust_state_list")]
        public string EntrustStateList { get; set; }

        [UFXDataAttribute("extsystem_id")]
        public int ExtSystemId { get; set; }

        [UFXDataAttribute("third_reff")]
        public string ThirdReff { get; set; }

        [UFXDataAttribute("position_str")]
        public string PositionStr { get; set; }

        [UFXDataAttribute("request_num")]
        public int RequestNum { get; set; }
    }

    public class UFXQueryHistEntrustRequest: UFXQueryEntrustRequest
    {
        [UFXDataAttribute("start_date")]
        public int StartDate { get; set; }

        [UFXDataAttribute("end_date", Data.DataValueType.Int)]
        public int EndDate { get; set; }
    }

    public class UFXQueryEntrustResponse
    {
        [UFXDataAttribute("entrust_date", Data.DataValueType.Int)]
        public int EntrustDate { get; set; }

        [UFXDataAttribute("entrust_time", Data.DataValueType.Int)]
        public int EntrustTime { get; set; }

        [UFXDataAttribute("operator_no")]
        public string OperatorNo { get; set; }

        [UFXDataAttribute("batch_no", Data.DataValueType.Int)]
        public int BatchNo { get; set; }

        [UFXDataAttribute("entrust_no", Data.DataValueType.Int)]
        public int EntrustNo { get; set; }

        [UFXDataAttribute("report_no")]
        public string ReportNo { get; set; }

        [UFXDataAttribute("extsystem_id", Data.DataValueType.Int)]
        public int ExtSystemId { get; set; }

        [UFXDataAttribute("third_reff")]
        public string ThirdReff { get; set; }

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

        [UFXDataAttribute("pre_buy_frozen_balance", Data.DataValueType.Float)]
        public double PreBuyFrozenBalance { get; set; }

        [UFXDataAttribute("pre_sell_balance", Data.DataValueType.Float)]
        public double PreSellBalance { get; set; }

        [UFXDataAttribute("confirm_no")]
        public string ConfirmNo { get; set; }

        [UFXDataAttribute("entrust_state")]
        public string EntrustState { get; set; }

        [UFXDataAttribute("first_deal_time", Data.DataValueType.Int)]
        public int FirstDealTime { get; set; }

        [UFXDataAttribute("deal_amount", Data.DataValueType.Int)]
        public int DealAmount { get; set; }

        [UFXDataAttribute("deal_balance", Data.DataValueType.Int)]
        public double DealBalance { get; set; }

        [UFXDataAttribute("deal_price", Data.DataValueType.Float)]
        public double DealPrice { get; set; }

        [UFXDataAttribute("deal_times", Data.DataValueType.Int)]
        public int DealTimes { get; set; }

        [UFXDataAttribute("withdraw_amount", Data.DataValueType.Int)]
        public int WithdrawAmount { get; set; }

        [UFXDataAttribute("withdraw_cause")]
        public string WithdrawCause { get; set; }

        [UFXDataAttribute("position_str")]
        public string PositionStr { get; set; }

        [UFXDataAttribute("exchange_report_no")]
        public string ExchangeReportNo { get; set; }
    }

    public class UFXQueryDealRequest
    {
        //public string UserToken { get; set; }

        [UFXDataAttribute("account_code")]
        public string AccountCode { get; set; }

        [UFXDataAttribute("asset_no")]
        public string AssetNo { get; set; }

        [UFXDataAttribute("combi_no")]
        public string CombiNo { get; set; }

        [UFXDataAttribute("entrust_no", Data.DataValueType.Int)]
        public int EntrustNo { get; set; }

        [UFXDataAttribute("deal_no", Data.DataValueType.Int)]
        public int DealNo { get; set; }

        [UFXDataAttribute("stockholder_id")]
        public string StockHolderId { get; set; }

        [UFXDataAttribute("market_no")]
        public string MarketNo { get; set; }

        [UFXDataAttribute("stock_code")]
        public string StockCode { get; set; }

        [UFXDataAttribute("entrust_direction")]
        public string EntrustDirection { get; set; }

        [UFXDataAttribute("extsystem_id", Data.DataValueType.Int)]
        public int ExtSystemId { get; set; }

        [UFXDataAttribute("third_reff")]
        public string ThirdReff { get; set; }

        [UFXDataAttribute("position_str")]
        public string PositionStr { get; set; }

        [UFXDataAttribute("request_num", Data.DataValueType.Int)]
        public int RequestNum { get; set; }
    }

    public class UFXQueryDealResponse
    {
        [UFXDataAttribute("deal_date", Data.DataValueType.Int)]
        public int DealDate { get; set; }

        [UFXDataAttribute("deal_no", Data.DataValueType.Int)]
        public int DealNo { get; set; }

        [UFXDataAttribute("entrust_no", Data.DataValueType.Int)]
        public int EntrustNo { get; set; }

        [UFXDataAttribute("extsystem_id", Data.DataValueType.Int)]
        public int ExtSystemId { get; set; }

        [UFXDataAttribute("third_reff")]
        public string ThirdReff { get; set; }

        [UFXDataAttribute("account_code")]
        public string AccountCode { get; set; }

        [UFXDataAttribute("asset_no")]
        public string AssetNo { get; set; }

        [UFXDataAttribute("combi_no")]
        public string CombiNo { get; set; }

        [UFXDataAttribute("instance_no")]
        public string InstanceNo { get; set; }

        [UFXDataAttribute("stockholder_id")]
        public string StockHolderId { get; set; }

        [UFXDataAttribute("market_no")]
        public string MarketNo { get; set; }

        [UFXDataAttribute("stock_code")]
        public string StockCode { get; set; }

        [UFXDataAttribute("entrust_direction")]
        public string EntrustDirection { get; set; }

        [UFXDataAttribute("deal_amount", Data.DataValueType.Int)]
        public int DealAmount { get; set; }

        [UFXDataAttribute("deal_price", Data.DataValueType.Float)]
        public double DealPrice { get; set; }

        [UFXDataAttribute("deal_balance", Data.DataValueType.Float)]
        public double DealBalance { get; set; }

        [UFXDataAttribute("total_fee", Data.DataValueType.Float)]
        public double TotalFee { get; set; }

        [UFXDataAttribute("deal_time", Data.DataValueType.Int)]
        public int DealTime { get; set; }

        [UFXDataAttribute("position_str", Data.DataValueType.Int)]
        public string PositionStr { get; set; }
    }

    public class UFXQueryHistDealRequest : UFXQueryDealRequest
    {
        [UFXDataAttribute("start_date", Data.DataValueType.Int)]
        public int StartDate { get; set; }

        [UFXDataAttribute("end_date", Data.DataValueType.Int)]
        public int EndDate { get; set; }
    }
}
