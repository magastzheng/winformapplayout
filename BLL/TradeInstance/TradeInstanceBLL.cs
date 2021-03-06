﻿using BLL.Manager;
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
        private TradeInstanceDAO _tradeinstancedao = new TradeInstanceDAO();
        private TradeInstanceSecurityDAO _tradeinstsecudao = new TradeInstanceSecurityDAO();
        private TradeInstanceTransactionDAO _tradeinstancetrandao = new TradeInstanceTransactionDAO();
        private ProductBLL _productBLL = new ProductBLL();
        private UserBLL _userBll = new UserBLL();
        private PermissionManager _permissionManager = new PermissionManager();

        public TradeInstanceBLL()
        { 
        }

        public int Create(Model.UI.TradeInstance tradeInstance, List<OpenPositionSecurityItem> secuItems)
        {
            var tradeinstSecus = GetTradingInstanceSecurities(tradeInstance.InstanceId, secuItems);
            int ret = _tradeinstancetrandao.Create(tradeInstance, tradeinstSecus);
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

        public int Update(Model.UI.TradeInstance tradeInstance, List<OpenPositionSecurityItem> secuItems)
        {
            int userId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(userId, tradeInstance.InstanceId, ResourceType.TradeInstance, PermissionMask.Edit))
            {
                var tradeinstSecus = GetTradingInstanceSecurities(tradeInstance.InstanceId, secuItems);
                return _tradeinstancetrandao.Update(tradeInstance, tradeinstSecus);
            }
            else
            {
                return -1;
            }
        }

        public int Update(Model.UI.TradeInstance tradeInstance, List<ClosePositionSecurityItem> secuItems)
        {
            int userId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(userId, tradeInstance.InstanceId, ResourceType.TradeInstance, PermissionMask.Edit))
            {
                var tradeinstSecus = GetTradingInstanceSecurities(tradeInstance.InstanceId, secuItems);
                return _tradeinstancetrandao.Update(tradeInstance, tradeinstSecus);
            }
            else
            {
                return -1;
            }
        }

        public int Update(Model.UI.TradeInstance tradeInstance, List<TradeInstanceSecurity> modifiedSecuItems, List<TradeInstanceSecurity> cancelSecuItems)
        { 
            int userId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(userId, tradeInstance.InstanceId, ResourceType.TradeInstance, PermissionMask.Edit))
            {
                return _tradeinstancetrandao.Update(tradeInstance, modifiedSecuItems, cancelSecuItems);
            }
            else
            {
                return -1;
            }
        }

        public int UpdateTradeInstance(Model.UI.TradeInstance tradeInstance)
        {
            int userId = LoginManager.Instance.GetUserId();
            if (_permissionManager.HasPermission(userId, tradeInstance.InstanceId, ResourceType.TradeInstance, PermissionMask.Edit))
            {
                return _tradeinstancedao.Update(tradeInstance);
            }
            else
            {
                return -1;
            }
        }

        public Model.UI.TradeInstance GetInstance(int instanceId)
        {
            int userId = LoginManager.Instance.GetUserId();

            if (_permissionManager.HasPermission(userId, instanceId, ResourceType.TradeInstance, PermissionMask.View))
            {
                return _tradeinstancedao.GetCombine(instanceId);
            }
            else
            {
                return new Model.UI.TradeInstance();
            }
        }

        public Model.UI.TradeInstance GetInstance(string instanceCode)
        {
            int userId = LoginManager.Instance.GetUserId();
            var instance = _tradeinstancedao.GetCombineByCode(instanceCode);
            if (_permissionManager.HasPermission(userId, instance.InstanceId, ResourceType.TradeInstance, PermissionMask.View))
            {
                return instance;
            }
            else
            {
                return new Model.UI.TradeInstance();
            }
        }

        public List<Model.UI.TradeInstance> GetAllInstance()
        {
            int userId = LoginManager.Instance.GetUserId();
            var allInstances = _tradeinstancedao.GetCombineAll();
            var instances = new List<Model.UI.TradeInstance>();
            foreach (var instance in allInstances)
            {
                if (_permissionManager.HasPermission(userId, instance.InstanceId, ResourceType.TradeInstance, PermissionMask.View))
                {
                    instances.Add(instance);
                }
            }

            return instances;
        }

        public List<Model.UI.TradeInstance> GetPortfolioInstance(string portfolioCode)
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
            var currentUser = LoginManager.Instance.GetUser();

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
                    MonitorUnitId = instance.MonitorUnitId,
                    MonitorUnitName = instance.MonitorUnitName,
                    DCreatedDate = instance.CreatedDate,
                    Owner = instance.Owner,
                    Notes = instance.Notes,
                };

                instItem.Creator = GetUserName(instItem.Owner, currentUser);

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
                return _tradeinstancedao.Delete(instanceId);
            }
            else
            {
                return -1;
            }
        }

        #region

        private List<TradeInstanceSecurity> GetTradingInstanceSecurities(int instanceId, List<OpenPositionSecurityItem> secuItems)
        {
            List<TradeInstanceSecurity> tradeInstanceSecuItems = new List<TradeInstanceSecurity>();
            foreach (var item in secuItems)
            {
                TradeInstanceSecurity tiSecuItem = new TradeInstanceSecurity
                {
                    InstanceId = instanceId,
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

        private List<TradeInstanceSecurity> GetTradingInstanceSecurities(int instanceId, List<ClosePositionSecurityItem> closeSecuItems)
        {
            List<TradeInstanceSecurity> tradeInstanceSecuItems = new List<TradeInstanceSecurity>();
            foreach (var item in closeSecuItems)
            {
                TradeInstanceSecurity tiSecuItem = new TradeInstanceSecurity
                {
                    InstanceId = instanceId,
                    SecuCode = item.SecuCode
                };

                var findItem = SecurityInfoManager.Instance.Get(item.SecuCode, item.SecuType);
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

        private void SetPreItem(TradeInstanceSecurity tiSecuItem, EntrustDirection direction, int entrustAmount)
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

        private string GetUserName(int userId, User currentUser)
        {
            string name = string.Empty;
            if (currentUser.Id == userId)
            {
                name = currentUser.Name;
            }
            else
            {
                var user = _userBll.GetById(userId);
                if (user != null)
                {
                    name = user.Name;
                }
            }

            return name;
        }
        #endregion
    }
}
