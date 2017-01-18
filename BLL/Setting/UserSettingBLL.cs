using Config;
using DBAccess.Setting;
using Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Setting
{
    public class UserSettingBLL
    {
        private UserSettingDAO _usersettingdao = new UserSettingDAO();

        public UserSettingBLL()
        { 
        }

        public int Create(DefaultSetting setting)
        {
            int userId = LoginManager.Instance.GetUserId();

            return _usersettingdao.Create(userId, setting);
        }

        public DefaultSetting Get()
        {
            int userId = LoginManager.Instance.GetUserId();

            return _usersettingdao.Get(userId);
        }

        public int Delete()
        {
            int userId = LoginManager.Instance.GetUserId();

            return _usersettingdao.Delete(userId);
        }
    }
}
