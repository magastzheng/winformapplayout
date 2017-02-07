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
    public class AmountRoundUtilTest
    {
        [TestMethod]
        public void Test_Round()
        {
            int amount = 146;
            int actual = AmountRoundUtil.Round(amount);
            Assert.AreEqual(100, actual);

            amount = 150;
            actual = AmountRoundUtil.Round(amount);
            Assert.AreEqual(200, actual);

            amount = 1190;
            actual = AmountRoundUtil.Round(amount);
            Assert.AreEqual(1200, actual);
        }

        [TestMethod]
        public void Test_Ceiling()
        {
            int amount = 146;
            int actual = AmountRoundUtil.Ceiling(amount);
            Assert.AreEqual(200, actual);

            amount = 150;
            actual = AmountRoundUtil.Ceiling(amount);
            Assert.AreEqual(200, actual);

            amount = 1190;
            actual = AmountRoundUtil.Ceiling(amount);
            Assert.AreEqual(1200, actual);
        }

        [TestMethod]
        public void Test_Floor()
        {
            int amount = 146;
            int actual = AmountRoundUtil.Floor(amount);
            Assert.AreEqual(100, actual);

            amount = 150;
            actual = AmountRoundUtil.Floor(amount);
            Assert.AreEqual(100, actual);

            amount = 1190;
            actual = AmountRoundUtil.Floor(amount);
            Assert.AreEqual(1100, actual);
        }
    }
}
