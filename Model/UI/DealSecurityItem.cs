using Model.EnumType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class DealSecurityItem
    {
        public int RequestId { get; set; }

        public int SubmitId { get; set; }

        public int CommandId { get; set; }

        public string SecuCode { get; set; }

        public string DealNo { get; set; }

        public int BatchNo { get; set; }

        public int EntrustNo { get; set; }

        public string ExchangeCode { get; set; }

        public string AccountCode { get; set; }

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

        public double DealBalance { get; set; }

        public double DealFee { get; set; }

        public int TotalDealAmount { get; set; }

        public double TotalDealBalance { get; set; }

        public int CancelAmount { get; set; }
    }
}
