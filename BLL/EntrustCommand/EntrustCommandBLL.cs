using DBAccess;
using DBAccess.Entrust;
using Model.EnumType;
using Model.UI;
using System.Collections.Generic;

namespace BLL.EntrustCommand
{
    public class EntrustCommandBLL
    {
        private EntrustCommandDAO _entrustcmddao = new EntrustCommandDAO();

        public EntrustCommandBLL()
        { 
        }

        #region update

        public int UpdateEntrustCommandStatus(int submitId, EntrustStatus entrustStatus)
        {
            return _entrustcmddao.UpdateEntrustCommandStatus(submitId, entrustStatus);
        }

        public int UpdateEntrustCommandBatchNo(int submitId, int batchNo, EntrustStatus entrustStatus, int entrustFailCode, string entrustFailCause)
        {
            return _entrustcmddao.UpdateEntrustCommandBatchNo(submitId, batchNo, entrustStatus, entrustFailCode, entrustFailCause);
        }

        public int UpdateDealStatus(int submitId, DealStatus dealStatus)
        {
            return _entrustcmddao.UpdateDealStatus(submitId, dealStatus);
        }

        #endregion

        #region delete

        public int DeleteByCommandId(int commandId)
        {
            return _entrustcmddao.DeleteByCommandId(commandId);
        }

        public int DeleteByCommandIdStatus(int commandId, EntrustStatus status)
        {
            return _entrustcmddao.DeleteByCommandIdStatus(commandId, status);
        }

        #endregion

        #region get/fetch

        public List<EntrustCommandItem> GetCancel(int commandId)
        {
            return _entrustcmddao.GetCancel(commandId);
        }

        #endregion
    }
}
