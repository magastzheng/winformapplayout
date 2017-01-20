using Model.config;
using Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util;

namespace TradingSystem.TradeUtil
{
    public static class ComboBoxHelper
    {
        public static OddShareMode GetPriceType(ComboBox comboBox)
        {
            var selectItem = (ComboOptionItem)comboBox.SelectedItem;
            return EnumHelper.ParseEnum<OddShareMode>(selectItem.Id, OddShareMode.Floor);
        }
    }
}
