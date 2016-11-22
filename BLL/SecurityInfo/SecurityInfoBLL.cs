using DBAccess.SecurityInfo;
using Model.SecurityInfo;
using System.Collections.Generic;

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

        public SecurityItem Get(string secuCode)
        {
            var secuItems = GetAllItems();
            var secuItem = secuItems.Find(p => p.SecuCode.Equals(secuCode));

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
}
