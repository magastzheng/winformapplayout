using BLL.SecurityInfo;
using DBAccess;
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

        public WithdrawBLL()
        { 
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
