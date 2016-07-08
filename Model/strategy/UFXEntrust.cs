using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.strategy
{
    public class UFXEntrustRequest
    {
        public string UserToken { get; set; }

        public int BatchNo { get; set; }

        public string AccountCode { get; set; }

        public string AssetNo { get; set; }

        public string CombiNo { get; set; }

        public string StockHolderId { get; set; }

        public string ReportSeat { get; set; }

        public string MarketNo { get; set; }

        public string StockCode { get; set; }

        public string EntrustDirection { get; set; }

        public string PriceType { get; set; }

        public double EntrustPrice { get; set; }

        public int EntrustAmount { get; set; }

        public int ExtSystemId { get; set; }

        public string ThirdReff { get; set; }
    }

    public class UFXEntrustResponse
    {
        public int BatchNo { get; set; }

        public int EntrustNo { get; set; }

        public int ExtSystemId { get; set; }
    }

    public class UFXWithdrawRequest
    {
        public string UserToken { get; set; }

        public int EntrustNo { get; set; }
    }

    public class UFXBasketEntrustRequest
    {
        public string UserToken { get; set;}

        public string AccountCode { get; set; }

        public string AssetNo { get; set; }

        public string CombiNo { get; set; }

        public string StockHolderId { get; set; }

        public string ReportSeat { get; set; }

        public string MarketNo { get; set; }

        public string StockCode { get; set; }

        public string EntrustDirection { get; set; }

        public string FuturesDirection { get; set; }

        public int CoveredFlag { get; set; }

        public string PriceType { get; set; }

        public double EntrustPrice { get; set; }

        public int EntrustAmount { get; set; }

        public double LimitEntrustRatio { get; set; }

        public double FutuLimitEntrustRatio { get; set; }

        public double OptLimitEntrustRatio { get; set; }

        public int ExtSystemId { get; set; }

        public string ThirdReff { get; set; }
    }

    public class UFXBasketEntrustResponse
    {
        public int BatchNo { get; set; }

        public int EntrustNo { get; set; }

        public int RequestOrder { get; set; }

        public int ExtSystemId { get; set; }

        public int EntrustFailCode { get; set; }

        public string FailCause { get; set; }

        public int RiskSerialNo { get; set; }

        public string MarketNo { get; set; }

        public string StockCode { get; set; }

        public string EntrustDirection { get; set; }

        public string FuturesDirection { get; set; }

        public int RiskNo { get; set; }

        public int RiskType { get; set; }

        public string RiskSummary { get; set; }

        public string RiskOperation { get; set; }

        public string RemarkShort { get; set; }

        public double RiskThresholdValue { get; set; }

        public double RiskValue { get; set; }
    }

    public class UFXBasketWithdrawRequest
    {
        public string UserToken { get; set; }

        public int BatchNo { get; set; }
    }

    public class UFXBasketWithdrawResponse
    {
        public int EntrustNo { get; set; }

        public string MarketNo { get; set; }

        public string StockCode { get; set; }

        public int SuccessFlag { get; set; }

        public string FailCause { get; set; }
    }
}
