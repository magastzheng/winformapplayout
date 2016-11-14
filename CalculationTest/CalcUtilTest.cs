using Calculation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationTest
{
    [TestClass]
    public class CalcUtilTest
    {
        [TestMethod]
        public void Test_CalcStockAmountPerCopyCeiling()
        {
            double totalMoney = 120000.0;
            double[] weights = new double[10] { 12.0, 15.0, 18.0, 5.7, 4.3, 6.4, 3.6, 14.2, 13.8, 7.0 };
            //double[] prices = new double[10] { 21.35, 1.56, 4.5, 3.27, 1.97, 33.24, 18.30, 54.2, 12.31, 7.25 };
            double[] prices = new double[10] { 54.2, 33.24, 21.35, 18.30, 12.31, 7.25, 4.5, 3.27, 1.97, 1.56 };
            for (int i = 0, count = weights.Length; i < count; i++)
            {
                weights[i] = weights[i] / 100.0;
            }
            //weights.e

            var amounts = CalcUtil.CalcStockAmountPerCopyCeiling(totalMoney, weights, prices);

            int[] expected = new int[10] { 300, 600, 1100, 400, 500, 1100, 1000, 5300, 8500, 5400};
            Assert.AreEqual(weights.Length, amounts.Length);
            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                Assert.AreEqual(expected[i], amounts[i]);
            }
        }
        
        [TestMethod]
        public void Test_CalcStockAmountPerCopy_Adjust()
        {
            double totalMoney = 120000.0;
            double[] weights = new double[10] { 12.0, 15.0, 18.0, 5.7, 4.3, 6.4, 3.6, 14.2, 13.8, 7.0 };
            //double[] prices = new double[10] { 21.35, 1.56, 4.5, 3.27, 1.97, 33.24, 18.30, 54.2, 12.31, 7.25 };
            double[] prices = new double[10] { 54.2, 33.24, 21.35, 18.30, 12.31, 7.25, 4.5, 3.27,1.97, 1.56 };
            for (int i = 0, count = weights.Length; i < count; i++)
            {
                weights[i] = weights[i] / 100.0;
            }
            //weights.e

            var amounts = CalcUtil.CalcStockAmountPerCopyAdjust(totalMoney, weights, prices);

            int[] expected = new int[10] { 300, 500, 1000, 400, 400, 1100, 900, 5100, 8400, 5300 };
            Assert.AreEqual(weights.Length, amounts.Length);
            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                Assert.AreEqual(expected[i], amounts[i]);
            }

            var total = 0.0;
            for (int i = 0, count = prices.Length; i < count; i++)
            {
                total += (double)amounts[i] * prices[i];
            }

            Console.WriteLine("Total: " + total.ToString());

            double[] w = new double[amounts.Length];
            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                w[i] = amounts[i] * prices[i] / total;
            }

            Console.WriteLine("Old weight: " + ((double)weights.Sum()).ToString());
            foreach (var a in weights)
            {
                Console.WriteLine(a);
            }

            Console.WriteLine("New weight" + ((double)w.Sum()).ToString());
            foreach (var a in w)
            {
                Console.WriteLine(a);
            }
        }

        [TestMethod]
        public void Test_CalcStockAmountPerCopyFloor()
        {
            double totalMoney = 120000.0;
            double[] weights = new double[10] { 12.0, 15.0, 18.0, 5.7, 4.3, 6.4, 3.6, 14.2, 13.8, 7.0 };
            //double[] prices = new double[10] { 21.35, 1.56, 4.5, 3.27, 1.97, 33.24, 18.30, 54.2, 12.31, 7.25 };
            double[] prices = new double[10] { 54.2, 33.24, 21.35, 18.30, 12.31, 7.25, 4.5, 3.27, 1.97, 1.56 };
            for (int i = 0, count = weights.Length; i < count; i++)
            {
                weights[i] = weights[i] / 100.0;
            }

            var amounts = CalcUtil.CalcStockAmountPerCopyFloor(totalMoney, weights, prices);

            int[] expected = new int[10] { 200, 500, 1000, 300, 400, 1000, 900, 5200, 8400, 5300 };
            Assert.AreEqual(weights.Length, amounts.Length);

            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                Assert.AreEqual(expected[i], amounts[i]);
            }

            foreach (int a in amounts)
            {
                Console.WriteLine(a);
            }

            var total = 0.0;
            for (int i = 0, count = prices.Length; i < count; i++)
            {
                total += (double)amounts[i] * prices[i];
            }

            Console.WriteLine("Total: " + total.ToString());

            double[] w = new double[amounts.Length];
            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                w[i] = amounts[i] * prices[i] / total;
            }

            Console.WriteLine("Old weight: " + ((double)weights.Sum()).ToString());
            foreach (var a in weights)
            {
                Console.WriteLine(a);
            }

            Console.WriteLine("New weight" + ((double)w.Sum()).ToString());
            foreach (var a in w)
            {
                Console.WriteLine(a);
            }
        }

        [TestMethod]
        public void Test_CalcStockAmountPerCopy_Round()
        {
            double totalMoney = 120000.0;
            double[] weights = new double[10] { 12.0, 15.0, 18.0, 5.7, 4.3, 6.4, 3.6, 14.2, 13.8, 7.0 };
            //double[] prices = new double[10] { 21.35, 1.56, 4.5, 3.27, 1.97, 33.24, 18.30, 54.2, 12.31, 7.25 };
            double[] prices = new double[10] { 54.2, 33.24, 21.35, 18.30, 12.31, 7.25, 4.5, 3.27, 1.97, 1.56 };
            for (int i = 0, count = weights.Length; i < count; i++)
            {
                weights[i] = weights[i] / 100.0;
            }
            //weights.e

            var amounts = CalcUtil.CalcStockAmountPerCopyRound(totalMoney, weights, prices);

            int[] expected = new int[10] { 300, 500, 1000, 400, 400, 1100, 1000, 5200, 8400, 5400 };
            Assert.AreEqual(weights.Length, amounts.Length);

            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                Assert.AreEqual(expected[i], amounts[i]);
            }

            foreach (int a in amounts)
            {
                Console.WriteLine(a);
            }

            double adjustMoney = 0.0f;
            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                adjustMoney += amounts[i] * prices[i];
            }

            Console.WriteLine(adjustMoney);

            double[] w = new double[amounts.Length];
            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                w[i] = amounts[i] * prices[i] / adjustMoney;
            }

            Console.WriteLine("Old weight: " + ((double)weights.Sum()).ToString());
            foreach (var a in weights)
            {
                Console.WriteLine(a);
            }

            Console.WriteLine("New weight" + ((double)w.Sum()).ToString());
            foreach (var a in w)
            {
                Console.WriteLine(a);
            }
        }

        [TestMethod]
        public void Test_CalcStockAmountPerCopy_Round_RandomData()
        {
            double totalMoney = 120000.0;
            const int Number = 100;
            const double Times = 100.0;
            double[] rawWeight = new double[Number];
            double[] prices = new double[Number];
            double[] weights = new double[Number];

            Random r = new Random();
            for (int i = 0; i < Number; i++)
            {
                double v = Times * r.NextDouble();
                if (v < 10.0)
                {
                    v = 10.0 + rawWeight.Average();
                }
                rawWeight[i] = v;
            }


            Console.WriteLine(rawWeight);

            for (int i = 0; i < Number; i++)
            {
                double v = Times * r.NextDouble();
                if (v < 10.0)
                {
                    v = 10.0 + prices.Average();
                }
                prices[i] = v / 10.0;
            }

            double totalWeight = rawWeight.Sum();
            for (int i = 0; i < Number; i++)
            {
                weights[i] = rawWeight[i] / totalWeight;
            }

            var amounts = CalcUtil.CalcStockAmountPerCopyRound(totalMoney, weights, prices);

            foreach (int a in amounts)
            {
                Console.WriteLine(a);
            }

            double adjustMoney = 0.0f;
            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                adjustMoney += amounts[i] * prices[i];
            }

            Console.WriteLine(adjustMoney);

            double[] w = new double[amounts.Length];
            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                w[i] = amounts[i] * prices[i] / adjustMoney;
            }

            Console.WriteLine("Old weight: " + ((double)weights.Sum()).ToString());
            foreach (var a in weights)
            {
                Console.WriteLine(a);
            }

            Console.WriteLine("New weight" + ((double)w.Sum()).ToString());
            foreach (var a in w)
            {
                Console.WriteLine(a);
            }
        }

        [TestMethod]
        public void Test_CalcStockWeightByAmount()
        {
            int[] amounts = new int[10]{2000, 1500, 4200, 800, 900, 3100, 4500, 1500, 500, 1000};

            var weights = CalcUtil.CalcStockWeightByAmount(amounts);
            
            double[] expected = new double[10] { 0.1, 0.075, 0.21, 0.04, 0.045, 0.155, 0.225, 0.075, 0.025, 0.05 };
            Assert.AreEqual(amounts.Length, weights.Length);

            for (int i = 0, count = weights.Length; i < count; i++)
            {
                Assert.AreEqual(expected[i], weights[i]);
            }

            var total = weights.Sum();
            Console.WriteLine(total);
            foreach (var a in weights)
            {
                Console.WriteLine(a);
            }
        }
    }
}
