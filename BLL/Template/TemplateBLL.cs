using BLL.Permission;
using Config;
using DBAccess.SecurityInfo;
using DBAccess.Template;
using Model.Permission;
using Model.UI;
using System.Collections.Generic;

namespace BLL.Template
{
    public class TemplateBLL
    {
        private StockTemplateDAO _tempdbdao = new StockTemplateDAO();
        private TemplateStockDAO _stockdbdao = new TemplateStockDAO();
        private SecurityInfoDAO _secudbdao = new SecurityInfoDAO();
        private PermissionManager _permissionManager = new PermissionManager();

        public TemplateBLL()
        { 
        
        }

        public StockTemplate CreateTemplate(StockTemplate template)
        {
            int templateId = _tempdbdao.Create(template);
            if (templateId > 0)
            {
                template.TemplateId = templateId;

                int userId = LoginManager.Instance.GetUserId();
                var perms = _permissionManager.GetOwnerPermission();
                _permissionManager.GrantPermission(userId, templateId, ResourceType.SpotTemplate, perms);
            }

            return template;
        }

        public int UpdateTemplate(StockTemplate template)
        {
            int userId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(userId, template.TemplateId, ResourceType.SpotTemplate, PermissionMask.Edit))
            {
                return _tempdbdao.Update(template);
            }
            else
            {
                return -1;
            }
        }

        public int DeleteTemplate(StockTemplate template)
        {
            return _tempdbdao.Delete(template.TemplateId);
        }

        public List<StockTemplate> GetTemplates()
        {
            var allTemplates = _tempdbdao.Get(-1);
            var templates = new List<StockTemplate>();

            int userId = LoginManager.Instance.GetUserId();
            foreach (var template in allTemplates)
            {
                if (_permissionManager.HasPermission(userId, template.TemplateId, ResourceType.SpotTemplate, PermissionMask.Veiw))
                {
                    templates.Add(template);
                }
                else if (template.UserId == userId)
                {
                    templates.Add(template);
                }
                else
                {
                    //no permission
                }
            }

            return templates;
        }

        public List<StockTemplate> GetTemplateByUser(int userId)
        {
            var allTemplates = _tempdbdao.GetByUser(userId);
            int loginUserId = LoginManager.Instance.GetUserId();
            var templates = new List<StockTemplate>();

            foreach (var template in allTemplates)
            {
                if (_permissionManager.HasPermission(loginUserId, template.TemplateId, ResourceType.SpotTemplate, PermissionMask.Veiw))
                {
                    templates.Add(template);
                }
                else if (template.UserId == loginUserId)
                {
                    templates.Add(template);
                }
                else
                { 
                    //no permission
                }
            }

            return templates;
        }

        public StockTemplate GetTemplate(int templateId)
        {
            StockTemplate targetTemplate = null;
            int loginUserId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(loginUserId, templateId, ResourceType.SpotTemplate, PermissionMask.Veiw))
            {
                var template = _tempdbdao.Get(templateId);
                if (template != null && template.Count == 1)
                {
                    targetTemplate = template[0];
                }
                else
                {
                    targetTemplate = new StockTemplate();
                }
            }
            else
            {
                targetTemplate = new StockTemplate();
            }

            return targetTemplate;
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

        //public List<Benchmark> GetBenchmark()
        //{
        //    return _tempdbdao.GetBenchmark();
        //}

        #endregion
    }
}
