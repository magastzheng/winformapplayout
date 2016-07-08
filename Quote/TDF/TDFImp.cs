using System;
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
