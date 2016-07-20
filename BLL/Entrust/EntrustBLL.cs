using BLL.SecurityInfo;
using DBAccess;
using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Entrust
{
    public class EntrustBLL
    {
        private EntrustCommandDAO _entrustcmddao = new EntrustCommandDAO();
        private EntrustSecurityDAO _entrustsecudao = new EntrustSecurityDAO();
        private EntrustDAO _entrustdao = new EntrustDAO();
        private TradingCommandDAO _tradecmddao = new TradingCommandDAO();

        private UFXEntrustBLL _ufxEntrustBLL = new UFXEntrustBLL();
       
        public EntrustBLL()
        { 
        }

        #region create
        public int SubmitOne(List<CancelRedoItem> cancelItems)
        {
            var commandIds = cancelItems.Select(p => p.CommandId).Distinct().ToList();
            var commandId = commandIds.First();

            var entrustCmdItems = _entrustcmddao.GetCancelRedo(commandId);
            int copies = entrustCmdItems.Sum(p => p.Copies);
            EntrustCommandItem cmdItem = new EntrustCommandItem 
            {
                CommandId = commandId,
                Copies = copies,
            };

            //TODO: adjust the EntrustAmount
            List<EntrustSecurityItem> entrustItems = new List<EntrustSecurityItem>();
            DateTime now = DateTime.Now;
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
                    EntrustDate = now,
                };

                entrustItems.Add(item);
            }

            return SubmitOne(cmdItem, entrustItems);
        }

        public int SubmitOne(EntrustCommandItem cmdItem, List<EntrustSecurityItem> entrustItems)
        {
            int ret = _entrustdao.Submit(cmdItem, entrustItems);

            if (ret > 0)
            {
                entrustItems.Where(p => p.CommandId == cmdItem.CommandId)
                    .ToList()
                    .ForEach(o => o.SubmitId = cmdItem.SubmitId);

                ret = _ufxEntrustBLL.Submit(cmdItem, entrustItems);
            }

            //更新交易指令中目标份数
            if (ret > 0)
            {
                var tradeCmdItem = _tradecmddao.Get(cmdItem.CommandId);
                int targetNum = tradeCmdItem.TargetNum + cmdItem.Copies;
                ret = _tradecmddao.UpdateTargetNum(cmdItem.CommandId, targetNum);
            }
            
            return ret;
        }

        public int Submit(List<EntrustCommandItem> cmdItems, List<EntrustSecurityItem> entrustItems)
        {
            int ret = -1;
            foreach (var cmdItem in cmdItems)
            {
                var cmdSecuItems = entrustItems.Where(p => p.CommandId == cmdItem.CommandId).ToList();
                ret = SubmitOne(cmdItem, cmdSecuItems);
            }

            return -1;
        }

        #endregion

        #region cancel

        public int CancelOne(TradingCommandItem cmdItem)
        {
            int ret = -1;

            var entrustCmdItems = _entrustcmddao.GetCancel(cmdItem.CommandId);
            if (entrustCmdItems == null || entrustCmdItems.Count == 0)
            {
                return -1;
            }

            var entrustSecuItems = _entrustsecudao.GetCancel(cmdItem.CommandId);
            if (entrustSecuItems == null || entrustSecuItems.Count == 0)
            {
                return ret;
            }

            int copies = 0;
            foreach (var entrustCmdItem in entrustCmdItems)
            {
                var entrustSecuCancelItems = entrustSecuItems.Where(p => p.SubmitId == entrustCmdItem.SubmitId).ToList();
                if (entrustSecuCancelItems != null && entrustSecuCancelItems.Count > 0)
                {
                    //set the status as EntrustStatus.CancelToDB in database
                    _entrustdao.UpdateOneEntrustStatus(entrustCmdItem.SubmitId, EntrustStatus.CancelToDB);

                    ret = _ufxEntrustBLL.Cancel(entrustCmdItem, entrustSecuCancelItems);
                    if (ret > 0)
                    {
                        copies += entrustCmdItem.Copies;
                        _entrustdao.UpdateOneEntrustStatus(entrustCmdItem.SubmitId, EntrustStatus.CancelSuccess);
                    }
                    else
                    {
                        _entrustdao.UpdateOneEntrustStatus(entrustCmdItem.SubmitId, EntrustStatus.CancelFail);
                    }
                }
            }

            //Update the tradingcommand table TargetNum
            if (copies > 0)
            {
                cmdItem.TargetNum -= copies;
                ret = _tradecmddao.UpdateTargetNum(cmdItem.CommandId, cmdItem.TargetNum);
            }

            return ret;
        }

        #endregion

        #region update

        //public int Cancel(List<CancelRedoItem> cancelItems)
        //{
        //    int ret = -1;
        //    var submitIds = cancelItems.Select(p => p.SubmitId).ToList();
        //    foreach (var submitId in submitIds)
        //    { 
        //        var matchItems = cancelItems.Where(p => p.SubmitId == submitId).ToList();

        //        ret = UpdateCommandSecurityEntrustStatus(submitId, matchItems, EntrustStatus.CancelToDB);
        //    }

        //    return ret;
        //}

        //public int CancelSuccess(List<CancelRedoItem> cancelItems)
        //{
        //    int ret = -1;
        //    var submitIds = cancelItems.Select(p => p.SubmitId).ToList();
        //    foreach (var submitId in submitIds)
        //    {
        //        var matchItems = cancelItems.Where(p => p.SubmitId == submitId).ToList();

        //        ret = UpdateCommandSecurityEntrustStatus(submitId, matchItems, EntrustStatus.CancelSuccess);
        //    }

        //    return ret;
        //}

        #endregion

        #region get/fetch

        public List<EntrustFlowItem> GetEntrustFlow()
        {
            List<EntrustFlowItem> efItems = new List<EntrustFlowItem>();
            var allItems = _entrustsecudao.GetAllCombine();
            var entrustedNoDealItems = allItems.Where(p =>
                (p.EntrustStatus == EntrustStatus.Completed || p.EntrustStatus == EntrustStatus.CancelFail || p.EntrustStatus == EntrustStatus.CancelSuccess)
                && (p.DealStatus != DealStatus.Completed)
                );
          
            foreach (var item in entrustedNoDealItems)
            {
                EntrustFlowItem efItem = new EntrustFlowItem 
                {
                    CommandNo = item.CommandId,
                    SecuCode = item.SecuCode,
                    PriceType = item.PriceType.ToString(),
                    EntrustStatus = item.EntrustStatus.ToString(),
                    EntrustAmount = item.EntrustAmount,
                    EntrustedDate = item.EntrustDate.ToString("yyyy-MM-dd"),
                    EntrustedTime = item.EntrustDate.ToString("hhmmss"),
                    EntrustDirection = item.EntrustDirection.ToString(),
                    EntrustNo = item.EntrustNo,
                    DealAmount = item.TotalDealAmount,
                    DealTimes = item.DealTimes,
                    InstanceId = item.InstanceId,
                    InstanceNo = item.InstanceCode,
                    EntrustBatchNo = item.BatchNo,
                    PortfolioName = item.PortfolioName,
                    FundName = item.AccountName,
                };

                var secuInfoItem = SecurityInfoManager.Instance.Get(item.SecuCode, item.SecuType);
                if (secuInfoItem != null)
                {
                    efItem.SecuName = secuInfoItem.SecuName;
                    efItem.Market = secuInfoItem.ExchangeCode;
                }

                efItems.Add(efItem);
            }

            return efItems;               
        }

        public List<DealFlowItem> GetDealFlow()
        {
            List<DealFlowItem> dfItems = new List<DealFlowItem>();

            var allItems = _entrustsecudao.GetAllCombine();
            var entrustedNoDealItems = allItems.Where(p =>p.DealStatus == DealStatus.Completed);

            foreach (var item in entrustedNoDealItems)
            {
                DealFlowItem dfItem = new DealFlowItem
                {
                    CommandNo = item.CommandId,
                    SecuCode = item.SecuCode,
                    PriceType = item.PriceType.ToString(),
                    FundNo = item.AccountCode,
                    FundName = item.AccountName,
                    PortfolioCode = item.PortfolioCode,
                    PortfolioName = item.PortfolioName,
                    EntrustPrice = item.EntrustPrice,
                    DealAmount = item.TotalDealAmount,
                    DealTime = item.ModifiedDate.ToString("hhmmss"),
                    EntrustBatchNo = item.BatchNo.ToString(),
                    InstanceId = item.InstanceId.ToString(),
                    InstanceNo = item.InstanceCode,
                    DealNo = item.EntrustNo.ToString(),
                };

                dfItems.Add(dfItem);
            }

            return dfItems;
        }

        public List<EntrustSecurityItem> GetEntrustSecurityItems(List<TradingCommandItem> cmdItems)
        {
            var entrustSecuItems = new List<EntrustSecurityItem>();

            foreach (var cmdItem in cmdItems)
            {
                var cmdSecuItems = _entrustsecudao.GetByCommandId(cmdItem.CommandId);
                entrustSecuItems.AddRange(cmdSecuItems);
            }

            return entrustSecuItems;
        }

        #endregion

        #region private

        //private int UpdateCommandSecurityEntrustStatus(int submitId, List<CancelRedoItem> cancelItems, EntrustStatus entrustStatus)
        //{
        //    int ret = -1;
        //    var matchItems = cancelItems.Where(p => p.SubmitId == submitId).ToList();
        //    var entrustSecuItems = GetEntrustSecurityItems(matchItems);

        //    ret = _entrustdao.UpdateCommandSecurityEntrustStatus(submitId, entrustSecuItems, entrustStatus);
        //    return ret;
        //}

        //private List<EntrustSecurityItem> GetEntrustSecurityItems(List<CancelRedoItem> cancelItems)
        //{
        //    List<EntrustSecurityItem> secuItems = new List<EntrustSecurityItem>();
        //    foreach (var cancelItem in cancelItems)
        //    {
        //        EntrustSecurityItem item = new EntrustSecurityItem
        //        {
        //            CommandId = cancelItem.CommandId,
        //            SubmitId = cancelItem.SubmitId,
        //            SecuCode = cancelItem.SecuCode,
        //            SecuType = cancelItem.SecuType,
        //        };

        //        secuItems.Add(item);
        //    }

        //    return secuItems;
        //}

        #endregion
    }
}
