using BLL.Entrust.Futures;
using BLL.Entrust.Security;
using BLL.EntrustCommand;
using BLL.Product;
using BLL.UFX;
using BLL.UFX.impl;
using Config;
using log4net;
using Model;
using Model.Binding.BindingUtil;
using Model.BLL;
using Model.Converter;
using Model.EnumType.EnumTypeConverter;
using Model.UFX;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Util;

namespace BLL.Entrust
{
    public class UFXQueryEntrustBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ProductBLL _productBLL = new ProductBLL();
        private UFXQuerySecurityEntrustBLL _querySecurityEntrustBLL = new UFXQuerySecurityEntrustBLL();
        private UFXQueryFuturesEntrustBLL _queryFuturesEntrustBLL = new UFXQueryFuturesEntrustBLL();

        private int _timeOut = 30 * 1000;

        public UFXQueryEntrustBLL()
        {
            _timeOut = ConfigManager.Instance.GetDefaultSettingConfig().DefaultSetting.UFXSetting.Timeout;
        }

        public List<EntrustFlowItem> QueryToday(CallerCallback callback)
        {
            List<EntrustFlowItem> entrustItems = new List<EntrustFlowItem>();
            var portfolios = _productBLL.GetAll();

            var secuEntrustItems = _querySecurityEntrustBLL.QueryToday(portfolios, _timeOut, callback);
            if (secuEntrustItems != null && secuEntrustItems.Count > 0)
            {
                entrustItems.AddRange(secuEntrustItems);
            }

            var futuresEntrustItems = _queryFuturesEntrustBLL.QueryToday(portfolios, _timeOut, callback);
            if (futuresEntrustItems != null && futuresEntrustItems.Count > 0)
            {
                entrustItems.AddRange(futuresEntrustItems);
            }

            return entrustItems;
        }

        public List<EntrustFlowItem> QueryHistory(DateTime startDate, DateTime endDate, CallerCallback callback)
        {
            List<EntrustFlowItem> entrustItems = new List<EntrustFlowItem>();
            var portfolios = _productBLL.GetAll();

            var secuEntrustItems = _querySecurityEntrustBLL.QueryHistory(portfolios, startDate, endDate, _timeOut, callback);
            if (secuEntrustItems != null && secuEntrustItems.Count > 0)
            {
                entrustItems.AddRange(secuEntrustItems);
            }

            var futuresEntrustItems = _queryFuturesEntrustBLL.QueryHistory(portfolios, startDate, endDate, _timeOut, callback);
            if (futuresEntrustItems != null && futuresEntrustItems.Count > 0)
            {
                entrustItems.AddRange(futuresEntrustItems);
            }

            return entrustItems;
        }
    }
}
