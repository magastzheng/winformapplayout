using TradingSystem.View;
using UFX;

namespace TradingSystem.Controller
{
    public class MainController
    {
        private MainForm _mainForm;

        private T2SDKWrap _t2SDKWrap;
        public MainForm MainForm
        {
            get { return _mainForm; }
        }

        public MainController(MainForm mainForm, T2SDKWrap t2SDKWrap)
        {
            this._t2SDKWrap = t2SDKWrap;
            
            this._mainForm = mainForm;
        }
    }
}
