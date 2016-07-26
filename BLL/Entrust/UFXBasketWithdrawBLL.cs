﻿using BLL.UFX;
using BLL.UFX.impl;
using DBAccess;
using log4net;
using Model.Binding.BindingUtil;
using Model.t2sdk;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Entrust
{
    public class UFXBasketWithdrawBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;

        private EntrustDAO _entrustdao = new EntrustDAO();
        private EntrustCommandDAO _entrustcmddao = new EntrustCommandDAO();
        private TradingCommandDAO _tradecmddao = new TradingCommandDAO();

        private int _timeOut = 30 * 1000;

        private readonly object _locker = new object();
        private Dictionary<int, List<UFXBasketWithdrawResponse>> _responseDataMap = new Dictionary<int, List<UFXBasketWithdrawResponse>>();
        private Dictionary<int, UFXErrorResponse> _responseErrorMap = new Dictionary<int, UFXErrorResponse>();

        public UFXBasketWithdrawBLL()
        {
            _securityBLL = BLLManager.Instance.SecurityBLL;
        }

        public int Cancel(EntrustCommandItem cmdItem, List<EntrustSecurityItem> entrustItems, CallerCallback callerCallback)
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
                Token = new CallerToken
                {
                    SubmitId = cmdItem.SubmitId,
                    CommandId = cmdItem.CommandId,
                    WaitEvent = new AutoResetEvent(false),
                    Caller = callerCallback,
                },

                DataHandler = WithdrawDataHandler,
            };

            var result = _securityBLL.WithdrawBasket(requests, callbacker);
            if (result == Model.ConnectionCode.Success)
            {
                if (callbacker.Token.WaitEvent.WaitOne(_timeOut))
                {
                    List<UFXBasketWithdrawResponse> responseItems = null;
                    UFXErrorResponse errorResponse = null;
                    lock (_locker)
                    {
                        if (_responseDataMap.ContainsKey(cmdItem.SubmitId))
                        {
                            responseItems = _responseDataMap[cmdItem.SubmitId];
                            _responseDataMap.Remove(cmdItem.SubmitId);
                        }
                        if (_responseErrorMap.ContainsKey(cmdItem.SubmitId))
                        {
                            errorResponse = _responseErrorMap[cmdItem.SubmitId];
                            _responseErrorMap.Remove(cmdItem.SubmitId);
                        }
                    }

                    return callerCallback(callbacker.Token, responseItems, errorResponse);
                }
                else
                {
                    ret = -1;
                }
            }

            return ret;
        }

        private int WithdrawDataHandler(CallerToken token, DataParser dataParser)
        {
            List<UFXBasketWithdrawResponse> responseItems = new List<UFXBasketWithdrawResponse>();

            var errorResponse = T2ErrorHandler.Handle(dataParser);

            if (dataParser.DataSets.Count > 1)
            {
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

                if (entrustSecuItems.Count > 0)
                {
                    ret = _entrustdao.UpdateSecurityEntrustStatus(entrustSecuItems, Model.EnumType.EntrustStatus.CancelSuccess);
                    ret = _entrustcmddao.UpdateEntrustCommandStatus(token.SubmitId, Model.EnumType.EntrustStatus.CancelSuccess);
                    ret = _tradecmddao.UpdateTargetNumBySubmitId(token.SubmitId, token.CommandId);
                }

                lock (_locker)
                {
                    _responseDataMap[token.SubmitId] = responseItems;
                    _responseErrorMap[token.SubmitId] = errorResponse;
                }
            }

            if (token.WaitEvent != null)
            {
                token.WaitEvent.Set();
            }

            return ret;
        }
    }
}