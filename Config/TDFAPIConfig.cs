using Model.config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config
{
    public class TDFAPIConfig
    {
        private const string FileName = "tdfapiconfig.json";

        private TDFAPISetting _setting = null;

        public TDFAPIConfig()
        {
            Init();
        }

        public int Init()
        {
            string filePath = RuntimeEnv.Instance.GetConfigFile(FileName);
            string content = FileUtil.ReadFile(filePath);
            _setting = JsonConvert.DeserializeObject<TDFAPISetting>(content);

            return 0;
        }

        public TDFAPISetting GetSetting()
        {
            if (_setting == null)
            {
                Init();
            }

            return _setting;
        }
    }
}
