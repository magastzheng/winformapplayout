﻿using BLL.Deal;
using BLL.EntrustCommand;
using BLL.TradeInstance;
using BLL.UFX;
using BLL.UFX.impl;
using Config.ParamConverter;
using log4net;
using Model.Binding.BindingUtil;
using Model.Database;
using Model.UFX;
using System.Collections.Generic;

namespace BLL.Entrust.subscriber
{
    public class UFXEntrustDealBLL : IUFXSubsriberBLLBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private DealSecurityBLL _dealSecurityBLL = new DealSecurityBLL();
        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();
        private TradeInstanceSecurityBLL _tradeInstanceSecuBLL = new TradeInstanceSecurityBLL();

        public UFXEntrustDealBLL()
        { 
        }

        public int Handle(DataParser dataParser)
        {
            List<UFXEntrustDealResponse> responseItems = new List<UFXEntrustDealResponse>();
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                //TODO: check the count of dataset.
                responseItems = UFXDataSetHelper.ParseSubscribeData<UFXEntrustDealResponse>(dataParser);
            }

            //update the database
            if (responseItems != null && responseItems.Count > 0)
            {
                List<EntrustSecurity> entrustSecuItems = new List<EntrustSecurity>();
                foreach (var responseItem in responseItems)
                {
                    int commandId;
                    int submitId;
                    int requestId;

                    //TODO: add log
                    if (EntrustRequestHelper.ParseThirdReff(responseItem.ThirdReff, out commandId, out submitId, out requestId))
                    {
                        _entrustSecurityBLL.UpdateDeal(submitId, commandId, responseItem.StockCode, responseItem.DealAmount, responseItem.DealBalance, responseItem.DealFee);

                        //TODO: save into database
                        var dealItem = Convert(responseItem);
                        _dealSecurityBLL.Create(dealItem);

                        //Update the TradingInstanceSecurity
                        _tradeInstanceSecuBLL.UpdateToday(dealItem.EntrustDirection, commandId, dealItem.SecuCode, dealItem.DealAmount, dealItem.DealBalance, dealItem.DealFee);
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

        private DealSecurity Convert(UFXEntrustDealResponse responseItem)
        {
            DealSecurity dealItem = new DealSecurity 
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
            dealItem.EntrustState = Model.EnumType.EntrustStatus.Completed;
            //dealItem.EntrustState = UFXTypeConverter.GetEntrustState(responseItem.EntrustState);

            return dealItem;
        }
    }
}
