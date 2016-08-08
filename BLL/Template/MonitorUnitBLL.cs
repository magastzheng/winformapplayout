using DBAccess;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Template
{
    public class MonitorUnitBLL
    {
        private MonitorUnitDAO _monitorunitdao = new MonitorUnitDAO();

        public MonitorUnitBLL()
        { 
        }

        public List<OpenPositionItem> GetActive()
        {
            var monitorItems = _monitorunitdao.GetActive();
            List<OpenPositionItem> openItems = new List<OpenPositionItem>();

            foreach (var monitorItem in monitorItems)
            {
                OpenPositionItem openItem = new OpenPositionItem 
                {
                    TemplateId = monitorItem.StockTemplateId,
                    TemplateName = monitorItem.StockTemplateName,
                    MonitorId = monitorItem.MonitorUnitId,
                    MonitorName = monitorItem.MonitorUnitName,
                    PortfolioId = monitorItem.PortfolioId,
                    PortfolioName = monitorItem.PortfolioName,
                    FuturesContract = monitorItem.BearContract,
                    Copies = 1,
                };

                openItems.Add(openItem);
            }

            return openItems;
        }
    }
}
