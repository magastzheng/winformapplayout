using UFX.impl;
using hundsun.t2sdk;
using log4net;
using Model;
using Model.UFX;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util;

namespace UFX
{
    public delegate int ReceivedBizMsg(CT2BizMessage lpMsg);

    public enum SendType
    { 
        Sync = 0,
        Async = 1,
    }

    public unsafe class T2SDKWrap : CT2CallbackInterface
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private CT2Configinterface _config = null;
        protected CT2Connection _conn = null;
        protected uint _timeOut = 10000;
        protected bool _isInit = false;
        private Dictionary<UFXFunctionCode, DataHandlerCallback> _dataHandlerMap = new Dictionary<UFXFunctionCode, DataHandlerCallback>();
        //private Dictionary<FunctionCode, Dictionary<int, DataHandlerCallback>> _dataHandlerMap = new Dictionary<FunctionCode, Dictionary<int, DataHandlerCallback>>();

        private TaskScheduler _taskScheduler = new LimitedConcurrencyLevelTaskScheduler(2);

        public bool IsInit 
        { 
            get 
            { 
                return _isInit; 
            }
            set
            {
                _isInit = value;
            }
        }

        public T2SDKWrap(uint timeOut)
        {
            _timeOut = timeOut;
        }

        /// <summary>
        /// 创建连接。在使用UFX接口之前，必须调用本接口初始化UFX接口，并创建连接。只有连接成功创建之后，才可以调用其他接口
        /// </summary>
        /// <returns>返回连接码表示创建连接是否成功。Success表示成功，其他为失败。</returns>
        public ConnectionCode Connect()
        {
            //新建连接
            _config = new CT2Configinterface();
            int iRet = _config.Load("config/t2sdk.ini");

            if (iRet != 0)
            {
                string msg = "读取[config/t2sdk.ini]连接配置对象失败！";
                logger.Error(msg);
                return ConnectionCode.ErrorReadConf;
            }

            _conn = new CT2Connection(_config);

            //使用Create2BizMsg调用，则收到业务回调是OnReceivedBizMsg
            iRet = _conn.Create2BizMsg(this);
            if (iRet != 0)
            {
                string msg = string.Format("一般交易业务连接对象初始化失败: {0}, {1}", iRet, _conn.GetErrorMsg(iRet));
                logger.Error(msg);
                return ConnectionCode.ErrorInitConn;
            }

            try
            {
                iRet = _conn.Connect(_timeOut);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                logger.Error(e.Message);
            }
            if (iRet != 0)
            {
                string msg = string.Format("一般交易业务连接失败: {0}, {1}", iRet, _conn.GetErrorMsg(iRet));
                logger.Error(msg);
                return ConnectionCode.ErrorConn;
            }
            else
            {
                string msg = "一般交易业务连接成功";
                logger.Info(msg);
            }

            _isInit = true;

            return ConnectionCode.Success;
        }

        /// <summary>
        /// 关闭连接。使用完成之后，应该关闭连接。
        /// </summary>
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

        /// <summary>
        /// 注册功能号和委托，如果针对该功能号的调用成功，就会触发该功能号委托的回调。这里所说的调用成功，指的是针对UFX接口
        /// 的调用顺利，这个过程之后有可能会触发参数错误等各种问题，而不表示调用结果正确，获得正确的返回值。
        /// 因此，获取返回数据后，还需要通过分析数据中的返回码来确定本次调用是否无误。
        /// 注意：每个注册码只会保留最后一次注册的委托，多次注册时，先注册的委托会被后面的覆盖掉。
        /// </summary>
        /// <param name="functionCode">功能号</param>
        /// <param name="receiver">本功能号的委托</param>
        public void Register(UFXFunctionCode functionCode, DataHandlerCallback receiver)
        {
            if (!_dataHandlerMap.ContainsKey(functionCode))
            {
                _dataHandlerMap[functionCode] = receiver;
            }
            else
            {
                _dataHandlerMap[functionCode] = receiver;
            }
        }

        public void UnRegister(UFXFunctionCode functionCode)
        {
            if (!_dataHandlerMap.ContainsKey(functionCode))
            {
                _dataHandlerMap.Remove(functionCode);
            }
        }

        /// <summary>
        /// 通过调用UFX接口异步发送请求，调用前必须确保已经注册了功能号和回调代理，即调用Register函数注册。
        /// 同一功能号注册多次只会保留最后一次结果。
        /// </summary>
        /// <param name="message">CT2BizMessage对象的实例，包含用户信息，功能号，请求参数等信息</param>
        /// <returns>返回正值表示发送句柄，否则表示失败。</returns>
        public int SendAsync(CT2BizMessage message)
        {
            int hSend = _conn.SendBizMsg(message, (int)SendType.Async);
            if (hSend < 0)
            {
                string msg = string.Format("一般交易业务异步发送数据失败！ 错误码：{0}, 错误消息：{1}", hSend, _conn.GetErrorMsg(hSend));
                logger.Error(msg);
                return hSend;
            }

            return hSend;
        }

