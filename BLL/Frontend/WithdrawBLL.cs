using BLL.Entrust;
using BLL.EntrustCommand;
using BLL.SecurityInfo;
using BLL.UFX.impl;
using BLL.UsageTracking;
using Config;
using DBAccess.EntrustCommand;
using DBAccess.TradeCommand;
using Model.BLL;
using Model.Database;
using Model.EnumType;
using Model.Permission;
using Model.SecurityInfo;
using Model.UI;
using Model.UsageTracking;
using System;
using System.Collections.Generic;
using Util;
using System.Linq;
using BLL.TradeCommand;

namespace BLL.Frontend
{
    public class WithdrawBLL
    {
        private EntrustCombineBLL _entrustCombineBLL = new EntrustCombineBLL();
        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private UserActionTrackingBLL _userActionTrackingBLL = new UserActionTrackingBLL();
        private EntrustCommandBLL _entrustCommandBLL = new EntrustCommandBLL();
        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();
        private UFXBasketWithdrawBLL _ufxBasketWithdrawBLL = new UFXBasketWithdrawBLL();
        private UFXWithdrawBLL _ufxWithdrawBLL = new UFXWithdrawBLL();

        public WithdrawBLL()
        { 
        }

        #region cancel

        public List<Model.Database.EntrustCommand> CancelOne(TradeCommandItem cmdItem, CallerCallback callerCallback)
        {
            Tracking(ActionType.Cancel, ResourceType.TradeCommand, cmdItem.CommandId, cmdItem);
            List<Model.Database.EntrustCommand> cancelEntrustCmdItems = new List<Model.Database.EntrustCommand>();

            var entrustCmdItems = _entrustCommandBLL.GetCancel(cmdItem.CommandId);
            if (entrustCmdItems == null || entrustCmdItems.Count == 0)
            {
                return cancelEntrustCmdItems;
            }

            var entrustSecuItems = _entrustSecurityBLL.GetCancel(cmdItem.CommandId);
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
                    _entrustCombineBLL.UpdateOneEntrustStatus(entrustCmdItem.SubmitId, EntrustStatus.CancelToDB);

                    var bllResponse = _ufxBasketWithdrawBLL.Withdraw(entrustCmdItem, callerCallback);
                    if (BLLResponse.Success(bllResponse))
                    {
                        copies += entrustCmdItem.Copies;
                        _entrustCombineBLL.UpdateOneEntrustStatus(entrustCmdItem.SubmitId, EntrustStatus.CancelSuccess);

                        cancelEntrustCmdItems.Add(entrustCmdItem);
                    }
                    else
                    {
                        _entrustCombineBLL.UpdateOneEntrustStatus(entrustCmdItem.SubmitId, EntrustStatus.CancelFail);
                    }
                }
            }

            //Update the tradingcommand table TargetNum
            //if (copies > 0)
            //{
            //    copies = 0 - copies;
            //    int ret = _tradecmddao.UpdateTargetNum(cmdItem.CommandId, copies);
            //}

