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
    public class FloatUtilTest
    {
        [TestMethod]
        public void Test_Compare()
        {
            int result = FloatUtil.Compare(0.245612, 0.3546887);
            Assert.AreEqual(-1, result);

            result = FloatUtil.Compare(0.245612, 0.245611);
            Assert.AreEqual(1, result);

            result = FloatUtil.Compare(0.5, 0.500000000012);
            Assert.AreEqual(0, result);

            result = FloatUtil.Compare(0.5000012, 0.500000000012);
            Assert.AreEqual(1, result);

            result = FloatUtil.Compare(0.0, 0.0);
            Assert.AreEqual(0, result);

            result = FloatUtil.Compare(-0.0000001, 0.0);
            Assert.AreEqual(-1, result);

            result = FloatUtil.Compare(-0.0000001, -0.0001);
            Assert.AreEqual(1, result);

            result = FloatUtil.Compare(-0.0000001, -0.0000002);
            Assert.AreEqual(1, result);
        }
    }
}
