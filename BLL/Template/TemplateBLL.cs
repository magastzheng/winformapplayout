using DBAccess;
using Model.config;
using Model.UI;
using System.Collections.Generic;

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

        public int UpdateTemplate(StockTemplate template)
        {
            return _tempdbdao.Update(template);
        }

        public int DeleteTemplate(StockTemplate template)
        {
            return _tempdbdao.Delete(template.TemplateId);
        }

        public List<StockTemplate> GetTemplates()
        {
            return _tempdbdao.Get(-1);
        }

        public List<StockTemplate> GetTemplateByUser(int userId)
        {
            return _tempdbdao.GetByUser(userId);
        }

        public StockTemplate GetTemplate(int templateId)
        {
            var template = _tempdbdao.Get(templateId);
            if (template != null && template.Count == 1)
            {
                return template[0];
            }
            else
            {
                return new StockTemplate();
            }
        }

        public List<TemplateStock> GetStocks(int templateId)
        {
            return _stockdbdao.Get(templateId);
        }

        public int Replace(int templateNo, List<TemplateStock> tempStocks)
        {
            return _stockdbdao.Replace(templateNo, tempStocks);
        }

        #region

        public List<Benchmark> GetBenchmark()
        {
            return _tempdbdao.GetBenchmark();
        }

        #endregion
    }
}