            return cancelEntrustCmdItems;
        }

        public List<CancelRedoItem> CancelSecuItem(int submitId, int commandId, List<CancelRedoItem> cancelItems, CallerCallback callerCallback)
        {
            Tracking(ActionType.Cancel, ResourceType.TradeCommand, -1, null);

            List<CancelRedoItem> cancelSecuItems = new List<CancelRedoItem>();

            var entrustedSecuItems = ConvertToEntrustSecuItems(cancelItems);

            //set the status as EntrustStatus.CancelToDB in database
            int ret = _entrustCombineBLL.UpdateSecurityEntrustStatus(entrustedSecuItems, EntrustStatus.CancelToDB);
            if (ret <= 0)
            {
                return cancelSecuItems;
            }

            var bllResponse = _ufxWithdrawBLL.Withdraw(submitId, commandId, entrustedSecuItems, callerCallback);
            if (BLLResponse.Success(bllResponse))
            {
                //int copies = cmdItem.Copies;
                //_entrustdao.UpdateOneEntrustStatus(cmdItem.SubmitId, EntrustStatus.CancelSuccess);

                cancelSecuItems.AddRange(cancelItems);
            }
            else
            {
                ret = _entrustCombineBLL.UpdateSecurityEntrustStatus(entrustedSecuItems, EntrustStatus.CancelFail);
            }

            return cancelSecuItems;
        }

        public List<CancelSecurityItem> CancelSecuItem(int submitId, int commandId, List<CancelSecurityItem> cancelItems, CallerCallback callerCallback)
        {
            var cancelSecuItems = new List<CancelSecurityItem>();
            var entrustedSecuItems = new List<EntrustSecurity>();
            foreach (var cancelItem in cancelItems)
            {
                var entrustSecuItem = ConvertBack(cancelItem);
                entrustedSecuItems.Add(entrustSecuItem);
            }

            //set the status as EntrustStatus.CancelToDB in database
            int ret = _entrustCombineBLL.UpdateSecurityEntrustStatus(entrustedSecuItems, EntrustStatus.CancelToDB);
            if (ret <= 0)
            {
                return cancelSecuItems;
            }

            var bllResponse = _ufxWithdrawBLL.Withdraw(submitId, commandId, entrustedSecuItems, callerCallback);
            if (BLLResponse.Success(bllResponse))
            {
                //int copies = cmdItem.Copies;
                //_entrustdao.UpdateOneEntrustStatus(cmdItem.SubmitId, EntrustStatus.CancelSuccess);

                cancelSecuItems.AddRange(cancelItems);
            }
            else
            {
                ret = _entrustCombineBLL.UpdateSecurityEntrustStatus(entrustedSecuItems, EntrustStatus.CancelFail);
            }

            return cancelSecuItems;
        }

        #endregion

        #region get/fetch

        public List<Model.Database.EntrustCommand> GetEntrustedCmdItems(TradeCommandItem cmdItem)
        {
            return _entrustCommandBLL.GetCancel(cmdItem.CommandId);
        }

        public List<CancelRedoItem> GetEntrustedSecuItems(Model.Database.EntrustCommand cmdItem)
        {
            var entrustSecuItems = _entrustSecurityBLL.GetCancelBySumbitId(cmdItem.SubmitId);
            var cancelItemList = new List<CancelRedoItem>();
            if (entrustSecuItems == null)
            {
                return cancelItemList;
            }

            var tradeCommand = _tradeCommandBLL.GetTradeCommand(cmdItem.CommandId);
            if (tradeCommand == null)
            {
                return cancelItemList;
            }

            DateTime now = DateTime.Now;
            foreach (var p in entrustSecuItems)
            {
                //Only can cancel the entrusted security in the same entrusted day.
                if (p.ModifiedDate != null && p.ModifiedDate.Date.Equals(now.Date))
                {
                    var cancelRedoItem = Convert(p, tradeCommand);
                    cancelItemList.Add(cancelRedoItem);
                }
            }

            return cancelItemList;
        }

        public List<CancelRedoItem> GetCancelRedoBySubmitId(Model.Database.EntrustCommand cmdItem)
        {
            var entrustSecuItems = _entrustSecurityBLL.GetCancelRedoBySubmitId(cmdItem.SubmitId);
            var cancelItemList = new List<CancelRedoItem>();
            if (entrustSecuItems == null)
            {
                return cancelItemList;
            }

            var tradeCommand = _tradeCommandBLL.GetTradeCommand(cmdItem.CommandId);
            if (tradeCommand == null)
            {
                return cancelItemList;
            }

            foreach (var p in entrustSecuItems)
            {
                var cancelRedoItem = Convert(p, tradeCommand);
               
                cancelItemList.Add(cancelRedoItem);
            }

            return cancelItemList;
        }

        #endregion

        private List<EntrustSecurity> ConvertToEntrustSecuItems(List<CancelRedoItem> cancelItems)
        {
            var entrustedSecuItems = new List<EntrustSecurity>();
            foreach (var cancelItem in cancelItems)
            {
                var entrustItem = ConvertBack(cancelItem);
                entrustedSecuItems.Add(entrustItem);
            }

            return entrustedSecuItems;
        }

        private CancelRedoItem Convert(EntrustSecurity p, Model.Database.TradeCommand tradeCommand)
        {
            CancelRedoItem cancelRedoItem = new CancelRedoItem
            {
                Selection = true,
                CommandId = tradeCommand.CommandId,
                EDirection = p.EntrustDirection,
                EntrustPrice = p.EntrustPrice,
                SecuCode = p.SecuCode,
                SecuType = p.SecuType,
                EntrustNo = p.EntrustNo,
                ECommandPrice = p.PriceType,
                ReportPrice = p.EntrustPrice,
                EOriginPriceType = p.EntrustPriceType,
                LeftAmount = p.EntrustAmount - p.TotalDealAmount,
                ReportAmount = p.EntrustAmount,
                DealAmount = p.TotalDealAmount,
                EntrustDate = p.EntrustDate,
                SubmitId = p.SubmitId,
                EntrustBatchNo = p.BatchNo,
                PortfolioName = tradeCommand.PortfolioName,
                FundName = tradeCommand.AccountName,
            };

            cancelRedoItem.EntrustAmount = cancelRedoItem.LeftAmount;
            if (cancelRedoItem.SecuType == Model.SecurityInfo.SecurityType.Stock && cancelRedoItem.EDirection == EntrustDirection.BuySpot)
            {
                if (cancelRedoItem.LeftAmount % 100 > 0)
                {
                    cancelRedoItem.EntrustAmount = 100 * (int)Math.Round((double)(cancelRedoItem.LeftAmount / 100));
                }
            }

            var secuInfo = SecurityInfoManager.Instance.Get(p.SecuCode, p.SecuType);
            if (secuInfo != null)
            {
                cancelRedoItem.SecuName = secuInfo.SecuName;
                cancelRedoItem.ExchangeCode = secuInfo.ExchangeCode;
            }
            else
            {
                cancelRedoItem.ExchangeCode = SecurityItemHelper.GetExchangeCode(p.SecuCode, p.SecuType);
            }

            return cancelRedoItem;
        }

        private EntrustSecurity ConvertBack(CancelSecurityItem cancelItem)
        {
            var entrustItem = new EntrustSecurity 
            {
                CommandId = cancelItem.CommandId,
                SubmitId = cancelItem.SubmitId,
                SecuCode = cancelItem.SecuCode,
                SecuType = cancelItem.SecuType,
                EntrustNo = cancelItem.EntrustNo,
                BatchNo = cancelItem.EntrustBatchNo,
                PriceType = cancelItem.ECommandPrice,
                EntrustPriceType = cancelItem.EOriginPriceType,
                EntrustDirection = cancelItem.EDirection,
                EntrustAmount = cancelItem.ReportAmount,
                DealTimes = cancelItem.DealTimes,
                EntrustPrice = cancelItem.ReportPrice,
            };

            return entrustItem;
        }

        private int Tracking(ActionType actionType, ResourceType resourceType, int resourceId, TradeCommandItem cmdItem)
        {
            int userId = LoginManager.Instance.GetUserId();

            return _userActionTrackingBLL.Create(userId, actionType, resourceType, resourceId, JsonUtil.SerializeObject(cmdItem));
        }
    }
}
