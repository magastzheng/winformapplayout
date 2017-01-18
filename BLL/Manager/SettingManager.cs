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

        private UserSettingBLL _userSettingBLL = null;

        private SettingManager()
        {
            _userSettingBLL = new UserSettingBLL();
        }

        public DefaultSetting Get()
        {
            DefaultSetting setting = _userSettingBLL.Get();
            if (setting == null || setting.UFXSetting == null || setting.EntrustSetting == null)
            {
                setting = ConfigManager.Instance.GetDefaultSettingConfig().DefaultSetting;
            }

            return setting;
        }

        public int Create(DefaultSetting setting)
        {
            return _userSettingBLL.Create(setting);
        }
    }
}
