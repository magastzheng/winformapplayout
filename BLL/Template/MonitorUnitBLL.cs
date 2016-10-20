using BLL.Permission;
using Config;
using DBAccess;
using Model.EnumType;
using Model.Permission;
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
        private PermissionManager _permissionManager = new PermissionManager();

        public MonitorUnitBLL()
        { 
        }

        public int Create(MonitorUnit item)
        {
            int monitorId = _monitorunitdao.Create(item);
            if(monitorId > 0)
            {
                int userId = LoginManager.Instance.GetUserId();
                var perms = _permissionManager.GetOwnerPermission();
                _permissionManager.GrantPermission(userId, monitorId, ResourceType.MonitorUnit, perms);
            }

            return monitorId;
        }

        public List<MonitorUnit> GetAll()
        {
            int userId = LoginManager.Instance.GetUserId();
            var items = _monitorunitdao.GetCombine(-1);
            var validItems = new List<MonitorUnit>();
            foreach (var item in items)
            {
                if (_permissionManager.HasPermission(userId, item.MonitorUnitId, ResourceType.MonitorUnit, PermissionMask.View))
                {
                    validItems.Add(item);
                }
            }

            return validItems;
        }

        public List<OpenPositionItem> GetActive()
        {
            int userId = LoginManager.Instance.GetUserId();
            var monitorItems = _monitorunitdao.GetActive();
            List<OpenPositionItem> openItems = new List<OpenPositionItem>();

            foreach (var monitorItem in monitorItems)
            {
                if (_permissionManager.HasPermission(userId, monitorItem.MonitorUnitId, ResourceType.MonitorUnit, PermissionMask.View))
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
            }

            return openItems;
        }

        public int Update(MonitorUnit item)
        {
            int ret = -1;
            int userId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(userId, item.MonitorUnitId, ResourceType.MonitorUnit, PermissionMask.Edit))
            {
                ret = _monitorunitdao.Update(item);
            }

            return ret;
        }

        public int Active(int monitorUnitId, MonitorUnitStatus status)
        {
            int ret = -1;
            int userId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(userId, monitorUnitId, ResourceType.MonitorUnit, PermissionMask.Edit))
            {
                ret = _monitorunitdao.Active(monitorUnitId, MonitorUnitStatus.Active);
            }

            return ret;
        }

        public int Delete(int monitorUnitId)
        {
            int ret = -1;
            int userId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(userId, monitorUnitId, ResourceType.MonitorUnit, PermissionMask.Delete))
            {
                ret = _monitorunitdao.Delete(monitorUnitId);
            }

            return ret;
        }
    }
}
