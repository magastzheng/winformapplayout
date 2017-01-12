using Model.EnumType;

namespace Model.Database
{
    public class DealSecurity
    {
        public DealSecurity()
        { 
        }

        public DealSecurity(DealSecurity security)
        {
            this.RequestId = security.RequestId;
            this.SubmitId = security.SubmitId;
            this.CommandId = security.CommandId;
            this.SecuCode = security.SecuCode;
            this.DealNo = security.DealNo;
            this.BatchNo = security.BatchNo;
            this.EntrustNo = security.EntrustNo;
            this.ExchangeCode = security.ExchangeCode;
            this.AccountCode = security.AccountCode;

            this.PortfolioCode = security.PortfolioCode;
            this.StockHolderId = security.StockHolderId;
            this.ReportSeat = security.ReportSeat;
            this.DealDate = security.DealDate;
            this.DealTime = security.DealTime;
            this.EntrustDirection = security.EntrustDirection;
            this.EntrustAmount = security.EntrustAmount;
            this.EntrustState = security.EntrustState;
            this.DealAmount = security.DealAmount;
            this.DealPrice = security.DealPrice;

            this.DealBalance = security.DealBalance;
            this.DealFee = security.DealFee;
            this.TotalDealAmount = security.TotalDealAmount;
            this.TotalDealBalance = security.TotalDealBalance;
            this.CancelAmount = security.CancelAmount;
        }

        public int RequestId { get; set; }

        public int SubmitId { get; set; }

        public int CommandId { get; set; }

        public string SecuCode { get; set; }

        public string DealNo { get; set; }

        public int BatchNo { get; set; }

        public int EntrustNo { get; set; }

        public string ExchangeCode { get; set; }

        public string AccountCode { get; set; }

        //11
        public string PortfolioCode { get; set; }

        public string StockHolderId { get; set; }

        public string ReportSeat { get; set; }

        public int DealDate { get; set; }

        public int DealTime { get; set; }

        public EntrustDirection EntrustDirection { get; set; }

        public int EntrustAmount { get; set; }

        public EntrustStatus EntrustState { get; set; }

        public int DealAmount { get; set; }

        public double DealPrice { get; set; }

        //21
        public double DealBalance { get; set; }

        public double DealFee { get; set; }

        public int TotalDealAmount { get; set; }

        public double TotalDealBalance { get; set; }

        public int CancelAmount { get; set; }
    }
}