        /// <summary>
        /// 通过调用UFX接口同步发送请求，调用前必须确保已经注册了功能号和回调代理，即调用Register函数注册。
        /// 同一功能号注册多次只会保留最后一次结果。
        /// </summary>
        /// <param name="message">CT2BizMessage对象的实例，包含用户信息，功能号，请求参数等信息</param>
        /// <returns>返回正值表示发送句柄，否则表示失败。</returns>
        public int SendSync(CT2BizMessage message)
        {
            int iRet = _conn.SendBizMsg(message, (int)SendType.Sync);
            if (iRet < 0)
            {
                string msg = string.Format("一般交易业务同步发送数据失败！ 错误码：{0}, 错误消息：{1}", iRet, _conn.GetErrorMsg(iRet));
                logger.Error(msg);
                return iRet;
            }

            CT2BizMessage bizMessage = null;
            int retCode = _conn.RecvBizMsg(iRet, out bizMessage, (int)_timeOut, 1);
            if (retCode < 0)
            {
                string msg = "一般交易业务同步接收出错: " + _conn.GetErrorMsg(retCode);
                logger.Error(msg);
                return (int)ConnectionCode.ErrorSendMsg;
            }

            int iFunction = bizMessage.GetFunction();
            if (Enum.IsDefined(typeof(UFXFunctionCode), iFunction))
            {
                return HandleReceivedBizMsg(SendType.Sync, (UFXFunctionCode)iFunction, retCode, bizMessage);
            }
            else
            {
                string msg = string.Format("一般交易业务未支持的功能号：{0}", iFunction);
                logger.Error(msg);
                return (int)ConnectionCode.ErrorNoFunctionCode;
            }
        }

        /// <summary>
        /// 通过调用UFX接口同步发送请求，并将结果封装在DataParser对象中返回。
        /// </summary>
        /// <param name="message">CT2BizMessage对象的实例，包含用户信息，功能号，请求参数等信息</param>
        /// <returns>DataParser对象实例，包含错误代码和最终数据。</returns>
        public DataParser SendSync2(CT2BizMessage message)
        {
            DataParser dataParser = new DataParser();

            int iRet = _conn.SendBizMsg(message, (int)SendType.Sync);
            if (iRet < 0)
            {
                string msg = string.Format("一般交易业务同步发送数据失败！ 错误码：{0}, 错误消息：{1}", iRet, _conn.GetErrorMsg(iRet));
                logger.Error(msg);
                dataParser.ErrorCode = ConnectionCode.ErrorSendMsg;

                return dataParser;
            }

            CT2BizMessage bizMessage = null;
            int retCode = _conn.RecvBizMsg(iRet, out bizMessage, (int)_timeOut, 1);
            if (retCode < 0)
            {
                string msg = "一般交易业务同步接收出错: " + _conn.GetErrorMsg(retCode);
                logger.Error(msg);
                dataParser.ErrorCode = ConnectionCode.ErrorRecvMsg;

                return dataParser;
            }

            int iFunction = bizMessage.GetFunction();
            if (!Enum.IsDefined(typeof(UFXFunctionCode), iFunction))
            {
                dataParser.ErrorCode = ConnectionCode.ErrorNoFunctionCode;

                return dataParser;
            }

            dataParser.FunctionCode = (UFXFunctionCode)iFunction;

            int iRetCode = bizMessage.GetReturnCode();
            int iErrorCode = bizMessage.GetErrorNo();
            UFXFunctionCode functionCode = (UFXFunctionCode)iFunction;
            if (iRetCode != 0)
            {
                string msg = string.Format("同步接收数据出错： {0}, {1}", iErrorCode, bizMessage.GetErrorInfo());
                //Console.WriteLine(msg);
                logger.Error(msg);

                dataParser.ErrorCode = ConnectionCode.ErrorRecvMsg;
                return dataParser;
            }

            CT2UnPacker unpacker = null;
            unsafe
            {
                int iLen = 0;
                void* lpdata = bizMessage.GetContent(&iLen);
                unpacker = new CT2UnPacker(lpdata, (uint)iLen);
            }

            if (unpacker == null)
            {
                string msg = string.Format("提交UFX请求回调中，功能号[{0}]数据获取失败！", iFunction);
                logger.Error(msg);

                dataParser.ErrorCode = ConnectionCode.ErrorFailContent;
            }
            else
            {
                dataParser.Parse(unpacker);
                unpacker.Dispose();

                dataParser.ErrorCode = ConnectionCode.Success;
            }

            return dataParser;
        }

