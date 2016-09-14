using Model.Binding;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
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
        public string AccountType
        {
            get { return EnumTypeDisplayHelper.GetMonitorUnitAccountType(EAccountType); }
        }

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

        public int Owner { get; set; }

        public MonitorUnitAccountType EAccountType { get; set; }
    }
}
