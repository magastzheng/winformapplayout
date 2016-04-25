using Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSUnitTest
{
    [TestClass]
    public class FunctionConfigTest
    {
        [TestMethod]
        public void TestFunctionConfig()
        {
            FunctionConfig config = new FunctionConfig();
            try
            {
                config.Init();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Test only");
        }
    }
}
