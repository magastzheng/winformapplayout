using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UtilTest
{
    [TestClass]
    public class DigitTest
    {
        [TestMethod]
        public void Test_Int()
        {
            Regex re = new Regex(@"^\d+$");

            string s = "1234567891234";
            bool actaul = re.IsMatch(s);

            Assert.AreEqual(true, actaul);

            s = "s12345";
            actaul = re.IsMatch(s);
            Assert.AreEqual(false, actaul);

            s = "12345t";
            actaul = re.IsMatch(s);
            Assert.AreEqual(false, actaul);
        }

        [TestMethod]
        public void Test_Float()
        {
            Regex re = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");

            string s = "1234567891234";
            bool actaul = re.IsMatch(s);
            Assert.AreEqual(true, actaul);

            s = ".1234567891234";
            actaul = re.IsMatch(s);
            Assert.AreEqual(true, actaul);

            s = "0.1234567891234";
            actaul = re.IsMatch(s);
            Assert.AreEqual(true, actaul);

            s = "789.1234567891234";
            actaul = re.IsMatch(s);
            Assert.AreEqual(true, actaul);

            s = "789.123456789123.4";
            actaul = re.IsMatch(s);
            Assert.AreEqual(false, actaul);

            s = "789.12345s";
            actaul = re.IsMatch(s);
            Assert.AreEqual(false, actaul);
        }
    }
}
