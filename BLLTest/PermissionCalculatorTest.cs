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
        /// <summary>
        /// BIT: 7      6       5       4       3       2       1       0
        ///      1      1       1       1       1       1       1       1
        ///      128    64      32      16      8       4       2       1
        /// </summary>
        [TestMethod]
        public void TestHasPermission()
        {
            int perm = 103;
            PermissionCalculator calculator = new PermissionCalculator();

            var result = calculator.HasPermission(perm, Model.Permission.PermissionMask.Add);
            Assert.AreEqual(true, result);
            Console.WriteLine(result);

            perm = -1;
            result = calculator.HasPermission(perm, Model.Permission.PermissionMask.Add);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestGrantPermission()
        {
            int perm = 0;
            PermissionCalculator calculator = new PermissionCalculator();

            //首次授权
            var result = calculator.GrantPermission(perm, Model.Permission.PermissionMask.Add);
            var expected = (int)Model.Permission.PermissionMask.Add;
            Assert.AreEqual(expected, result);
            //Console.WriteLine(result);

            //授予其他权限
            result = calculator.GrantPermission(result, Model.Permission.PermissionMask.Delete);
            expected = expected + (int)Model.Permission.PermissionMask.Delete;
            Assert.AreEqual(expected, result);

            //重复给相同权限
            result = calculator.GrantPermission(result, Model.Permission.PermissionMask.Delete);
            Assert.AreEqual(expected, result);

            result = calculator.GrantPermission(result, Model.Permission.PermissionMask.Edit);
            expected = expected + (int)Model.Permission.PermissionMask.Edit;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestRevokePermission()
        {
            int perm = 255;
            PermissionCalculator calculator = new PermissionCalculator();

            var result = calculator.RevokePermission(perm, Model.Permission.PermissionMask.Add);
            var expected = perm - (int)Model.Permission.PermissionMask.Add;
            Assert.AreEqual(expected, result);

            result = calculator.RevokePermission(result, Model.Permission.PermissionMask.Add);
            Assert.AreEqual(expected, result);

            result = calculator.RevokePermission(result, Model.Permission.PermissionMask.Delete);
            expected = expected - (int)Model.Permission.PermissionMask.Delete;
            Assert.AreEqual(expected, result);
        }
    }
}
