using BLL.UFX.impl;
using Config.ParamConverter;
using DBAccess;
using log4net;
using Model.Binding.BindingUtil;
using Model.t2sdk;
using Model.UI;
using System.Collections.Generic;

namespace BLL.Entrust.subscriber
{
    public class UFXEntrustDealBLL : IUFXSubsriberBLLBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private EntrustSecurityDAO _entrustsecudao = new EntrustSecurityDAO();
        private DealSecurityDAO _dealsecudao = new DealSecurityDAO();

        public UFXEntrustDealBLL()
        { 
        }

        public int Handle(DataParser dataParser)
        {
            List<UFXEntrustDealResponse> responseItems = new List<UFXEntrustDealResponse>();
            var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXEntrustDealResponse>();

            //TODO: check the count of dataset.
            for (int i = 1, count = dataParser.DataSets.Count; i < count; i++)
            {
                var dataSet = dataParser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    UFXEntrustDealResponse p = new UFXEntrustDealResponse();
                    UFXDataSetHelper.SetValue<UFXEntrustDealResponse>(ref p, dataRow.Columns, dataFieldMap);
                    responseItems.Add(p);
                }
            }

            //update the database
            if (responseItems.Count > 0)
            {
                List<EntrustSecurityItem> entrustSecuItems = new List<EntrustSecurityItem>();
                foreach (var responseItem in responseItems)
                {
                    int commandId;
                    int submitId;
                    int requestId;

                    if (EntrustRequestHelper.ParseThirdReff(responseItem.ThirdReff, out commandId, out submitId, out requestId))
                    {
                        _entrustsecudao.UpdateDeal(submitId, commandId, responseItem.StockCode, responseItem.DealAmount, responseItem.DealBalance, responseItem.DealFee);

                       //TODO: save into database
                        var dealItem = Convert(responseItem);
                        _dealsecudao.Create(dealItem);
                    }
                    else
                    { 
                        string msg = string.Format("Fail to parse the third_reff: {0}", responseItem.ThirdReff);
                        logger.Error(msg); 
                    }
                }
            }

            return responseItems.Count;
        }

        private DealSecurityItem Convert(UFXEntrustDealResponse responseItem)
        {
            DealSecurityItem dealItem = new DealSecurityItem 
            { 
                SecuCode = responseItem.StockCode,
                DealNo = responseItem.DealNo,
                BatchNo = responseItem.BatchNo,
                EntrustNo = responseItem.EntrustNo,
                AccountCode = responseItem.AccountCode,
                PortfolioCode = responseItem.CombiNo,
                StockHolderId = responseItem.StockHolderId,
                ReportSeat = responseItem.ReportSeat,
                DealDate = responseItem.DealDate,
                DealTime = responseItem.DealTime,
                EntrustAmount = responseItem.EntrustAmount,
                DealAmount = responseItem.DealAmount,
                DealPrice = responseItem.DealPrice,
                DealBalance = responseItem.DealBalance,
                DealFee = responseItem.DealFee,
                TotalDealAmount = responseItem.TotalDealAmount,
                TotalDealBalance = responseItem.TotalDealBalance,
                CancelAmount = responseItem.CancelAmount,
            };

            int commandId;
            int submitId;
            int requestId;

            if (EntrustRequestHelper.ParseThirdReff(responseItem.ThirdReff, out commandId, out submitId, out requestId))
            {
                dealItem.CommandId = commandId;
                dealItem.SubmitId = submitId;
                dealItem.RequestId = requestId;
            }

            dealItem.ExchangeCode = EntrustRequestHelper.GetExchangeCode(responseItem.MarketNo);
            dealItem.EntrustDirection = EntrustRequestHelper.GetEntrustDirectionType(responseItem.EntrustDirection, dealItem.ExchangeCode);
            //dealItem.EntrustState = 

            return dealItem;
        }
    }
}
