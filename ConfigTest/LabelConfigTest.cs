using Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTest
{
    [TestClass]
    public class LabelConfigTest
    {
        [TestMethod]
        public void Test_GetErrorMessage()
        {
            var config = new LabelConfig();
            var msg = config.GetErrorMessage(-3);

            Assert.AreEqual("链接错误", msg);
        }

        [TestMethod]
        public void Test_GetLabelText()
        {
            var config = new LabelConfig();
            var msg = config.GetLabelText("failtitle");

            Assert.AreEqual("失败", msg);
        }
    }
}
