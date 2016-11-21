using BLL.Permission;
using BLL.UsageTracking;
using Config;
using DBAccess.Template;
using DBAccess.TradeCommand;
using DBAccess.TradeInstance;
using log4net;
using Model.Database;
using Model.EnumType;
using Model.Permission;
using Model.SecurityInfo;
using Model.UI;
using Model.UsageTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using Util;

namespace BLL.Frontend
{
    public class TradeCommandBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private TradingInstanceDAO _tradeinstdao = new TradingInstanceDAO();
        private CommandDAO _commanddao = new CommandDAO();
        private TemplateStockDAO _tempstockdao = new TemplateStockDAO();
        private TradingInstanceSecurityDAO _tradeinstsecudao = new TradingInstanceSecurityDAO();
        private TradingCommandDAO _tradecommandao = new TradingCommandDAO();
        private TradingCommandSecurityDAO _tradecmdsecudao = new TradingCommandSecurityDAO();

        private UserActionTrackingBLL _userActionTrackingBLL = new UserActionTrackingBLL();
        private PermissionManager _permissionManager = new PermissionManager();
        private QueryBLL _queryBLL = new QueryBLL();

        public TradeCommandBLL()
        { 
        }

        #region submit
        public int Submit(Model.Database.TradeCommand cmdItem, List<TradeCommandSecurity> secuItems)
        {
            int userId = LoginManager.Instance.GetUserId();
            cmdItem.SubmitPerson = userId;
            //TODO: add the permission control
            int commandId = _commanddao.Create(cmdItem, secuItems);
            if (commandId > 0)
            {
                Tracking(ActionType.Submit, ResourceType.TradeCommand, commandId, cmdItem);

                var perm = _permissionManager.GetOwnerPermission();
                _permissionManager.GrantPermission(userId, commandId, ResourceType.TradeCommand, perm);

                //对管理员和交易员进行授权
                var dealPerms = new List<PermissionMask> { PermissionMask.View, PermissionMask.Execute};
                _permissionManager.GrantByRole(RoleType.Administrator, commandId, ResourceType.TradeCommand, dealPerms);
                _permissionManager.GrantByRole(RoleType.Dealer, commandId, ResourceType.TradeCommand, dealPerms);
            }

            return commandId;
        }

        public int SubmitClosePosition(Model.Database.TradeCommand cmdItem, ClosePositionItem closePositionItem, List<ClosePositionSecurityItem> closeSecuItems)
        {
            var secuItems = GetSelectCommandSecurities(closePositionItem, closeSecuItems);

            return Submit(cmdItem, secuItems);
        }

