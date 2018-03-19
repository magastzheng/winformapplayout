using BLL.EntrustCommand;
using BLL.Permission;
using BLL.TradeInstance;
using BLL.UsageTracking;
using Config;
using DBAccess.TradeCommand;
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

namespace BLL.TradeCommand
{
    public class TradeCommandBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private CommandDAO _commanddao = new CommandDAO();
        private TradeCommandDAO _tradecommandao = new TradeCommandDAO();
        private TradeCommandSecurityDAO _tradecmdsecudao = new TradeCommandSecurityDAO();

        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();
        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();

        private UserActionTrackingBLL _userActionTrackingBLL = new UserActionTrackingBLL();
        private PermissionManager _permissionManager = new PermissionManager();
        
        public TradeCommandBLL()
        { 
        }

        #region submit

        /// <summary>
        /// Submit a new the TradeCommand to open the position. It will create a new row in the tradecommand table.
        /// It will check whether the tradeinstance is existed and it will create a new tradeinstance if there is no one.
        /// </summary>
        /// <param name="openItem">An object of OpenPositionItem contains some basic information, such as portfolio,
        /// monitor, instancecode, entrust direction. 
        /// </param>
        /// <param name="secuItems">A list of detailed securities will be entrusted.</param>
        /// <param name="startDate">The trade command start date and time.</param>
        /// <param name="endDate">The trade command end date and time.</param>
        /// <returns>An integer value to indicate whether it is successful or fail. A positive value means success.
        /// Otherwise failure.</returns>
        public int SubmitOpenPosition(OpenPositionItem openItem, List<OpenPositionSecurityItem> secuItems, DateTime startDate, DateTime endDate)
        {
            int instanceId = -1;
            string instanceCode = openItem.InstanceCode;
            var instance = _tradeInstanceBLL.GetInstance(instanceCode);
            if (instance != null && !string.IsNullOrEmpty(instance.InstanceCode) && instance.InstanceCode.Equals(instanceCode))
            {
                instanceId = instance.InstanceId;
                instance.OperationCopies += openItem.Copies;
                _tradeInstanceBLL.Update(instance, secuItems);
            }
            else
            {
                Model.UI.TradeInstance tradeInstance = new Model.UI.TradeInstance
                {
                    InstanceCode = instanceCode,
                    PortfolioId = openItem.PortfolioId,
                    MonitorUnitId = openItem.MonitorId,
                    TemplateId = openItem.TemplateId,
                    StockDirection = EntrustDirection.BuySpot,
                    FuturesContract = openItem.FuturesContract,
                    FuturesDirection = EntrustDirection.SellOpen,
                    OperationCopies = openItem.Copies,
                    StockPriceType = StockPriceType.NoLimit,
                    FuturesPriceType = FuturesPriceType.NoLimit,
                    Status = TradeInstanceStatus.Active,
                };

                tradeInstance.Owner = LoginManager.Instance.GetUserId();
                instanceId = _tradeInstanceBLL.Create(tradeInstance, secuItems);
            }

            int ret = -1;
            if (instanceId > 0)
            {
                //success! Will send generate TradingCommand
                Model.Database.TradeCommand cmdItem = new Model.Database.TradeCommand
                {
                    InstanceId = instanceId,
                    ECommandType = CommandType.Arbitrage,
                    EExecuteType = ExecuteType.OpenPosition,
                    CommandNum = openItem.Copies,
                    EStockDirection = EntrustDirection.BuySpot,
                    EFuturesDirection = EntrustDirection.SellOpen,
                    EEntrustStatus = EntrustStatus.NoExecuted,
                    EDealStatus = DealStatus.NoDeal,
                    ModifiedTimes = 1,
                    DStartDate = startDate,
                    DEndDate = endDate,
                    Notes = openItem.Notes,
                };

                var cmdSecuItems = GetSelectCommandSecurities(openItem.MonitorId, -1, secuItems);

                ret = SubmitInternal(cmdItem, cmdSecuItems);
            }
            else
            {
                //TODO: error message
            }

            return ret;
        }

