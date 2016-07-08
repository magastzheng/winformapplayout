using hundsun.mcapi;
using hundsun.t2sdk;
using log4net;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UFX
{
    public unsafe class T2Subscriber : CT2CallbackInterface
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private CT2Configinterface _config = null;
        protected CT2Connection _conn = null;
        protected uint _timeOut = 10000;
        protected bool _isInit = false;
        //private Dictionary<FunctionCode, ReceivedBizMsg> _bizCallbackMap = new Dictionary<FunctionCode, ReceivedBizMsg>();
        private T2SubCallback callback = null;
        private CT2SubscribeInterface subcribe = null;

        public T2Subscriber()
        { 
            
        }

        public ConnectionCode Connect()
        {
            //新建连接
            _config = new CT2Configinterface();
            int iRet = _config.Load("config/t2sdk-subscriber.ini");

            if (iRet != 0)
            {
                string msg = "读取连接配置对象失败！";
                logger.Error(msg);
                return ConnectionCode.ErrorReadConf;
            }

            _conn = new CT2Connection(_config);

            //使用Create2BizMsg调用，则收到业务回调是OnReceivedBizMsg
            iRet = _conn.Create2BizMsg(this);
            if (iRet != 0)
            {
                string msg = string.Format("连接对象初始化失败: {0}, {1}", iRet, _conn.GetErrorMsg(iRet));
                logger.Error(msg);
                return ConnectionCode.ErrorInitConn;
            }

            try
            {
                iRet = _conn.Connect(_timeOut);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (iRet != 0)
            {
                string msg = string.Format("连接失败: {0}, {1}", iRet, _conn.GetErrorMsg(iRet));
                logger.Error(msg);
                return ConnectionCode.ErrorConn;
            }
            else
            {
                string msg = "连接成功";
                logger.Info(msg);
            }

            _isInit = true;

            return ConnectionCode.Success;
        }

        public void Close()
        {
            if (_conn != null)
            {
                _conn.Close();
            }
            if (_isInit)
            {
                _isInit = false;
            }
        }

        //public void Register(FunctionCode functionCode, ReceivedBizMsg receiver)
        //{
        //    if (!_bizCallbackMap.ContainsKey(functionCode))
        //    {
        //        _bizCallbackMap[functionCode] = receiver;
        //    }
        //    else
        //    {
        //        _bizCallbackMap[functionCode] = receiver;
        //    }
        //}

        //public void UnRegister(FunctionCode functionCode)
        //{
        //    if (!_bizCallbackMap.ContainsKey(functionCode))
        //    {
        //        _bizCallbackMap.Remove(functionCode);
        //    }
        //}

        public ConnectionCode Subscribe(User user)
        {
            callback = new T2SubCallback();
            subcribe = _conn.NewSubscriber(callback, "ufx_subscribe", (int)_timeOut, 2000, 100);
            if (subcribe == null)
            { 
                string msg = string.Format("订阅创建失败: {0}", _conn.GetMCLastError());
                logger.Error(msg);
                return ConnectionCode.ErrorFailSubscribe;
            }

            CT2SubscribeParamInterface args = new CT2SubscribeParamInterface();
            args.SetTopicName("ufx_topic");
            args.SetReplace(false);
            args.SetFilter("operator_no", user.Operator);

            CT2Packer req = new CT2Packer(2);
            req.BeginPack();
            req.AddField("login_operator_no", Convert.ToSByte('S'), 16, 4);
            req.AddField("password", Convert.ToSByte('S'), 16, 4);
            req.AddStr(user.Operator);
            req.AddStr(user.Password);
            req.EndPack();

            CT2UnPacker back = null;
            int ret = subcribe.SubscribeTopicEx(args, 50000, out back, req);
            req.Dispose();

            if (ret > 0)
            {
                string msg = string.Format("订阅创建失败: {0}", _conn.GetMCLastError());
                logger.Error(msg);
                return ConnectionCode.SuccessSubscribe;
            }
            else
            {
                if (back != null)
                {
                    //Show(back);
                }

                return ConnectionCode.ErrorFailSubscribe;
            }
        }

        #region Implement the interface CT2CallbackInterface
        public override void OnClose(CT2Connection lpConnection)
        {
            //_conn.Close();
            logger.Info("OnClose: 连接断开！");
        }

        public override void OnConnect(CT2Connection lpConnection)
        {
            logger.Info("OnConnect:连接成功建立！");
        }

        public override void OnReceivedBiz(CT2Connection lpConnection, int hSend, string lppStr, CT2UnPacker lppUnPacker, int nResult)
        {
            throw new NotImplementedException();
        }

        public override void OnReceivedBizEx(CT2Connection lpConnection, int hSend, CT2RespondData lpRetData, string lppStr, CT2UnPacker lppUnPacker, int nResult)
        {
            throw new NotImplementedException();
        }

        public override void OnReceivedBizMsg(CT2Connection lpConnection, int hSend, CT2BizMessage lpMsg)
        {
            logger.Info("OnReceivedBizMsg: 成功建立安全连接！");
            //获取返回码
            int iRetCode = lpMsg.GetReturnCode();

            //获取错误码
            int iErrorCode = lpMsg.GetErrorNo();

            int iFunction = lpMsg.GetFunction();

            if (iRetCode != 0)
            {
                logger.Error("异步接收数据出错：" + lpMsg.GetErrorNo().ToString() + lpMsg.GetErrorInfo());
            }
            else
            {
                if (Enum.IsDefined(typeof(FunctionCode), iFunction))
                {
                    FunctionCode functionCode = (FunctionCode)Enum.ToObject(typeof(FunctionCode), iFunction);
                    switch (functionCode)
                    { 
                        case FunctionCode.MCHeartBeat:
                            break;
                        case FunctionCode.MCRecvSubscribe:
                            break;
                        case FunctionCode.MCRecvUnsubscribe:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    string msg = string.Format("为支持的功能号：{0}", iFunction);
                    logger.Error(msg);
                }
            }
        }

        public override void OnRegister(CT2Connection lpConnection)
        {
            logger.Info("OnRegister: 服务端成功注册！");
        }

        public override void OnSafeConnect(CT2Connection lpConnection)
        {
            logger.Info("OnSafeConnect: 成功建立安全连接！");
        }

        public override void OnSent(CT2Connection lpConnection, int hSend, void* lpData, int nLength, int nQueuingData)
        {
            logger.Info("OnSend: 发送数据成功！");
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            if (_config != null)
            {
                _config.Dispose();
            }

            if (_conn != null)
            {
                _conn.Dispose();
            }
            _config = null;
            _conn = null;
        }
        #endregion
    }
}
