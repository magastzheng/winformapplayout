using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public enum PositionType
    { 
        Long = 1,
        Short = 2,
    }

    public class TradingInstanceSecurity
    {
        public int InstanceId { get; set; }

        public string SecuCode { get; set; }

        public SecurityType SecuType { get; set; }

        public PositionType PositionType { get; set; }

        public int PositionAmount { get; set; }

        public int AvailableAmount { get; set; }

        public int InstructionPreBuy { get; set; }

        public int InstructionPreSell { get; set; }
    }
}
