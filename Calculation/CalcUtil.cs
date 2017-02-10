using System;
using System.Linq;

namespace Calculation
{
    public class CalcUtil
    {
        /// <summary>
        /// 计算基差
        /// 基差=期货最新价-指数最新价
        /// </summary>
        /// <param name="futurePrice">股指期货指数</param>
        /// <param name="indexPrice">股票指数</param>
        /// <returns>基差</returns>
        public static double CalcBasis(double futurePrice, double indexPrice)
        {
            if (double.IsNaN(futurePrice) || double.IsNaN(indexPrice))
            {
                throw new ArgumentException("Wrong input futurePrice or indexPrice");
            }

            return futurePrice - indexPrice;
        }


        /// <summary>
        /// 计算现货组合市值
        /// 现货组合市值=股票1数量*股票1最新价+...+股票n数量*股票n最新价
        /// </summary>
        /// <param name="amounts">组合中每个个股数量</param>
        /// <param name="prices">组合中每个个股价格</param>
        /// <returns>组合的总市值</returns>
        public static double CalcSpotCombiMarketValue(int[] amounts, double[] prices)
        {
            if (amounts == null || prices == null || amounts.Length != prices.Length)
            {
                throw new ArgumentException("Input argument error!");
            }

            double totalMarketValue = double.NaN;
            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                if (amounts[i] > 0 && !double.IsNaN(prices[i]))
                {
                    totalMarketValue += amounts[i] * prices[i];
                }
            }

            return totalMarketValue;
        }


        /// <summary>
        /// 计算每份个股数量
        /// 每份个股数量=每份总金额*个股权重/个股最价格
        /// 直接向上取整，获取100股整数倍
        /// </summary>
        /// <param name="totalMoney">组合允许的总市值</param>
        /// <param name="weights">组合中个股的权重</param>
        /// <param name="prices">组合中个股的价格</param>
        /// <returns>组合中个股数量</returns>
        public static int[] CalcStockAmountPerCopyCeiling(double totalMoney, double[] weights, double[] prices)
        {
            if (double.IsNaN(totalMoney))
            {
                throw new ArgumentException("The totalMoney is invalid!");
            }
            if (weights == null || prices == null)
            {
                throw new ArgumentException("Invalid input stock weights or stock prices.");
            }
            if (weights.Length != prices.Length)
            {
                throw new ArgumentException("Invalid input stock weights or stock prices length.");
            }

            int[] amounts = new int[weights.Length];
            for (int i = 0, count = weights.Length; i < count; i++)
            {
                double dValue = totalMoney * weights[i] / prices[i];
                int iValue = (int)Math.Ceiling(dValue / 100) * 100;
                amounts[i] = iValue;
            }

            return amounts;
        }


        /// <summary>
        /// 计算每份个股数量，个股数量向下取整
        /// 每份个股数量=每份总金额*个股权重/个股最价格
        /// 直接向下取整，获取100股整数倍
        /// </summary>
        /// <param name="totalMoney">组合允许的总市值</param>
        /// <param name="weights">组合中个股的权重</param>
        /// <param name="prices">组合中个股的价格</param>
        /// <returns>组合中个股数量</returns>
        public static int[] CalcStockAmountPerCopyFloor(double totalMoney, double[] weights, double[] prices)
        {
            if (double.IsNaN(totalMoney))
            {
                throw new ArgumentException("The totalMoney is invalid!");
            }
            if (weights == null || prices == null)
            {
                throw new ArgumentException("Invalid input stock weights or stock prices.");
            }
            if (weights.Length != prices.Length)
            {
                throw new ArgumentException("Invalid input stock weights or stock prices length.");
            }

            int[] amounts = new int[weights.Length];
            for (int i = 0, count = weights.Length; i < count; i++)
            {
                double dValue = totalMoney * weights[i] / prices[i];
                int iValue = (int)Math.Floor(dValue / 100) * 100;
                amounts[i] = iValue;
            }

            return amounts;
        }

        /// <summary>
        /// 计算每份个股数量，个股数量直接四舍五入
        /// 每份个股数量=每份总金额*个股权重/个股最价格
        /// 直接四舍五入
        /// </summary>
        /// <param name="totalMoney">组合允许的总市值</param>
        /// <param name="weights">组合中个股的权重</param>
        /// <param name="prices">组合中个股的价格</param>
        /// <returns>组合中个股数量</returns>
        public static int[] CalcStockAmountPerCopyRound(double totalMoney, double[] weights, double[] prices)
        {
            if (double.IsNaN(totalMoney))
            {
                throw new ArgumentException("The totalMoney is invalid!");
            }
            if (weights == null || prices == null)
            {
                throw new ArgumentException("Invalid input stock weights or stock prices.");
            }
            if (weights.Length != prices.Length)
            {
                throw new ArgumentException("Invalid input stock weights or stock prices length.");
            }

            int[] amounts = new int[weights.Length];
            for (int i = 0, count = weights.Length; i < count; i++)
            {
                double dValue = totalMoney * weights[i] / prices[i];
                int unit = (int)Math.Round(dValue / 100);
                
                //数量最少必须为100股
                if(unit == 0)
                {
                    unit = 1;
                }

                int iValue = unit*100;
                amounts[i] = iValue;
            }

            return amounts;
        }


        /// <summary>
        /// 计算每份个股数量, 每计算一个个股，就会调整剩余的允许总市值。这样的目的是为了减少组合和对冲标的之间的偏差。
        /// 每份个股数量=每份总金额*个股权重/个股最价格
        /// 每次获取100股倍数之后，使用剩余部分重算余下股票数量
        /// 价格按从高到低排序后，计算后权重偏差较小
        /// </summary>
        /// <param name="totalMoney">组合允许的总市值</param>
        /// <param name="weights">组合中个股的权重</param>
        /// <param name="prices">组合中个股的价格</param>
        /// <returns>组合中个股数量</returns>
        public static int[] CalcStockAmountPerCopyAdjust(double totalMoney, double[] weights, double[] prices)
        {
            if (double.IsNaN(totalMoney))
            {
                throw new ArgumentException("The totalMoney is invalid!");
            }
            if (weights == null || prices == null)
            {
                throw new ArgumentException("Invalid input stock weights or stock prices.");
            }
            if (weights.Length != prices.Length)
            {
                throw new ArgumentException("Invalid input stock weights or stock prices length.");
            }

            double restMoney = totalMoney;
            double restWeight = 1.0;
            int[] amounts = new int[weights.Length];
            for (int i = 0, count = weights.Length; i < count && restWeight > 0.000001; i++)
            {
                double dValue = restMoney * weights[i] / (prices[i] * restWeight);
                int iValue = (int)Math.Round(dValue / 100) * 100;
                amounts[i] = iValue;
                restMoney = restMoney - iValue * prices[i];
                restWeight = restWeight - weights[i];
            }

            return amounts;
        }

        /// <summary>
        /// 通过数量计算权重
        /// </summary>
        /// <param name="amounts">组合个股数量</param>
        /// <returns>返回现货个股浮点型权重，不进行百分比处理</returns>
        public static double[] CalcStockWeightByAmount(int[] amounts)
        {
            double[] weights = new double[amounts.Length];
            double total = (double)amounts.Sum();
            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                weights[i] = amounts[i] / total;
            }

            return weights;
        }
    }
}
