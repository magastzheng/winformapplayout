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

        public int DeleteBySubmitId(int submitId)
        {
            return _dealsecudao.DeleteBySubmitId(submitId);
        }

        public List<DealSecurity> GetAll()
        {
            return _dealsecudao.GetAll();
        }

        public List<DealSecurity> GetByCommandId(int commandId)
        {
            return _dealsecudao.GetByCommandId(commandId);
        }

        public List<DealSecurity> GetBySubmitId(int submitId)
        {
            return _dealsecudao.GetBySubmitId(submitId);
        }

        public bool IsExist(int commandId, int submitId, int requestId, string dealNo)
        {
            int count = _dealsecudao.Count(commandId, submitId, requestId, dealNo);
            return count > 0;
        }
    }
}
