using BLL.EntrustCommand;
using BLL.SecurityInfo;
using DBAccess;
using DBAccess.TradeCommand;
using log4net;
using Model.Database;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.TradeCommand
{
    public class TradeCommandSecurityBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private TradeCommandSecurityDAO _tradecmdsecudao = new TradeCommandSecurityDAO();

        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();

        public TradeCommandSecurityBLL()
        { 
        
        }

        /// <summary>
        /// Get raw TradeCommand securites. It only includes the command information, such as SecuCode, SecuType, Direction, Amount, Price
        /// It does not contains those entrusted status information.
        /// </summary>
        /// <param name="commandId">The command id.</param>
        /// <returns>A list of TradeCommandSecurity with the same commandId.</returns>
        public List<TradeCommandSecurity> GetTradeCommandSecurities(int commandId)
        {
            return _tradecmdsecudao.Get(commandId);
        }

        public List<CommandSecurityItem> GetCommandSecurityItems(TradeCommandItem cmdItem)
        {
            List<CommandSecurityItem> finalSecuItems = new List<CommandSecurityItem>();
            var cmdSecuItems = GetTradeCommandSecurities(cmdItem.CommandId);
            var entrustSecuItems = _entrustSecurityBLL.GetByCommandId(cmdItem.CommandId);

            int weightAmount = 0;
            foreach (var secuItem in cmdSecuItems)
            {
                weightAmount = secuItem.CommandAmount / cmdItem.CommandNum;

                CommandSecurityItem csItem = new CommandSecurityItem 
                {
                    Selection = true,
                    CommandId = secuItem.CommandId,
                    SecuCode = secuItem.SecuCode,
                    SecuType = secuItem.SecuType,
                    CommandAmount = secuItem.CommandAmount,
                    CommandPrice = secuItem.CommandPrice,
                    EDirection = secuItem.EDirection,
                    CommandCopies = cmdItem.CommandNum,
                    TargetCopies = cmdItem.TargetNum,
                    TargetAmount = cmdItem.TargetNum * weightAmount,
                    FundCode = cmdItem.FundCode,
                    PortfolioName = cmdItem.PortfolioName,
                };
                
                var entrustedItems = entrustSecuItems.Where(p => p.SecuCode.Equals(secuItem.SecuCode)
                    && p.SecuType == secuItem.SecuType
                    && p.EntrustStatus == Model.EnumType.EntrustStatus.Completed)
                    .ToList();
                if (entrustedItems.Count > 0)
                {
                    csItem.EntrustedAmount = entrustedItems.Sum(p => p.EntrustAmount);
                }

                var dealItems = entrustSecuItems.Where(p => p.SecuCode.Equals(secuItem.SecuCode)
                    && p.SecuType == secuItem.SecuType
                    && (p.DealStatus == Model.EnumType.DealStatus.PartDeal || p.DealStatus == Model.EnumType.DealStatus.Completed))
                    .ToList();
                if (dealItems.Count > 0)
                {
                    csItem.DealAmount = dealItems.Sum(p => p.TotalDealAmount);
                }

                //TODO:待补足数量=目标数量-已委托数量
                //csItem.WaitAmount = csItem.TargetAmount - csItem.EntrustedAmount;

                var findItem = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                if (findItem != null)
                {
                    csItem.SecuName = findItem.SecuName;
                }

                finalSecuItems.Add(csItem);
            }

            return finalSecuItems;
        }
    }
}
