using Model.EnumType;
using Model.SecurityInfo;
using System;

namespace Model.UI
{
    public class TradeInstanceSecurity
    {
        public int InstanceId { get; set; }

        public string SecuCode { get; set; }

        public SecurityType SecuType { get; set; }

        public PositionType PositionType { get; set; }

        public int PositionAmount { get; set; }

        public int AvailableAmount
        {
            get
            {
                if (PositionAmount > BuyToday)
                {
                    return PositionAmount - BuyToday;
                }

                return 0;
            }
        }

        public int InstructionPreBuy { get; set; }

        public int InstructionPreSell { get; set; }

        public double BuyBalance { get; set; }

        public double SellBalance { get; set; }

        public double DealFee { get; set; }

        public int BuyToday { get; set; }

        public int SellToday { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime LastDate { get; set; }
    }
}
