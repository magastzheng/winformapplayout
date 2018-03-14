using Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class ClosePositionItem
    {
        [BindingAttribute("selection")]
        public bool Selection { get; set; }

        [BindingAttribute("copies")]
        public int Copies { get; set; }

        [BindingAttribute("instanceid")]
        public int InstanceId { get; set; }

        [BindingAttribute("instancecode")]
        public string InstanceCode { get; set; }

        [BindingAttribute("monitorname")]
        public string MonitorName { get; set; }

        [BindingAttribute("templateid")]
        public int TemplateId { get; set; }

        [BindingAttribute("portfolioid")]
        public int PortfolioId { get; set; }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        [BindingAttribute("stockmktcap")]
        public double StockMktCap { get; set; }

        [BindingAttribute("futuresmktcap")]
        public double FuturesMktCap { get; set; }

        [BindingAttribute("risk")]
        public double Risk { get; set; }

        [BindingAttribute("totalprofitloss")]
        public double TotalProfitLoss { get; set; }

        [BindingAttribute("spotfloatprofitloss")]
        public double SpotFloatProfitLoss { get; set; }

        [BindingAttribute("futurefloatprofitloss")]
        public double FutureFloatProfitLoss { get; set; }

        [BindingAttribute("spotrealizeprofitloss")]
        public double SpotRealizeProfitLoss { get; set; }

        [BindingAttribute("futurerealizeprofitloss")]
        public double FutureRealizeProfitLoss { get; set; }

        [BindingAttribute("monitorid")]
        public int MonitorId { get; set; }

        public string FuturesContract { get; set; }

        public string PortfolioCode { get; set; }

        public string TemplateName { get; set; }
    }
}
