﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
    }
}
