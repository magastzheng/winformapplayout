using BLL.Entrust;
using BLL.SecurityInfo;
using BLL.UFX.impl;
using DBAccess;
using Model.BLL;
using Model.EnumType;
using Model.SecurityInfo;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Frontend
{
    public class WithdrawBLL
    {
        private EntrustSecurityDAO _entrustsecudao = new EntrustSecurityDAO();
        private EntrustCommandDAO _entrustcmddao = new EntrustCommandDAO();
        private EntrustDAO _entrustdao = new EntrustDAO();

        private UFXBasketWithdrawBLL _ufxBasketWithdrawBLL = new UFXBasketWithdrawBLL();

        public WithdrawBLL()
        { 
        }

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

        public List<CancelRedoItem> GetCancelRedoBySubmitId(EntrustCommandItem cmdItem)
        {
            var entrustSecuItems = _entrustsecudao.GetCancelRedoBySubmitId(cmdItem.SubmitId);
            var cancelItemList = new List<CancelRedoItem>();
            if (entrustSecuItems == null)
            {
                return cancelItemList;
            }
            foreach (var p in entrustSecuItems)
            {
                CancelRedoItem cancelRedoItem = new CancelRedoItem
                {
                    Selection = true,
                    CommandId = cmdItem.CommandId,
                    //EntrustAmount = p.EntrustAmount,
                    //EntrustDirection = p.EntrustDirection,
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
                };

                cancelRedoItem.EntrustAmount = cancelRedoItem.LeftAmount;
                if (cancelRedoItem.SecuType == Model.SecurityInfo.SecurityType.Stock && cancelRedoItem.EDirection == EntrustDirection.BuySpot)
                {
                    if (cancelRedoItem.LeftAmount % 100 != 0)
                    {
                        cancelRedoItem.EntrustAmount = 100 * (int)Math.Round((double)(cancelRedoItem.LeftAmount / 100));
                    }
                }

                var secuInfo = SecurityInfoManager.Instance.Get(p.SecuCode, p.SecuType);
                if (secuInfo != null)
                {
                    cancelRedoItem.ExchangeCode = secuInfo.ExchangeCode;
                }
                else
                {
                    cancelRedoItem.ExchangeCode = SecurityItemHelper.GetExchangeCode(p.SecuCode);
                }

                cancelItemList.Add(cancelRedoItem);
            }

            return cancelItemList;
        }
    }
}
