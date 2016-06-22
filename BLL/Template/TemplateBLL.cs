using DBAccess;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Template
{
    public class TemplateBLL
    {
        private StockTemplateDAO _tempdbdao = new StockTemplateDAO();
        private TemplateStockDAO _stockdbdao = new TemplateStockDAO();
        private SecurityInfoDAO _secudbdao = new SecurityInfoDAO();

        public TemplateBLL()
        { 
        
        }

        public StockTemplate CreateTemplate(StockTemplate template)
        {
            int templateId = _tempdbdao.Create(template);
            if (templateId > 0)
            {
                template.TemplateId = templateId;
            }

            return template;
        }

        public int DeleteTemplate(StockTemplate template)
        {
            return _tempdbdao.Delete(template.TemplateId);
        }
    }
}
