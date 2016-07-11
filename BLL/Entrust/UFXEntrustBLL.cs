using BLL.UFX.impl;
using Config.ParamConverter;
using Model.strategy;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entrust
{
    public class UFXEntrustBLL
    {
        private SecurityBLL _securityBLL = null;

        public UFXEntrustBLL()
        {
            _securityBLL = BLLManager.Instance.SecurityBLL;
        }

        public int Submit(EntrustCommandItem cmdItem, List<EntrustSecurityItem> entrustItems)
        {
            int ret = -1;

            var ufxRequests = new List<UFXBasketEntrustRequest>();
            var futuItem = entrustItems.Find(p => p.SecuType == Model.SecurityInfo.SecurityType.Futures);
            foreach (var secuItem in entrustItems)
            {
                UFXBasketEntrustRequest request = new UFXBasketEntrustRequest 
                {
                    StockCode = secuItem.SecuCode,
                    EntrustDirection = EntrustRequestHelper.GetEntrustDirection(secuItem.EntrustDirection),
                    EntrustPrice = secuItem.EntrustPrice,
                    EntrustAmount = secuItem.EntrustAmount,
                    PriceType = EntrustRequestHelper.GetEntrustPriceType(secuItem.EntrustPriceType),
                    ExtSystemId = secuItem.SubmitId,
                };

                if (secuItem.SecuType == Model.SecurityInfo.SecurityType.Futures)
                {
                    request.FuturesDirection = EntrustRequestHelper.GetEntrustDirection(secuItem.EntrustDirection);
                }
                else if (futuItem != null)
                {
                    request.FuturesDirection = EntrustRequestHelper.GetEntrustDirection(futuItem.EntrustDirection);
                }
                else
                { 
                    //do nothing
                }
            }

            return ret;
        }
    }
}
