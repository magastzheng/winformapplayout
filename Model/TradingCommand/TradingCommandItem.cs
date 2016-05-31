using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TradingCommand
{
    public enum CommandType
    {
        Arbitrage = 1,

    }

    public enum ExecuteType
    {
        OpenPosition = 1, //开仓
        ClosePosition = 2, //平仓
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

    public class TradingCommandItem
    {
        public int CommandId { get; set; }

        public int InstanceId { get; set; }

        public int OperationCopies { get; set; }

        public int ModifiedTimes { get; set; }

        public CommandType CommandType { get; set; }

        public ExecuteType ExecuteType { get; set; }

        public EntrustDirection StockDirection { get; set; }

        public EntrustDirection FuturesDirection { get; set; }

        public EntrustStatus EntrustStatus { get; set; }

        public DealStatus DealStatus { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
