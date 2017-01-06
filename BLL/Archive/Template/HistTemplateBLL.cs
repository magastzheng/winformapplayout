using BLL.Permission;
using Config;
using DBAccess.Archive.Template;
using DBAccess.Template;
using Model.UI;
using System;
using System.Collections.Generic;

namespace BLL.Archive.Template
{
    public class HistTemplateBLL
    {
        private HistoricalTemplateDAO _templatedao = new HistoricalTemplateDAO();
        private HistoricalTemplateStockDAO _tempstockdao = new HistoricalTemplateStockDAO();

        private PermissionManager _permissionManager = new PermissionManager();
        public HistTemplateBLL()
        { 
        }

        #region historical template

        public int CreateTemplate(StockTemplate template)
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

        public int DeleteTemplate(int archiveId)
        {
            return _templatedao.Delete(archiveId);
        }

        public List<HistStockTemplate> GetTemplates()
        {
            int userId = LoginManager.Instance.GetUserId();
            var allTemplates = _templatedao.Get();
            var templates = new List<HistStockTemplate>();
            foreach (var template in allTemplates)
            {
                if (_permissionManager.HasPermission(userId, template.ArchiveId, Model.Permission.ResourceType.HistoricalSpotTemplate, Model.Permission.PermissionMask.View))
                {
                    templates.Add(template);
                }
            }

            return templates;
        }

        #endregion

        #region historical template stock

        public int CreateStocks(int archiveId, List<TemplateStock> tempStocks)
        {
            return _tempstockdao.Create(archiveId, tempStocks);
        }

        public int DeleteStocks(int archiveId)
        {
            return _tempstockdao.DeleteOneArchive(archiveId);
        }

        public List<HistTemplateStock> GetStocks(int archiveId)
        {
            return _tempstockdao.Get(archiveId);
        }

        #endregion
    }
}
