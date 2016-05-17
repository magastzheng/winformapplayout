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
    public partial class StrategyTradingForm : Forms.DefaultForm
    {
        private GridConfig _gridConfig = null;

        public StrategyTradingForm()
            :base()
        {
            InitializeComponent();
        }

        public StrategyTradingForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;
        }
    }
}
