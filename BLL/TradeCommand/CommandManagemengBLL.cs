using BLL.EntrustCommand;
using BLL.Frontend;
using BLL.Manager;
using BLL.SecurityInfo;
using log4net;
using Model.Database;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace BLL.TradeCommand
{
    public class CommandManagemengBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private TradeCommandSecurityBLL _tradeCommandSecurityBLL = new TradeCommandSecurityBLL();
        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();

        public CommandManagemengBLL()
        { 
        }

        #region CommandManagementItem

        public List<CommandManagementItem> GetCommandItems()
        {
            var commandItems = new List<CommandManagementItem>();
            var tradeCommandItems = _tradeCommandBLL.GetAll();
            foreach (var item in tradeCommandItems)
            {
                CommandManagementItem cmdItem = new CommandManagementItem
                {
                    DDate = item.CreatedDate,
                    CommandId = item.CommandId,
                    ECommandStatus = item.ECommandStatus,
                    ArbitrageCopies = item.CommandNum,
                    DStartDate = item.DStartDate,
                    DEndDate = item.DEndDate,
                    EExecutype = item.EExecuteType,
                    EDealStatus = item.EDealStatus,
                    EEntrustStatus = item.EEntrustStatus,
                    CommandModifiedTimes = item.ModifiedTimes,
                    //DDispatchDate = item.d
                    InstanceId = item.InstanceId,
                    InstanceCode = item.InstanceCode,
                    PortfolioId = item.PortfolioId,
                    PortfolioCode = item.PortfolioCode,
                    PortfolioName = item.PortfolioName,
                    TemplateId = item.TemplateId,
                    TemplateName = item.TemplateName,
                    BearContract = item.BearContract,
                    FundCode = item.AccountCode,
                    FundName = item.AccountName,
                    Notes = item.Notes,
                    ModifiedCause = item.ModifiedCause,
                    CancelCause = item.CancelCause,
                };

                commandItems.Add(cmdItem);
            }

            return commandItems;
        }

        public int Update(CommandManagementItem cmdMngItem)
        {
            DateTime startDate = DateUtil.GetStartDate(cmdMngItem.DStartDate);
            DateTime endDate = DateUtil.GetEndDate(cmdMngItem.DEndDate, startDate);

            Model.Database.TradeCommand cmdItem = new Model.Database.TradeCommand
            {
                CommandId = cmdMngItem.CommandId,
                ECommandStatus = Model.EnumType.CommandStatus.Canceled,
                ModifiedDate = DateTime.Now,
                DStartDate = startDate,
                DEndDate = endDate,
                Notes = cmdMngItem.Notes??string.Empty,
                ModifiedCause = cmdMngItem.ModifiedCause,
                CancelCause = cmdMngItem.CancelCause,
            };

            return _tradeCommandBLL.Update(cmdItem);
        }

        #endregion

        #region Command Management Security Item

        public List<CommandManagementSecurityItem> GetSecurityItems(CommandManagementItem cmdMngItem)
        {
            var cmdMngSecuItems = new List<CommandManagementSecurityItem>();
            var cmdSecuItems = _tradeCommandSecurityBLL.GetTradeCommandSecurities(cmdMngItem.CommandId);
            var entrustSecuItems = _entrustSecurityBLL.GetByCommandId(cmdMngItem.CommandId);
            foreach (var secuItem in cmdSecuItems)
            {
                var cmdMngSecuItem = new CommandManagementSecurityItem 
                {
                    SecuCode = secuItem.SecuCode,
                    SecuType = secuItem.SecuType,
                    CommandAmount = secuItem.CommandAmount,
                    EDirection = secuItem.EDirection,
                    FundName = cmdMngItem.FundName,
                    PortfolioName = cmdMngItem.PortfolioName,
                    CommandId = cmdMngItem.CommandId,
                };

                //计算累积委托数量和金额
                var entrustedItems = entrustSecuItems.Where(p => p.SecuCode.Equals(secuItem.SecuCode)
                   && p.SecuType == secuItem.SecuType
                   && p.EntrustStatus == Model.EnumType.EntrustStatus.Completed)
                   .ToList();
                if (entrustedItems.Count > 0)
                {
                    cmdMngSecuItem.TotalEntrustAmount = entrustedItems.Sum(p => p.EntrustAmount);
                    cmdMngSecuItem.TotalEntrustMoney = entrustedItems.Sum(p => p.EntrustAmount * p.EntrustPrice);
                }

                //计算累积成交数量和金额
                var dealItems = entrustSecuItems.Where(p => p.SecuCode.Equals(secuItem.SecuCode)
                    && p.SecuType == secuItem.SecuType
                    && (p.DealStatus == Model.EnumType.DealStatus.PartDeal || p.DealStatus == Model.EnumType.DealStatus.Completed))
                    .ToList();
                if (dealItems.Count > 0)
                {
                    cmdMngSecuItem.TotalDealAmount = dealItems.Sum(p => p.TotalDealAmount);
                    cmdMngSecuItem.TotalDealMoney = dealItems.Sum(p => p.TotalDealBalance);
                }

                cmdMngSecuItem.UnentrustAmount = cmdMngSecuItem.CommandAmount - cmdMngSecuItem.TotalEntrustAmount;
                cmdMngSecuItem.UndealAmount = cmdMngSecuItem.TotalEntrustAmount - cmdMngSecuItem.TotalDealAmount;

                if (cmdMngSecuItem.UnentrustAmount == 0)
                {
                    cmdMngSecuItem.EEntrustStatus = Model.EnumType.EntrustStatus.Completed;
                }
                else if(cmdMngSecuItem.TotalEntrustAmount > 0)
                {
                    cmdMngSecuItem.EEntrustStatus = Model.EnumType.EntrustStatus.PartExecuted;
                }
                else
                {
                    cmdMngSecuItem.EEntrustStatus = Model.EnumType.EntrustStatus.NoExecuted;
                }

                if (cmdMngSecuItem.UndealAmount == 0 && cmdMngSecuItem.EEntrustStatus == Model.EnumType.EntrustStatus.Completed)
                {
                    cmdMngSecuItem.EDealStatus = Model.EnumType.DealStatus.Completed;
                }
                else if (cmdMngSecuItem.TotalDealAmount > 0)
                {
                    cmdMngSecuItem.EDealStatus = Model.EnumType.DealStatus.PartDeal;
                }
                else
                {
                    cmdMngSecuItem.EDealStatus = Model.EnumType.DealStatus.NoDeal;
                }

                var findItem = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                if (findItem != null)
                {
                    cmdMngSecuItem.SecuName = findItem.SecuName;
                }

                cmdMngSecuItems.Add(cmdMngSecuItem);
            }

            return cmdMngSecuItems;
        }

        public List<CommandManagementEntrustItem> GetEntrustItems(CommandManagementItem cmdMngItem)
        {
            var entrustItems = new List<CommandManagementEntrustItem>();
            var entrustSecuItems = _entrustSecurityBLL.GetByCommandId(cmdMngItem.CommandId);
            if (entrustSecuItems != null && entrustSecuItems.Count > 0)
            {
                var validItems = entrustSecuItems.Where(p => p.EntrustAmount > 0).ToList();
                if (validItems.Count > 0)
                {
                    foreach (var validItem in validItems)
                    {
                        var entrustItem = new CommandManagementEntrustItem 
                        {
                            SecuCode = validItem.SecuCode,
                            SecuType = validItem.SecuType,
                            EntrustNo = validItem.EntrustNo,
                            EDirection = validItem.EntrustDirection,
                            EntrustPrice = validItem.EntrustPrice,
                            EEntrustPriceType = validItem.EntrustPriceType,
                            EEntrustStatus = validItem.EntrustStatus,
                            TodayDealAmount = validItem.TotalDealAmount,
                            TodayDealMoney = validItem.TotalDealBalance,
                            DEntrustDate = validItem.CreatedDate,
                            FundName = cmdMngItem.FundName,
                            PortfolioName = cmdMngItem.PortfolioName,
                            CommandId = cmdMngItem.CommandId,
                        };

                        if (entrustItem.EntrustAmount > 0)
                        {
                            entrustItem.DealPercent = entrustItem.TodayDealAmount / entrustItem.EntrustAmount;
                        }

                        var secuItem = SecurityInfoManager.Instance.Get(entrustItem.SecuCode, entrustItem.SecuType);
                        if (secuItem != null)
                        {
                            entrustItem.SecuName = secuItem.SecuName;
                        }

                        entrustItems.Add(entrustItem);
                    }
                }
            }
            return entrustItems;
        }

        public List<CommandManagementDealItem> GetDealItems(CommandManagementItem cmdMngItem)
        {
            var dealItems = new List<CommandManagementDealItem>();
            var entrustSecuItems = _entrustSecurityBLL.GetByCommandId(cmdMngItem.CommandId);
            if (entrustSecuItems != null && entrustSecuItems.Count > 0)
            {
                var validItems = entrustSecuItems.Where(p => p.TotalDealAmount > 0).ToList();
                if (validItems.Count > 0)
                {
                    foreach (var validItem in validItems)
                    {
                        var dealItem = new CommandManagementDealItem 
                        {
                            SecuCode = validItem.SecuCode,
                            SecuType = validItem.SecuType,
                            EDirection = validItem.EntrustDirection,
                            DealAmount = validItem.TotalDealAmount,
                            DealMoney = validItem.TotalDealBalance,
                            //PriceType = validItem.EntrustPriceType,

                            FundName = cmdMngItem.FundName,
                            PortfolioName = cmdMngItem.PortfolioName,
                            CommandId = cmdMngItem.CommandId,
                        };

                        if (dealItem.DealAmount > 0)
                        {
                            dealItem.DealPrice = dealItem.DealMoney / dealItem.DealAmount;
                        }

                        var secuItem = SecurityInfoManager.Instance.Get(dealItem.SecuCode, dealItem.SecuType);
                        if (secuItem != null)
                        {
                            dealItem.SecuName = secuItem.SecuName;
                        }

                        dealItems.Add(dealItem);
                    }
                }
            }

            return dealItems;
        }

        #endregion
    }
}
