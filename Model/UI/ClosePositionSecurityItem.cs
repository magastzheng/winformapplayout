using Model.Binding;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class ClosePositionSecurityItem
    {
        [BindingAttribute("selection")]
        public bool Selection { get; set; }

        [BindingAttribute("instanceid")]
        public int InstanceId { get; set; }

        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        public string ExchangeCode { get; set; }

        [BindingAttribute("exchange")]
        public string Exchange { get { return ExchangeCode; } }

        [BindingAttribute("longshort")]
        public string LongShort
        {
            get
            {
                string ret = string.Empty;
                switch (SecuType)
                {
                    case SecurityType.Stock:
                        ret = "多头";
                        break;
                    case SecurityType.Futures:
                        ret = "空头";
                        break;
                    default:
                        break;
                }
                return ret;
            }
        }

        [BindingAttribute("portfolioid")]
        public int PortfolioId { get; set; }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        [BindingAttribute("holdingamount")]
        public int HoldingAmount { get; set; }

        [BindingAttribute("availableamount")]
        public int AvailableAmount { get; set; }

        [BindingAttribute("entrustamount")]
        public int EntrustAmount { get; set; }

        [BindingAttribute("targetmktcap")]
        public double TargetMktCap { get; set; }

        [BindingAttribute("commandmoney")]
        public double CommandMoney { get; set; }

        [BindingAttribute("limitmove")]
        public int LimitMove { get; set; }

        [BindingAttribute("holdingweight")]
        public double HoldingWeight { get; set; }

        [BindingAttribute("targetweight")]
        public double TargetWeight { get; set; }

        [BindingAttribute("entrustdirection")]
        public int EntrustDirection { get; set; }

        [BindingAttribute("commandprice")]
        public double CommandPrice { get; set; }

        [BindingAttribute("lastprice")]
        public double LastPrice { get; set; }

        public SecurityType SecuType { get; set; }

        public PositionType PositionType { get; set; }
    }
}
