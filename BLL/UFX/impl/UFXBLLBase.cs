﻿using Config;
using hundsun.t2sdk;
using log4net;
using Model;
using Model.Binding.BindingUtil;
using Model.config;
using System;
using System.Collections.Generic;

namespace BLL.UFX.impl
{
    public class UFXBLLBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected T2SDKWrap _t2SDKWrap;
        protected ReceivedBizMsg _receivedBizMsg;
        protected Dictionary<FunctionCode, Queue<Callbacker>> _dataHandlerMap = new Dictionary<FunctionCode, Queue<Callbacker>>();

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
            if (unpacker != null)
            {
                DataParser parser = new DataParser();
                parser.Parse(unpacker);
                parser.Output();
                FunctionCode functionCode = (FunctionCode)iFunction;
                if (_dataHandlerMap.ContainsKey(functionCode))
                {
                    var callbacker = GetDataHandler(functionCode);
                    var token = callbacker.Token;
                    var callback = callbacker.Callback;
                    if (callback != null)
                    {
                        callback(token, parser);
                    }

                    ret = (int)ConnectionCode.Success;
                }
                else
                {
                    ret = (int)ConnectionCode.ErrorNoCallback;
                    string msg = string.Format("提交UFX请求时，未注册功能号[{0}]的回调方法！", iFunction);
                    logger.Error(msg);
                }
            }
            else
            {
                ret = (int)ConnectionCode.ErrorFailContent;
                string msg = string.Format("提交UFX请求回调中，功能号[{0}]数据获取失败！", iFunction);
                logger.Error(msg);
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

        protected void AddDataHandler(FunctionCode functionCode, Callbacker callbacker)
        {
            if (!_dataHandlerMap.ContainsKey(functionCode))
            {
                if (!_dataHandlerMap[functionCode].Contains(callbacker))
                {
                    _dataHandlerMap[functionCode].Enqueue(callbacker);
                }
            }
            else
            {
                _dataHandlerMap[functionCode] = new Queue<Callbacker>();
                _dataHandlerMap[functionCode].Enqueue(callbacker);
            }
        }

        //protected void RemoveDataHandler(FunctionCode funtionCode, Callbacker callbacker)
        //{
        //    if (_dataHandlerMap.ContainsKey(funtionCode))
        //    {
        //        _dataHandlerMap[funtionCode].Dequeue(callbacker);
        //    }
        //}

        protected Callbacker GetDataHandler(FunctionCode functionCode)
        {
            Callbacker callbacker = null;

            if (_dataHandlerMap.ContainsKey(functionCode))
            {
                var cbList = _dataHandlerMap[functionCode];
                callbacker = cbList.Dequeue();
            }

            return callbacker;
        }

        #endregion

        #region send request

        public ConnectionCode Submit<T>(FunctionCode functionCode, List<T> requests, Callbacker callbacker)
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(functionCode);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                string msg = string.Format("提交UFX请求号[{0}]未定义！", functionCode);
                logger.Error(msg);
                return ConnectionCode.ErrorNoFunctionCode;
            }

            string userToken = LoginManager.Instance.LoginUser.Token;
            if (string.IsNullOrEmpty(userToken))
            {
                string msg = string.Format("提交UFX请求[{0}]令牌失效！", functionCode);
                logger.Error(msg);
                return ConnectionCode.ErrorLogin;
            }

            //注册UFX返回数据后，需要调用的回调
            AddDataHandler(functionCode, callbacker);

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)functionCode);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            var dataFieldMap = UFXDataBindingHelper.GetProperty<T>();
            foreach (var request in requests)
            {
                foreach (FieldItem item in functionItem.RequestFields)
                {
                    if (dataFieldMap.ContainsKey(item.Name))
                    {
                        SetRequestField<T>(ref packer, request, item, dataFieldMap);
                    }
                    else
                    {
                        SetRequestDefaultField(ref packer, item, userToken);
                    }
                }
            }
            packer.EndPack();

            unsafe
            {
                bizMessage.SetContent(packer.GetPackBuf(), packer.GetPackLen());
            }

            int retCode = _t2SDKWrap.SendAsync(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (retCode < 0)
            {
                string msg = string.Format("提交UFX请求[{0}]失败！", functionCode);
                logger.Error(msg);
                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        #endregion

        #region private

        private void SetRequestField<T>(ref CT2Packer packer, T request, FieldItem fieldItem, Dictionary<string, UFXDataField> dataFieldMap)
        {
            var dataField = dataFieldMap[fieldItem.Name];
            Type type = request.GetType();
            object obj = type.GetProperty(dataField.Name).GetValue(request);
            if (fieldItem.Type == PackFieldType.IntType)
            {
                if (obj != null)
                {
                    packer.AddInt((int)obj);
                }
                else
                {
                    packer.AddInt(-1);
                }
            }
            else if (fieldItem.Type == PackFieldType.FloatType)
            {
                if (obj != null)
                {
                    packer.AddDouble((double)obj);
                }
                else
                {
                    packer.AddDouble(0f);
                }
            }
            else if (fieldItem.Type == PackFieldType.StringType)
            {
                if (obj != null)
                {
                    packer.AddStr(obj.ToString());
                }
                else
                {
                    packer.AddStr("");
                }
            }
            else
            {
                packer.AddStr("");
            }
        }

        private void SetRequestDefaultField(ref CT2Packer packer, FieldItem fieldItem, string userToken)
        {
            switch (fieldItem.Name)
            {
                case "user_token":
                    {
                        packer.AddStr(userToken);
                    }
                    break;
                default:
                    if (fieldItem.Type == PackFieldType.IntType)
                    {
                        packer.AddInt(-1);
                    }
                    else if (fieldItem.Type == PackFieldType.StringType || fieldItem.Type == PackFieldType.CharType)
                    {
                        packer.AddStr(fieldItem.Name);
                    }
                    else
                    {
                        packer.AddStr(fieldItem.Name);
                    }
                    break;
            }
        }

        #endregion
    }
}
