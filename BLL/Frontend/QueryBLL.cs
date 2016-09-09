using BLL.EntrustCommand;
using BLL.SecurityInfo;
using DBAccess;
using Model.Constant;
using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Frontend
{
    public class QueryBLL
    {
        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();

        public QueryBLL()
        { 
        }

        #region get/fetch

        public List<EntrustFlowItem> GetEntrustFlow()
        {
            List<EntrustFlowItem> efItems = new List<EntrustFlowItem>();
            var allItems = _entrustSecurityBLL.GetAllCombine();
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
                    EntrustedDate = DateFormat.Format(item.EntrustDate, ConstVariable.DateFormat),
                    EntrustedTime = DateFormat.Format(item.EntrustDate, ConstVariable.TimeFormat1),
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

            var allItems = _entrustSecurityBLL.GetAllCombine();
            var entrustedNoDealItems = allItems.Where(p => p.DealStatus == DealStatus.Completed);

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
                    DealTime = DateFormat.Format(item.ModifiedDate, ConstVariable.DateFormat1),
                    EntrustBatchNo = item.BatchNo.ToString(),
                    InstanceId = item.InstanceId.ToString(),
                    InstanceNo = item.InstanceCode,
                    DealNo = item.EntrustNo.ToString(),
                };

                dfItems.Add(dfItem);
            }

            return dfItems;
        }

        public List<EntrustSecurityItem> GetEntrustSecurityItems(List<Model.Database.TradeCommand> cmdItems)
        {
            var entrustSecuItems = new List<EntrustSecurityItem>();

            foreach (var cmdItem in cmdItems)
            {
                var cmdSecuItems = _entrustSecurityBLL.GetByCommandId(cmdItem.CommandId);
                entrustSecuItems.AddRange(cmdSecuItems);
            }

            return entrustSecuItems;
        }

        #endregion
    }
}
