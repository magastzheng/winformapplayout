using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDFAPI;

namespace Quote.TDF
{
    class TDFImp : TDFDataSource
    {
        public Action<TDFMSG> SysMsgDeal;
        public Action<TDFMSG> DataMsgDeal;

        public TDFImp(TDFOpenSetting openSetting)
            : base(openSetting)
        { 
        }

        public override void OnRecvDataMsg(TDFMSG msg)
        {
            DataMsgDeal(msg);
        }

        public override void OnRecvSysMsg(TDFMSG msg)
        {
            SysMsgDeal(msg);
        }
    }
}
