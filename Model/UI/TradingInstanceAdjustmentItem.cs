using Model.EnumType;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class TradingInstanceAdjustmentItem
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int SourceInstanceId { get; set; }

        public string SourceFundCode { get; set; }

        public string SourcePortfolioCode { get; set; }

        public int DestinationInstanceId { get; set; }

        public string DestinationFundCode { get; set; }

        public string DestinationPortfolioCode { get; set; }

        public string SecuCode { get; set; }

        public SecurityType SecuType { get; set; }

        public PositionType PositionType { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }

        public AdjustmentType AdjustType { get; set; }

        public string Operator { get; set; }

        public string StockHolderId { get; set; }

        public string SeatNo { get; set; }

        public string Notes { get; set; }
    }
}
