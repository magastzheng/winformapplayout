using Config;
using hundsun.t2sdk;
using log4net;
using Model;
using Model.Binding.BindingUtil;
using Model.config;
using Model.UFX;
using System;
using System.Collections.Generic;

namespace UFX.impl
{
    public class SecurityBLL:UFXBLLBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SecurityBLL(T2SDKWrap t2SDKWrap)
            : base(t2SDKWrap)
        {
            RegisterUFX(UFXFunctionCode.Entrust);
            RegisterUFX(UFXFunctionCode.Withdraw);
            RegisterUFX(UFXFunctionCode.EntrustBasket);
            RegisterUFX(UFXFunctionCode.WithdrawBasket);
            RegisterUFX(UFXFunctionCode.QuerySecurityEntrust);
            RegisterUFX(UFXFunctionCode.QuerySecurityEntrustHistorical);
            RegisterUFX(UFXFunctionCode.QuerySecurityDeal);
            RegisterUFX(UFXFunctionCode.QuerySecurityDealHistorical);
            RegisterUFX(UFXFunctionCode.QuerySecurityHolding);
            RegisterUFX(UFXFunctionCode.QueryMultipleHolding);
            RegisterUFX(UFXFunctionCode.QueryFuturesEntrust);
            RegisterUFX(UFXFunctionCode.QueryFuturesEntrustHistorical);
            RegisterUFX(UFXFunctionCode.QueryFuturesDeal);
            RegisterUFX(UFXFunctionCode.QueryFuturesDealHistorical);
        }

        #region entrust/withdraw

        public ConnectionCode Entrust(List<UFXEntrustRequest> requests, Callbacker callbacker)
        {
            return SubmitAsync<UFXEntrustRequest>(UFXFunctionCode.Entrust, requests, callbacker);
        }

        public ConnectionCode Withdraw(List<UFXWithdrawRequest> requests, Callbacker callbacker)
        {
            return SubmitAsync<UFXWithdrawRequest>(UFXFunctionCode.Withdraw, requests, callbacker);
        }

        public ConnectionCode EntrustBasket(List<UFXBasketEntrustRequest> requests, Callbacker callbacker)
        {
            return SubmitAsync<UFXBasketEntrustRequest>(UFXFunctionCode.EntrustBasket, requests, callbacker);
        }

        public ConnectionCode WithdrawBasket(UFXBasketWithdrawRequest request, Callbacker callbacker)
        {
            List<UFXBasketWithdrawRequest> requests = new List<UFXBasketWithdrawRequest> { request };

            return SubmitAsync<UFXBasketWithdrawRequest>(UFXFunctionCode.WithdrawBasket, requests, callbacker);
        }

        #endregion

        #region query normal security entrust/deal

        public ConnectionCode QueryEntrust(List<UFXQueryEntrustRequest> requests, Callbacker callbacker)
        {
            return SubmitAsync<UFXQueryEntrustRequest>(UFXFunctionCode.QuerySecurityEntrust, requests, callbacker);
        }

        public ConnectionCode QueryEntrustHistory(List<UFXQueryHistEntrustRequest> requests, Callbacker callbacker)
        {
            return SubmitAsync<UFXQueryHistEntrustRequest>(UFXFunctionCode.QuerySecurityEntrustHistorical, requests, callbacker);
        }

        public ConnectionCode QueryDeal(List<UFXQueryDealRequest> requests, Callbacker callbacker)
        {
            return SubmitAsync<UFXQueryDealRequest>(UFXFunctionCode.QuerySecurityDeal, requests, callbacker);
        }

        public ConnectionCode QueryDealHistory(List<UFXQueryHistDealRequest> requests, Callbacker callbacker)
        {
            return SubmitAsync<UFXQueryHistDealRequest>(UFXFunctionCode.QuerySecurityDealHistorical, requests, callbacker);
        }

        #endregion

        #region query futures entrust/deal

        public ConnectionCode QueryFuturesEntrust(List<UFXQueryFuturesEntrustRequest> requests, Callbacker callbacker)
        {
            return SubmitAsync<UFXQueryFuturesEntrustRequest>(UFXFunctionCode.QueryFuturesEntrust, requests, callbacker);
        }

        public ConnectionCode QueryFuturesEntrustHistory(List<UFXQueryFuturesHistEntrustRequest> requests, Callbacker callbacker)
        {
            return SubmitAsync<UFXQueryFuturesHistEntrustRequest>(UFXFunctionCode.QueryFuturesEntrustHistorical, requests, callbacker);
        }

        public ConnectionCode QueryFuturesDeal(List<UFXQueryFuturesDealRequest> requests, Callbacker callbacker)
        {
            return SubmitAsync<UFXQueryFuturesDealRequest>(UFXFunctionCode.QueryFuturesDeal, requests, callbacker);
        }

        public ConnectionCode QueryFuturesDealHistory(List<UFXQueryFuturesHistDealRequest> requests, Callbacker callbacker)
        {
            return SubmitAsync<UFXQueryFuturesHistDealRequest>(UFXFunctionCode.QueryFuturesDealHistorical, requests, callbacker);
        }

        #endregion

        #region query holding

        public ConnectionCode QueryHolding(List<UFXHoldingRequest> requests, Callbacker callbacker)
        {
            return SubmitAsync<UFXHoldingRequest>(UFXFunctionCode.QuerySecurityHolding, requests, callbacker);
        }

        public ConnectionCode QueryMultipleHolding(List<UFXHoldingRequest> requests, Callbacker callbacker)
        {
            return SubmitAsync<UFXHoldingRequest>(UFXFunctionCode.QueryMultipleHolding, requests, callbacker);
        }

        #endregion
    }
}
