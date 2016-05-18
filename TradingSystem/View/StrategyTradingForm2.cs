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
    public partial class StrategyTradingForm2 : Forms.BaseForm
    {
        private const string GridCmdTradingId = "cmdtrading";
        private const string GridEntrustFlowId = "entrustflow";
        private const string GridDealFlowId = "dealflow";
        private const string GridCmdSecurityId = "cmdsecurity";
        private const string GridBuySellId = "buysell";

        GridConfig _gridConfig;
        public StrategyTradingForm2()
            :base()
        {
            InitializeComponent();
        }

        public StrategyTradingForm2(GridConfig gridConfig)
            :this()
        {
            _gridConfig = gridConfig;

            LoadControl += new FormLoadHandler(Form_LoadControl);
            LoadData += new FormLoadHandler(Form_LoadData);
            tbCopies.KeyPress += new KeyPressEventHandler(TextBox_Copies_KeyPress);
        }

        #region form loading

        private void Form_LoadControl(object sender, object data)
        {
            
        }

        private void Form_LoadData(object sender, object data)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region control event handler
        
        private void TextBox_Copies_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion
    }
}
