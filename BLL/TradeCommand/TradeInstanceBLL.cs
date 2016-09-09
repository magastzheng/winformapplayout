using BLL.Product;
using BLL.SecurityInfo;
using DBAccess;
using DBAccess.TradeInstance;
using Model.EnumType;
using Model.SecurityInfo;
using Model.UI;
using System.Collections.Generic;
using System.Linq;

namespace BLL.TradeCommand
{
    public class TradeInstanceBLL
    {
        private TradingInstanceDAO _tradinginstancedao = new TradingInstanceDAO();
        private TradingInstanceSecurityDAO _tradeinstsecudao = new TradingInstanceSecurityDAO();
        private TradeInstanceDAO _tradeinstancedao = new TradeInstanceDAO();
        private ProductBLL _productBLL = new ProductBLL();

        public TradeInstanceBLL()
        { 
        }

        public int Create(TradingInstance tradeInstance, OpenPositionItem openItem, List<OpenPositionSecurityItem> secuItems)
        {
            var tradeinstSecus = GetTradingInstanceSecurities(tradeInstance, openItem, secuItems);
            int ret = _tradeinstancedao.Create(tradeInstance, tradeinstSecus);
            if (ret > 0)
            {
                return tradeInstance.InstanceId;
            }
            else
            {
                return -1;
            }
        }

        public int Update(TradingInstance tradeInstance, OpenPositionItem openItem, List<OpenPositionSecurityItem> secuItems)
        {
            var tradeinstSecus = GetTradingInstanceSecurities(tradeInstance, openItem, secuItems);
            return _tradeinstancedao.Update(tradeInstance, tradeinstSecus);
        }

        //public int Update(TradingInstance tradeInstance, ClosePositionItem closeItem, List<ClosePositionSecurityItem> secuItems)
        //{
        //    return -1;
        //}

        public TradingInstance GetInstance(int instanceId)
        {
            return _tradinginstancedao.GetCombine(instanceId);
        }

        public TradingInstance GetInstance(string instanceCode)
        {
            var instance = _tradinginstancedao.GetCombineByCode(instanceCode);
            return instance;
        }

        public List<TradingInstance> GetAllInstance()
        {
            return _tradinginstancedao.GetCombineAll();
        }

        public List<TradingInstance> GetPortfolioInstance(string portfolioCode)
        {
            var allInstances = GetAllInstance();
            var portInstances = allInstances.Where(p => p.PortfolioCode.Equals(portfolioCode));

            return portInstances.ToList();
        }

        public List<InstanceItem> GetAllInstanceItem()
        {
            var allInstances = GetAllInstance();
            var portolios = _productBLL.GetAll();
            var instItems = new List<InstanceItem>();

            foreach (var instance in allInstances)
            {
                InstanceItem instItem = new InstanceItem 
                {
                    InstanceId = instance.InstanceId,
                    InstanceCode = instance.InstanceCode,
                    PortfolioId = instance.PortfolioId,
                    PortfolioCode = instance.PortfolioCode,
                    PortfolioName = instance.PortfolioName,
                    TemplateId = instance.TemplateId,
                    TemplateName = instance.TemplateName,
                    MonitorUnitName = instance.MonitorUnitName,
                    DCreatedDate = instance.CreatedDate,
                    Owner = instance.Owner
                };

                var portfolio = portolios.Find(p => p.PortfolioId == instItem.PortfolioId);
                if (portfolio != null)
                {
                    instItem.FundCode = portfolio.FundCode;
                    instItem.FundName = portfolio.FundName;
                    instItem.AssetUnitCode = portfolio.AssetNo;
                    instItem.AssetUnitName = portfolio.AssetName;
                }

                instItems.Add(instItem);
            }

            return instItems;
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
                    SecuType = item.SecuType
                };

                //var findItem = SecurityInfoManager.Instance.Get(item.SecuCode);
                //if (findItem != null)
                //{
                //    tiSecuItem.SecuType = findItem.SecuType;
                //}

                if (item.Selection)
                {
                    switch (tiSecuItem.SecuType)
                    {
                        case SecurityType.Stock:
                            {
                                tiSecuItem.InstructionPreBuy = openItem.Copies * item.WeightAmount;
                                if (item.EDirection == EntrustDirection.BuySpot)
                                {
                                    tiSecuItem.PositionType = PositionType.StockLong;
                                }
                                else if (item.EDirection == EntrustDirection.SellSpot)
                                {
                                    tiSecuItem.PositionType = PositionType.StockShort;
                                }
                            }
                            break;
                        case SecurityType.Futures:
                            {
                                tiSecuItem.InstructionPreSell = openItem.Copies * item.WeightAmount;
                                if (item.EDirection == EntrustDirection.SellOpen)
                                {
                                    tiSecuItem.PositionType = PositionType.FuturesShort;
                                }
                                else if (item.EDirection == EntrustDirection.BuyClose)
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

        private List<TradingInstanceSecurity> GetTradingInstanceSecurities(TradingInstance tradingInstance, ClosePositionItem closeItem, List<ClosePositionSecurityItem> closeSecuItems)
        {
            List<TradingInstanceSecurity> tradeInstanceSecuItems = new List<TradingInstanceSecurity>();
            foreach (var item in closeSecuItems)
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
                                //tiSecuItem.InstructionPreBuy = closeItem.Copies * item.WeightAmount;
                                //if (item.EntrustDirection == EntrustDirection.BuySpot)
                                //{
                                //    tiSecuItem.PositionType = PositionType.StockLong;
                                //}
                                //else if (item.DirectionType == EntrustDirection.SellSpot)
                                //{
                                //    tiSecuItem.PositionType = PositionType.StockShort;
                                //}
                            }
                            break;
                        case SecurityType.Futures:
                            {
                                //tiSecuItem.InstructionPreSell = openItem.Copies * item.WeightAmount;
                                //if (item.DirectionType == EntrustDirection.SellOpen)
                                //{
                                //    tiSecuItem.PositionType = PositionType.FuturesShort;
                                //}
                                //else if (item.DirectionType == EntrustDirection.BuyClose)
                                //{
                                //    tiSecuItem.PositionType = PositionType.FuturesLong;
                                //}
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
