using BLL.Permission;
using BLL.UsageTracking;
using Config;
using DBAccess.SecurityInfo;
using DBAccess.Template;
using Model.Database;
using Model.Permission;
using Model.UI;
using System.Collections.Generic;
using Util;
using System.Linq;
using BLL.Manager;
using Model.SecurityInfo;

namespace BLL.Template
{
    public class TemplateBLL
    {
        private StockTemplateDAO _tempdbdao = new StockTemplateDAO();
        private TemplateStockDAO _stockdbdao = new TemplateStockDAO();
        private TemplateDao _templatedao = new TemplateDao();
        private SecurityInfoDAO _secudbdao = new SecurityInfoDAO();

        private UserActionTrackingBLL _userActionTrackingBLL = new UserActionTrackingBLL();
        private PermissionManager _permissionManager = new PermissionManager();
        private TokenResourcePermissionBLL _urPermissionBLL = new TokenResourcePermissionBLL();
        private UserBLL _userBLL = new UserBLL();

        public TemplateBLL()
        { 
        
        }

        public StockTemplate CreateTemplate(StockTemplate template)
        {
            var dbItem = ConvertToDBItem(template);
            int templateId = _tempdbdao.Create(dbItem);
            if (templateId > 0)
            {
                template.TemplateId = templateId;
                int userId = LoginManager.Instance.GetUserId();

                //Add the usage tracking information
                _userActionTrackingBLL.Create(userId, Model.UsageTracking.ActionType.Create, ResourceType.SpotTemplate, templateId, 1, Model.UsageTracking.ActionStatus.Success, JsonUtil.SerializeObject(template));

                GrantPermission(userId, templateId, template);
            }

            return template;
        }

