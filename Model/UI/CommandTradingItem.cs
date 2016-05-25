using Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class CommandTradingItem
    {
        //选中
        [BindingAttribute("selection")]
        public bool Selection { get; set; }

        //指令序号
        [BindingAttribute("commandno")]
        public int CommandNo { get; set; }

        //指令类型
        [BindingAttribute("commandtype")]
        public string CommandType { get; set; }

        //执行类型
        [BindingAttribute("executetype")]
        public string ExecuteType { get; set; }

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
        public string StartDate { get; set; }

        //结束日期yyyyMMdd
        [BindingAttribute("enddate")]
        public string EndDate { get; set; }

        //开始时间
        [BindingAttribute("starttime")]
        public string StartTime { get; set; }

        //结束时间
        [BindingAttribute("endtime")]
        public string EndTime { get; set; }

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
        public string InstanceId { get; set; }

        //实例编号
        [BindingAttribute("instanceno")]
        public string InstanceNo { get; set; }

        //监控单元
        [BindingAttribute("monitorunit")]
        public string MonitorUnit { get; set; }

        public int TemplateId { get; set; }
    }
}
