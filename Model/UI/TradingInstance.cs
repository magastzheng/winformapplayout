using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class TradingInstance
    {
        public int InstanceId { get; set; }

        public string InstanceCode { get; set; }

        public int MonitorUnitId { get; set; }

        public EntrustDirection StockDirection { get; set; }

        public string FuturesContract { get; set; }

        public EntrustDirection FuturesDirection { get; set; }

        public int OperationCopies { get; set; }

        public StockPriceType StockPriceType { get; set; }

        public FuturesPriceType FuturesPriceType { get; set; }

        public int Status { get; set; }

        public string Owner { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string MonitorUnitName { get; set; }

        public int TemplateId { get; set; }

        public string TemplateName { get; set; }

        public int PortfolioId { get; set; }

        public string PortfolioCode { get; set; }

        public string PortfolioName { get; set; }
    }
}
