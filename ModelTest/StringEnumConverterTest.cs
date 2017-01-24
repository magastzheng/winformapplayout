using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Converter;
using Model.EnumType;
using Model.UFX;
using System;

namespace ModelTest
{
    [TestClass]
    public class StringEnumConverterTest
    {
        [TestMethod]
        public void TestConvertToEnum()
        {
            string typeCode = "A";

            var priceType = StringEnumConverter.GetCharType<EntrustPriceType>(typeCode);

            Console.WriteLine(priceType);
        }

        [TestMethod]
        public void TestConvertToEnum_UFXMarketCode()
        {
            string code = "7";

            var val = StringEnumConverter.GetIntType<UFXMarketCode>(code);

            Console.WriteLine(val);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestConvertToEnum_UFXMarketCode_Exception()
        {
            string code = "10";

            UFXMarketCode testCode;

            var val = StringEnumConverter.GetIntType<UFXMarketCode>(code);

            Console.WriteLine(val);
        }
    }
}
