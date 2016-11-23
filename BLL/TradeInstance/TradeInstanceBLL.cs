using BLL.Permission;
using BLL.Product;
using BLL.SecurityInfo;
using Config;
using DBAccess.TradeInstance;
using Model.EnumType;
using Model.Permission;
using Model.SecurityInfo;
using Model.UI;
using System.Collections.Generic;
using System.Linq;

namespace BLL.TradeInstance
{
    public class TradeInstanceBLL
    {
        private TradingInstanceDAO _tradinginstancedao = new TradingInstanceDAO();
        private TradingInstanceSecurityDAO _tradeinstsecudao = new TradingInstanceSecurityDAO();
        private TradeInstanceDAO _tradeinstancedao = new TradeInstanceDAO();
        private ProductBLL _productBLL = new ProductBLL();
        private PermissionManager _permissionManager = new PermissionManager();

        public TradeInstanceBLL()
        { 
        }

        public int Create(TradingInstance tradeInstance, OpenPositionItem openItem, List<OpenPositionSecurityItem> secuItems)
        {
            var tradeinstSecus = GetTradingInstanceSecurities(tradeInstance, openItem, secuItems);
            int ret = _tradeinstancedao.Create(tradeInstance, tradeinstSecus);
            if (ret > 0)
            {
                int userId = LoginManager.Instance.GetUserId();
                var perms = _permissionManager.GetOwnerPermission();
                _permissionManager.GrantPermission(userId, tradeInstance.InstanceId, ResourceType.TradeInstance, perms);

                return tradeInstance.InstanceId;
            }
            else
            {
                return -1;
            }
        }

        public int Update(TradingInstance tradeInstance, OpenPositionItem openItem, List<OpenPositionSecurityItem> secuItems)
        {
            int userId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(userId, tradeInstance.InstanceId, ResourceType.TradeInstance, PermissionMask.Edit))
            {
                var tradeinstSecus = GetTradingInstanceSecurities(tradeInstance, openItem, secuItems);
                return _tradeinstancedao.Update(tradeInstance, tradeinstSecus);
            }
            else
            {
                return -1;
            }
        }

        public int Update(TradingInstance tradeInstance, ClosePositionItem closeItem, List<ClosePositionSecurityItem> secuItems)
        {
            int userId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(userId, tradeInstance.InstanceId, ResourceType.TradeInstance, PermissionMask.Edit))
            {
                var tradeinstSecus = GetTradingInstanceSecurities(tradeInstance, closeItem, secuItems);
                return _tradeinstancedao.Update(tradeInstance, tradeinstSecus);
            }
            else
            {
                return -1;
            }
        }

        public int Update(TradingInstance tradeInstance, List<TradingInstanceSecurity> modifiedSecuItems, List<TradingInstanceSecurity> cancelSecuItems)
        { 
            int userId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(userId, tradeInstance.InstanceId, ResourceType.TradeInstance, PermissionMask.Edit))
            {
                return _tradeinstancedao.Update(tradeInstance, modifiedSecuItems, cancelSecuItems);
            }
            else
            {
                return -1;
            }
        }

        public TradingInstance GetInstance(int instanceId)
        {
            int userId = LoginManager.Instance.GetUserId();

            if (_permissionManager.HasPermission(userId, instanceId, ResourceType.TradeInstance, PermissionMask.View))
            {
                return _tradinginstancedao.GetCombine(instanceId);
            }
            else
            {
                return new TradingInstance();
            }
        }

        public TradingInstance GetInstance(string instanceCode)
        {
            int userId = LoginManager.Instance.GetUserId();
            var instance = _tradinginstancedao.GetCombineByCode(instanceCode);
            if (_permissionManager.HasPermission(userId, instance.InstanceId, ResourceType.TradeInstance, PermissionMask.View))
            {
                return instance;
            }
            else
            {
                return new TradingInstance();
            }
        }

        public List<TradingInstance> GetAllInstance()
        {
            int userId = LoginManager.Instance.GetUserId();
            var allInstances = _tradinginstancedao.GetCombineAll();
            var instances = new List<TradingInstance>();
            foreach (var instance in allInstances)
            {
                if (_permissionManager.HasPermission(userId, instance.InstanceId, ResourceType.TradeInstance, PermissionMask.View))
                {
                    instances.Add(instance);
                }
            }

            return instances;
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

        public int Delete(int instanceId)
        { 
            var userId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(userId, instanceId, ResourceType.TradeInstance, PermissionMask.Delete))
            {
                return _tradinginstancedao.Delete(instanceId);
            }
            else
            {
                return -1;
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
                    SecuType = item.SecuType
                };

                //var findItem = SecurityInfoManager.Instance.Get(item.SecuCode);
                //if (findItem != null)
                //{
                //    tiSecuItem.SecuType = findItem.SecuType;
                //}

                if (item.Selection)
                {
                    SetPreItem(tiSecuItem, item.EDirection, item.EntrustAmount);
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
                else
                {
                    tiSecuItem.SecuType = item.SecuType;
                }

                if (item.Selection)
                {
                    SetPreItem(tiSecuItem, item.EDirection, item.EntrustAmount);
                }

                tradeInstanceSecuItems.Add(tiSecuItem);
            }

            return tradeInstanceSecuItems;
        }

        private void SetPreItem(TradingInstanceSecurity tiSecuItem, EntrustDirection direction, int entrustAmount)
        {
            switch (tiSecuItem.SecuType)
            {
                case SecurityType.Stock:
                    {
                        if (direction == EntrustDirection.BuySpot)
                        {
                            tiSecuItem.InstructionPreBuy = entrustAmount;
                            tiSecuItem.PositionType = PositionType.SpotLong;
                        }
                        else if (direction == EntrustDirection.SellSpot)
                        {
                            tiSecuItem.InstructionPreSell = entrustAmount;
                            tiSecuItem.PositionType = PositionType.SpotShort;
                        }
                    }
                    break;
                case SecurityType.Futures:
                    {
                        if (direction == EntrustDirection.SellOpen)
                        {
                            tiSecuItem.InstructionPreBuy = entrustAmount;
                            tiSecuItem.PositionType = PositionType.FuturesShort;
                        }
                        else if (direction == EntrustDirection.BuyClose)
                        {
                            tiSecuItem.InstructionPreSell = entrustAmount;
                            tiSecuItem.PositionType = PositionType.FuturesLong;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
