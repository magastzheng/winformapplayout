using BLL.UFX;
using TradingSystem.View;

namespace TradingSystem.Controller
{
    public class MainController
    {
        private MainForm _mainForm;
        //private StrategyBLL _strategyBLL;
        //private SecurityBLL _securityBLL;
        //private ArbitrageBLL _arbitrageBLL;
        private T2SDKWrap _t2SDKWrap;
        public MainForm MainForm
        {
            get { return _mainForm; }
        }

        //public StrategyBLL StrategyBLL
        //{
        //    get { return _strategyBLL; }
        //}

        //public SecurityBLL SecurityBLL
        //{
        //    get { return _securityBLL; }
        //}

        //public ArbitrageBLL ArbitrageBLL
        //{
        //    get { return _arbitrageBLL; }
        //}

        public MainController(MainForm mainForm, T2SDKWrap t2SDKWrap)
        {
            this._t2SDKWrap = t2SDKWrap;
            //this._strategyBLL = new StrategyBLL(this._t2SDKWrap);
            //this._securityBLL = new SecurityBLL(this._t2SDKWrap);
            //this._arbitrageBLL = new ArbitrageBLL(this._t2SDKWrap);

            //var accounts = LoginManager.Instance.Accounts;
            //foreach (var account in accounts)
            //{
            //    _arbitrageBLL.QueryInstance(account);
            //}

            //_arbitrageBLL.QueryMonitorItem();

            //Console.WriteLine("===========查询委托证券 开始==============");
            //_securityBLL.QueryEntrust();
            //Console.WriteLine("===========查询委托证券 结束==============");

            //_strategyBLL.QueryTrading();

            this._mainForm = mainForm;
            //this._mainForm.MainController = this;
        }
    }
}
