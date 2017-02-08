using DBAccess.EntrustCommand;
using Model.Database;
using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Util;

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

        public int UpdateEntrustStatus(int submitId, int commandId, string secuCode, EntrustStatus entrustStatus)
        {
            return _entrustsecudao.UpdateEntrustStatus(submitId, commandId, secuCode, entrustStatus);
        }

        public int UpdateEntrustStatusByEntrustNo(int entrustNo, EntrustStatus entrustStatus)
        {
            return _entrustsecudao.UpdateEntrustStatusByEntrustNo(entrustNo, entrustStatus);
        }

        public int UpdateEntrustStatusByRequestId(int requestId, int entrustNo, int batchNo, int entrustFailCode, string entrustFailCause)
        {
            return _entrustsecudao.UpdateEntrustStatusByRequestId(requestId, entrustNo, batchNo, entrustFailCode, entrustFailCause);
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

        public List<EntrustSecurity> GetEntrustSecurities(List<int> commandIds)
        {
            var entrustSecuItems = new List<EntrustSecurity>();

            foreach (var commandId in commandIds)
            {
                var secuItem = GetByCommandId(commandId);
                entrustSecuItems.AddRange(secuItem);
            }

            return entrustSecuItems;
        }

        public List<EntrustSecurity> GetByCommandId(int commandId)
        {
            return _entrustsecudao.GetByCommandId(commandId);
        }

        public List<EntrustSecurityCombine> GetFailItemByCommandId(int commandId)
        {
            //获取今天委托不成功的证券
            var allItems = GetCombineByCommandId(commandId);
            return allItems.Where(p => p.EntrustDate.Subtract(DateTime.Now).Days == 0 && (p.EntrustNo <= 0 || p.EntrustFailCode > 0)).ToList();
        }

        public List<EntrustSecurity> GetBySubmitId(int submitId)
        {
            var combineItems = GetCombineBySubmitId(submitId);

            return new List<EntrustSecurity>(combineItems);
        }

        public List<EntrustSecurity> GetCancel(int commandId)
        {
            return _entrustsecudao.GetCancel(commandId);
        }

        public List<EntrustSecurity> GetCancelBySumbitId(int submitId)
        {
            return _entrustsecudao.GetCancelBySumbitId(submitId);
        }

        public List<EntrustSecurity> GetCancelRedoBySubmitId(int submitId)
        {
            return _entrustsecudao.GetCancelRedoBySubmitId(submitId);
        }

        #endregion

        #region get combine

        public List<EntrustSecurityCombine> GetAllCombine()
        {
            return _entrustsecudao.GetAllCombine();
        }

        public EntrustSecurityCombine GetByRequestId(int requestId)
        {
            return _entrustsecudao.GetByRequestId(requestId);
        }

        public List<EntrustSecurityCombine> GetCombineBySubmitId(int submitId)
        {
            return _entrustsecudao.GetCombineBySubmitId(submitId);
        }

        private List<EntrustSecurityCombine> GetCombineByCommandId(int commandId)
        {
            return _entrustsecudao.GetCombineByCommandId(commandId);
        }

        #endregion
    }
}
