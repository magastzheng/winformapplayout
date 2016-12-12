using BLL.Permission;
using Config;
using DBAccess.TradeInstance;
using log4net;
using Model.Permission;
using Model.UI;
using System.Collections.Generic;

namespace BLL.TradeInstance
{
    public class TradingInstanceAdjustmentBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private TradeInstanceAdjustmentDAO _tradeinstadjustmentdao = new TradeInstanceAdjustmentDAO();
        private PermissionManager _permissionManager = new PermissionManager();

        public TradingInstanceAdjustmentBLL()
        { 
        
        }

        #region public method

        public int Create(List<TradeInstanceAdjustmentItem> items)
        {
            foreach (var item in items)
            {
                Create(item);
            }

            return 1;
        }

        //Add new record into the table and then add the permission for the user.
        public int Create(TradeInstanceAdjustmentItem item)
        {
            int id = _tradeinstadjustmentdao.Create(item);
            int finalId = -1;
            if (id > 0)
            {
                int userId = LoginManager.Instance.GetUserId();
                var perms = _permissionManager.GetOwnerPermission();
                int ret = _permissionManager.GrantPermission(userId, id, ResourceType.TradeInstanceAdjustment, perms);
                if (ret > 0)
                {
                    finalId = id;
                }
            }

            return finalId;
        }

        /// <summary>
        /// Delete the specified item if it has the permission.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            var userId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(userId, id, ResourceType.TradeInstanceAdjustment, PermissionMask.Delete))
            {
                return _tradeinstadjustmentdao.Delete(id);
            }
            else
            {
                return -1;
            }
        }

        public List<TradeInstanceAdjustmentItem> GetAll()
        {
            int userId = LoginManager.Instance.GetUserId();
            var allItems = _tradeinstadjustmentdao.GetAll();
            var items = new List<TradeInstanceAdjustmentItem>();
            foreach (var item in allItems)
            {
                if (_permissionManager.HasPermission(userId, item.Id, ResourceType.TradeInstanceAdjustment, PermissionMask.View))
                {
                    items.Add(item);
                }
            }

            return items;
        }

        #endregion
    }
}
