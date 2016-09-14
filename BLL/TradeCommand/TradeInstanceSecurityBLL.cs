using DBAccess;
using DBAccess.TradeCommand;
using log4net;
using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.TradeCommand
{
    public class TradeInstanceSecurityBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private TradingInstanceSecurityDAO _tradeinstsecudao = new TradingInstanceSecurityDAO();
        private TradingCommandDAO _tradecmddao = new TradingCommandDAO();

        public TradeInstanceSecurityBLL()
        { 
        }

        #region settle position 

        /// <summary>
        /// 清算：将所有LastDate不是今天的持仓并入持仓，并将BuyToday, SellToday重置为0
        /// </summary>
        /// <returns></returns>
        public int SettlePosition()
        {
            return _tradeinstsecudao.Settle();
        }

        public int UpdateToday(EntrustDirection direction, int commandId, string secuCode, int dealAmount, double dealBalance, double dealFee)
        {
            int ret = -1;
            switch (direction)
            {
                case EntrustDirection.BuySpot:
                case EntrustDirection.BuyClose:
                    {
                        ret = UpdateBuyToday(commandId, secuCode, dealAmount, dealBalance, dealFee);
                    }
                    break;
                case EntrustDirection.SellSpot:
                case EntrustDirection.SellOpen:
                    {
                        ret = UpdateSellToday(commandId, secuCode, dealAmount, dealBalance, dealFee);
                    }
                    break;
                default:
                    break;
            }

            return ret;
        }

        #endregion

        #region get security of instance

        public List<TradingInstanceSecurity> Get(int instanceId)
        {
            return _tradeinstsecudao.Get(instanceId);
        }

        #endregion

        #region update the BuyToday, SellToday data

        private int UpdateBuyToday(int commandId, string secuCode, int buyAmount, double buyBalance, double dealFee)
        {
            var tradeItem = _tradecmddao.Get(commandId);

            var secuItem = new TradingInstanceSecurity
            {
                InstanceId = tradeItem.InstanceId,
                SecuCode = secuCode,
                BuyToday = buyAmount,
                BuyBalance = buyBalance,
                DealFee = dealFee,
            };

            return _tradeinstsecudao.UpdateBuyToday(secuItem);
        }

        private int UpdateSellToday(int commandId, string secuCode, int sellAmount, double sellBalance, double dealFee)
        {
            var tradeItem = _tradecmddao.Get(commandId);

            var secuItem = new TradingInstanceSecurity
            {
                InstanceId = tradeItem.InstanceId,
                SecuCode = secuCode,
                SellToday = sellAmount,
                SellBalance = sellBalance,
                DealFee = dealFee,
            };

            return _tradeinstsecudao.UpdateSellToday(secuItem);
        }

        #endregion
    }
}
