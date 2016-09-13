using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilTest
{
    [TestClass]
    public class PermissionTest
    {
        [TestMethod]
        public void TestPermissionMask()
        {
            //7 6 5 4 3 2 1 0
            //0 1 1 0 0 0 0 0   = 96
            //0 1 1 0 0 1 0 0   = 100
            //0 1 1 0 0 1 0 1   = 101
            int perm = 101;
            Console.WriteLine(perm & (1 << 0));

            perm = 0;
            Console.WriteLine(perm & (1 << 0));
        }

        [TestMethod]
        public void TestPermissionValue()
        {
            //7 6 5 4 3 2 1 0
            //0 1 1 0 0 0 0 0   = 96
            //0 1 1 0 0 1 0 0   = 100
            //0 1 1 0 0 1 0 1   = 101
            int p1 = 0;
            int p2 = 1;
            int perm = (1 << p1) | (1 << p2);
            Console.WriteLine(perm);
        }
    }
}
