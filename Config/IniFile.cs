using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Config
{
    public class IniFile
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private string _path;
        public IniFile(string iniPath)
        {
            _path = iniPath;
        }

        public void IniWriteValue(string section, string key, string val)
        {
            WritePrivateProfileString(section, key, val, _path);
        }

        public string IniReadValue(string section, string key)
        {
            StringBuilder sb = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", sb, 255, _path);

            return sb.ToString();
        }
    }
}