        /// <summary>
        /// Submit to close the position.
        /// </summary>
        /// <param name="cmdItem"></param>
        /// <param name="selectedSecuItems"></param>
        /// <returns></returns>
        public int SubmitClosePosition(Model.Database.TradeCommand cmdItem, List<ClosePositionSecurityItem> selectedSecuItems)
        {
            int instanceId = cmdItem.InstanceId;
            string instanceCode = cmdItem.InstanceCode;

            var secuItems = GetSelectCommandSecurities(instanceId, selectedSecuItems);
            //TODO: update the TradingInstance
            //string instanceCode = closeItem.InstanceCode;
            var instance = _tradeInstanceBLL.GetInstance(instanceCode);
            if (instance != null && !string.IsNullOrEmpty(instance.InstanceCode) && instance.InstanceCode.Equals(instanceCode))
            {
                _tradeInstanceBLL.Update(instance, selectedSecuItems);
            }

            return SubmitInternal(cmdItem, secuItems);
        }

        #endregion

        #region get/fetch

        /// <summary>
        /// 获取那些有效的，委托完成的，没有完成成交的指令。
        /// </summary>
        /// <returns></returns>
        public List<TradeCommandItem> GetTradeCommandItems()
        {
            var uiCommands = new List<TradeCommandItem>();
            var validTradeCommands = new List<Model.Database.TradeCommand>();
            var allItems = GetAll();
            foreach (var item in allItems)
            {
                if (IsValidCommand(item) && IsValidTime(item))
                {
                    validTradeCommands.Add(item);
                }
            }

            var commandIds = validTradeCommands.Select(p => p.CommandId).Distinct().ToList();
            var cmdSecuItems = GetCommandSecurityItems(commandIds);
            var entrustSecuItems = _entrustSecurityBLL.GetEntrustSecurities(commandIds);

            foreach (var tradeCommand in validTradeCommands)
            {
                var uiCommand = BuildUICommand(tradeCommand);
                CalculateUICommand(ref uiCommand, cmdSecuItems, entrustSecuItems);

                uiCommands.Add(uiCommand);
            }
            return uiCommands;
        }

        public List<Model.Database.TradeCommand> GetInvalidTradeCommands()
        {
            var invalidTradeCommands = new List<Model.Database.TradeCommand>();
            var tradeCommands = _tradecommandao.GetAll();
            if (tradeCommands != null && tradeCommands.Count > 0)
            {
                foreach (var tradeCommand in tradeCommands)
                {
                    if (!IsValidCommand(tradeCommand) || IsExpireTime(tradeCommand))
                    {
                        invalidTradeCommands.Add(tradeCommand);
                    }
                }
            }

            return invalidTradeCommands;
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

        public Model.Database.TradeCommand GetTradeCommand(int commandId)
        {
            Tracking(ActionType.Get, ResourceType.TradeCommand, commandId, null);
            return GetTradeCommandInternal(commandId);
        }

        public Model.UI.TradeInstance GetTradeInstance(int commandId)
        {
            var tradeCommand = GetTradeCommandInternal(commandId);
            return _tradeInstanceBLL.GetInstance(tradeCommand.InstanceId);
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

        #region Delete

        public int Delete(int commandId)
        {
            return _tradecommandao.Delete(commandId);
        }

        #endregion

        #region private

        private int SubmitInternal(Model.Database.TradeCommand cmdItem, List<TradeCommandSecurity> secuItems)
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
            }

            return commandId;
        }

        private Model.Database.TradeCommand GetTradeCommandInternal(int commandId)
        {
            return _tradecommandao.Get(commandId);
        }

        private bool IsValidCommand(Model.Database.TradeCommand tradeCommand)
        {
            return tradeCommand.ECommandStatus == CommandStatus.Effective
                || tradeCommand.ECommandStatus == CommandStatus.Entrusted
                || tradeCommand.ECommandStatus == CommandStatus.Modified;
        }

        private bool IsValidTime(Model.Database.TradeCommand tradeCommand)
        {
            return tradeCommand.DEndDate > tradeCommand.DStartDate && tradeCommand.DEndDate > DateUtil.OpenDate && tradeCommand.DStartDate < DateUtil.CloseDate;
        }

        private bool IsExpireTime(Model.Database.TradeCommand tradeCommand)
        {
            return tradeCommand.DEndDate < DateUtil.OpenDate;
        }

        private List<TradeCommandSecurity> GetCommandSecurityItems(List<int> commandIds)
        {
            var cmdSecuItems = new List<TradeCommandSecurity>();

            foreach (var commandId in commandIds)
            {
                var secuItems = _tradecmdsecudao.Get(commandId);

                cmdSecuItems.AddRange(secuItems);
            }

            return cmdSecuItems;
        }

