using Calculation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationTest
{
    [TestClass]
    public class AmountRoundUtilTest
    {
        [TestMethod]
        public void Test_Math_Round()
        {
            double val1 = 2.5;
            double val2 = 3.5;

            int ival1 = (int)Math.Round(val1);
            int ival2 = (int)Math.Round(val2);

            string fmt = "{0}:{1}";
            string msg = string.Format(fmt, val1, ival1);
            Trace.WriteLine(msg);
            msg = string.Format(fmt, val2, ival2);
            Trace.WriteLine(msg);
        }

        [TestMethod]
        public void Test_Math_Round_AwayFromZero()
        {
            double val1 = 2.5;
            double val2 = 3.5;

            int ival1 = (int)Math.Round(val1, MidpointRounding.AwayFromZero);
            int ival2 = (int)Math.Round(val2, MidpointRounding.AwayFromZero);

            string fmt = "{0}:{1}";
            string msg = string.Format(fmt, val1, ival1);
            Trace.WriteLine(msg);
            msg = string.Format(fmt, val2, ival2);
            Trace.WriteLine(msg);
        }

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
