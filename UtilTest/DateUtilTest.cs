using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace UtilTest
{
    [TestClass]
    public class DateUtilTest
    {
        [TestMethod]
        public void Test_GetIntDate()
        {
            DateTime dt = new DateTime(2016, 4, 22, 12, 13, 14);
            int iDt = DateUtil.GetIntDate(dt);
            Assert.AreEqual(20160422, iDt);

            dt = new DateTime(2015, 10, 25, 12, 13, 14);
            iDt = DateUtil.GetIntDate(dt);
            Assert.AreEqual(20151025, iDt);
        }

        [TestMethod]
        public void Test_GetIntTime()
        {
            DateTime dt = new DateTime(2016, 4, 22, 12, 13, 14);
            int iTime = DateUtil.GetIntTime(dt);
            Assert.AreEqual(121314, iTime);

            dt = new DateTime(2015, 10, 25, 23, 30, 59);
            iTime = DateUtil.GetIntTime(dt);
            Assert.AreEqual(233059, iTime);
        }

        [TestMethod]
        public void Test_IsValidDate_Date()
        {
            string str = "20161010";
            string format = "yyyyMMdd";

            DateTime dt;
            var result = DateUtil.IsValidDate(str, format, out dt);

            Assert.AreEqual(true, result);

            str = "20171245";
            result = DateUtil.IsValidDate(str, format, out dt);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Test_IsValidDate_Time()
        {
            string str = "154512";
            string format = "HHmmss";

            DateTime dt;
            var result = DateUtil.IsValidDate(str, format, out dt);

            Assert.AreEqual(true, result);

            str = "541213";
            result = DateUtil.IsValidDate(str, format, out dt);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Test_GetDateFromInt()
        {
            int dtInt = 20160102;
            DateTime dt = DateUtil.GetDateFromInt(dtInt);

            DateTime expect = new DateTime(2016, 1, 2);
            Assert.AreEqual(expect, dt);
            Console.WriteLine(dt);
        }

        [TestMethod]
        public void Test_GetDateFromInt_Invalid()
        {
            int dtInt = 20162102;
            DateTime dt = DateUtil.GetDateFromInt(dtInt);

            Assert.AreEqual(DateTime.MinValue, dt);
        }

        [TestMethod]
        public void Test_GetDateTimeFromInt()
        {
            int dtInt = 20160102;
            int timeInt = 122345;
            DateTime dt = DateUtil.GetDateTimeFromInt(dtInt, timeInt);

            DateTime expect = new DateTime(2016, 1, 2, 12, 23, 45);
            Assert.AreEqual(expect, dt);

            dtInt = 20160102;
            timeInt = 82345;
            dt = DateUtil.GetDateTimeFromInt(dtInt, timeInt);
            expect = new DateTime(2016, 1, 2, 8, 23, 45);
            Assert.AreEqual(expect, dt);
        }

        [TestMethod]
        public void Test_GetDateTimeFromInt_Invalid_Day()
        {
            int dtInt = 20162102;
            int timeInt = 122345;
            DateTime dt = DateUtil.GetDateTimeFromInt(dtInt, timeInt);

            DateTime expect = DateTime.MinValue;
            Assert.AreEqual(expect, dt);

            dtInt = 20160152;
            timeInt = 82345;
            dt = DateUtil.GetDateTimeFromInt(dtInt, timeInt);
            Assert.AreEqual(expect, dt);

            dtInt = 0;
            timeInt = 0;
            dt = DateUtil.GetDateTimeFromInt(dtInt, timeInt);
            Assert.AreEqual(expect, dt);
        }

        [TestMethod]
        public void Test_GetDateTimeFromInt_Invalid_Time()
        {
            int dtInt = 20160102;
            int timeInt = 282345;
            DateTime dt = DateUtil.GetDateTimeFromInt(dtInt, timeInt);

            DateTime expect = DateTime.MinValue;
            Assert.AreEqual(expect, dt);

            dtInt = 20160102;
            timeInt = 186345;
            dt = DateUtil.GetDateTimeFromInt(dtInt, timeInt);
            Assert.AreEqual(expect, dt);

            dtInt = 20160102;
            timeInt = 182365;
            dt = DateUtil.GetDateTimeFromInt(dtInt, timeInt);
            Assert.AreEqual(expect, dt);
        }
    }
}
