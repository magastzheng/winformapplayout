using Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    //case "ts_secucode":
    //                    column.DataPropertyName = "SecuCode";
    //                    break;
    //                case "ts_secuname":
    //                    column.DataPropertyName = "SecuName";
    //                    break;
    //                case "ts_market":
    //                    column.DataPropertyName = "Exchange";
    //                    break;
    //                case "ts_amount":
    //                    column.DataPropertyName = "Amount";
    //                    break;
    //                case "ts_marketcap":
    //                    column.DataPropertyName = "MarketCap";
    //                    break;
    //                case "ts_marketcapweight":
    //                    column.DataPropertyName = "MarketCapWeight";
    //                    break;
    //                case "ts_setweight":
    //                    column.DataPropertyName = "SettingWeight";
    //                    break;

    public class TemplateStock
    {
        public int TemplateNo { get; set; }

        [BindingAttribute("ts_secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("ts_secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("ts_market")]
        public string Exchange { get; set; }

        [BindingAttribute("ts_amount")]
        public int Amount { get; set; }

        [BindingAttribute("ts_marketcap")]
        public double MarketCap { get; set; }

        [BindingAttribute("ts_marketcapweight")]
        public double MarketCapWeight { get; set; }

        [BindingAttribute("ts_setweight")]
        public double SettingWeight { get; set; }
    }
}
