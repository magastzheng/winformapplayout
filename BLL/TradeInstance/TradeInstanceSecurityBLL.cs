﻿using BLL.TradeCommand;
using Config;
using DBAccess.TradeCommand;
using DBAccess.TradeInstance;
using log4net;
using Model.EnumType;
using Model.UI;
using System.Collections.Generic;

namespace BLL.TradeInstance
{
    public class TradeInstanceSecurityBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private TradeInstanceSecurityDAO _tradeinstsecudao = new TradeInstanceSecurityDAO();
        //private TradeCommandDAO _tradecmddao = new TradeCommandDAO();
        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private TradeInstanceAdjustmentBLL _tradeInstanceAdjustBLL = new TradeInstanceAdjustmentBLL();

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

        public List<TradeInstanceSecurity> Get(int instanceId)
        {
            return _tradeinstsecudao.Get(instanceId);
        }

        #endregion

        #region update the BuyToday, SellToday data

        private int UpdateBuyToday(int commandId, string secuCode, int buyAmount, double buyBalance, double dealFee)
        {
            var tradeItem = _tradeCommandBLL.GetTradeCommand(commandId);

            var secuItem = new TradeInstanceSecurity
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
            var tradeItem = _tradeCommandBLL.GetTradeCommand(commandId);

            var secuItem = new TradeInstanceSecurity
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

        #region transfer

        public int Transfer(Model.UI.TradeInstance dest, Model.UI.TradeInstance src, List<SourceHoldingItem> transferItems, string notes)
        {
            List<TradeInstanceSecurity> srcNewItems = new List<TradeInstanceSecurity>();
            List<TradeInstanceSecurity> destNewItems = new List<TradeInstanceSecurity>();
            List<TradeInstanceAdjustmentItem> adjustItems = new List<TradeInstanceAdjustmentItem>();

            //获取src中所有的持仓
            var srcItems = Get(src.InstanceId);
            
            //获取dest中所有的持仓
            var destItems = Get(dest.InstanceId);

            //对于src,减去划转部分
            //对于dest,加入划转部分
            foreach (var transferItem in transferItems)
            {
                //对源实例中的证券进行更新
                TradeInstanceSecurity srcOutItem = new TradeInstanceSecurity
                {
                    SecuCode = transferItem.SecuCode,
                    SecuType = transferItem.SecuType,
                    InstanceId = src.InstanceId,
                };

                //TODO: Fix the BuyToday and SellToday
                var srcOldItem = srcItems.Find(p => p.SecuCode.Equals(transferItem.SecuCode) && p.SecuType == transferItem.SecuType);
                if (srcOldItem != null)
                {
                    srcOutItem.PositionAmount = srcOldItem.PositionAmount - transferItem.TransferedAmount;
                    srcOutItem.PositionType = srcOldItem.PositionType;
                    srcOutItem.SellToday = srcOldItem.SellToday;
                    srcOutItem.SellBalance = srcOldItem.SellBalance;
                    srcOutItem.BuyToday = srcOldItem.BuyToday;
                    srcOutItem.BuyBalance = srcOldItem.BuyBalance;
                    srcOutItem.DealFee = srcOldItem.DealFee;
                }
                else
                { 
                    //TODO:
                }
                 
                srcNewItems.Add(srcOutItem);

                //对目标实例中的证券进行更新
                TradeInstanceSecurity destInItem = new TradeInstanceSecurity 
                {
                    SecuCode = transferItem.SecuCode,
                    SecuType = transferItem.SecuType,
                    PositionType = transferItem.PositionType,
                    InstanceId = dest.InstanceId,
                };

                var destOldItem = destItems.Find(p => p.SecuCode.Equals(transferItem.SecuCode) && p.SecuType == transferItem.SecuType);
                if (destOldItem != null)
                {
                    destInItem.PositionAmount = destOldItem.PositionAmount + transferItem.TransferedAmount;
                    destInItem.PositionType = destOldItem.PositionType;
                    destInItem.SellToday = destOldItem.SellToday;
                    destInItem.SellBalance = destOldItem.SellBalance;
                    destInItem.DealFee = destOldItem.DealFee;
                }
                else
                {
                    destInItem.PositionAmount = transferItem.TransferedAmount;
                    //TODO:
                    if (transferItem.SecuType == Model.SecurityInfo.SecurityType.Stock)
                    {
                        destInItem.PositionType = PositionType.SpotLong;
                    }
                    else if (transferItem.SecuType == Model.SecurityInfo.SecurityType.Futures)
                    {
                        destInItem.PositionType = PositionType.FuturesShort;
                    }
                }

                destNewItems.Add(destInItem);

                //对调整做记录
                TradeInstanceAdjustmentItem adjustItem = new TradeInstanceAdjustmentItem 
                {
                    SourceInstanceId = src.InstanceId,
                    SourceFundCode = src.AccountCode,
                    SourcePortfolioCode = src.PortfolioCode,
                    DestinationInstanceId = dest.InstanceId,
                    DestinationFundCode = dest.AccountCode,
                    DestinationPortfolioCode = dest.PortfolioCode,
                    SecuCode = transferItem.SecuCode,
                    SecuType = transferItem.SecuType,
                    PositionType = PositionType.SpotLong,
                    Price = transferItem.TransferedPrice,
                    Amount = transferItem.TransferedAmount,
                    AdjustType = AdjustmentType.Transfer,
                    Operator = LoginManager.Instance.LoginUser.Operator,
                    StockHolderId = string.Empty,
                    SeatNo = string.Empty,
                    Notes = notes,
                };

                adjustItems.Add(adjustItem);
            }

            int result = -1;
            List<int> idList = _tradeInstanceAdjustBLL.CreateTran(adjustItems);
            if (idList.Count > 0 && idList.Count == adjustItems.Count)
            {
                //更新数据库,指向要更新变化部分即可,通过提交事务
                result = _tradeinstsecudao.Transfer(destNewItems, srcNewItems);
            }

            return result;
        }

        #endregion
    }
}
