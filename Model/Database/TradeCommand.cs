using Model.EnumType;
using System;

namespace Model.Database
{
    public class TradeCommand
    {
        //指令序号
        public int CommandId { get; set; }

        //指令份数
        public int CommandNum { get; set; }

        //指令状态
        public CommandStatus ECommandStatus { get; set; }

        //指令数量
        public int CommandAmount { get; set; }

        //实例号
        public int InstanceId { get; set; }

        //实例编号
        public string InstanceCode { get; set; }
        
        //指令修改次数
        public int ModifiedTimes { get; set; }

        //指令类型
        public CommandType ECommandType { get; set; }

        //执行类型
        public ExecuteType EExecuteType { get; set; }

        public EntrustDirection EStockDirection { get; set; }

        public EntrustDirection EFuturesDirection { get; set; }

        public EntrustStatus EEntrustStatus { get; set; }

        public DealStatus EDealStatus { get; set; }

        //指令有效开始时间
        public DateTime DStartDate { get; set; }

        //指令有效结束
        public DateTime DEndDate { get; set; }

        public int SubmitPerson { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int MonitorUnitId { get; set; }

        //监控单元
        public string MonitorUnitName { get; set; }

        public int TemplateId { get; set; }

        public string TemplateName { get; set; }

        //空头合约
        public string BearContract { get; set; }

        public int PortfolioId { get; set; }

        public string PortfolioCode { get; set; }

        public string PortfolioName { get; set; }

        public string AccountCode { get; set; }

        public string AccountName { get; set; }

        //备注
        public string Notes { get; set; }
    }
}
