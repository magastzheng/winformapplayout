using BLL.UFX.impl;
using hundsun.mcapi;
using hundsun.t2sdk;
using log4net;
using Model;
using Model.UFX;
using System;

namespace BLL.UFX
{
    public unsafe class T2Subscriber : CT2CallbackInterface
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string SubscribeName = "ufx_subscribe";

        private CT2Configinterface _config = null;
        protected CT2Connection _conn = null;
        protected uint _timeOut = 10000;
        protected bool _isInit = false;

        private T2SubCallback callback = null;
        private CT2SubscribeInterface subcribe = null;

        public T2Subscriber(uint timeOut)
        {
            _timeOut = timeOut;
        }

        public ConnectionCode Connect()
        {
            //新建连接
            _config = new CT2Configinterface();
            int iRet = _config.Load("config/t2sdk-subscriber.ini");

            if (iRet != 0)
            {
                string msg = "主推业务读取[config/t2sdk-subscriber.ini]连接配置对象失败！";
                logger.Error(msg);
                return ConnectionCode.ErrorReadConf;
            }

            _conn = new CT2Connection(_config);

            //使用Create2BizMsg调用，则收到业务回调是OnReceivedBizMsg
            iRet = _conn.Create2BizMsg(this);
            if (iRet != 0)
            {
                string msg = string.Format("主推业务连接对象初始化失败: {0}, {1}", iRet, _conn.GetErrorMsg(iRet));
                logger.Error(msg);
                return ConnectionCode.ErrorInitConn;
            }

            try
            {
                iRet = _conn.Connect(_timeOut);
            }
            catch (Exception e)
            {
                string msg = e.Message;
                logger.Error(msg);
            }
            if (iRet != 0)
            {
                string msg = string.Format("主推业务连接失败: {0}, {1}", iRet, _conn.GetErrorMsg(iRet));
                logger.Error(msg);

                return ConnectionCode.ErrorConn;
            }
            else
            {
                string msg = "主推业务连接成功";
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

        public ConnectionCode Subscribe(LoginUser user)
        {
            callback = new T2SubCallback();
            subcribe = _conn.NewSubscriber(callback, SubscribeName, (int)_timeOut, 2000, 100);
            if (subcribe == null)
            {
                string msg = string.Format("主推业务订阅创建失败: {0}", _conn.GetMCLastError());
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

            CT2UnPacker unpacker = null;
            int ret = subcribe.SubscribeTopicEx(args, 50000, out unpacker, req);
            req.Dispose();
            //根据文档说明，返回值大于0表示有效的订阅标识，否则其他表示错误。
            if (ret > 0)
            {
                string msg = string.Format("主推业务订阅创建成功, 返回码: {0}, 消息: {1}", ret, _conn.GetErrorMsg(ret));
                logger.Info(msg);
                return ConnectionCode.SuccessSubscribe;
            }
            else
            {
                string outMsg = string.Empty;
                if (unpacker != null)
                {
                    //Show(back);
                    DataParser parser = new DataParser();
                    parser.Parse(unpacker);
                    unpacker.Dispose();

                    var errResponse = T2ErrorHandler.Handle(parser);
                    outMsg = errResponse.MessageDetail;
                }

                string msg = string.Format("主推业务订阅创建失败,返回码: {0}, 消息: {1}, 返回数据包消息: {2}", ret, _conn.GetErrorMsg(ret), outMsg);
                logger.Error(msg);
                return ConnectionCode.ErrorFailSubscribe;
            }
        }

        #region Implement the interface CT2CallbackInterface
        public override void OnClose(CT2Connection lpConnection)
        {
            //_conn.Close();
            UFXLogger.Info(logger, "OnClose: 连接断开！");
        }

        public override void OnConnect(CT2Connection lpConnection)
        {
            UFXLogger.Info(logger, "OnConnect:连接成功建立！");
        }

        public override void OnReceivedBiz(CT2Connection lpConnection, int hSend, string lppStr, CT2UnPacker lppUnPacker, int nResult)
        {
            UFXLogger.Info(logger, "OnReceivedBiz:成功触发回调接收数据！");
        }

        public override void OnReceivedBizEx(CT2Connection lpConnection, int hSend, CT2RespondData lpRetData, string lppStr, CT2UnPacker lppUnPacker, int nResult)
        {
            UFXLogger.Info(logger, "OnReceivedBizEx:成功触发回调接收数据！");
        }

        public override void OnReceivedBizMsg(CT2Connection lpConnection, int hSend, CT2BizMessage lpMsg)
        {
            UFXLogger.Info(logger, "OnReceivedBizMsg: 成功触发回调接收数据！");
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
                //Console.WriteLine("主推业务订阅 - 返回码：{0}, 错误码： {1}, 功能号： {2}", iRetCode, iErrorCode, iFunction);
                if (Enum.IsDefined(typeof(UFXFunctionCode), iFunction))
                {
                    UFXFunctionCode functionCode = (UFXFunctionCode)Enum.ToObject(typeof(UFXFunctionCode), iFunction);
                    switch (functionCode)
                    { 
                        case UFXFunctionCode.MCHeartBeat:
                            break;
                        case UFXFunctionCode.MCRecvSubscribe:
                            break;
                        case UFXFunctionCode.MCRecvUnsubscribe:
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
            UFXLogger.Info(logger, "OnRegister: 服务端成功注册！");
        }

        public override void OnSafeConnect(CT2Connection lpConnection)
        {
            UFXLogger.Info(logger, "OnSafeConnect: 成功建立安全连接！");
        }

        public override void OnSent(CT2Connection lpConnection, int hSend, void* lpData, int nLength, int nQueuingData)
        {
            UFXLogger.Info(logger, "OnSend: 发送数据成功！");
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
