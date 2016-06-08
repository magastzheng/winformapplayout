using Model.Binding;
using Model.Data;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class OpenPositionSecurityItem
    {
        [BindingAttribute("selection")]
        public bool Selection { get; set; }

        [BindingAttribute("monitorid")]
        public int MonitorId { get; set; }

        [BindingAttribute("monitorname")]
        public string MonitorName { get; set; }

        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("weightamount")]
        public int WeightAmount { get; set; }

        [BindingAttribute("entrustamount")]
        public int EntrustAmount { get; set; }

        [BindingAttribute("commandprice")]
        public double CommandPrice { get; set; }

        [BindingAttribute("commandmoney")]
        public double CommandMoney { get; set; }

        public EntrustDirection DirectionType { get; set; }

        [BindingAttribute("entrustdirection")]
        public string EntrustDirection
        {
            get
            {
                string ret = string.Empty;
                switch (DirectionType)
                {
                    case Data.EntrustDirection.Buy:
                        {
                            ret = "买入";
                        }
                        break;
                    case Data.EntrustDirection.Sell:
                        {
                            ret = "卖出";
                        }
                        break;
                    case Data.EntrustDirection.AdjustedToBuySell:
                        {
                            ret = "调整到[买卖]";
                        }
                        break;
                    case Data.EntrustDirection.BuySpot:
                        {
                            ret = "买入";
                        }
                        break;
                    case Data.EntrustDirection.SellSpot:
                        {
                            ret = "卖出";
                        }
                        break;
                    case Data.EntrustDirection.SellOpen:
                        {
                            ret = "卖出开仓";
                        }
                        break;
                    case  Data.EntrustDirection.BuyClose:
                        {
                            ret = "买入平仓";
                        }
                        break;
                    default:
                        break;
                }

                return ret;
            }
        }

        [BindingAttribute("lastprice")]
        public double LastPrice { get; set; }

        [BindingAttribute("buyamount")]
        public int BuyAmount { get; set; }

        [BindingAttribute("sellamount")]
        public int SellAmount { get; set; }

        [BindingAttribute("suspensionflag")]
        public int SuspensionFlag { get; set; }

        [BindingAttribute("replacestatus")]
        public int ReplaceStatus { get; set; }

        [BindingAttribute("limitmove")]
        public int LimitMove { get; set; }

        public SecurityType SecuType { get; set; }
    }
}