        public int SubmitCloseAll(ClosePositionItem closeItem, List<ClosePositionSecurityItem> closeSecuItems)
        {
            var instance = _tradeinstdao.GetCombine(closeItem.InstanceId);
            var tccmdItem = new Model.Database.TradeCommand
            {
                InstanceId = closeItem.InstanceId,
                ECommandType = CommandType.Arbitrage,
                EExecuteType = ExecuteType.ClosePosition,
                EEntrustStatus = EntrustStatus.NoExecuted,
                EDealStatus = DealStatus.NoDeal,
                ModifiedTimes = 1
            };

            if (instance.FuturesDirection == EntrustDirection.SellOpen)
            {
                tccmdItem.EFuturesDirection = EntrustDirection.BuyClose;
            }
            else if (instance.FuturesDirection == EntrustDirection.BuyClose)
            {
                tccmdItem.EFuturesDirection = EntrustDirection.SellOpen;
            }

            if (instance.StockDirection == EntrustDirection.BuySpot)
            {
                tccmdItem.EStockDirection = EntrustDirection.SellSpot;
            }
            else if (instance.StockDirection == EntrustDirection.SellSpot)
            {
                tccmdItem.EStockDirection = EntrustDirection.BuySpot;
            }

            //var tradeinstSecuItems = _tradeinstsecudbo.Get(closeItem.InstanceId);
            var tempStockItems = _tempstockdao.Get(closeItem.TemplateId);

            List<TradeCommandSecurity> cmdSecuItems = new List<TradeCommandSecurity>();

            foreach (var item in closeSecuItems)
            {
                TradeCommandSecurity secuItem = new TradeCommandSecurity
                {
                    SecuCode = item.SecuCode,
                    SecuType = item.SecuType,
                    CommandAmount = item.EntrustAmount,
                    CommandPrice = item.CommandPrice,
                    //EDirection = (EntrustDirection)item.EntrustDirection,
                    EntrustStatus = EntrustStatus.NoExecuted
                };

                if (secuItem.SecuType == Model.SecurityInfo.SecurityType.Stock)
                {
                    secuItem.EDirection = tccmdItem.EStockDirection;
                }
                else if (secuItem.SecuType == Model.SecurityInfo.SecurityType.Futures)
                {
                    secuItem.EDirection = tccmdItem.EFuturesDirection;
                }

                //var availItem = tradeinstSecuItems.Find(p => p.SecuCode.Equals(secuItem.SecuCode));
                //if (availItem != null)
                //{
                //    secuItem.CommandAmount = availItem.AvailableAmount;
                //}

                //var tempStockItem = tempStockItems.Find(p => p.SecuCode.Equals(secuItem.SecuCode));
                //if (tempStockItem != null)
                //{
                //    secuItem.WeightAmount = tempStockItem.Amount;
                //}

                cmdSecuItems.Add(secuItem);
            }


            return Submit(tccmdItem, cmdSecuItems);
        }

        #endregion

        #region get/fetch

        public List<TradingCommandItem> GetTradeCommandUIAll()
        {
            var uiCommands = new List<TradingCommandItem>();
            var validTradeCommands = GetAll();
            
            var tradeSecuItems = GetCommandSecurityItems(validTradeCommands);
            var entrustSecuItems = _queryBLL.GetEntrustSecurityItems(validTradeCommands);

            foreach (var tradeCommand in validTradeCommands)
            {
                var uiCommand = BuildUICommand(tradeCommand);
                CalculateUICommand(ref uiCommand, tradeSecuItems, entrustSecuItems);

                uiCommands.Add(uiCommand);
            }
            return uiCommands;
        }

        /// <summary>
        /// 获取那些有效的，委托完成的，没有完成成交的指令。
        /// </summary>
        /// <returns></returns>
        public List<TradingCommandItem> GetTradeCommand()
        {
            var uiCommands = new List<TradingCommandItem>();
            var validTradeCommands = new List<Model.Database.TradeCommand>();
            var allItems = GetAll();
            foreach (var item in allItems)
            {
                if (item.ECommandStatus == CommandStatus.Effective || item.ECommandStatus == CommandStatus.Entrusted)
                {
                    validTradeCommands.Add(item);
                }
            }

            var tradeSecuItems = GetCommandSecurityItems(validTradeCommands);
            var entrustSecuItems = _queryBLL.GetEntrustSecurityItems(validTradeCommands);

            foreach (var tradeCommand in validTradeCommands)
            {
                var uiCommand = BuildUICommand(tradeCommand);
                CalculateUICommand(ref uiCommand, tradeSecuItems, entrustSecuItems);

                uiCommands.Add(uiCommand);
            }
            return uiCommands;
        }

        public List<Model.Database.TradeCommand> GetAll()
        {
            int userId = LoginManager.Instance.GetUserId();
            var tradeCommands = _tradecommandao.GetAll();

            Tracking(ActionType.Get, ResourceType.TradeCommand, -1, null);

            var validTradeCommands = new List<Model.Database.TradeCommand>();
            foreach (var tradeCommand in tradeCommands)
            {
                if (_permissionManager.HasPermission(userId, tradeCommand.CommandId, ResourceType.TradeCommand, PermissionMask.View)
                    || _permissionManager.HasUserRolePermission(userId, tradeCommand.CommandId, ResourceType.TradeCommand, PermissionMask.View)
                    )
                {
                    validTradeCommands.Add(tradeCommand);
                }
            }

            return validTradeCommands;
        }

