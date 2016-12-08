
using System.ComponentModel;
namespace Model.EnumType
{
    public enum CommandType
    {
        Arbitrage = 1,

    }

    public enum ExecuteType
    {
        [Description("开仓")]
        OpenPosition = 1, //开仓

        [Description("平仓")]
        ClosePosition = 2, //平仓

        [Description("调仓")]
        AdjustPosition = 3, //调仓
    }

    public enum EntrustStatus
    {
        [Description("提交到数据库")]
        SubmitToDB = 0,     //提交到数据库

        [Description("提交到UFX")]
        SubmitToUFX = 1,    //提交到UFX

        [Description("未执行")]
        NoExecuted = 2,     //未执行

        [Description("部分执行")]
        PartExecuted = 3,   //部分执行

        [Description("已完成")]
        Completed = 4,      //已完成

        [Description("撤单")]
        CancelToDB = 10,    //撤单

        [Description("撤单到UFX")]
        CancelToUFX = 11,   //撤单到UFX

        [Description("撤单成功")]
        CancelSuccess = 12, //撤单成功 

        [Description("委托失败")]
        EntrustFailed = -4,  //委托失败

        [Description("撤单失败")]
        CancelFail = -12,    //撤单失败
    }

    public enum DealStatus
    {
        [Description("未成交")]
        NoDeal = 1,     //未成交

        [Description("部分成交")]
        PartDeal = 2,   //部分成交

        [Description("已完成")]
        Completed = 3,  //已完成
    }
}
