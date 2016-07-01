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

        public int SubmitOne(List<CancelRedoItem> cancelItems)
        {
            var commandIds = cancelItems.Select(p => p.CommandId).Distinct().ToList();
            var commandId = commandIds.First();
            EntrustCommandItem cmdItem = new EntrustCommandItem 
            {
                CommandId = commandId,
            };

            //TODO: adjust the EntrustAmount
            List<EntrustSecurityItem> entrustItems = new List<EntrustSecurityItem>();
            foreach (var cancelItem in cancelItems)
            {
                EntrustSecurityItem item = new EntrustSecurityItem
                {
                    CommandId = cancelItem.CommandId,
                    SecuCode = cancelItem.SecuCode,
                    SecuType = cancelItem.SecuType,
                    EntrustAmount = cancelItem.LeftAmount,
                    EntrustPrice = cancelItem.EntrustPrice,
                    EntrustDirection = cancelItem.EntrustDirection,
                    EntrustPriceType = cancelItem.EEntrustPriceType,
                    PriceType = cancelItem.EPriceSetting,
                    EntrustDate = DateTime.Now,
                };

                entrustItems.Add(item);
            }

            return SubmitOne(cmdItem, entrustItems);
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
            int ret = -1;
            var submitIds = cancelItems.Select(p => p.SubmitId).ToList();
            foreach (var submitId in submitIds)
            { 
                var matchItems = cancelItems.Where(p => p.SubmitId == submitId).ToList();

                ret = UpdateCommandSecurityEntrustStatus(submitId, matchItems, EntrustStatus.CancelToDB);
            }

            return ret;
        }

        public int CancelSuccess(List<CancelRedoItem> cancelItems)
        {
            int ret = -1;
            var submitIds = cancelItems.Select(p => p.SubmitId).ToList();
            foreach (var submitId in submitIds)
            {
                var matchItems = cancelItems.Where(p => p.SubmitId == submitId).ToList();

                ret = UpdateCommandSecurityEntrustStatus(submitId, matchItems, EntrustStatus.CancelSuccess);
            }

            return ret;
        }

        private int UpdateCommandSecurityEntrustStatus(int submitId, List<CancelRedoItem> cancelItems, EntrustStatus entrustStatus)
        {
            int ret = -1;
            var matchItems = cancelItems.Where(p => p.SubmitId == submitId).ToList();
            var entrustSecuItems = GetEntrustSecurityItems(matchItems);

            ret = _entrustdao.UpdateCommandSecurityEntrustStatus(submitId, entrustSecuItems, entrustStatus);
            return ret;
        }

        private List<EntrustSecurityItem> GetEntrustSecurityItems(List<CancelRedoItem> cancelItems)
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

            return secuItems;
        }
    }
}