        public void PrintUnPack(CT2UnPacker lpUnPack)
        {
            Console.WriteLine("记录行数： {0}", lpUnPack.GetRowCount());
            Console.WriteLine("列行数：{0}", lpUnPack.GetColCount());

            for (int i = 0; i < lpUnPack.GetDatasetCount(); i++)
            {
                //设置当前结果集
                lpUnPack.SetCurrentDatasetByIndex(i);

                //打印字段
                for (int j = 0; j < lpUnPack.GetColCount(); j++)
                {
                    Console.Write("{0,20:G}", lpUnPack.GetColName(j));
                }

                Console.WriteLine();

                //打印所有记录
                for (int k = 0; k < lpUnPack.GetRowCount(); k++)
                {
                    //打印每条记录
                    for (int t = 0; t < lpUnPack.GetColCount(); t++)
                    {
                        switch (lpUnPack.GetColType(t))
                        {
                            case (sbyte)'I':  //I 整数
                                Console.Write("{0,20:D}", lpUnPack.GetIntByIndex(t));
                                break;
                            case (sbyte)'C':  //C 
                                Console.Write("{0,20:G}", (char)lpUnPack.GetCharByIndex(t));
                                break;
                            case (sbyte)'S':   //S
                                Console.Write("{0,20:G}", lpUnPack.GetStrByIndex(t));
                                break;
                            case (sbyte)'F':  //F
                                Console.Write("{0,20:F2}", lpUnPack.GetDoubleByIndex(t));
                                break;
                            case (sbyte)'R':  //R
                                {
                                    break;
                                }
                            default:
                                // 未知数据类型
                                Console.Write("未知数据类型\n");
                                break;
                        }
                    }

                    Console.WriteLine();
                    lpUnPack.Next();
                }
            }

            Console.WriteLine();


            /*
            while (lpUnPack.IsEOF() != 1)
            {
                for (int i = 0; i < lpUnPack.GetColCount(); i++)
                {
                    String colName = lpUnPack.GetColName(i);
                    sbyte colType = lpUnPack.GetColType(i);
                    if (!colType.Equals('R'))
                    {
                        String colValue = lpUnPack.GetStrByIndex(i);
                        Console.WriteLine("{0}：{1}", colName, colValue);
                    }
                    else
                    {
                        int colLength = 0;
                        unsafe
                        {
                            void* colValue = (char*)lpUnPack.GetRawByIndex(i, &colLength);
                            string str = String.Format("{0}:[{1}]({2})", colName, Marshal.PtrToStringAuto(new IntPtr(colValue)), colLength);
                        }
                    }
                }
                lpUnPack.Next();
            }
            */

        }

        #region private method

        #endregion

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
                HandleReceivedBizMsg(SendType.Async, (UFXFunctionCode)iFunction, hSend, lpMsg);
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

        //public override void OnSent(CT2Connection lpConnection, int hSend, IntPtr lpData, int nLength, int nQueuingData)
        //{
        //    logger.Info("OnSend: 发送数据成功！");
        //}
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

        #region private

        private int HandleReceivedBizMsg(SendType sendType, UFXFunctionCode functionCode, int hSend, CT2BizMessage bizMessage)
        {
            int iRetCode = bizMessage.GetReturnCode();
            int iErrorCode = bizMessage.GetErrorNo();
            int iFunction = bizMessage.GetFunction();
            //FunctionCode functionCode = (FunctionCode)iFunction;
            if (iRetCode != 0)
            {
                string msg = string.Format("同步接收数据出错： {0}, {1}", iErrorCode, bizMessage.GetErrorInfo());
                logger.Error(msg);
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
            if (unpacker == null)
            {
                ret = (int)ConnectionCode.ErrorFailContent;
                string msg = string.Format("提交UFX请求回调中，功能号[{0}]数据获取失败！", iFunction);
                logger.Error(msg);

                return ret;
            }

            DataParser parser = new DataParser();
            parser.FunctionCode = functionCode;
            parser.Parse(unpacker);
            unpacker.Dispose();
            
            //parser.Output();
            if (!_dataHandlerMap.ContainsKey(functionCode))
            {
                ret = (int)ConnectionCode.ErrorNoCallback;
                string msg = string.Format("提交UFX请求时，未注册功能号[{0}]的回调方法！", iFunction);
                logger.Error(msg);

                return ret;
            }

            var dataHandler = _dataHandlerMap[functionCode];
            if (dataHandler == null)
            {
                ret = (int)ConnectionCode.ErrorNoCallback;
                string msg = string.Format("提交UFX请求时，未注册功能号[{0}]的回调方法！", iFunction);
                logger.Error(msg);

                return ret;
            }

            if (sendType == SendType.Sync)
            {
                return dataHandler(functionCode, hSend, parser);
            }
            else
            {
                //TODO: control the maximum number of the thread?
                //Task task = new Task(() => dataHandler(parser));
                //task.Start();

                //use the TaskScheduler to limit the maximum thread number
                TaskFactory taskFactory = new TaskFactory(_taskScheduler);
                taskFactory.StartNew(() => dataHandler(functionCode, hSend, parser));

                return (int)ConnectionCode.Success;
            }
        }

        #endregion
    }
}
