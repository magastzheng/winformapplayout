using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum ConnectionCode
    {
        Success = 0,
        ErrorReadConf = -1,
        ErrorInitConn = -2,
        ErrorConn = -3,
        ErrorSendMsg = -4,
        ErrorLogin = -10001,
        ErrorNoLogin = -10002,
        ErrorNoCallback = -30001,
        ErrorNoFunctionCode = -30001,
    }
}
