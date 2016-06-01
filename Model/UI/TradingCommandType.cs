using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public enum CommandType
    {
        Arbitrage = 1,

    }

    public enum ExecuteType
    {
        OpenPosition = 1, //开仓
        ClosePosition = 2, //平仓
        AdjustPosition = 3, //调仓
    }

    public enum EntrustStatus
    {
        NoExecuted = 1,     //未执行
        PartExecuted = 2,   //部分执行
        Completed = 3,      //已完成
    }

    public enum DealStatus
    {
        NoDeal = 1,     //未成交
        PartDeal = 2,   //部分成交
        Completed = 3,  //已完成
    }
}
