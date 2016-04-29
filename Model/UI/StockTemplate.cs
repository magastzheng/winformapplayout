using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class StockTemplate
    {
        public int TemplateNo { get; set; }
        public string TemplateName { get; set; }
        public int FutureCopies { get; set; }
        public double MarketCapOpt { get; set; }
        public string Benchmark { get; set; }
        public int WeightType { get; set; }
        public int ReplaceType { get; set; }
    }
}
