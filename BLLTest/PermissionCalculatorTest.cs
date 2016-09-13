using BLL.Permission;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLTest
{
    [TestClass]
    public class PermissionCalculatorTest
    {
        [TestMethod]
        public void TestHasPermission()
        {
            int perm = 103;
            PermissionCalculator calculator = new PermissionCalculator();

            var result = calculator.HasPermission(perm, Model.Permission.PermissionMask.Add);
            Assert.AreEqual(true, result);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void TestGrantPermission()
        {
            int perm = 0;
            PermissionCalculator calculator = new PermissionCalculator();

            var result = calculator.GrantPermission(perm, Model.Permission.PermissionMask.Add);
            Assert.AreEqual((int)Model.Permission.PermissionMask.Add, result);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void TestRevokePermission()
        {
            int perm = 3;
            PermissionCalculator calculator = new PermissionCalculator();

            var result = calculator.RevokePermission(perm, Model.Permission.PermissionMask.Add);
            Assert.AreEqual(1, result);
            Console.WriteLine(result);
        }
    }
}
