using Model.Binding;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class TradingCommandItem
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
                string ret = string.Empty;
                switch (ECommandType)
                {
                    case UI.CommandType.Arbitrage:
                        {
                            ret = "期现套利";
                        }
                        break;
                    default:
                        break;
                }
                return ret;
            } 
        }

        //执行类型
        [BindingAttribute("executetype")]
        public string ExecuteType 
        {
            get
            {
                string ret = string.Empty;
                switch (EExecuteType)
                {
                    case UI.ExecuteType.OpenPosition:
                        {
                            ret = "开仓";
                        }
                        break;
                    case UI.ExecuteType.ClosePosition:
                        {
                            ret = "平仓";
                        }
                        break;
                    case UI.ExecuteType.AdjustPosition:
                        {
                            ret = "调仓";
                        }
                        break;
                    default:
                        break;
                }

                return ret;
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
            get { return DStartDate.ToString("yyyyMMdd"); }
        }

        //结束日期yyyyMMdd
        [BindingAttribute("enddate")]
        public string EndDate 
        {
            get { return DEndDate.ToString("yyyyMMdd"); }
        }

        //开始时间
        [BindingAttribute("starttime")]
        public string StartTime 
        {
            get 
            {
                return DStartDate.ToString("hh:mm:ss");
            }
        }

        //结束时间
        [BindingAttribute("endtime")]
        public string EndTime
        {
            get
            {
                return DEndDate.ToString("hh:mm:ss");
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
        [BindingAttribute("instanceno")]
        public string InstanceNo { get; set; }

        //监控单元
        [BindingAttribute("monitorunit")]
        public string MonitorUnit { get; set; }

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
    }
}