        public Model.Database.TradeCommand GetTradeCommandItem(int commandId)
        {
            Tracking(ActionType.Get, ResourceType.TradeCommand, commandId, null);
            return _tradecommandao.Get(commandId);
        }

        public List<TradeCommandSecurity> GetCommandSecurityItems(List<Model.Database.TradeCommand> cmdItems)
        {
            var cmdSecuItems = new List<TradeCommandSecurity>();

            foreach (var cmdItem in cmdItems)
            {
                var secuItems = _tradecmdsecudao.Get(cmdItem.CommandId);
                
                cmdSecuItems.AddRange(secuItems);
            }

            return cmdSecuItems;
        }

        #endregion

        #region update

        public int Update(Model.Database.TradeCommand cmdItem, List<TradeCommandSecurity> modifiedSecuItems, List<TradeCommandSecurity> cancelSecuItems)
        {
            return _commanddao.Update(cmdItem, modifiedSecuItems, cancelSecuItems);
        }

        public int Update(Model.Database.TradeCommand cmdItem)
        {
            return _tradecommandao.Update(cmdItem);
        }

        public int UpdateStatus(Model.Database.TradeCommand cmdItem)
        {
            return _tradecommandao.UpdateStatus(cmdItem);
        }

        #endregion

        #region private

        private List<TradeCommandSecurity> GetSelectCommandSecurities(ClosePositionItem closePositionItem, List<ClosePositionSecurityItem> closeSecuItems)
        {
            List<TradeCommandSecurity> cmdSecuItems = new List<TradeCommandSecurity>();

            var tempStockItems = _tempstockdao.Get(closePositionItem.TemplateId);
            var selectedSecuItems = closeSecuItems.Where(p => p.InstanceId.Equals(closePositionItem.InstanceId)).ToList();
            foreach (var item in selectedSecuItems)
            {
                TradeCommandSecurity secuItem = new TradeCommandSecurity
                {
                    SecuCode = item.SecuCode,
                    SecuType = item.SecuType,
                    CommandAmount = item.EntrustAmount,
                    CommandPrice = item.CommandPrice,
                    EDirection = item.EDirection,
                    EntrustStatus = EntrustStatus.NoExecuted
                };

                //var tempStockItem = tempStockItems.Find(p => p.SecuCode.Equals(secuItem.SecuCode));
                //if (tempStockItem != null)
                //{
                //    secuItem.WeightAmount = tempStockItem.Amount;
                //}

                cmdSecuItems.Add(secuItem);
            }

            return cmdSecuItems;
        }

        private TradingCommandItem BuildUICommand(Model.Database.TradeCommand tradeCommand)
        {
            var uiCommand = new TradingCommandItem
            {
                CommandId = tradeCommand.CommandId,
                InstanceId = tradeCommand.InstanceId,
                CommandNum = tradeCommand.CommandNum,
                ModifiedTimes = tradeCommand.ModifiedTimes,
                ECommandType = tradeCommand.ECommandType,
                EExecuteType = tradeCommand.EExecuteType,
                EStockDirection = tradeCommand.EStockDirection,
                EFuturesDirection = tradeCommand.EFuturesDirection,
                EEntrustStatus = tradeCommand.EEntrustStatus,
                EDealStatus = tradeCommand.EDealStatus,
                SubmitPerson = tradeCommand.SubmitPerson,
                CreatedDate = tradeCommand.CreatedDate,
                ModifiedDate = tradeCommand.ModifiedDate,
                DStartDate = tradeCommand.DStartDate,
                DEndDate = tradeCommand.DEndDate,
                MonitorUnitId = tradeCommand.MonitorUnitId,
                MonitorUnitName = tradeCommand.MonitorUnitName,
                InstanceCode = tradeCommand.InstanceCode,
                PortfolioId = tradeCommand.PortfolioId,
                PortfolioCode = tradeCommand.PortfolioCode,
                PortfolioName = tradeCommand.PortfolioName,
                FundCode = tradeCommand.AccountCode,
                FundName = tradeCommand.AccountName,
            };

            return uiCommand;
        }

