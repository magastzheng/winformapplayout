using BLL.SecurityInfo;
using DBAccess;
using Model.EnumType;
using Model.SecurityInfo;
using Model.UI;
using System.Collections.Generic;

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
                tradingInstance.InstanceId = instanceId;
                var tradeinstSecus = GetTradingInstanceSecurities(tradingInstance, openItem, secuItems);
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

        public TradingInstance GetInstance(int instanceId)
        {
            var instances = _tradeinstdao.GetCombine(instanceId);
            if (instances != null && instances.Count == 1)
            {
                return instances[0];
            }
            else
            {
                return new TradingInstance();
            }
        }

        #region

        private List<TradingInstanceSecurity> GetTradingInstanceSecurities(TradingInstance tradingInstance, OpenPositionItem openItem, List<OpenPositionSecurityItem> secuItems)
        {
            List<TradingInstanceSecurity> tradeInstanceSecuItems = new List<TradingInstanceSecurity>();
            foreach (var item in secuItems)
            {
                TradingInstanceSecurity tiSecuItem = new TradingInstanceSecurity
                {
                    InstanceId = tradingInstance.InstanceId,
                    SecuCode = item.SecuCode,
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
                                if (item.DirectionType == EntrustDirection.BuySpot)
                                {
                                    tiSecuItem.PositionType = PositionType.StockLong;
                                }
                                else if (item.DirectionType == EntrustDirection.SellSpot)
                                {
                                    tiSecuItem.PositionType = PositionType.StockShort;
                                }
                            }
                            break;
                        case SecurityType.Futures:
                            {
                                tiSecuItem.InstructionPreSell = openItem.Copies * item.WeightAmount;
                                if (item.DirectionType == EntrustDirection.SellOpen)
                                {
                                    tiSecuItem.PositionType = PositionType.FuturesShort;
                                }
                                else if (item.DirectionType == EntrustDirection.BuyClose)
                                {
                                    tiSecuItem.PositionType = PositionType.FuturesLong;
                                }
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
