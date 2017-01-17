using DBAccess.EntrustCommand;
using Model.Database;
using Model.EnumType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.EntrustCommand
{
    public class EntrustCombineBLL
    {
        private EntrustDAO _entrustdao = new EntrustDAO();

        public EntrustCombineBLL()
        { 
        }

        public int Submit(Model.Database.EntrustCommand cmdItem, List<EntrustSecurity> entrustItems)
        {
            return _entrustdao.Submit(cmdItem, entrustItems);
        }

        //public int UpdateEntrustStatus(List<int> submitIds, EntrustStatus entrustStatus)
        //{
        //    return _entrustdao.UpdateEntrustStatus(submitIds, entrustStatus);
        //}

        public int UpdateOneEntrustStatus(int submitId, EntrustStatus entrustStatus)
        {
            return _entrustdao.UpdateOneEntrustStatus(submitId, entrustStatus);
        }

        public int UpdateSecurityEntrustStatus(List<EntrustSecurity> entrustItems, EntrustStatus entrustStatus)
        {
            return _entrustdao.UpdateSecurityEntrustStatus(entrustItems, entrustStatus);
        }

        public int UpdateSecurityEntrustResponseByRequestId(List<EntrustSecurity> entrustItems)
        {
            return _entrustdao.UpdateSecurityEntrustResponseByRequestId(entrustItems);
        }

        //public int UpdateCommandSecurityEntrustStatus(int submitId, List<EntrustSecurity> entrustItems, EntrustStatus entrustStatus)
        //{
        //    return _entrustdao.UpdateCommandSecurityEntrustStatus(submitId, entrustItems, entrustStatus);
        //}

        public int Delete(int submitId)
        {
            return _entrustdao.Delete(submitId);
        }
    }
}
