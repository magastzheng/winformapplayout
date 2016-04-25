using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Config
{
    public class TerminalConfig
    {
        //private static readonly TerminalConfig _instance = new TerminalConfig();
        //public static TerminalConfig Instance { get { return _instance; } }

        private string _macAddress;
        private string _opStation;
        private string _ipAddress;
        private string _hdVolserial;
        private string _authorizationId;

        //static TerminalConfig()
        //{ 
        
        //}

        public TerminalConfig(SystemConfig systemConfig)
        {
            Init(systemConfig);
        }

        public void Init(SystemConfig systemConfig)
        {
            string hostInfo = Dns.GetHostName();
            IPAddress[] addressList = Dns.GetHostEntry(hostInfo).AddressList;
            foreach(IPAddress ip in addressList)
            {
                if (ip.AddressFamily.ToString().ToUpper() == "INTERNETWORK")
                {
                    _ipAddress = ip.ToString();
                    break;
                }
            }

            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["IPEnabled"].ToString() == "True")
                {
                    _macAddress = mo["MacAddress"].ToString();
                    break;
                }
            }

            mc = new ManagementClass("Win32_DiskDrive");
            moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                _hdVolserial = mo.Properties["Model"].Value.ToString();
                break;
            }

            _opStation = systemConfig.GetSystemStr("opstation");
            _authorizationId = systemConfig.GetSystemStr("authorizationid");
        }

        public string MacAddress { get { return _macAddress; } }
        public string OpStation { get { return _opStation; } }
        public string IPAddress { get { return _ipAddress; } }
        public string HDVolserial { get { return _hdVolserial; } }
        public string AuthorizationId { get { return _authorizationId; } }
    }
}
