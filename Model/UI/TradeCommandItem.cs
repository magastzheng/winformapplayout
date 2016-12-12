using Model.Binding;
using Model.Constant;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using System;

namespace Model.UI
{
    /// <summary>
    /// 对应数据库中的表：tradingcommand，并关联这些表tradinginstance，monitorunit，ufxportfolio获取辅助信息
    /// </summary>
    public class TradeCommandItem
    {
        //选中
        [BindingAttribute("selection")]
        public bool Selection { get; set; }

        //指令序号
        [BindingAttribute("commandid")]
        public int CommandId { get; set; }

        //指令类型
        [BindingAttribute("commandtype")]
        public string CommandType 
        { 
            get 
            {
                return EnumTypeDisplayHelper.GetCommandType(ECommandType);
            } 
        }

        //执行类型
        [BindingAttribute("executetype")]
        public string ExecuteType 
        {
            get
            {
                return EnumTypeDisplayHelper.GetExecuteType(EExecuteType);
            }
        }

        //指令份数
        [BindingAttribute("commandnum")]
        public int CommandNum { get; set; }

        //目标份数
        [BindingAttribute("targetnum")]
        public int TargetNum { get; set; }

        //基差|价差
        [BindingAttribute("baisprice")]
        public double BaisPrice { get; set; }

        //多头委比
        [BindingAttribute("longmorethan")]
        public double LongMoreThan { get; set; }

        //空头委比
        [BindingAttribute("bearmorethan")]
        public double BearMoreThan { get; set; }

        //多头成比
        [BindingAttribute("longratio")]
        public double LongRatio { get;set; }

        //空头成比
        [BindingAttribute("bearratio")]
        public double BearRatio { get; set; }

        //指令数量
        [BindingAttribute("commandamount")]
        public int CommandAmount { get; set; }

        //已委托数量
        [BindingAttribute("entrustedamount")]
        public int EntrustedAmount { get; set; }

        //已成数量
        [BindingAttribute("dealamount")]
        public int DealAmount { get; set; }

        //指令金额
        [BindingAttribute("commandmoney")]
        public double CommandMoney { get; set; }

        //裸敞口
        [BindingAttribute("exposure")]
        public double Exposure { get; set; }

        //开始日期yyyyMMdd
        [BindingAttribute("startdate")]
        public string StartDate 
        {
            get { return DateFormat.Format(DStartDate, ConstVariable.DateFormat1); }
        }

        //结束日期yyyyMMdd
        [BindingAttribute("enddate")]
        public string EndDate 
        {
            get { return DateFormat.Format(DEndDate, ConstVariable.DateFormat1); }
        }

        //开始时间
        [BindingAttribute("starttime")]
        public string StartTime 
        {
            get 
            {
                return DateFormat.Format(DStartDate, ConstVariable.TimeFormat1);
            }
        }

        //结束时间
        [BindingAttribute("endtime")]
        public string EndTime
        {
            get
            {
                return DateFormat.Format(DEndDate, ConstVariable.TimeFormat1);
            }
        }

        //分发时间
        [BindingAttribute("dispatchtime")]
        public string DispatchTime { get; set; }

        //下达人
        [BindingAttribute("executeperson")]
        public string ExecutePerson { get; set; }

        //分发人
        [BindingAttribute("dispatchperson")]
        public string DispatchPerson { get; set; }

        //实例号
        [BindingAttribute("instanceid")]
        public int InstanceId { get; set; }

        //实例编号
        [BindingAttribute("instancecode")]
        public string InstanceCode { get; set; }

        //监控单元
        [BindingAttribute("monitorunitname")]
        public string MonitorUnitName { get; set; }

        public int TemplateId { get; set; }

        public int ModifiedTimes { get; set; }

        public CommandType ECommandType { get; set; }

        public ExecuteType EExecuteType { get; set; }

        public EntrustDirection EStockDirection { get; set; }

        public EntrustDirection EFuturesDirection { get; set; }

        public EntrustStatus EEntrustStatus { get; set; }

        public DealStatus EDealStatus { get; set; }

        public DateTime DStartDate { get; set; }

        public DateTime DEndDate { get; set; }

        public int SubmitPerson { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int MonitorUnitId { get; set; }

        public int PortfolioId { get; set; }

        public string PortfolioCode { get; set; }

        public string PortfolioName { get; set; }

        public string FundCode { get; set; }

        public string FundName { get; set; }
    }
}
