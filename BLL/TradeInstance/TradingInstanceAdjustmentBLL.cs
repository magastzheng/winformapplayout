using BLL.Permission;
using Config;
using DBAccess.TradeInstance;
using log4net;
using Model.Permission;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.TradeInstance
{
    public class TradingInstanceAdjustmentBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private TradingInstanceAdjustmentDAO _tradeinstadjustmentdao = new TradingInstanceAdjustmentDAO();
        private PermissionManager _permissionManager = new PermissionManager();

        public TradingInstanceAdjustmentBLL()
        { 
        
        }

        public int Create(TradingInstanceAdjustmentItem item)
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

        public List<TradingInstanceAdjustmentItem> GetAll()
        {
            int userId = LoginManager.Instance.GetUserId();
            var allItems = _tradeinstadjustmentdao.GetAll();
            var items = new List<TradingInstanceAdjustmentItem>();
            foreach (var item in allItems)
            {
                if (_permissionManager.HasPermission(userId, item.Id, ResourceType.TradeInstanceAdjustment, PermissionMask.Veiw))
                {
                    items.Add(item);
                }
            }

            return items;
        }
    }
}
