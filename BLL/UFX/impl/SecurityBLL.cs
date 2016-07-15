using Config;
using hundsun.t2sdk;
using log4net;
using Model;
using Model.Binding.BindingUtil;
using Model.config;
using Model.t2sdk;
using System;
using System.Collections.Generic;

namespace BLL.UFX.impl
{
    public class SecurityBLL:UFXBLLBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SecurityBLL(T2SDKWrap t2SDKWrap)
            : base(t2SDKWrap)
        {
            RegisterUFX(FunctionCode.Entrust);
            RegisterUFX(FunctionCode.Withdraw);
            RegisterUFX(FunctionCode.EntrustBasket);
            RegisterUFX(FunctionCode.WithdrawBasket);
            RegisterUFX(FunctionCode.QuerySecurityEntrust);
            RegisterUFX(FunctionCode.QuerySecurityEntrustHistorical);
            RegisterUFX(FunctionCode.QuerySecurityDeal);
            RegisterUFX(FunctionCode.QuerySecurityDealHistorical);
        }

        public ConnectionCode Entrust(List<UFXEntrustRequest> requests, Callbacker callbacker)
        {
            return Submit<UFXEntrustRequest>(FunctionCode.Entrust, requests, callbacker);
        }

        public ConnectionCode Withdraw(List<UFXWithdrawRequest> requests, Callbacker callbacker)
        {
            return Submit<UFXWithdrawRequest>(FunctionCode.Withdraw, requests, callbacker);
        }

        public ConnectionCode EntrustBasket(List<UFXBasketEntrustRequest> requests, Callbacker callbacker)
        {
            return Submit<UFXBasketEntrustRequest>(FunctionCode.EntrustBasket, requests, callbacker);
        }

        public ConnectionCode WithdrawBasket(List<UFXBasketWithdrawRequest> requests, Callbacker callbacker)
        {
            return Submit<UFXBasketWithdrawRequest>(FunctionCode.WithdrawBasket, requests, callbacker);
        }

        public ConnectionCode QueryEntrust(List<UFXQueryEntrustRequest> requests, Callbacker callbacker)
        {
            return Submit<UFXQueryEntrustRequest>(FunctionCode.QuerySecurityEntrust, requests, callbacker);
        }

        public ConnectionCode QueryEntrustHistory(List<UFXQueryHistEntrustRequest> requests, Callbacker callbacker)
        {
            return Submit<UFXQueryHistEntrustRequest>(FunctionCode.QuerySecurityEntrustHistorical, requests, callbacker);
        }

        public ConnectionCode QueryDeal(List<UFXQueryDealRequest> requests, Callbacker callbacker)
        {
            return Submit<UFXQueryDealRequest>(FunctionCode.QuerySecurityDeal, requests, callbacker);
        }

        public ConnectionCode QueryDealHistory(List<UFXQueryHistDealRequest> requests, Callbacker callbacker)
        {
            return Submit<UFXQueryHistDealRequest>(FunctionCode.QuerySecurityDealHistorical, requests, callbacker);
        }
    }
}
