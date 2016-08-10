using Config;
using DBAccess;
using log4net;
using Model.Database;
using Model.EnumType;
using Model.UI;
using System.Collections.Generic;
using System.Linq;

namespace BLL.TradeCommand
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

        public TradeCommandBLL()
        { 
        }

        #region submit
        public int Submit(Model.Database.TradeCommand cmdItem, List<TradeCommandSecurity> secuItems)
        {
            cmdItem.SubmitPerson = LoginManager.Instance.LoginUser.Operator;
            return _commanddao.Create(cmdItem, secuItems);
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

        public List<TradingCommandItem> GetTradeCommandItems()
        {
            var uiCommands = new List<TradingCommandItem>();
            var tradeCommands = _tradecommandao.GetAll();
            foreach (var tradeCommand in tradeCommands)
            {
                var uiCommand = BuildUICommand(tradeCommand);
                uiCommands.Add(uiCommand);
            }
            return uiCommands;
        }

        public TradingCommandItem GetTradeCommandItem(int commandId)
        {
            var tradeCommand = _tradecommandao.Get(commandId);
            return BuildUICommand(tradeCommand);
        }

        public List<TradeCommandSecurity> GetCommandSecurityItems(List<TradingCommandItem> cmdItems)
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
        #endregion
    }
}
