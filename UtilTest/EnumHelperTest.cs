using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace UtilTest
{
    public enum Enum1 { 
        None,
        One,
        Two,
        Three,
        Four,
        Five,
    }

    [TestClass]
    public class EnumHelperTest
    {
        [TestMethod]
        public void Test_ParseEnum()
        {
            var result = EnumHelper.ParseEnum<Enum1>("One", Enum1.None);

            Assert.AreEqual(Enum1.One, result);
        }
    }
}
