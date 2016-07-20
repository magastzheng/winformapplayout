using Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class OpenPositionItem
    {
        [BindingAttribute("copies")]
        public int Copies { get; set; }

        [BindingAttribute("monitorid")]
        public int MonitorId { get; set; }

        [BindingAttribute("monitorname")]
        public string MonitorName { get; set; }

        [BindingAttribute("portfolioid")]
        public int PortfolioId { get; set; }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        [BindingAttribute("templateid")]
        public int TemplateId { get; set; }

        [BindingAttribute("templatename")]
        public string TemplateName { get; set; }

        [BindingAttribute("futurescontract")]
        public string FuturesContract { get; set; }

        [BindingAttribute("basis")]
        public double Basis { get; set; }

        [BindingAttribute("cost")]
        public double Cost { get; set; }

        [BindingAttribute("risk")]
        public double Risk { get; set; }

        [BindingAttribute("arbitrageopporunity")]
        public double ArbitrageOpporunity { get; set; }

        [BindingAttribute("futuremktcap")]
        public double FutureMktCap { get; set; }

        [BindingAttribute("stockmktcap")]
        public double StockMktCap { get; set; }

        [BindingAttribute("stocknumbers")]
        public int StockNumbers { get; set; }

        public string InstanceCode { get; set; }
    }
}
