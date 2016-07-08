using hundsun.t2sdk;
using log4net;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UFX.impl
{
    public class UFXBLLBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected T2SDKWrap _t2SDKWrap;
        protected ReceivedBizMsg _receivedBizMsg;
        protected Dictionary<FunctionCode, DataHandlerCallback> _dataHandlerMap = new Dictionary<FunctionCode, DataHandlerCallback>();

        public UFXBLLBase(T2SDKWrap t2SDKWrap)
        {
            _t2SDKWrap = t2SDKWrap;
            _receivedBizMsg = OnReceivedBizMsg;
        }

        public int OnReceivedBizMsg(CT2BizMessage bizMessage)
        {
            int iRetCode = bizMessage.GetReturnCode();
            int iErrorCode = bizMessage.GetErrorNo();
            int iFunction = bizMessage.GetFunction();
            if (iRetCode != 0)
            {
                string msg = string.Format("同步接收数据出错： {0}, {1}", iErrorCode, bizMessage.GetErrorInfo());
                Console.WriteLine(msg);
                return iRetCode;
            }

            CT2UnPacker unpacker = null;
            unsafe
            {
                int iLen = 0;
                void* lpdata = bizMessage.GetContent(&iLen);
                unpacker = new CT2UnPacker(lpdata, (uint)iLen);
            }

            int ret = (int)ConnectionCode.ErrorConn;
            if (unpacker != null)
            {
                DataParser parser = new DataParser();
                parser.Parse(unpacker);
                //parser.Output();
                FunctionCode functionCode = (FunctionCode)iFunction;
                if (_dataHandlerMap.ContainsKey(functionCode))
                {
                    _dataHandlerMap[functionCode](parser);
                    _dataHandlerMap.Remove(functionCode);

                    ret = (int)ConnectionCode.Success;
                }
                else
                {
                    ret = (int)ConnectionCode.ErrorNoCallback;
                }
            }
            else
            {
                ret = (int)ConnectionCode.ErrorFailContent;
            }

            return ret;
        }

        #region protect method

        protected void RegisterUFX(FunctionCode functionCode)
        {
            _t2SDKWrap.Register(functionCode, _receivedBizMsg);
        }

        protected void UnRegisterUFX(FunctionCode functionCode)
        {
            _t2SDKWrap.UnRegister(functionCode);
        }

        protected void AddDataHandler(FunctionCode functionCode, DataHandlerCallback callback)
        {
            if (!_dataHandlerMap.ContainsKey(functionCode))
            {
                _dataHandlerMap.Add(functionCode, callback);
            }
            else
            {
                _dataHandlerMap[functionCode] = callback;
            }
        }

        #endregion
    }
}
