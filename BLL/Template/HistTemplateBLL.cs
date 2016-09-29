using BLL.Permission;
using Config;
using DBAccess.Template;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Template
{
    public class HistTemplateBLL
    {
        private HistoricalTemplateDAO _templatedao = new HistoricalTemplateDAO();
        private PermissionManager _permissionManager = new PermissionManager();
        public HistTemplateBLL()
        { 
        }

        public int Create(StockTemplate template)
        {
            HistStockTemplate hst = new HistStockTemplate(template);
            hst.DArchiveDate = DateTime.Now;

            int archiveId = _templatedao.Create(hst);
            if (archiveId > 0)
            {
                //add permission
                foreach (var perm in hst.Permissions)
                {
                    _permissionManager.ChangePermission(perm.Token, archiveId, Model.Permission.ResourceType.HistoricalSpotTemplate, perm.Permission, false);
                }
            }
            else
            {
                archiveId = -1;
            }

            return archiveId;
        }

        public int Delete(int archiveId)
        {
            return _templatedao.Delete(archiveId);
        }

        public List<HistStockTemplate> Get()
        {
            int userId = LoginManager.Instance.GetUserId();
            var allTemplates = _templatedao.Get();
            var templates = new List<HistStockTemplate>();
            foreach (var template in allTemplates)
            {
                if (_permissionManager.HasPermission(userId, template.ArchiveId, Model.Permission.ResourceType.HistoricalSpotTemplate, Model.Permission.PermissionMask.Veiw))
                {
                    templates.Add(template);
                }
            }

            return templates;
        }
    }
}
