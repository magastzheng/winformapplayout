using hundsun.t2sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IDataReceiver
    {
        int ReceivedBizMsg(CT2BizMessage lpMsg);
    }
}
