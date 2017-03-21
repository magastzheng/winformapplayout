using Model.Binding;
using Model.Quote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class TemplateStock
    {
        public int TemplateNo { get; set; }

        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("market")]
        public string Exchange { get; set; }

        [BindingAttribute("amount")]
        public int Amount { get; set; }

        [BindingAttribute("marketcap")]
        public double MarketCap { get; set; }

        [BindingAttribute("marketcapweight")]
        public double MarketCapWeight { get; set; }

        [BindingAttribute("setweight")]
        public double SettingWeight { get; set; }

        [BindingAttribute("suspendflag")]
        public string SuspendFlag
        {
            get { return EnumQuoteHelper.GetSuspendFlag(ESuspendFlag); }
        }

        [BindingAttribute("limitupdown")]
        public string LimitUpOrDown
        {
            get { return EnumQuoteHelper.GetLimitUpDownFlag(ELimitUpDownFlag); }
        }

        public SuspendFlag ESuspendFlag { get; set; }

        public LimitUpDownFlag ELimitUpDownFlag { get; set; }
    }
}