        private void CalculateUICommand(ref TradingCommandItem uiCommand, List<TradeCommandSecurity> tradeSecuItems, List<EntrustSecurityItem> entrustSecuItems)
        {
            int commandId = uiCommand.CommandId;
            var cmdSecuItems = tradeSecuItems.Where(p => p.CommandId == commandId).ToList();
            var cmdEntrustSecuItems = entrustSecuItems.Where(p => p.CommandId == commandId).ToList();


            var totalLongCmdAmount = cmdSecuItems.Where(p => p.SecuType == SecurityType.Stock)
                                    .ToList()
                                    .Sum(o => o.CommandAmount);
            var totalLongEntrustAmount = cmdEntrustSecuItems.Where(p => p.SecuType == SecurityType.Stock && p.EntrustStatus == EntrustStatus.Completed)
                                        .ToList()
                                        .Sum(o => o.EntrustAmount);
            var totalLongDealAmount = cmdEntrustSecuItems.Where(p => p.SecuType == SecurityType.Stock && (p.DealStatus == DealStatus.Completed || p.DealStatus == DealStatus.PartDeal))
                                    .ToList()
                                    .Sum(o => o.TotalDealAmount);
            var totalShortCmdAmount = cmdSecuItems.Where(p => p.SecuType == SecurityType.Futures)
                                        .ToList()
                                        .Sum(o => o.CommandAmount);
            var totalShortEntrustAmount = cmdEntrustSecuItems.Where(p => p.SecuType == SecurityType.Futures && p.EntrustStatus == EntrustStatus.Completed)
                                            .ToList()
                                            .Sum(o => o.EntrustAmount);
            var totalShortDealAmount = cmdEntrustSecuItems.Where(p => p.SecuType == SecurityType.Futures && (p.DealStatus == DealStatus.Completed || p.DealStatus == DealStatus.PartDeal))
                                        .ToList()
                                        .Sum(o => o.TotalDealAmount);

            var totalCmdAmount = totalLongCmdAmount + totalShortCmdAmount;
            var eachCopyAmount = totalCmdAmount / uiCommand.CommandNum;
            var totalEntrustAmount = totalLongEntrustAmount + totalShortEntrustAmount;
            var totalDealAmount = totalLongDealAmount + totalShortDealAmount;

            double entrustRatio = GetRatio(totalEntrustAmount, eachCopyAmount);
            double longEntrustRatio = GetRatio(totalLongEntrustAmount, totalLongCmdAmount);
            double longDealRatio = GetRatio(totalLongDealAmount, totalLongCmdAmount);
            double shortEntrustRatio = GetRatio(totalShortEntrustAmount, totalShortCmdAmount);
            double shortDealRatio = GetRatio(totalShortDealAmount, totalShortCmdAmount);

            uiCommand.CommandAmount = totalCmdAmount;
            uiCommand.TargetNum = (int)Math.Ceiling(entrustRatio);
            uiCommand.LongMoreThan = longEntrustRatio;
            uiCommand.BearMoreThan = shortEntrustRatio;
            uiCommand.LongRatio = longDealRatio;
            uiCommand.BearRatio = shortDealRatio;
            uiCommand.EntrustedAmount = totalEntrustAmount;
            uiCommand.DealAmount = totalDealAmount;
        }

        private double GetRatio(int eachOne, int total)
        {
            if (total > 0)
            {
                return (double)(eachOne) / total;
            }

            return 0.0f;
        }
        #endregion

        #region User action tracking

        private int Tracking(ActionType actionType, ResourceType resourceType, int resourceId, Model.Database.TradeCommand cmdItem)
        {
            int userId = LoginManager.Instance.GetUserId();
            return _userActionTrackingBLL.Create(userId, actionType, resourceType, resourceId, JsonUtil.SerializeObject(cmdItem));
        }
        #endregion
    }
}
