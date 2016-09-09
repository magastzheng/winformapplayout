using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.UFX;
using System;

namespace ModelTest
{
    [TestClass]
    public class EnumTest
    {
        [TestMethod]
        public void TestEnumChar()
        {
            UFXPushMessageType messageType = UFXPushMessageType.None;
            var typeChar = Char.Parse("b");
            UFXPushMessageType tempType;
            if(Enum.TryParse(typeChar.ToString(), true, out tempType))
            {
                messageType = tempType;
            }
            else
            {
                messageType = (UFXPushMessageType)typeChar;
            }
            
            Assert.AreEqual(UFXPushMessageType.EntrustConfirm, messageType);
        }
    }
}
