using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.config
{
    public class TDFAPISetting
    {
        public string Ip { get; set; }

        public int Port { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public int ReconnectCount { get; set; }

        public int ReconnectGap { get; set; }

        public int ConnectionId { get; set; }

        public string Markets { get; set; }

        public int Date { get; set; }

        public int Time { get; set; }

        public int TypeFlags { get; set; }
    }
}
