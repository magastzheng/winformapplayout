using Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
