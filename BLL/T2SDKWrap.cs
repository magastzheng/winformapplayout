﻿using hundsun.t2sdk;
using log4net;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public delegate int ReceivedBizMsg(CT2BizMessage lpMsg);

    public unsafe class T2SDKWrap : CT2CallbackInterface
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private CT2Configinterface _config = null;
        protected CT2Connection _conn = null;
        protected uint _timeOut = 10000;
        protected bool _isInit = false;
        private Dictionary<FunctionCode, ReceivedBizMsg> _bizCallbackMap = new Dictionary<FunctionCode, ReceivedBizMsg>();

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

        public T2SDKWrap()
        {
        }

        public ConnectionCode Connect()
        {
            //新建连接
            _config = new CT2Configinterface();
            int iRet = _config.Load("config/t2sdk.ini");

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

        public void Register(FunctionCode functionCode, ReceivedBizMsg receiver)
        {
            if (!_bizCallbackMap.ContainsKey(functionCode))
            {
                _bizCallbackMap[functionCode] = receiver;
            }
            else
            {
                _bizCallbackMap[functionCode] = receiver;
            }
        }

        public void UnRegister(FunctionCode functionCode)
        {
            if(!_bizCallbackMap.ContainsKey(functionCode))
            {
                _bizCallbackMap.Remove(functionCode);
            }
        }

        public int SendAsync(CT2BizMessage message)
        {
            int iRet = _conn.SendBizMsg(message, 1);
            if (iRet < 0)
            {
                logger.Error(string.Format("异步发送数据失败！ 错误码：{0}, 错误消息：{1}", iRet, _conn.GetErrorMsg(iRet)));
                return iRet;
            }

            return iRet;
        }

        public int SendSync(CT2BizMessage message)
        {
            int iRet = _conn.SendBizMsg(message, 0);
            if (iRet < 0)
            {
                logger.Error(string.Format("同步发送数据失败！ 错误码：{0}, 错误消息：{1}", iRet, _conn.GetErrorMsg(iRet)));
                return iRet;
            }

            CT2BizMessage bizMessage = null;
            int retCode = _conn.RecvBizMsg(iRet, out bizMessage, (int)_timeOut, 1);
            if (retCode < 0)
            {
                logger.Error("同步接收出错: " + _conn.GetErrorMsg(retCode));
                return (int)ConnectionCode.ErrorSendMsg;
            }

            int iFunction = bizMessage.GetFunction();
            if (Enum.IsDefined(typeof(FunctionCode), iFunction))
            {
                FunctionCode functionCode = (FunctionCode)Enum.ToObject(typeof(FunctionCode), iFunction);
                if (_bizCallbackMap.ContainsKey(functionCode) && _bizCallbackMap[functionCode] != null)
                {
                    retCode = _bizCallbackMap[functionCode](bizMessage);
                }
                else
                {
                    string msg = string.Format("异步接收数据回调函数找不到 - 功能号：{0}", iFunction);
                    logger.Error(msg);
                    return (int)ConnectionCode.ErrorNoCallback;
                }
            }
            else
            {
                string msg = string.Format("为支持的功能号：{0}", iFunction);
                logger.Error(msg);
                return (int)ConnectionCode.ErrorNoFunctionCode;
            }

            return retCode;
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
                    if (_bizCallbackMap.ContainsKey(functionCode) && _bizCallbackMap[functionCode] != null)
                    {
                        _bizCallbackMap[functionCode](lpMsg);
                    }
                    else
                    {
                        string msg = string.Format("异步接收数据回调函数找不到 - 功能号：{0}", iFunction);
                        logger.Error(msg);
                    }
                }
                else
                {
                    string msg = string.Format("为支持的功能号：{0}", iFunction);
                    logger.Error(msg);
                    //throw new NotSupportedException(msg);
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

        //public override void OnSent(CT2Connection lpConnection, int hSend, IntPtr lpData, int nLength, int nQueuingData)
        //{
        //    logger.Info("OnSend: 发送数据成功！");
        //}
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
