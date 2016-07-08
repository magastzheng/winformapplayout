using Config;

namespace TradingSystem.View
{
    public partial class StrategyTradingForm_old : Forms.DefaultForm
    {
        private GridConfig _gridConfig = null;

        public StrategyTradingForm_old()
            :base()
        {
            InitializeComponent();
        }

        public StrategyTradingForm_old(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;
        }
    }
}
