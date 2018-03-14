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

            var amounts = CalcUtil.CalcStockAmountPerCopyAdjust(totalMoney, weights, prices, 0);

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

            Console.WriteLine("Difference: " + (total - totalMoney).ToString());

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

            Console.WriteLine("Difference: " + (total - totalMoney).ToString());

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

            var amounts = CalcUtil.CalcStockAmountPerCopyRound(totalMoney, weights, prices, 0);

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
            
            Console.WriteLine("Difference: " + (adjustMoney - totalMoney).ToString());

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

            var amounts = CalcUtil.CalcStockAmountPerCopyRound(totalMoney, weights, prices, 0);

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

            Console.WriteLine("Difference: " + (adjustMoney - totalMoney).ToString());

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
        public void Test_CalcStockAmount_ChinaRound_RandomData()
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

            var amounts = CalcUtil.CalcStockAmountByChinaRound(totalMoney, weights, prices, 0);

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
            Console.WriteLine("Difference: " + (adjustMoney - totalMoney).ToString());

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
        public void Test_CalcStockAmount_Round_ChinaRound_RandomData()
        {
            double totalMoney = 1200000.0;
            const int Number = 250;
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

            var amounts = CalcUtil.CalcStockAmountPerCopyRound(totalMoney, weights, prices, 0);

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
            Console.WriteLine("Bank Difference: " + (adjustMoney - totalMoney).ToString());

            amounts = CalcUtil.CalcStockAmountByChinaRound(totalMoney, weights, prices, 0);

            foreach (int a in amounts)
            {
                Console.WriteLine(a);
            }

            adjustMoney = 0.0f;
            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                adjustMoney += amounts[i] * prices[i];
            }

            Console.WriteLine(adjustMoney);
            Console.WriteLine("China Difference: " + (adjustMoney - totalMoney).ToString());

            amounts = CalcUtil.CalcStockAmountPerCopyAdjust(totalMoney, weights, prices, 0);

            foreach (int a in amounts)
            {
                Console.WriteLine(a);
            }

            adjustMoney = 0.0f;
            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                adjustMoney += amounts[i] * prices[i];
            }

            Console.WriteLine(adjustMoney);
            Console.WriteLine("Adjust Difference: " + (adjustMoney - totalMoney).ToString());

            //double[] w = new double[amounts.Length];
            //for (int i = 0, count = amounts.Length; i < count; i++)
            //{
            //    w[i] = amounts[i] * prices[i] / adjustMoney;
            //}

            //Console.WriteLine("Old weight: " + ((double)weights.Sum()).ToString());
            //foreach (var a in weights)
            //{
            //    Console.WriteLine(a);
            //}

            //Console.WriteLine("New weight" + ((double)w.Sum()).ToString());
            //foreach (var a in w)
            //{
            //    Console.WriteLine(a);
            //}
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

        [TestMethod]
        public void Test_CalcStockAmount_Round_ChinaRound_RealData()
        {
            double totalMoney = 1242648.72;
            const int Number = 240;
            //const double Times = 100.0;
            double[] rawWeight = new double[Number];
            double[] prices = new double[Number] { 59.41, 18.58, 10.04, 8.44, 60.45, 8.41, 10.32, 73.31, 5.66, 18.44, 16.34, 73.5, 8.88, 7.21, 4.17, 24.09, 7.15, 63.6, 5.02, 10.15, 7.97, 4.3, 179, 4.6, 9.75, 6.13, 4.11, 9.12, 8.69, 5.98, 5.18, 6.67, 25.5, 20.42, 9.63, 11.84, 28.66, 7.91, 5.61, 10.65, 16.8, 26.25, 6.85, 15.94, 10.33, 5.46, 22.2, 14.64, 43.21, 15.59, 20.15, 16.53, 11.98, 5.21, 11.34, 13.02, 9.17, 12.89, 8.28, 38.35, 27.63, 8.86, 6, 6.89, 12.09, 17.45, 45.41, 113.22, 10.67, 22.82, 52.04, 18.4, 6.52, 20.84, 18.15, 23.88, 43.15, 9.14, 8.98, 23.43, 12.88, 14.27, 5.63, 17.08, 26.56, 8.04, 35.07, 4, 12.66, 18.75, 14.17, 17.1, 18.83, 12.82, 49.11, 14.66, 7.81, 28.33, 10.46, 10.99, 10.99, 9.13, 6.65, 8.1, 18.03, 5.11, 9.91, 7.1, 15.97, 6.43, 8.27, 6.21, 22.81, 23.12, 8.23, 36.71, 22.32, 6.69, 23.06, 12.62, 16.99, 8.93, 17.56, 17.13, 22.05, 29.74, 5.85, 34.74, 33.85, 21.58, 79.49, 5.17, 9.13, 20.9, 7.6, 32.63, 6.74, 17.44, 12.5, 6.77, 9.87, 43.2, 18.5, 54.69, 9.79, 6.45, 8.1, 27.61, 724.01, 29.28, 16.58, 9.85, 49.3, 4.15, 45, 8.43, 14.38, 10.6, 4.91, 42.64, 10.83, 9.51, 14.13, 15.69, 11.8, 5.95, 9.31, 5.6, 4.27, 20.39, 25.79, 3.63, 24.53, 39.2, 6.93, 10.29, 7.26, 9.79, 6.41, 3.93, 15.13, 4.09, 12.59, 22.53, 21.32, 8.44, 24.78, 4.73, 45.53, 16.73, 7.84, 13.1, 10.1, 10.16, 8.67, 3.98, 6.06, 8.48, 7.14, 9, 36.71, 18.62, 23.49, 7.37, 10.34, 8.36, 24.99, 22.86, 6.43, 3.43, 9.44, 26.82, 10.39, 11.93, 12.71, 8, 5.33, 7.22, 13.78, 50, 34.2, 7.56, 38.31, 54.06, 58.01, 16.73, 165.16, 35.76, 22.16, 45.49, 17.13, 32.19, 50, 128.54, 64.25, 8.57, 62.81, 30.2, 66.05, 9.22 };
            double[] weights = new double[Number] { 0.6, 0.4, 0.55, 0.52, 0.61, 0.61, 0.61, 1.25, 0.4, 1.2, 0.4, 1.68, 0.25, 0.4, 0.04, 0.89, 0.4, 0.8, 0.14, 0.4, 0.58, 0.61, 0.6, 0.4, 0.14, 0.4, 0.4, 0.4, 0.4, 0.61, 0.05, 0.4, 0.86, 0.4, 0.1, 0.06, 0.61, 0.4, 0.61, 0.04, 0.61, 0.03, 0.28, 0.25, 0.4, 0.04, 0.4, 0.61, 0.45, 0.61, 0.89, 0.06, 0.4, 0.28, 0.4, 1.41, 0.45, 0.4, 0.61, 0.8, 0.61, 0.07, 0.26, 0.4, 0.06, 0.05, 0.4, 0.09, 0.26, 0.99, 0.61, 0.11, 0.4, 0.61, 0.4, 0.4, 0.54, 0.4, 0.09, 0.4, 0.11, 0.45, 0.01, 0.61, 0.16, 0.4, 0.61, 0.4, 0.4, 0.61, 0.4, 0.3, 0.11, 0.02, 0.36, 0.4, 0.01, 0.11, 0.4, 0.4, 0.81, 0.4, 0.4, 0.4, 0.4, 0.4, 0.26, 0.4, 0.2, 0.61, 0.61, 0.37, 0.04, 0.4, 0.4, 0.4, 0.11, 0.45, 0.22, 0.2, 0.64, 0.44, 0.8, 1.21, 0.53, 1.28, 0.61, 0.4, 0.05, 0.61, 0.2, 0.45, 0.06, 0.04, 0.4, 0.53, 0.17, 0.58, 1.48, 0.06, 0.4, 0.4, 0.4, 0.4, 0.4, 0.4, 0.4, 0.8, 0.61, 0.2, 0.57, 0.06, 0.25, 0.15, 1.41, 0.37, 0.4, 0.2, 0.52, 0.33, 0.4, 0.11, 0.11, 0.55, 0.04, 0.4, 0.65, 0.61, 0.4, 0.45, 0.81, 0.56, 0.67, 0.42, 0.4, 0.4, 0.04, 0.26, 0.4, 0.4, 0.46, 0.05, 0.4, 0.4, 0.6, 0.07, 0.6, 0.4, 0.4, 0.61, 0.01, 0.07, 0.2, 0.06, 0.07, 0.4, 0.46, 0.11, 0.35, 0.61, 0.61, 0.18, 0.13, 0.01, 0.61, 0.61, 0.4, 0.53, 0.43, 0.4, 0.22, 0.41, 0.68, 0.4, 0.91, 0.41, 0.15, 0.4, 0.01, 0.26, 0.77, 0.4, 0.2, 0.4, 0.4, 0.3, 0.11, 0.11, 0.54, 0.9, 0.4, 0.4, 0.27, 0.34, 0.87, 0.4, 0.05, 0.01, 0.6, 0.61 };

            Console.WriteLine(weights.Sum());

            double totalWeight = weights.Sum();
            for (int i = 0, count = weights.Length; i < count; i++)
            {
                weights[i] = weights[i] / totalWeight;
            }

            //double totalWeight = rawWeight.Sum();
            //for (int i = 0; i < Number; i++)
            //{
            //    weights[i] = rawWeight[i] / totalWeight;
            //}

            var amounts = CalcUtil.CalcStockAmountPerCopyRound(totalMoney, weights, prices, 0);

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
            Console.WriteLine("Bank Difference: " + (adjustMoney - totalMoney).ToString());

            amounts = CalcUtil.CalcStockAmountByChinaRound(totalMoney, weights, prices, 0);

            foreach (int a in amounts)
            {
                Console.WriteLine(a);
            }

            adjustMoney = 0.0f;
            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                adjustMoney += amounts[i] * prices[i];
            }

            Console.WriteLine(adjustMoney);
            Console.WriteLine("China Difference: " + (adjustMoney - totalMoney).ToString());

            amounts = CalcUtil.CalcStockAmountPerCopyAdjust(totalMoney, weights, prices, 0);

            foreach (int a in amounts)
            {
                Console.WriteLine(a);
            }

            adjustMoney = 0.0f;
            for (int i = 0, count = amounts.Length; i < count; i++)
            {
                adjustMoney += amounts[i] * prices[i];
            }

            Console.WriteLine(adjustMoney);
            Console.WriteLine("Adjust Difference: " + (adjustMoney - totalMoney).ToString());

        }
    }
}
