using Model.Binding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class MonitorUnit
    {
        [BindingAttribute("monitorunitid")]
        public int MonitorUnitId { get; set; }

        [BindingAttribute("monitorunitname")]
        public string MonitorUnitName { get; set; }

        [BindingAttribute("accounttype")]
        public int AccountType { get; set; }

        [BindingAttribute("portfolioid")]
        public int PortfolioId { get; set; }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        [BindingAttribute("bearcontract")]
        public string BearContract { get; set; }

        [BindingAttribute("stocktempid")]
        public int StockTemplateId { get; set; }

        [BindingAttribute("stocktempname")]
        public string StockTemplateName { get; set; }

        [BindingAttribute("selection")]
        //[TypeConverter(typeof(IntBoolConverter))]
        public bool Selection { get; set; }

        public string Owner { get; set; }
    }
}
