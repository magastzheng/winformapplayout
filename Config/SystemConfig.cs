using Newtonsoft.Json;
using System.Collections.Generic;

namespace Config
{
    public class GlobalConfig
    {
        public Dictionary<string, string> System { get; set; }
        public Dictionary<string, string> Server { get; set; }
        public Dictionary<string, string> User { get; set; }
        public List<string> Operators { get; set; }
    }

    public class SystemConfig
    {
        private const string FileName = "global.json";
        private GlobalConfig _globalConfig = new GlobalConfig();
        public GlobalConfig GlobalConfig { get { return _globalConfig; } }

        public SystemConfig()
        {
            Init();
        }

        private void Init()
        {
            string filePath = RuntimeEnv.Instance.GetConfigFile(FileName);
            string content = FileUtil.ReadFile(filePath);
            _globalConfig = JsonConvert.DeserializeObject<GlobalConfig>(content);
        }

        #region System
        public void AddSystem(string key, string val)
        {
            _globalConfig.System[key] = val;
        }

        public void AddSystem(string key, int val)
        {
            _globalConfig.System[key] = val.ToString();
        }

        public string GetSystemStr(string key)
        {
            if (_globalConfig.System.ContainsKey(key))
            {
                return _globalConfig.System[key];
            }

            return string.Empty;
        }

        public int GetSystemInt(string key)
        {
            string val = string.Empty;
            if (_globalConfig.System.ContainsKey(key))
            {
                val = _globalConfig.System[key];
            }

            int ret = 0;
            int temp;
            if (int.TryParse(val, out temp))
            {
                ret = temp;
            }

            return ret;
        }
        #endregion

        #region Server
        public void AddServer(string key, string val)
        {
            _globalConfig.Server[key] = val;
        }

        public string GetServerStr(string key)
        {
            if (_globalConfig.Server.ContainsKey(key))
            {
                return _globalConfig.Server[key];
            }

            return string.Empty;
        }

        public int GetServerInt(string key)
        {
            string val = string.Empty;
            if (_globalConfig.Server.ContainsKey(key))
            {
                val = _globalConfig.Server[key];
            }

            int ret = 0;
            int temp;
            if (int.TryParse(val, out temp))
            {
                ret = temp;
            }

            return ret;
        }
        #endregion

        #region User
        public void AddUser(string key, string val)
        {
            _globalConfig.User[key] = val;
        }

        public string GetUserStr(string key)
        {
            if (_globalConfig.User.ContainsKey(key))
            {
                return _globalConfig.User[key];
            }

            return string.Empty;
        }

        public int GetUserInt(string key)
        {
            string val = string.Empty;
            if (_globalConfig.User.ContainsKey(key))
            {
                val = _globalConfig.User[key];
            }

            int ret = 0;
            int temp;
            if (int.TryParse(val, out temp))
            {
                ret = temp;
            }

            return ret;
        }
        #endregion

        #region Operator

        public void AddOperator(string key)
        {
            if (!_globalConfig.Operators.Contains(key))
            {
                _globalConfig.Operators.Add(key);
            }
        }

        public List<string> GetOperators()
        {
            return _globalConfig.Operators;
        }

        public void RemoveOperators()
        {
            _globalConfig.Operators.Clear();
        }
        #endregion
    }
}
