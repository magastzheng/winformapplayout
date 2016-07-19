using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.t2sdk;
using System;

namespace ModelTest
{
    [TestClass]
    public class EnumTest
    {
        [TestMethod]
        public void TestEnumChar()
        {
            PushMessageType messageType = PushMessageType.None;
            var typeChar = Char.Parse("b");
            PushMessageType tempType;
            if(Enum.TryParse(typeChar.ToString(), true, out tempType))
            {
                messageType = tempType;
            }
            else
            {
                messageType = (PushMessageType)typeChar;
            }
            
            Assert.AreEqual(PushMessageType.EntrustConfirm, messageType);
        }
    }
}
