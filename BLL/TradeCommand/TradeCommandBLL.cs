using DBAccess;
using log4net;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.TradingCommand
{
    public class TradeCommandBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private TradingInstanceDAO _tradeinstdao = new TradingInstanceDAO();
        private CommandDAO _commanddao = new CommandDAO();

        public TradeCommandBLL()
        { 
        }

        public int Submit(TradingCommandItem cmdItem, List<CommandSecurityItem> secuItems)
        {
            return _commanddao.Create(cmdItem, secuItems);
        }
    }
}
