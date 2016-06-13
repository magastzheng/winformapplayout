using DBAccess;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SecurityInfo
{
    public class SecurityInfoBLL
    {
        private SecurityInfoDAO _dbdao;
        private List<SecurityItem> _secuItems;
        public SecurityInfoBLL()
        {
            _dbdao = new SecurityInfoDAO();
        }

        public SecurityItem Get(string secuCode, SecurityType secuType)
        {
            var secuItems = GetAllItems();
            var secuItem = secuItems.Find(p => p.SecuCode.Equals(secuCode) && p.SecuType == secuType);

            return secuItem;
        }

        public List<SecurityItem> GetAllItems()
        {
            if (_secuItems == null || _secuItems.Count == 0)
            {
                _secuItems = _dbdao.Get(SecurityType.All);
            }

            return _secuItems;
        }
    }

    public class SecurityInfoManager
    {
        private static readonly SecurityInfoManager _instance = new SecurityInfoManager();
        private SecurityInfoBLL _secuInfoBLL;

        private SecurityInfoManager()
        {
            _secuInfoBLL = new SecurityInfoBLL();
        }

        static SecurityInfoManager()
        { 
        
        }

        public SecurityItem Get(string secuCode, SecurityType secuType)
        {
            return _secuInfoBLL.Get(secuCode, secuType);
        }

        public List<SecurityItem> Get()
        {
            return _secuInfoBLL.GetAllItems();
        }

        public static SecurityInfoManager Instance { get { return _instance; } } 
    }
}
