using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Quote
{
    public class EnumQuoteHelper
    {
        public static string GetSuspendFlag(SuspendFlag suspendFlag)
        {
            string flag = string.Empty;

            switch(suspendFlag)
            {
                case SuspendFlag.NoSuspension:
                    {
                        flag = "交易";
                    }
                    break;
                case SuspendFlag.Suspend1Hour:
                    {
                        flag = "停牌1小时";
                    }
                    break;
                case SuspendFlag.Suspend2Hour:
                    {
                        flag = "停牌2小时";
                    }
                    break;
                case SuspendFlag.SuspendHalfDay:
                    {
                        flag = "停牌半天";
                    }
                    break;
                case SuspendFlag.SuspendAfternoon:
                    {
                        flag = "停牌下午";
                    }
                    break;
                case SuspendFlag.SuspendHalfHour:
                    {
                        flag = "停牌半小时";
                    }
                    break;
                case SuspendFlag.SuspendTemp:
                    {
                        flag = "临时停牌";
                    }
                    break;
                case SuspendFlag.Suspend1Day:
                    {
                        flag = "停牌一天";
                    }
                    break;
                case SuspendFlag.SuspendLimit:
                    {
                        flag = "停牌";
                    }
                    break;
                default:
                    break;
            }

            return flag;
        }

        public static string GetLimitUpDownFlag(LimitUpDownFlag limitUpDownFlag)
        {
            string limitUpDown = string.Empty;
            switch (limitUpDownFlag)
            {
                case LimitUpDownFlag.Suspend:
                    limitUpDown = "停牌";
                    break;
                case LimitUpDownFlag.Normal:
                    limitUpDown = "正常";
                    break;
                case LimitUpDownFlag.LimitUp:
                    limitUpDown = "涨停";
                    break;
                case LimitUpDownFlag.LimitDown:
                    limitUpDown = "跌停";
                    break;
                default:
                    break;
            }

            return limitUpDown;
        }
    }
}
