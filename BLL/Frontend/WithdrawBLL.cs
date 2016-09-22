using BLL.Entrust;
using BLL.EntrustCommand;
using BLL.SecurityInfo;
using BLL.UFX.impl;
using DBAccess.Entrust;
using DBAccess.TradeCommand;
using Model.BLL;
using Model.EnumType;
using Model.SecurityInfo;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Frontend
{
    public class WithdrawBLL
    {
        private EntrustDAO _entrustdao = new EntrustDAO();
        private TradingCommandDAO _tradecmddao = new TradingCommandDAO();

        private EntrustCommandBLL _entrustCommandBLL = new EntrustCommandBLL();
        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();
        private UFXBasketWithdrawBLL _ufxBasketWithdrawBLL = new UFXBasketWithdrawBLL();
        private UFXWithdrawBLL _ufxWithdrawBLL = new UFXWithdrawBLL();

        public WithdrawBLL()
        { 
        }

        #region cancel

        public List<EntrustCommandItem> CancelOne(TradingCommandItem cmdItem, CallerCallback callerCallback)
        {
            List<EntrustCommandItem> cancelEntrustCmdItems = new List<EntrustCommandItem>();

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
                    _entrustdao.UpdateOneEntrustStatus(entrustCmdItem.SubmitId, EntrustStatus.CancelToDB);

                    var bllResponse = _ufxBasketWithdrawBLL.Cancel(entrustCmdItem, entrustSecuCancelItems, callerCallback);
                    if (BLLResponse.Success(bllResponse))
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
            //if (copies > 0)
            //{
            //    copies = 0 - copies;
            //    int ret = _tradecmddao.UpdateTargetNum(cmdItem.CommandId, copies);
            //}

            return cancelEntrustCmdItems;
        }

        public List<CancelRedoItem> CancelSecuItem(int submitId, int commandId, List<CancelRedoItem> cancelItems, CallerCallback callerCallback)
        {
            List<CancelRedoItem> cancelSecuItems = new List<CancelRedoItem>();

            var entrustedSecuItems = ConvertToEntrustSecuItems(cancelItems);

            //set the status as EntrustStatus.CancelToDB in database
            int ret = _entrustdao.UpdateSecurityEntrustStatus(entrustedSecuItems, EntrustStatus.CancelToDB);
            if (ret <= 0)
            {
                return cancelSecuItems;
            }

            var bllResponse = _ufxWithdrawBLL.Cancel(submitId, commandId, entrustedSecuItems, callerCallback);
            if (BLLResponse.Success(bllResponse))
            {
                //int copies = cmdItem.Copies;
                //_entrustdao.UpdateOneEntrustStatus(cmdItem.SubmitId, EntrustStatus.CancelSuccess);

                cancelSecuItems.AddRange(cancelItems);
            }
            else
            {
                ret = _entrustdao.UpdateSecurityEntrustStatus(entrustedSecuItems, EntrustStatus.CancelFail);
            }

            return cancelSecuItems;
        }

        public List<CancelSecurityItem> CancelSecuItem(int submitId, int commandId, List<CancelSecurityItem> cancelItems, CallerCallback callerCallback)
        {
            var cancelSecuItems = new List<CancelSecurityItem>();
            var entrustedSecuItems = new List<EntrustSecurityItem>();
            foreach (var cancelItem in cancelItems)
            {
                var entrustSecuItem = ConvertBack(cancelItem);
                entrustedSecuItems.Add(entrustSecuItem);
            }

            //set the status as EntrustStatus.CancelToDB in database
            int ret = _entrustdao.UpdateSecurityEntrustStatus(entrustedSecuItems, EntrustStatus.CancelToDB);
            if (ret <= 0)
            {
                return cancelSecuItems;
            }

            var bllResponse = _ufxWithdrawBLL.Cancel(submitId, commandId, entrustedSecuItems, callerCallback);
            if (BLLResponse.Success(bllResponse))
            {
                //int copies = cmdItem.Copies;
                //_entrustdao.UpdateOneEntrustStatus(cmdItem.SubmitId, EntrustStatus.CancelSuccess);

                cancelSecuItems.AddRange(cancelItems);
            }
            else
            {
                ret = _entrustdao.UpdateSecurityEntrustStatus(entrustedSecuItems, EntrustStatus.CancelFail);
            }

            return cancelSecuItems;
        }

        #endregion

        #region get/fetch

        public List<EntrustCommandItem> GetEntrustedCmdItems(TradingCommandItem cmdItem)
        {
            return _entrustCommandBLL.GetCancel(cmdItem.CommandId);
        }

        public List<CancelRedoItem> GetEntrustedSecuItems(EntrustCommandItem cmdItem)
        {
            var entrustSecuItems = _entrustSecurityBLL.GetCancelBySumbitId(cmdItem.SubmitId);
            var cancelItemList = new List<CancelRedoItem>();
            if (entrustSecuItems == null)
            {
                return cancelItemList;
            }

            var tradeCommand = _tradecmddao.Get(cmdItem.CommandId);
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

        public List<CancelRedoItem> GetCancelRedoBySubmitId(EntrustCommandItem cmdItem)
        {
            var entrustSecuItems = _entrustSecurityBLL.GetCancelRedoBySubmitId(cmdItem.SubmitId);
            var cancelItemList = new List<CancelRedoItem>();
            if (entrustSecuItems == null)
            {
                return cancelItemList;
            }

            var tradeCommand = _tradecmddao.Get(cmdItem.CommandId);
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

        private List<EntrustSecurityItem> ConvertToEntrustSecuItems(List<CancelRedoItem> cancelItems)
        {
            var entrustedSecuItems = new List<EntrustSecurityItem>();
            foreach (var cancelItem in cancelItems)
            {
                var entrustItem = ConvertBack(cancelItem);
                entrustedSecuItems.Add(entrustItem);
            }

            return entrustedSecuItems;
        }

        private CancelRedoItem Convert(EntrustSecurityItem p, Model.Database.TradeCommand tradeCommand)
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
                cancelRedoItem.ExchangeCode = SecurityItemHelper.GetExchangeCode(p.SecuCode);
            }

            return cancelRedoItem;
        }

        private EntrustSecurityItem ConvertBack(CancelSecurityItem cancelItem)
        {
            var entrustItem = new EntrustSecurityItem 
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
    }
}
