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
        private TemplateStockDAO _stockdbdao = new TemplateStockDAO();
        private ArchiveTemplateDAO _templatedao = new ArchiveTemplateDAO();
        private ArchiveTemplateStockDAO _tempstockdao = new ArchiveTemplateStockDAO();

        private PermissionManager _permissionManager = new PermissionManager();
        public HistTemplateBLL()
        { 
        }

        #region historical template

        public int ArchiveTemplate(StockTemplate template)
        {
            ArchiveStockTemplate hst = new ArchiveStockTemplate(template);
            hst.DArchiveDate = DateTime.Now;

            int archiveId = -1;
            var stocks = _stockdbdao.Get(template.TemplateId);
            if (stocks != null)
            {

                archiveId = _templatedao.Archive(hst, stocks);
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
            }

            return archiveId;
        }

        public int DeleteTemplate(int archiveId)
        {
            int ret = _templatedao.Delete(archiveId);
            if (ret > 0)
            {
                ret = _permissionManager.Delete(archiveId, Model.Permission.ResourceType.HistoricalSpotTemplate);
            }

            return ret;
        }

        public List<ArchiveStockTemplate> GetTemplates()
        {
            int userId = LoginManager.Instance.GetUserId();
            var allTemplates = _templatedao.Get();
            var templates = new List<ArchiveStockTemplate>();
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

        public List<ArchiveTemplateStock> GetStocks(int archiveId)
        {
            return _tempstockdao.Get(archiveId);
        }

        #endregion
    }
}
