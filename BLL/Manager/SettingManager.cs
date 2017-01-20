using BLL.Setting;
using Config;
using Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Manager
{
    public class SettingManager
    {
        private static readonly SettingManager _instance = new SettingManager();

        public static SettingManager Instance { get { return _instance; } }

        private UserSettingBLL _userSettingBLL = null;
        private DefaultSetting _setting = null;

        private SettingManager()
        {
            _userSettingBLL = new UserSettingBLL();
        }

        public DefaultSetting Get()
        {
            return GetSetting();
        }

        public int Create(DefaultSetting setting)
        {
            return _userSettingBLL.Create(setting);
        }

        public int Update(DefaultSetting setting)
        {
            _setting = setting;
            return Create(setting);
        }

        #region private method

        private DefaultSetting GetSetting()
        {
            if (_setting == null)
            {
                _setting = _userSettingBLL.Get();
                if (_setting == null 
                    || _setting.UFXSetting == null 
                    || _setting.EntrustSetting == null
                    || _setting.Timeout == 0
                    || _setting.UFXSetting.Timeout == 0
                    || _setting.EntrustSetting.BuyFutuPrice == Model.EnumType.PriceType.None
                    )
                {
                    _setting = ConfigManager.Instance.GetDefaultSettingConfig().DefaultSetting;
                }
            }

            return _setting;
        }

        #endregion
    }
}
