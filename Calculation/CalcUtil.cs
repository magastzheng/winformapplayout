using System;
using System.Linq;

namespace Calculation
{
    public class CalcUtil
    {
        //计算基差
        //基差=期货最新价-指数最新价
        public static double CalcBasis(double futurePrice, double indexPrice)
        {
            if (double.IsNaN(futurePrice) || double.IsNaN(indexPrice))
            {
                throw new ArgumentException("Wrong input futurePrice or indexPrice");
            }

            return futurePrice - indexPrice;
        }

        //计算现货组合市值
        //现货组合市值=股票1数量*股票1最新价+...+股票n数量*股票n最新价
        public static double CalcSpotCombiMarketValue(int[] amounts, double[] prices)
        {
            if (amounts == null || prices == null || amounts.Length != prices.Length)
            {
                throw new ArgumentException("Input argument error!");
            }

            double totalMarketValue = double.NaN;
            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                totalMarketValue += amounts[i] * prices[i];
            }

            return totalMarketValue;
        }

        //计算每份个股数量
        //每份个股数量=每份总金额*个股权重/个股最价格
        //直接向上取整，获取100股整数倍
        public static int[] CalcStockAmountPerCopy(double totalMoney, double[] weights, double[] prices)
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

        //计算每份个股数量
        //每份个股数量=每份总金额*个股权重/个股最价格
        //直接四舍五入
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

        //计算每份个股数量
        //每份个股数量=每份总金额*个股权重/个股最价格
        //每次获取100股倍数之后，使用剩余部分重算余下股票数量
        //价格按从高到低排序后，计算后权重偏差较小
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
        /// <param name="amounts"></param>
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
