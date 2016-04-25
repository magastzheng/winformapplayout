using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UITradingCommandItem
    {
        //选中
        public int Selected { get; set; }

        //指令序号
        public int CommandNo { get; set; }

        //指令类型
        public string CommandType { get; set; }

        //执行类型
        public string ExecuteType { get; set; }

        //指令份数
        public int CommandNum { get; set; }

        //目标份数
        public int TargetNum { get; set; }

        //基差|价差
        public double BaisPrice { get; set; }

        //多头委比
        public double LongMoreThan { get; set; }

        //空头委比
        public double BearMoreThan { get; set; }

        //多头成比
        public double LongRatio { get;set; }

        //空头成比
        public double BearRatio { get; set; }

        //指令数量
        public int CommandAmount { get; set; }

        //已委托数量
        public int EntrustedAmount { get; set; }

        //已成数量
        public int DealAmount { get; set; }

        //指令金额
        public double CommandMoney { get; set; }

        //裸敞口
        public double Exposure { get; set; }

        //开始日期yyyyMMdd
        public string StartDate { get; set; }

        //结束日期yyyyMMdd
        public string EndDate { get; set; }

        //开始时间
        public string StartTime { get; set; }

        //结束时间
        public string EndTime { get; set; }

        //分发时间
        public string DispatchTime { get; set; }

        //下达人
        public string ExecutePerson { get; set; }

        //分发人
        public string DispatchPerson { get; set; }

        //实例号
        public string InstanceId { get; set; }

        //实例编号
        public string InstanceNo { get; set; }

        //监控单元
        public string MonitorUnit { get; set; }
    }
}
