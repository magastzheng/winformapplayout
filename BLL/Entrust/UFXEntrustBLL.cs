﻿using BLL.SecurityInfo;
using BLL.TradeCommand;
using BLL.UFX.impl;
using Config;
using Config.ParamConverter;
using DBAccess;
using log4net;
using Model.Binding.BindingUtil;
using Model.Data;
using Model.t2sdk;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BLL.Entrust
{
    public class UFXEntrustBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;
        private TradeCommandBLL _tradeCommandBLL = null;
        private EntrustDAO _entrustdao = new EntrustDAO();
        private EntrustCommandDAO _entrustcmddao = new EntrustCommandDAO();
        private TradingCommandDAO _tradecmddao = new TradingCommandDAO();

        private int _timeOut = 10 * 1000;

        public UFXEntrustBLL()
        {
            _securityBLL = BLLManager.Instance.SecurityBLL;
            _tradeCommandBLL = new TradeCommandBLL();
        }

        public int Submit(EntrustCommandItem cmdItem, List<EntrustSecurityItem> entrustItems)
        {
            int ret = -1;

            var cmdEntrustItems = entrustItems.Where(p => p.CommandId == cmdItem.CommandId && p.SubmitId == cmdItem.SubmitId).ToList();
            if (cmdEntrustItems == null || cmdEntrustItems.Count == 0)
            {
                return ret;
            }
            var tradeCommandItem = _tradeCommandBLL.GetTradeCommandItem(cmdItem.CommandId);
            var portfolio = LoginManager.Instance.GetPortfolio(tradeCommandItem.PortfolioCode);
            //var stockholder = LoginManager.Instance.GetHolder(tradeCommandItem.

            var ufxRequests = new List<UFXBasketEntrustRequest>();
            var futuItem = cmdEntrustItems.Find(p => p.SecuType == Model.SecurityInfo.SecurityType.Futures);
            foreach (var secuItem in cmdEntrustItems)
            {
                UFXBasketEntrustRequest request = new UFXBasketEntrustRequest 
                {
                    StockCode = secuItem.SecuCode,
                    EntrustPrice = secuItem.EntrustPrice,
                    EntrustAmount = secuItem.EntrustAmount,
                    PriceType = EntrustRequestHelper.GetEntrustPriceType(secuItem.EntrustPriceType),
                    ExtSystemId = secuItem.RequestId,
                    ThirdReff = string.Format("{0};{1};{2}", secuItem.CommandId, secuItem.SubmitId, secuItem.RequestId),
                    LimitEntrustRatio = 100,
                    FutuLimitEntrustRatio = 100,
                    OptLimitEntrustRatio = 100,
                };

                if (secuItem.SecuType == Model.SecurityInfo.SecurityType.Stock)
                {
                    request.EntrustDirection = EntrustRequestHelper.GetEntrustDirection(secuItem.EntrustDirection);
                    request.FuturesDirection = string.Empty;
                }
                else if (secuItem.SecuType == Model.SecurityInfo.SecurityType.Futures)
                {
                    request.EntrustDirection = EntrustRequestHelper.GetEntrustDirection(secuItem.EntrustDirection);
                    request.FuturesDirection = EntrustRequestHelper.GetFuturesDirection(secuItem.EntrustDirection);
                }

                var secuInfo = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                if (secuInfo != null)
                {
                    request.MarketNo = EntrustRequestHelper.GetMarketNo(secuInfo.ExchangeCode);
                }

                if (tradeCommandItem != null)
                {
                    request.AccountCode = tradeCommandItem.FundCode;
                    request.CombiNo = tradeCommandItem.PortfolioCode;
                }

                ufxRequests.Add(request);
            }

            Callbacker callbacker = new Callbacker
            {
                Token = new CallbackToken
                {
                    SubmitId = cmdItem.SubmitId,
                    CommandId = cmdItem.CommandId,
                },

                Callback = EntrustBasketCallback,
            };

            var result = _securityBLL.EntrustBasket(ufxRequests, callbacker);

            if (result == Model.ConnectionCode.Success)
            {
                ret = 1;
            }
            
            return ret;
        }

        public int Cancel(EntrustCommandItem cmdItem, List<EntrustSecurityItem> entrustItems)
        {
            int ret = -1;

            UFXBasketWithdrawRequest request = new UFXBasketWithdrawRequest 
            {
                BatchNo = cmdItem.BatchNo,   
            };

            List<UFXBasketWithdrawRequest> requests = new List<UFXBasketWithdrawRequest>();
            requests.Add(request);

            Callbacker callbacker = new Callbacker
            {
                Token = new CallbackToken
                {
                    SubmitId = cmdItem.SubmitId,
                    CommandId = cmdItem.CommandId,
                },

                Callback = WithdrawBasketCallback,
            };

            var result = _securityBLL.WithdrawBasket(requests, callbacker);

            if (result == Model.ConnectionCode.Success)
            {
                ret = 1;
            }

            return ret;
        }

        private int EntrustBasketCallback(CallbackToken token, DataParser dataParser)
        {
            List<UFXBasketEntrustResponse> responseItems = new List<UFXBasketEntrustResponse>();
            var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXBasketEntrustResponse>();
            for (int i = 1, count = dataParser.DataSets.Count; i < count; i++)
            {
                var dataSet = dataParser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    UFXBasketEntrustResponse p = new UFXBasketEntrustResponse();
                    UFXDataSetHelper.SetValue<UFXBasketEntrustResponse>(ref p, dataRow.Columns, dataFieldMap);
                    responseItems.Add(p);
                }
            }

            int ret = -1;
            List<EntrustSecurityItem> entrustSecuItems = new List<EntrustSecurityItem>();
            foreach (var responseItem in responseItems)
            {
                var entrustItem = new EntrustSecurityItem
                {
                    SubmitId = token.SubmitId,
                    RequestId = responseItem.ExtSystemId,
                    SecuCode = responseItem.StockCode,
                    EntrustNo = responseItem.EntrustNo,
                    BatchNo = responseItem.BatchNo,
                };

                entrustSecuItems.Add(entrustItem);
            }

            ret = _entrustdao.UpdateSecurityEntrustResponseByRequestId(entrustSecuItems);

            var batchNos = responseItems.Select(p => p.BatchNo).Distinct().ToList();
            if (batchNos.Count == 1)
            {
                int batchNo = batchNos[0];

                ret = _entrustcmddao.UpdateEntrustCommandBatchNo(token.SubmitId, batchNo, Model.EnumType.EntrustStatus.Completed);
            }
            else
            {
                //TODO:
                string msg = string.Format("The SubmitId [{0}] was split into several batch no", token.SubmitId);
                logger.Warn(msg);
            }

            return ret;
        }

        private int WithdrawBasketCallback(CallbackToken token, DataParser dataParser)
        {
            List<UFXBasketWithdrawResponse> responseItems = new List<UFXBasketWithdrawResponse>();

            var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXBasketWithdrawResponse>();
            for (int i = 1, count = dataParser.DataSets.Count; i < count; i++)
            {
                var dataSet = dataParser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    UFXBasketWithdrawResponse p = new UFXBasketWithdrawResponse();
                    UFXDataSetHelper.SetValue<UFXBasketWithdrawResponse>(ref p, dataRow.Columns, dataFieldMap);
                    responseItems.Add(p);
                }
            }

            int ret = -1;
            if (token.SubmitId > 0)
            {
                List<EntrustSecurityItem> entrustSecuItems = new List<EntrustSecurityItem>();
                foreach (var responseItem in responseItems)
                {
                    var entrustItem = new EntrustSecurityItem
                    {
                        SubmitId = token.SubmitId,
                        CommandId = token.CommandId,
                        SecuCode = responseItem.StockCode,
                        EntrustNo = responseItem.EntrustNo,
                    };

                    entrustSecuItems.Add(entrustItem);
                }

                ret = _entrustdao.UpdateSecurityEntrustStatus(entrustSecuItems, Model.EnumType.EntrustStatus.CancelSuccess);
                ret = _entrustcmddao.UpdateEntrustCommandStatus(token.SubmitId, Model.EnumType.EntrustStatus.CancelSuccess);
                ret = _tradecmddao.UpdateTargetNumBySubmitId(token.SubmitId, token.CommandId);
            }

           
            return ret;
        }
    }
}
