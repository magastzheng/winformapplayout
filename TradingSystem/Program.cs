using BLL;
using Config;
using TradingSystem.Controller;
using log4net;
using System;
using System.Windows.Forms;
using TradingSystem.View;
using log4net.Config;
using BLL.UFX;
using BLL.TradeCommand;
using WAPIWrapperCSharp;
using System.Configuration;

namespace TradingSystem
{
    static class Program
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static MainController _s_mainfrmController = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var log4net = ConfigurationManager.AppSettings["log4net"];
            string logFilePath = Application.StartupPath + "\\" + log4net;
            //XmlConfigurator.Configure();
            XmlConfigurator.Configure(new Uri(logFilePath));
            logger.Info("Log4net initialize...: " + logFilePath);
            //XmlConfigurator.ConfigureAndWatch("log4net.config");

            //处理未捕获的异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            var buttonConfig = ConfigManager.Instance.GetButtonConfig();

            //清算交易实例
            var tradeInstanceSecuBLL = new TradeInstanceSecurityBLL();
            tradeInstanceSecuBLL.SettlePosition();

            T2SDKWrap t2SDKWrap = new T2SDKWrap();
            var conRet = t2SDKWrap.Connect();
            if (conRet != Model.ConnectionCode.Success)
            {
                glExitApp = true;
                return;
            }

            T2Subscriber t2Subscriber = new T2Subscriber();
            conRet = t2Subscriber.Connect();

            if (conRet != Model.ConnectionCode.Success)
            {
                t2SDKWrap.Close();
                glExitApp = true;
                return;
            }

            BLLManager.Instance.Init(t2SDKWrap);
            BLLManager.Instance.Subscriber = t2Subscriber;

            //启动wind行情服务
            WindAPIWrap.Instance.Start();

            //TODO: subscribe the message after getting login information
            LoginController loginController = new LoginController(new LoginForm(), t2SDKWrap);
            Application.Run(loginController.LoginForm);
            if (_s_mainfrmController != null)
            {
                Application.Run(_s_mainfrmController.MainForm);
            }

            //realloc the connection and service
            loginController.Logout();
            t2SDKWrap.Close();
            t2Subscriber.Close();
            WindAPIWrap.Instance.Dispose();

            glExitApp = true;
        }

        static bool glExitApp = false;

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            logger.Info("CurrentDomain_UnhandledException: " + e.ExceptionObject.ToString());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            logger.Info("Application_ThreadException: " + e.Exception.Message);
        }

    }
}
