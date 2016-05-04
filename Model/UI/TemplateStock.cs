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
        public string SecuCode { get; set; }
        public string SecuName { get; set; }
        public string Exchange { get; set; }
        public int Amount { get; set; }
        public double MarketCap { get; set; }
        public double MarketCapWeight { get; set; }
        public double SettingWeight { get; set; }
    }
}
