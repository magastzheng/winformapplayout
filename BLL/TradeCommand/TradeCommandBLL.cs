using Config;
using DBAccess;
using log4net;
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
        public int Submit(TradingCommandItem cmdItem, List<CommandSecurityItem> secuItems)
        {
            cmdItem.SubmitPerson = LoginManager.Instance.LoginUser.Operator;
            return _commanddao.Create(cmdItem, secuItems);
        }

        public int SubmitClosePosition(TradingCommandItem cmdItem, ClosePositionItem closePositionItem, List<ClosePositionSecurityItem> closeSecuItems)
        {
            var secuItems = GetSelectCommandSecurities(closePositionItem, closeSecuItems);

            return Submit(cmdItem, secuItems);
        }

        public int SubmitCloseAll(ClosePositionItem closeItem, List<ClosePositionSecurityItem> closeSecuItems)
        {
            var instance = _tradeinstdao.GetCombine(closeItem.InstanceId);
            TradingCommandItem tccmdItem = new TradingCommandItem 
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

            List<CommandSecurityItem> cmdSecuItems = new List<CommandSecurityItem>();

            foreach (var item in closeSecuItems)
            {
                CommandSecurityItem secuItem = new CommandSecurityItem
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

                var tempStockItem = tempStockItems.Find(p => p.SecuCode.Equals(secuItem.SecuCode));
                if (tempStockItem != null)
                {
                    secuItem.WeightAmount = tempStockItem.Amount;
                }

                cmdSecuItems.Add(secuItem);
            }


            return Submit(tccmdItem, cmdSecuItems);
        }

        #endregion

        public List<TradingCommandItem> GetTradeCommandItems()
        {
            return _tradecommandao.GetAll();
        }

        public TradingCommandItem GetTradeCommandItem(int commandId)
        {
            return _tradecommandao.Get(commandId);
        }

        public List<CommandSecurityItem> GetCommandSecurityItems(List<TradingCommandItem> cmdItems)
        {
            var cmdSecuItems = new List<CommandSecurityItem>();

            foreach (var cmdItem in cmdItems)
            {
                var secuItems = _tradecmdsecudao.Get(cmdItem.CommandId);
                cmdSecuItems.AddRange(secuItems);
            }

            return cmdSecuItems;
        }

        #region private

        private List<CommandSecurityItem> GetSelectCommandSecurities(ClosePositionItem closePositionItem, List<ClosePositionSecurityItem> closeSecuItems)
        {
            List<CommandSecurityItem> cmdSecuItems = new List<CommandSecurityItem>();

            var tempStockItems = _tempstockdao.Get(closePositionItem.TemplateId);
            var selectedSecuItems = closeSecuItems.Where(p => p.InstanceId.Equals(closePositionItem.InstanceId)).ToList();
            foreach (var item in selectedSecuItems)
            {
                CommandSecurityItem secuItem = new CommandSecurityItem
                {
                    SecuCode = item.SecuCode,
                    SecuType = item.SecuType,
                    CommandAmount = item.EntrustAmount,
                    CommandPrice = item.CommandPrice,
                    EDirection = (EntrustDirection)item.EntrustDirection,
                    EntrustStatus = EntrustStatus.NoExecuted
                };

                var tempStockItem = tempStockItems.Find(p => p.SecuCode.Equals(secuItem.SecuCode));
                if (tempStockItem != null)
                {
                    secuItem.WeightAmount = tempStockItem.Amount;
                }

                cmdSecuItems.Add(secuItem);
            }

            return cmdSecuItems;
        }

        #endregion
    }
}
