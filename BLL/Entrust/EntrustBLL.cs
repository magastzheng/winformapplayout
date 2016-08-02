using BLL.SecurityInfo;
using BLL.UFX.impl;
using DBAccess;
using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BLL.Entrust
{
    public class EntrustBLL
    {
        private EntrustCommandDAO _entrustcmddao = new EntrustCommandDAO();
        private EntrustSecurityDAO _entrustsecudao = new EntrustSecurityDAO();
        private EntrustDAO _entrustdao = new EntrustDAO();
        private TradingCommandDAO _tradecmddao = new TradingCommandDAO();

        //private UFXEntrustBLL _ufxEntrustBLL = new UFXEntrustBLL();
        private UFXBasketEntrustBLL _ufxBasketEntrustBLL = new UFXBasketEntrustBLL();
        private UFXBasketWithdrawBLL _ufxBasketWithdrawBLL = new UFXBasketWithdrawBLL();
       
        public EntrustBLL()
        { 
        }

        #region create
        public int SubmitOne(List<EntrustCommandItem> oldCmdItems, List<CancelRedoItem> cancelItems)
        {
            EntrustCommandItem cmdItem = MergeEntrustCommandItem(oldCmdItems);

            //TODO: adjust the EntrustAmount
            List<EntrustSecurityItem> entrustItems = new List<EntrustSecurityItem>();
            DateTime now = DateTime.Now;

            //merge the same security in with the same commandId
            var uniqueSecuCodes = cancelItems.Select(p => p.SecuCode).Distinct().ToList();
            foreach (var secuCode in uniqueSecuCodes)
            {
                EntrustSecurityItem item = new EntrustSecurityItem
                {
                    CommandId = cmdItem.CommandId,
                    SecuCode = secuCode,
                    EntrustDate = now,
                };

                var originSecuItems = cancelItems.Where(p => p.SecuCode.Equals(secuCode)).ToList();
                if (originSecuItems != null && originSecuItems.Count > 0)
                {
                    item.SecuType = originSecuItems[0].SecuType;
                    item.EntrustPrice = originSecuItems[0].EntrustPrice;
                    item.EntrustDirection = originSecuItems[0].EDirection;
                    item.EntrustPriceType = originSecuItems[0].EEntrustPriceType;
                    item.PriceType = originSecuItems[0].EPriceSetting;

                    item.EntrustAmount = originSecuItems.Sum(o => o.LeftAmount);

                    entrustItems.Add(item);
                }
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

                //ret = _ufxEntrustBLL.Submit(cmdItem, entrustItems);
                ret = _ufxBasketEntrustBLL.Submit(cmdItem, entrustItems, null);
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

        public List<EntrustCommandItem> CancelOne(TradingCommandItem cmdItem, CallerCallback callerCallback)
        {
            List<EntrustCommandItem> cancelEntrustCmdItems = new List<EntrustCommandItem>();

            var entrustCmdItems = _entrustcmddao.GetCancel(cmdItem.CommandId);
            if (entrustCmdItems == null || entrustCmdItems.Count == 0)
            {
                return cancelEntrustCmdItems;
            }

            var entrustSecuItems = _entrustsecudao.GetCancel(cmdItem.CommandId);
            if (entrustSecuItems == null || entrustSecuItems.Count == 0)
            {
                return cancelEntrustCmdItems;
            }

            int copies = 0;
            foreach (var entrustCmdItem in entrustCmdItems)
            {
                var entrustSecuCancelItems = entrustSecuItems.Where(p => p.SubmitId == entrustCmdItem.SubmitId).ToList();
                if (entrustSecuCancelItems != null && entrustSecuCancelItems.Count > 0)
                {
                    //set the status as EntrustStatus.CancelToDB in database
                    _entrustdao.UpdateOneEntrustStatus(entrustCmdItem.SubmitId, EntrustStatus.CancelToDB);

                    int ret = _ufxBasketWithdrawBLL.Cancel(entrustCmdItem, entrustSecuCancelItems, callerCallback);
                    if (ret > 0)
                    {
                        copies += entrustCmdItem.Copies;
                        _entrustdao.UpdateOneEntrustStatus(entrustCmdItem.SubmitId, EntrustStatus.CancelSuccess);

                        cancelEntrustCmdItems.Add(entrustCmdItem);
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
                int ret = _tradecmddao.UpdateTargetNum(cmdItem.CommandId, cmdItem.TargetNum);
            }

            return cancelEntrustCmdItems;
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

        private EntrustCommandItem MergeEntrustCommandItem(List<EntrustCommandItem> oldCmdItems)
        {
            Debug.Assert(oldCmdItems.Select(p => p.CommandId).Distinct().Count() == 1, "撤销的委托指令不是同一指令号");
            var commandId = oldCmdItems.Select(p => p.CommandId).Distinct().Single();
            var copies = oldCmdItems.Where(p => p.CommandId == commandId).Select(o => o.Copies).Sum();
            EntrustCommandItem cmdItem = new EntrustCommandItem
            {
                CommandId = commandId,
                Copies = copies,
            };

            return cmdItem;
        }

        #endregion
    }
}
