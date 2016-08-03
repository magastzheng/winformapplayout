using BLL.SecurityInfo;
using DBAccess;
using log4net;
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

        private TradingCommandSecurityDAO _tradecmdsecudao = new TradingCommandSecurityDAO();
        private EntrustSecurityDAO _entrustsecudao = new EntrustSecurityDAO();

        public TradeCommandSecurityBLL()
        { 
        
        }

        public List<CommandSecurityItem> GetCommandSecurityItems(TradingCommandItem cmdItem)
        {
            List<CommandSecurityItem> finalSecuItems = new List<CommandSecurityItem>();
            var cmdSecuItems = _tradecmdsecudao.Get(cmdItem.CommandId);
            var entrustSecuItems = _entrustsecudao.GetByCommandId(cmdItem.CommandId);

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

                var findItem = SecurityInfoManager.Instance.Get(secuItem.SecuCode);
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
