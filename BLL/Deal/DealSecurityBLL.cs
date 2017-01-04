using DBAccess.Deal;
using Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Deal
{
    public class DealSecurityBLL
    {
        private DealSecurityDAO _dealsecudao = new DealSecurityDAO();

        public DealSecurityBLL()
        { 
        
        }

        public int Create(DealSecurity item)
        {
            return _dealsecudao.Create(item);
        }

        public int DeleteByDealNo(string dealNo)
        {
            return _dealsecudao.DeleteByDealNo(dealNo);
        }

        public List<DealSecurity> GetAll()
        {
            return _dealsecudao.GetAll();
        }
    }
}