        public int UpdateTemplate(StockTemplate template)
        {
            int userId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(userId, template.TemplateId, ResourceType.SpotTemplate, PermissionMask.Edit))
            {
                var dbItem = ConvertToDBItem(template);
                int tempId = _tempdbdao.Update(dbItem);
                if (tempId > 0)
                {
                    //add the usage tracking
                    _userActionTrackingBLL.Create(userId, Model.UsageTracking.ActionType.Edit, ResourceType.SpotTemplate, template.TemplateId, 1, Model.UsageTracking.ActionStatus.Success, JsonUtil.SerializeObject(template));

                    //update the permission
                    foreach (var perm in template.Permissions)
                    {
                        bool isUpdated = (perm.Id > 0) ? true : false;
                        _permissionManager.ChangePermission(perm.Token, template.TemplateId, ResourceType.SpotTemplate, perm.Permission, isUpdated);
                    }

                    return tempId;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        public int DeleteTemplate(StockTemplate template)
        {
            int ret = -1;
            int userId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(userId, template.TemplateId, ResourceType.SpotTemplate, PermissionMask.Delete))
            {
                //TODO: delete the permission, too.
                ret = _tempdbdao.Delete(template.TemplateId);
                if (ret > 0)
                {
                    //Remove the permission row in the database directly. NOT revoke!!!!!
                    _permissionManager.Delete(template.TemplateId, ResourceType.SpotTemplate);
                    //add the usage tracking
                    _userActionTrackingBLL.Create(userId, Model.UsageTracking.ActionType.Delete, ResourceType.SpotTemplate, template.TemplateId, 1, Model.UsageTracking.ActionStatus.Success, JsonUtil.SerializeObject(template));
                }
            }

            return ret;
        }

        public List<StockTemplate> GetTemplates()
        {
            var allTemplates = _tempdbdao.GetAll();
            int userId = LoginManager.Instance.GetUserId();

            _userActionTrackingBLL.Create(userId, Model.UsageTracking.ActionType.Get, ResourceType.SpotTemplate, -1, -1, Model.UsageTracking.ActionStatus.Normal, "Get all template");

            return GetPermissionTemplates(userId, allTemplates);
        }

        public List<StockTemplate> GetTemplateByUser(int userId)
        {
            var allTemplates = _tempdbdao.GetByUser(userId);

            return GetPermissionTemplates(userId, allTemplates);
        }

        public StockTemplate GetTemplate(int templateId)
        {
            StockTemplate targetTemplate = null;
            int loginUserId = LoginManager.Instance.GetUserId();
            
            if (_permissionManager.HasPermission(loginUserId, templateId, ResourceType.SpotTemplate, PermissionMask.View))
            {
                var template = _tempdbdao.Get(templateId);
                if (template != null && template.TemplateId == templateId)
                {
                    targetTemplate = ConvertToUIItem(template);
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

            Model.UsageTracking.ActionStatus actionStatus = Model.UsageTracking.ActionStatus.Normal;
            if (targetTemplate.TemplateId == templateId)
            {
                actionStatus = Model.UsageTracking.ActionStatus.Success;
            }
            _userActionTrackingBLL.Create(loginUserId, Model.UsageTracking.ActionType.Get, ResourceType.SpotTemplate, templateId, 1, actionStatus, JsonUtil.SerializeObject(targetTemplate));

            return targetTemplate;
        }

        public List<TemplateStock> GetStocks(int templateId)
        {
            int loginUserId = LoginManager.Instance.GetUserId();
            _userActionTrackingBLL.Create(loginUserId, Model.UsageTracking.ActionType.Get, ResourceType.SpotTemplate, templateId, 1, Model.UsageTracking.ActionStatus.Normal, "stocks");

            var stocks = _stockdbdao.Get(templateId);
            if (stocks != null)
            {
                stocks.ForEach(p => {
                    var secuInfo = SecurityInfoManager.Instance.Get(p.SecuCode, Model.SecurityInfo.SecurityType.Stock);
                    if (secuInfo != null)
                    {
                        p.SecuName = secuInfo.SecuName;
                        p.Exchange = SecurityItemHelper.GetExchange(secuInfo.ExchangeCode);
                    }
                });
            }

            return stocks;
        }

        public int DeleteStock(List<TemplateStock> tempStocks)
        {
            int loginUserId = LoginManager.Instance.GetUserId();
            var templateIds = tempStocks.Select(p => p.TemplateNo).ToList();
            if (templateIds.Count > 0)
            {
                var templateId = templateIds[0];
                _userActionTrackingBLL.Create(loginUserId, Model.UsageTracking.ActionType.Delete, ResourceType.SpotTemplate, templateId, tempStocks.Count, Model.UsageTracking.ActionStatus.Normal, JsonUtil.SerializeObject(tempStocks));
            }

            return _stockdbdao.Delete(tempStocks);
        }

        public int Replace(int templateNo, List<TemplateStock> tempStocks)
        {
            int loginUserId = LoginManager.Instance.GetUserId();
            _userActionTrackingBLL.Create(loginUserId, Model.UsageTracking.ActionType.Edit, ResourceType.SpotTemplate, templateNo, tempStocks.Count, Model.UsageTracking.ActionStatus.Normal, JsonUtil.SerializeObject(tempStocks));

            int ret = _stockdbdao.Replace(templateNo, tempStocks);
            if (ret > 0)
            {
                //如果更新模板组合成功，则更新模板修改时间
                return _tempdbdao.UpdateModifiedDate(templateNo);
            }

            return ret;
        }

        public int Copy(int oldTemplateId, StockTemplate template)
        {
            var dbItem = ConvertToDBItem(template);
            int templateId = _templatedao.Copy(oldTemplateId, dbItem);
            if (templateId > 0)
            {
                template.TemplateId = templateId;

                int userId = LoginManager.Instance.GetUserId();
                _userActionTrackingBLL.Create(userId, Model.UsageTracking.ActionType.Edit, ResourceType.SpotTemplate, templateId, template.TemplateId, Model.UsageTracking.ActionStatus.Normal, "Copy template stock");
                GrantPermission(userId, templateId, template);
            }

            return templateId;
        }

        #region handle the permission

        private List<StockTemplate> GetPermissionTemplates(int userId, List<TemplateItem> allTemplates)
        {
            var users = _userBLL.GetAll();
            var templates = new List<StockTemplate>();
            foreach (var template in allTemplates)
            {
                if (_permissionManager.HasPermission(userId, template.TemplateId, ResourceType.SpotTemplate, PermissionMask.View))
                {
                    var uiItem = ConvertToUIItem(template);
                    var urPerm = _urPermissionBLL.GetByResource(template.TemplateId, ResourceType.SpotTemplate);
                    uiItem.Permissions = urPerm;

                    templates.Add(uiItem);
                }
                else if (template.CreatedUserId == userId)
                {
                    var uiItem = ConvertToUIItem(template);
                    var urPerm = _urPermissionBLL.GetByResource(template.TemplateId, ResourceType.SpotTemplate);
                    uiItem.Permissions = urPerm;

                    templates.Add(uiItem);
                }
                else
                {
                    //no permission
                }
            }

            foreach (var temp in templates)
            {
                foreach (var perm in temp.Permissions)
                {
                    //Get the view user list
                    if (_permissionManager.HasPermission(perm.Token, temp.TemplateId, ResourceType.SpotTemplate, PermissionMask.View))
                    {
                        var user = users.Find(p => p.Id == perm.Token);
                        if (user != null)
                        {
                            if (temp.CanViewUsers == null)
                            {
                                temp.CanViewUsers = new List<User>();
                            }

                            temp.CanViewUsers.Add(user);
                        }
                    }

                    //Get the edit user list
                    if (_permissionManager.HasPermission(perm.Token, temp.TemplateId, ResourceType.SpotTemplate, PermissionMask.Edit))
                    {
                        var user = users.Find(p => p.Id == perm.Token);
                        if (user != null)
                        {
                            if (temp.CanEditUsers == null)
                            {
                                temp.CanEditUsers = new List<User>();
                            }

                            temp.CanEditUsers.Add(user);
                        }
                    }
                }
            }

            return templates;
        }
        #endregion

        #region

        private TemplateItem ConvertToDBItem(StockTemplate uiItem)
        {
            var dbItem = new TemplateItem 
            {
                TemplateId = uiItem.TemplateId,
                TemplateName = uiItem.TemplateName,
                EStatus = uiItem.EStatus,
                EWeightType = uiItem.EWeightType,
                EReplaceType = uiItem.EReplaceType,
                FutureCopies = uiItem.FutureCopies,
                MarketCapOpt = uiItem.MarketCapOpt,
                Benchmark = uiItem.Benchmark,
                DCreatedDate = uiItem.DCreatedDate,
                DModifiedDate = uiItem.DModifiedDate,
                CreatedUserId = uiItem.CreatedUserId,
            };

            return dbItem;
        }

        private StockTemplate ConvertToUIItem(TemplateItem dbItem)
        {
            var uiItem = new StockTemplate 
            {
                TemplateId = dbItem.TemplateId,
                TemplateName = dbItem.TemplateName,
                EStatus = dbItem.EStatus,
                EWeightType = dbItem.EWeightType,
                EReplaceType = dbItem.EReplaceType,
                FutureCopies = dbItem.FutureCopies,
                MarketCapOpt = dbItem.MarketCapOpt,
                Benchmark = dbItem.Benchmark,
                DCreatedDate = dbItem.DCreatedDate,
                DModifiedDate = dbItem.DModifiedDate,
                CreatedUserId = dbItem.CreatedUserId,
            };

            return uiItem;
        }

        private void GrantPermission(int userId, int templateId, StockTemplate template)
        {
            var perms = _permissionManager.GetOwnerPermission();
            _permissionManager.GrantPermission(userId, templateId, ResourceType.SpotTemplate, perms);

            foreach (var perm in template.Permissions)
            {
                if (perm.Token != userId)
                {
                    bool isUpdated = (perm.Id > 0) ? true : false;
                    _permissionManager.ChangePermission(perm.Token, template.TemplateId, ResourceType.SpotTemplate, perm.Permission, isUpdated);
                }
            }
        }
        #endregion
    }
}
