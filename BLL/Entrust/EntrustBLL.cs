using DBAccess;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entrust
{
    public class EntrustBLL
    {
        private EntrustCommandDAO _entrustcmddao = new EntrustCommandDAO();
        private EntrustSecurityDAO _entrustsecudao = new EntrustSecurityDAO();
        private EntrustDAO _entrustdao = new EntrustDAO();

        public EntrustBLL()
        { 
        }

        public int SubmitOne(EntrustCommandItem cmdItem, List<EntrustSecurityItem> entrustItems)
        {
            return _entrustdao.Submit(cmdItem, entrustItems);
        }

        public int Submit(List<EntrustCommandItem> cmdItems, List<EntrustSecurityItem> entrustItems)
        {
            int ret = -1;
            foreach (var cmdItem in cmdItems)
            {
                var cmdSecuItems = entrustItems.Where(p => p.CommandId == cmdItem.CommandId).ToList();
                ret = _entrustdao.Submit(cmdItem, cmdSecuItems);
            }

            return -1;
        }

        public int Cancel(List<CancelRedoItem> cancelItems)
        {
            return UpdateEntrustStatus(cancelItems, EntrustStatus.CancelToDB);
        }

        public int CancelSuccess(List<CancelRedoItem> cancelItems)
        {
            int ret = UpdateEntrustStatus(cancelItems, EntrustStatus.CancelSuccess);
            ret = UpdateCommandEntrustStatus(cancelItems, EntrustStatus.CancelSuccess);

            return ret;
        }

        private int UpdateEntrustStatus(List<CancelRedoItem> cancelItems, EntrustStatus entrustStatus)
        {
            List<EntrustSecurityItem> secuItems = new List<EntrustSecurityItem>();
            foreach (var cancelItem in cancelItems)
            {
                EntrustSecurityItem item = new EntrustSecurityItem
                {
                    CommandId = cancelItem.CommandId,
                    SubmitId = cancelItem.SubmitId,
                    SecuCode = cancelItem.SecuCode,
                    SecuType = cancelItem.SecuType,
                };

                secuItems.Add(item);
            }

            int ret = _entrustdao.UpdateSecurityEntrustStatus(secuItems, entrustStatus);

            return ret;
        }

        private int UpdateCommandEntrustStatus(List<CancelRedoItem> cancelItems, EntrustStatus entrustStatus)
        {
            var submitIds = cancelItems.Select(p => p.SubmitId).ToList();
            int ret = -1;
            foreach (var submitId in submitIds)
            {
                ret = _entrustcmddao.UpdateEntrustCommandStatus(submitId, entrustStatus);
            }

            return ret;
        }
    }
}
