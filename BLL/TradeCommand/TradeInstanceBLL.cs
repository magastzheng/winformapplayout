using BLL.SecurityInfo;
using DBAccess;
using Model.SecurityInfo;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.TradeCommand
{
    public class TradeInstanceBLL
    {
        private TradingInstanceDAO _tradeinstdao = new TradingInstanceDAO();
        private TradingInstanceSecurityDAO _tradeinstsecudao = new TradingInstanceSecurityDAO();

        public TradeInstanceBLL()
        { 
        }

        public int Create(TradingInstance tradingInstance, OpenPositionItem openItem, List<OpenPositionSecurityItem> secuItems)
        {
            int instanceId = _tradeinstdao.Create(tradingInstance);

            if (instanceId > 0)
            {
                var tradeinstSecus = GetTradingInstanceSecurities(instanceId, openItem, secuItems);
                if (tradeinstSecus != null && tradeinstSecus.Count > 0)
                {
                    foreach (var tiItem in tradeinstSecus)
                    {
                        string rowid = _tradeinstsecudao.Create(tiItem);
                        if (string.IsNullOrEmpty(rowid))
                        {
                            //TODO: find to store the ....
                        }
                    }
                }
            }

            return instanceId;
        }

        public int Update(TradingInstance tradeInstance)
        {
            return _tradeinstdao.Update(tradeInstance);
        }

        #region

        private List<TradingInstanceSecurity> GetTradingInstanceSecurities(int instanceId, OpenPositionItem openItem, List<OpenPositionSecurityItem> secuItems)
        {
            List<TradingInstanceSecurity> tradeInstanceSecuItems = new List<TradingInstanceSecurity>();
            foreach (var item in secuItems)
            {
                TradingInstanceSecurity tiSecuItem = new TradingInstanceSecurity
                {
                    InstanceId = instanceId,
                    SecuCode = item.SecuCode
                };

                var findItem = SecurityInfoManager.Instance.Get(item.SecuCode);
                if (findItem != null)
                {
                    tiSecuItem.SecuType = findItem.SecuType;
                }

                if (item.Selection)
                {
                    switch (tiSecuItem.SecuType)
                    {
                        case SecurityType.Stock:
                            {
                                tiSecuItem.InstructionPreBuy = openItem.Copies * item.WeightAmount;
                            }
                            break;
                        case SecurityType.Futures:
                            {
                                tiSecuItem.InstructionPreSell = openItem.Copies * item.WeightAmount;
                            }
                            break;
                        default:
                            break;
                    }
                }

                tradeInstanceSecuItems.Add(tiSecuItem);
            }

            return tradeInstanceSecuItems;
        }

        #endregion
    }
}
