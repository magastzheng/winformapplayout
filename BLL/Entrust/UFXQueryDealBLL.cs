using BLL.Entrust.Futures;
using BLL.Entrust.Security;
using BLL.Manager;
using BLL.Product;
using log4net;
using Model.UI;
using System;
using System.Collections.Generic;
using UFX.impl;

namespace BLL.Entrust
{
    public class UFXQueryDealBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ProductBLL _productBLL = new ProductBLL();
        private UFXQuerySecurityDealBLL _querySecurityDealBLL = new UFXQuerySecurityDealBLL();
        private UFXQueryFuturesDealBLL _queryFuturesDealBLL = new UFXQueryFuturesDealBLL();

        private int _timeOut = 30 * 1000;

        public UFXQueryDealBLL()
        {
            _timeOut = SettingManager.Instance.Get().UFXSetting.Timeout;
        }

        public List<DealFlowItem> QueryToday(CallerCallback callback)
        {
            List<DealFlowItem> dealItems = new List<DealFlowItem>();

            var portfolios = _productBLL.GetAll();

            var dealSecuItems = _querySecurityDealBLL.QueryToday(portfolios, _timeOut, callback);
            if (dealSecuItems != null && dealSecuItems.Count > 0)
            {
                dealItems.AddRange(dealSecuItems);
            }

            var dealFuturesItems = _queryFuturesDealBLL.QueryToday(portfolios, _timeOut, callback);
            if (dealFuturesItems != null && dealFuturesItems.Count > 0)
            {
                dealItems.AddRange(dealFuturesItems);
            }

            return dealItems;
        }

        public List<DealFlowItem> QueryHistory(DateTime startDate, DateTime endDate, CallerCallback callback)
        {
            List<DealFlowItem> dealItems = new List<DealFlowItem>();

            var portfolios = _productBLL.GetAll();

            var dealSecuItems = _querySecurityDealBLL.QueryHistory(portfolios, startDate, endDate, _timeOut, callback);
            if (dealSecuItems != null && dealSecuItems.Count > 0)
            {
                dealItems.AddRange(dealSecuItems);
            }

            var dealFuturesItems = _queryFuturesDealBLL.QueryHistory(portfolios, startDate, endDate, _timeOut, callback);
            if (dealFuturesItems != null && dealFuturesItems.Count > 0)
            {
                dealItems.AddRange(dealFuturesItems);
            }

            return dealItems;
        }
    }
}
