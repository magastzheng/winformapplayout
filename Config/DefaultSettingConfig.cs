using Model.Setting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config
{
    public class DefaultSettingConfig
    {
        private const string FileName = "defaultsetting.json";

        private DefaultSetting _defaultSetting = null;

        public DefaultSetting DefaultSetting { get { return _defaultSetting; } }

        public DefaultSettingConfig()
        {
            Init();
        }

        private int Init()
        {
 	        string filePath = RuntimeEnv.Instance.GetConfigFile(FileName);
            string content = FileUtil.ReadFile(filePath);
            _defaultSetting = JsonConvert.DeserializeObject<DefaultSetting>(content);

            return 0;
        } 
    }
}
