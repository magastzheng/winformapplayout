using hundsun.t2sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public unsafe class T2SDKCallbackImpl : CT2CallbackInterface
    {
        private T2SDKBase _app = null;
        public T2SDKCallbackImpl(T2SDKBase app)
        {
            _app = app;
        }

        #region Implement the interface CT2CallbackInterface
        public override void OnClose(CT2Connection lpConnection)
        {
            _app.OnClose(lpConnection);
        }

        public override void OnConnect(CT2Connection lpConnection)
        {
            _app.OnConnect(lpConnection);
        }

        public override void OnReceivedBiz(CT2Connection lpConnection, int hSend, string lppStr, CT2UnPacker lppUnPacker, int nResult)
        {
            _app.OnReceivedBiz(lpConnection, hSend, lppStr, lppUnPacker, nResult);
        }

        public override void OnReceivedBizEx(CT2Connection lpConnection, int hSend, CT2RespondData lpRetData, string lppStr, CT2UnPacker lppUnPacker, int nResult)
        {
            _app.OnReceivedBizEx(lpConnection, hSend, lpRetData, lppStr, lppUnPacker, nResult);
        }

        public override void OnReceivedBizMsg(CT2Connection lpConnection, int hSend, CT2BizMessage lpMsg)
        {
            _app.OnReceivedBizMsg(lpConnection, hSend, lpMsg);
        }

        public override void OnRegister(CT2Connection lpConnection)
        {
            _app.OnRegister(lpConnection);
        }

        public override void OnSafeConnect(CT2Connection lpConnection)
        {
            _app.OnSafeConnect(lpConnection);
        }

        public override void OnSent(CT2Connection lpConnection, int hSend, void* lpData, int nLength, int nQueuingData)
        {
            //TODO:Fixed the convertion
            //IntPtr intPtr = new IntPtr(lpData);
            //_app.OnSent(lpConnection, hSend, intPtr, nLength, nQueuingData);
            _app.OnSent(lpConnection, hSend, lpData, nLength, nQueuingData);
        }

        #endregion
    }
}
