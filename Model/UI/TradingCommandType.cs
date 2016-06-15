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
        SubmitToDB = 0,     //提交到数据库
        SubmitToUFX = 1,    //提交到UFX
        NoExecuted = 2,     //未执行
        PartExecuted = 3,   //部分执行
        Completed = 4,      //已完成

        CancelToDB = 10,    //撤单
        CancelToUFX = 11,   //撤单到UFX
        CancelSuccess = 12, //撤单成功 
        CancelFail = 13,    //撤单失败
    }

    public enum DealStatus
    {
        NoDeal = 1,     //未成交
        PartDeal = 2,   //部分成交
        Completed = 3,  //已完成
    }
}
