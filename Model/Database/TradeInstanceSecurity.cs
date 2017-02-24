using Model.EnumType;
using Model.SecurityInfo;
using System;

namespace Model.UI
{
    public class TradeInstanceSecurity
    {
        public TradeInstanceSecurity()
        { 
        }

        public TradeInstanceSecurity(TradeInstanceSecurity security)
        {
            InstanceId = security.InstanceId;
            SecuCode = security.SecuCode;
            SecuType = security.SecuType;
            PositionType = security.PositionType;
            PositionAmount = security.PositionAmount;
            InstructionPreBuy = security.InstructionPreBuy;
            InstructionPreSell = security.InstructionPreSell;
            BuyBalance = security.BuyBalance;
            SellBalance = security.SellBalance;
            DealFee = security.DealFee;
            BuyToday = security.BuyToday;
            SellToday = security.SellToday;
            CreatedDate = security.CreatedDate;
            ModifiedDate = security.ModifiedDate;
            LastDate = security.LastDate;
        }

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