        private List<TradeCommandSecurity> GetSelectCommandSecurities(int monitorId, int commandId, List<OpenPositionSecurityItem> selectedSecuItems)
        {
            List<TradeCommandSecurity> cmdSecuItems = new List<TradeCommandSecurity>();
            foreach (var item in selectedSecuItems)
            {
                if (item.Selection && item.MonitorId == monitorId)
                {
                    TradeCommandSecurity secuItem = new TradeCommandSecurity 
                    {
                        CommandId = commandId,
                        SecuCode = item.SecuCode,
                        SecuType = item.SecuType,
                        CommandAmount = item.EntrustAmount,
                        CommandPrice = item.CommandPrice,
                        CurrentPrice = item.LastPrice,
                        EntrustStatus = EntrustStatus.NoExecuted
                    };

                    if(secuItem.SecuType == SecurityType.Stock)
                    {
                        secuItem.EDirection = EntrustDirection.BuySpot;
                    }
                    else
                    {
                        secuItem.EDirection = EntrustDirection.SellOpen;
                    }
                       
                    cmdSecuItems.Add(secuItem);
                }
            }

            return cmdSecuItems;
        }

        private List<TradeCommandSecurity> GetSelectCommandSecurities(int instanceId, List<ClosePositionSecurityItem> closeSecuItems)
        {
            List<TradeCommandSecurity> cmdSecuItems = new List<TradeCommandSecurity>();

            //var tempStockItems = _tempstockdao.Get(closePositionItem.TemplateId);
            var selectedSecuItems = closeSecuItems.Where(p => p.InstanceId.Equals(instanceId)).ToList();
            foreach (var item in selectedSecuItems)
            {
                TradeCommandSecurity secuItem = new TradeCommandSecurity
                {
                    SecuCode = item.SecuCode,
                    SecuType = item.SecuType,
                    CommandAmount = item.EntrustAmount,
                    CommandPrice = item.CommandPrice,
                    CurrentPrice = item.LastPrice,
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

        private TradeCommandItem BuildUICommand(Model.Database.TradeCommand tradeCommand)
        {
            var uiCommand = new TradeCommandItem
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

        private void CalculateUICommand(ref TradeCommandItem uiCommand, List<TradeCommandSecurity> rawCmdSecuItems, List<EntrustSecurity> entrustSecuItems)
        {
            int commandId = uiCommand.CommandId;
            var cmdSecuItems = rawCmdSecuItems.Where(p => p.CommandId == commandId).ToList();
            var cmdEntrustSecuItems = entrustSecuItems.Where(p => p.CommandId == commandId).ToList();

            var calcItems = new List<TradeCommandCalcItem>();
            foreach (var cmdSecuItem in cmdSecuItems)
            {
                int amount = cmdSecuItem.CommandAmount;
                double price = cmdSecuItem.CurrentPrice > 0 ? cmdSecuItem.CurrentPrice : 1;
                //基于下达指令时价格算出的市值
                double mktCapOrigin = amount * price;
                //基于委托时价格算出的市值
                double mktCapEntrusted = mktCapOrigin;
                //实际委托时的市值
                double entrustedMktCap = 0;
                //已成交的市值
                double dealMktCap = 0;
                var targetCmdEntrustedSecuItems = cmdEntrustSecuItems.Where(p => p.CommandId == cmdSecuItem.CommandId
                    && p.SecuCode == cmdSecuItem.SecuCode
                    && p.SecuType == cmdSecuItem.SecuType);
                if (targetCmdEntrustedSecuItems != null && targetCmdEntrustedSecuItems.Count() > 0)
                {
                    double tempMktCap = targetCmdEntrustedSecuItems.Sum(p => p.EntrustAmount * p.EntrustPrice);
                    int entrustedAmount = targetCmdEntrustedSecuItems.Sum(p => p.EntrustAmount);
                    
                    //加总委托部分的市值，使用委托处价格算出
                    if (amount > entrustedAmount)
                    {
                        mktCapEntrusted = tempMktCap + (amount - entrustedAmount) * price;
                    }
                    else
                    {
                        mktCapEntrusted = tempMktCap;
                    }
                }

                var cmdCompletedEntrustedSecuItems = cmdEntrustSecuItems.Where(p => p.CommandId == cmdSecuItem.CommandId
                    && p.SecuCode == cmdSecuItem.SecuCode
                    && p.SecuType == cmdSecuItem.SecuType
                    && p.EntrustStatus == EntrustStatus.Completed);
                if (cmdCompletedEntrustedSecuItems != null && cmdCompletedEntrustedSecuItems.Count() > 0)
                {
                    entrustedMktCap = cmdCompletedEntrustedSecuItems.Sum(p => p.EntrustAmount * p.EntrustPrice);
                }

                var cmdDealSecuItems = cmdEntrustSecuItems.Where(p => p.CommandId == cmdSecuItem.CommandId
                    && p.SecuCode == cmdSecuItem.SecuCode
                    && p.SecuType == cmdSecuItem.SecuType
                    && (p.DealStatus == DealStatus.Completed || p.DealStatus == DealStatus.PartDeal));
                if (cmdDealSecuItems != null && cmdDealSecuItems.Count() > 0)
                {
                    dealMktCap = cmdDealSecuItems.Sum(p => p.EntrustAmount * p.EntrustPrice);
                }

                var calcItem = new TradeCommandCalcItem 
                {
                    CommandId = cmdSecuItem.CommandId,
                    SecuCode = cmdSecuItem.SecuCode,
                    SecuType = cmdSecuItem.SecuType,
                    MktCapOrigin = mktCapOrigin,
                    MktCapEntrusted = mktCapEntrusted,
                    EntrustedMktCap = entrustedMktCap,
                    DealMktCap = dealMktCap,
                };

                calcItems.Add(calcItem);
            }

            //计算数量
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

            //计算市值
            var totalLongCmdMktCap = calcItems.Where(p => p.SecuType == SecurityType.Stock)
                                    .ToList()
                                    .Sum(o => o.MktCapEntrusted);
            var totalLongEntrustMktCap = calcItems.Where(p => p.SecuType == SecurityType.Stock)
                                        .ToList()
                                        .Sum(o => o.EntrustedMktCap);
            var totalLongDealMktCap = calcItems.Where(p => p.SecuType == SecurityType.Stock)
                                    .ToList()
                                    .Sum(o => o.DealMktCap);
            var totalShortCmdMktCap = calcItems.Where(p => p.SecuType == SecurityType.Futures)
                                        .ToList()
                                        .Sum(o => o.MktCapEntrusted);
            var totalShortEntrustMktCap = calcItems.Where(p => p.SecuType == SecurityType.Futures)
                                            .ToList()
                                            .Sum(o => o.EntrustedMktCap);
            var totalShortDealMktCap = calcItems.Where(p => p.SecuType == SecurityType.Futures)
                                        .ToList()
                                        .Sum(o => o.DealMktCap);

            var totalCmdAmount = totalLongCmdAmount + totalShortCmdAmount;
            var eachCopyAmount = totalCmdAmount / uiCommand.CommandNum;
            var totalEntrustAmount = totalLongEntrustAmount + totalShortEntrustAmount;
            var totalDealAmount = totalLongDealAmount + totalShortDealAmount;

            //基于数量计算出的比例
            double entrustRatio = GetRatio(totalEntrustAmount, eachCopyAmount);

            //基于市值计算出的比例
            double longEntrustRatio = GetRatio(totalLongEntrustMktCap, totalLongCmdMktCap);
            double longDealRatio = GetRatio(totalLongDealMktCap, totalLongCmdMktCap);
            double shortEntrustRatio = GetRatio(totalShortEntrustMktCap, totalShortCmdMktCap);
            double shortDealRatio = GetRatio(totalShortDealMktCap, totalShortCmdMktCap);

            uiCommand.CommandAmount = totalCmdAmount;
            uiCommand.TargetNum = (int)Math.Ceiling(entrustRatio);
            uiCommand.LongMoreThan = longEntrustRatio;
            uiCommand.BearMoreThan = shortEntrustRatio;
            uiCommand.LongRatio = longDealRatio;
            uiCommand.BearRatio = shortDealRatio;
            uiCommand.EntrustAmount = totalEntrustAmount;
            uiCommand.DealAmount = totalDealAmount;
        }

        private double GetRatio(double eachOne, double total)
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
            int num = 1;
            if (cmdItem == null)
            {
                num = -1;
            }

            return _userActionTrackingBLL.Create(userId, actionType, resourceType, resourceId, num, ActionStatus.Normal, JsonUtil.SerializeObject(cmdItem));
        }
        #endregion

        //用于计算委托比例和成交比例
        class TradeCommandCalcItem
        {
            public int CommandId { get; set; }
            public string SecuCode { get; set; }
            public SecurityType SecuType { get; set; }

            //指令下达时价格处市值
            public double MktCapOrigin { get; set; }

            //委托时价格处市值
            public double MktCapEntrusted { get; set; }

            //已委托的市值
            public double EntrustedMktCap { get; set; }

            //已成交的市值
            public double DealMktCap { get; set; }
        }
    }
}
