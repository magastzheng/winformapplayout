using BLL;
using Config;
//using hundsun.t2sdk;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSUnitTest.BLL
{
    [TestClass]
    public class LoginBLLTest
    {
        [TestMethod]
        public void TestLogin()
        {
            Console.WriteLine("Test");
            //try
            //{
                //新建连接
                //var config = new CT2Configinterface();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //int iRet = config.Load("config/t2sdk.ini");

            //if (iRet != 0)
            //{
            //    string msg = "读取连接配置对象失败！";
            //    return;
            //}

            //var cm = ConfigManager.Instance.GetTerminalConfig();
            //LoginBLL loginBLL = new LoginBLL(config);
            //User user = new User
            //{
            //    Operator = "10099",
            //    Password = "0"
            //};

            //var retCode = loginBLL.Login(user);
            //Console.WriteLine(retCode);
        }
    }
}
