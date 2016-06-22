using Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class StockTemplate
    {
        [BindingAttribute("templateid")]
        public int TemplateId { get; set; }

        [BindingAttribute("templatename")]
        public string TemplateName { get; set; }

        [BindingAttribute("status")]
        public int Status { get; set; }

        [BindingAttribute("futurescopies")]
        public int FutureCopies { get; set; }

        [BindingAttribute("marketcapoption")]
        public double MarketCapOpt { get; set; }

        [BindingAttribute("benchmark")]
        public string Benchmark { get; set; }

        [BindingAttribute("weighttype")]
        public int WeightType { get; set; }

        [BindingAttribute("replacetype")]
        public int ReplaceType { get; set; }

        [BindingAttribute("addeddate")]
        public DateTime CreatedDate { get; set; }

        [BindingAttribute("modifieddate")]
        public DateTime ModifiedDate { get; set; }

        public int UserId { get; set; }
    }
}
