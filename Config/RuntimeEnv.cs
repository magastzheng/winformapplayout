using System;
using System.IO;

namespace Config
{
    public class RuntimeEnv
    {
        private readonly static RuntimeEnv _instance = new RuntimeEnv();
        static RuntimeEnv()
        { 
        }

        private RuntimeEnv()
        {
            Init();
        }

        private string _binDir;
        private string _configDir;

        private void Init()
        {
            _binDir = Environment.CurrentDirectory;
            _configDir = Path.Combine(_binDir, "config");
        }

        public static RuntimeEnv Instance { get { return _instance; } }
        public string GetBinDir() 
        { 
            return _instance._binDir; 
        }
        
        public string GetConfigDir() 
        { 
            return _instance._configDir; 
        }

        public string GetConfigFile(string fileName) 
        {
            return Path.Combine(_configDir, fileName); 
        }
    }
}
