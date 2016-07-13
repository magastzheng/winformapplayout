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

        public int BatchNo { get; set; }

        public int EntrustNo { get; set; }

        public string AccountCode { get; set; }

        public string AssetNo { get; set; }

        public string CombiNo { get; set; }

        public string StockHolderId { get; set; }

        public string MarketNo { get; set; }

        public string StockCode { get; set; }

        public string EntrustDirection { get; set; }

        public string EntrustStateList { get; set; }

        public int ExtSystemId { get; set; }

        public string ThirdReff { get; set; }

        public string PositionStr { get; set; }

        public int RequestNum { get; set; }
    }

    public class UFXQueryHistEntrustRequest: UFXQueryEntrustRequest
    {
        public int StartDate { get; set; }

        public int EndDate { get; set; }
    }

    public class UFXQueryEntrustResponse
    {
        public int EntrustDate { get; set; }
        
        public int EntrustTime { get; set; }

        public string OperatorNo { get; set; }

        public int BatchNo { get; set; }

        public int EntrustNo { get; set; }

        public string ReportNo { get; set; }

        public int ExtSystemId { get; set; }

        public string ThirdReff { get; set; }

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

        public double PreBuyFrozenBalance { get; set; }

        public double PreSellBalance { get; set; }

        public string ConfirmNo { get; set; }

        public string EntrustState { get; set; }

        public int FirstDealTime { get; set; }

        public int DealAmount { get; set; }

        public double DealBalance { get; set; }

        public double DealPrice { get; set; }

        public int DealTimes { get; set; }

        public int WithdrawAmount { get; set; }

        public string WithdrawCause { get; set; }

        public string PositionStr { get; set; }

        public string ExchangeReportNo { get; set; }
    }

    public class UFXQueryDealRequest
    {
        //public string UserToken { get; set; }

        public string AccountCode { get; set; }

        public string AssetNo { get; set; }

        public string CombiNo { get; set; }

        public int EntrustNo { get; set; }

        public int DealNo { get; set; }

        public string StockHolderId { get; set; }

        public string MarketNo { get; set; }

        public string StockCode { get; set; }

        public string EntrustDirection { get; set; }

        public int ExtSystemId { get; set; }

        public string ThirdReff { get; set; }

        public string PositionStr { get; set; }

        public int RequestNum { get; set; }
    }

    public class UFXQueryDealResponse
    {
        public int DealDate { get; set; }

        public int DealNo { get; set; }

        public int EntrustNo { get; set; }

        public int ExtSystemId { get; set; }

        public string ThirdReff { get; set; }

        public string AccountCode { get; set; }

        public string AssetNo { get; set; }

        public string CombiNo { get; set; }

        public string InstanceNo { get; set; }

        public string StockHolderId { get; set; }

        public string MarketNo { get; set; }

        public string StockCode { get; set; }

        public string EntrustDirection { get; set; }

        public int DealAmount { get; set; }

        public double DealPrice { get; set; }

        public double DealBalance { get; set; }

        public double TotalFee { get; set; }

        public int DealTime { get; set; }

        public string PositionStr { get; set; }
    }

    public class UFXQueryHistDealRequest : UFXQueryDealRequest
    {
        public int StartDate { get; set; }

        public int EndDate { get; set; }
    }
}
