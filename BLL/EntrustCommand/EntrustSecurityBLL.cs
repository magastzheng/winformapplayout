using DBAccess.Entrust;
using Model.EnumType;
using Model.UI;
using System.Collections.Generic;

namespace BLL.EntrustCommand
{
    public class EntrustSecurityBLL
    {
        private EntrustSecurityDAO _entrustsecudao = new EntrustSecurityDAO();

        public EntrustSecurityBLL()
        { 
        }

        #region update

        public int UpdateDeal(int submitId, int commandId, string secuCode, int dealAmount, double dealBalance, double dealFee)
        {
            return _entrustsecudao.UpdateDeal(submitId, commandId, secuCode, dealAmount, dealBalance, dealFee);
        }

        #endregion

        #region delete
        public int Delete(int submitId, int commandId, string secuCode)
        {
            return _entrustsecudao.Delete(submitId, commandId, secuCode);
        }

        public int DeleteBySubmitId(int submitId)
        {
            return _entrustsecudao.DeleteBySubmitId(submitId);
        }

        public int DeleteByCommandId(int commandId)
        {
            return _entrustsecudao.DeleteByCommandId(commandId);
        }

        public int DeleteByCommandIdEntrustStatus(int commandId, EntrustStatus status)
        {
            return _entrustsecudao.DeleteByCommandIdEntrustStatus(commandId, status);
        }

        #endregion

        #region fetch/get

        public List<EntrustSecurityItem> GetByCommandId(int commandId)
        {
            return _entrustsecudao.GetByCommandId(commandId);
        }

        public List<EntrustSecurityItem> GetCancel(int commandId)
        {
            return _entrustsecudao.GetCancel(commandId);
        }

        public List<EntrustSecurityItem> GetCancelBySumbitId(int submitId)
        {
            return _entrustsecudao.GetCancelBySumbitId(submitId);
        }

        public List<EntrustSecurityItem> GetCancelRedoBySubmitId(int submitId)
        {
            return _entrustsecudao.GetCancelRedoBySubmitId(submitId);
        }

        #endregion

        #region get combine

        public List<EntrustSecurityCombineItem> GetAllCombine()
        {
            return _entrustsecudao.GetAllCombine();
        }

        public EntrustSecurityCombineItem GetByRequestId(int requestId)
        {
            return _entrustsecudao.GetByRequestId(requestId);
        }

        #endregion
    }
}
