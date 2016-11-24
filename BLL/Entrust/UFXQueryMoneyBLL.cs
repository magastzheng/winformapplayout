using BLL.Product;
using BLL.UFX.impl;
using log4net;
using Model.UFX;
using System.Collections.Generic;

namespace BLL.Entrust
{
    public class UFXQueryMoneyBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private QuerySyncBLL _querySyncBLL = null;
        private ProductBLL _productBLL = new ProductBLL();

        public UFXQueryMoneyBLL()
        {
            _querySyncBLL = BLLManager.Instance.QuerySyncBLL;
        }

        public void Query()
        { 
            var portfolios = _productBLL.GetAll();
            List<UFXQueryMoneyRequest> requests = new List<UFXQueryMoneyRequest>();
            foreach (var portfolio in portfolios)
            {
                UFXQueryMoneyRequest request = new UFXQueryMoneyRequest 
                {
                    AccountCode = portfolio.FundCode,
                    AssetNo = portfolio.AssetNo,
                    CombiNo = portfolio.PortfolioNo,
                };

                requests.Add(request);
            }

            var responses = _querySyncBLL.QueryAccountMoney(requests);

            logger.Info(responses.Count);
        }
    }
}
