using BLL.Permission;
using Config;
using DBAccess.SecurityInfo;
using DBAccess.Template;
using Model.Database;
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
        private UserResourcePermissionBLL _urPermissionBLL = new UserResourcePermissionBLL();
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
            //TODO: delete the permission, too.
            return _tempdbdao.Delete(template.TemplateId);
        }

        public List<StockTemplate> GetTemplates()
        {
            var allTemplates = _tempdbdao.Get(-1);
            int userId = LoginManager.Instance.GetUserId();

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
            if (_permissionManager.HasPermission(loginUserId, templateId, ResourceType.SpotTemplate, PermissionMask.Veiw))
            {
                var template = _tempdbdao.Get(templateId);
                if (template != null && template.Count == 1)
                {
                    targetTemplate = ConvertToUIItem(template[0]);
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

        #region handle the permission

        private List<StockTemplate> GetPermissionTemplates(int userId, List<TemplateItem> allTemplates)
        {
            var users = _userBLL.GetAll();
            var templates = new List<StockTemplate>();
            foreach (var template in allTemplates)
            {
                if (_permissionManager.HasPermission(userId, template.TemplateId, ResourceType.SpotTemplate, PermissionMask.Veiw))
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
                    if (_permissionManager.HasPermission(perm.Token, temp.TemplateId, ResourceType.SpotTemplate, PermissionMask.Veiw))
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

        #endregion
    }
}
