using Model.EnumType;
using Quote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.TradeUtil
{
    public class QuotePriceHelper
    {
        public static double GetPrice(PriceType priceType, MarketData marketData)
        {
            double price = 0f;
            switch (priceType)
            {
                case PriceType.Last:
                case PriceType.Arbitrary:
                case PriceType.Assign:
                case PriceType.Automatic:
                    price = marketData.CurrentPrice;
                    break;
                case PriceType.Sell1:
                    price = marketData.SellPrice1;
                    break;
                case PriceType.Sell2:
                    price = marketData.SellPrice2;
                    break;
                case PriceType.Sell3:
                    price = marketData.SellPrice3;
                    break;
                case PriceType.Sell4:
                    price = marketData.SellPrice4;
                    break;
                case PriceType.Sell5:
                case PriceType.Sell6:
                case PriceType.Sell7:
                case PriceType.Sell8:
                case PriceType.Sell9:
                case PriceType.Sell10:
                    price = marketData.SellPrice5;
                    break;
                case PriceType.Buy1:
                    price = marketData.BuyPrice1;
                    break;
                case PriceType.Buy2:
                    price = marketData.BuyPrice2;
                    break;
                case PriceType.Buy3:
                    price = marketData.BuyPrice3;
                    break;
                case PriceType.Buy4:
                    price = marketData.BuyPrice4;
                    break;
                case PriceType.Buy5:
                case PriceType.Buy6:
                case PriceType.Buy7:
                case PriceType.Buy8:
                case PriceType.Buy9:
                case PriceType.Buy10:
                    price = marketData.BuyPrice5;
                    break;
                default:
                    price = marketData.CurrentPrice;
                    break;
            }

            return price;
        }
    }
}
