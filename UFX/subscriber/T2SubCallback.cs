using UFX.impl;
using hundsun.mcapi;
using hundsun.t2sdk;
using log4net;
using Model.UFX;
using System;
using System.Runtime.InteropServices;

namespace UFX.subscriber
{
    //订阅回调
    public unsafe class T2SubCallback : CT2SubCallbackInterface
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IUFXMessageHandlerFactory _handlerFactory = null;

        private UFXFilterBLL _filterBLL = new UFXFilterBLL();

        public T2SubCallback(IUFXMessageHandlerFactory bllFactory)
        {
            _handlerFactory = bllFactory;
        }

        public override void OnReceived(CT2SubscribeInterface lpSub, int subscribeIndex, void* lpData, int nLength, tagSubscribeRecvData lpRecvData)
        {
            //收到主推数据 - 开始
            if (lpRecvData.iAppDataLen > 0)
            {
                unsafe
                {
                    //附加数据
                    string strInfo = string.Format("{0}", Marshal.PtrToStringAuto(new IntPtr(lpRecvData.lpAppData)));
                }
            }

            UFXPushMessageType messageType = UFXPushMessageType.None;
            //过滤字段部分
            if (lpRecvData.iFilterDataLen > 0)
            {
                CT2UnPacker lpUnpacker = new CT2UnPacker(lpRecvData.lpFilterData, (uint)lpRecvData.iFilterDataLen);
                //解包
                //.....
                DataParser parser = new DataParser();
                parser.Parse(lpUnpacker);


                Log("====推送=====过滤字段部分=====开始");
                //parser.Output();
                Log(parser);
                Log("====推送=====过滤字段部分=====结束");
                lpUnpacker.Dispose();

                messageType = _filterBLL.GetMessageType(parser);
            }

            if (nLength > 0)
            {
                CT2UnPacker lpUnpacker1 = new CT2UnPacker((void*)lpData, (uint)nLength);
                if (lpUnpacker1 != null)
                {
                    //解包
                    //....
                    DataParser parser = new DataParser();
                    parser.Parse(lpUnpacker1);
                    Log("====推送*****数据部分=====开始");
                    //parser.Output();
                    Log(parser);
                    Log("====推送*****数据部分=====结束");
                    lpUnpacker1.Dispose();

                    IUFXMessageHandlerBase subscriberHandler = _handlerFactory.Create(messageType);
                    if (subscriberHandler != null)
                    {
                        subscriberHandler.Handle(parser);
                    }
                }
            }

            //收到主推数据 - 结束
        }

        public override void OnRecvTickMsg(CT2SubscribeInterface lpSub, int subscribeIndex, string TickMsgInfo)
        {
            Console.WriteLine("T2SubCallback.OnRecvTickMsg");
        }

        private void Log(string msg)
        { 
#if DEBUG
            logger.Info(msg);
#endif
        }

        private void Log(DataParser parser)
        {
#if DEBUG
            string msg = parser.GetOutputStr();
            logger.Info(msg);
#endif
        }
    }
}
