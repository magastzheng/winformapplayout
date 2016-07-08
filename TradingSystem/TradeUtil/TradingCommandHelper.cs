using DBAccess;
using Model.UI;
using System.Collections.Generic;

namespace TradingSystem.TradeUtil
{
    public class TradingCommandHelper
    {
        private TradingCommandDAO _tradecmddao = new TradingCommandDAO();
        private TradingCommandSecurityDAO _tradecmdsecudao = new TradingCommandSecurityDAO();

        public TradingCommandHelper()
        { 
            
        }

        public int Submit(TradingCommandItem cmdItem, List<CommandSecurityItem> cmdSecuItems)
        {
            int total = 0;

            int commandId = _tradecmddao.Create(cmdItem);
            if (commandId > 0)
            {
                cmdSecuItems.ForEach(p =>
                {
                    p.CommandId = commandId;
                    int ret = _tradecmdsecudao.Create(p);
                    total += ret;
                });
            }

            return total;
        }
    }
}
