using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace UtilTest
{
    public class Test
    {
        public string Id { get; set; }
        public int Times { get; set; }
    }
    [TestClass]
    public class JsonUtilTest
    {
        [TestMethod]
        public void TestDeserializeObject()
        {
            string json = @"{'id':'1234567', 'times': 124}";
            Test test = JsonUtil.DeserializeObject<Test>(json);
            Assert.IsNotNull(test);
            Assert.AreEqual("1234567", test.Id);
        }
    }
}
