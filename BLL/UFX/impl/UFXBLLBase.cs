using Config;
using hundsun.t2sdk;
using log4net;
using Model;
using Model.Binding.BindingUtil;
using Model.config;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.UFX.impl
{
    public class UFXBLLBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected T2SDKWrap _t2SDKWrap;
        protected DataHandlerCallback _dataHandler;
        protected Dictionary<FunctionCode, Dictionary<int, Callbacker>> _dataHandlerMap = new Dictionary<FunctionCode, Dictionary<int, Callbacker>>();

        public UFXBLLBase(T2SDKWrap t2SDKWrap)
        {
            _t2SDKWrap = t2SDKWrap;
            _dataHandler = HandlData;
        }

        public int HandlData(FunctionCode functionCode, int hSend, DataParser parser)
        {
            //FunctionCode functionCode = (FunctionCode)parser.FunctionCode;
            if (_dataHandlerMap.ContainsKey(functionCode))
            {
                var callbacker = GetDataHandler(functionCode, hSend);
                if (callbacker != null)
                {
                    var token = callbacker.Token;
                    var callback = callbacker.DataHandler;
                    if (callback != null)
                    {
                        callback(token, parser);
                    }

                    return 1;
                }
                else
                {
                    string msg = string.Format("提交UFX请求时，功能号[{0}]的回调方法，未注册的句柄ID[{0}]！", functionCode, hSend);
                    logger.Error(msg);

                    return -1;
                }
            }
            else
            {
                string msg = string.Format("提交UFX请求时，未注册功能号[{0}]的回调方法！", functionCode);
                logger.Error(msg);

                return -1;
            }
        }

        #region protect method

        protected void RegisterUFX(FunctionCode functionCode)
        {
            _t2SDKWrap.Register(functionCode, _dataHandler);
        }

        protected void UnRegisterUFX(FunctionCode functionCode)
        {
            _t2SDKWrap.UnRegister(functionCode);
        }

        protected void AddDataHandler(FunctionCode functionCode, int hSend, Callbacker callbacker)
        {
            if (_dataHandlerMap.ContainsKey(functionCode))
            {
                if (_dataHandlerMap[functionCode].ContainsKey(hSend))
                {
                    _dataHandlerMap[functionCode][hSend] = callbacker;
                }
                else
                {
                    _dataHandlerMap[functionCode].Add(hSend, callbacker);
                }
            }
            else
            {
                _dataHandlerMap[functionCode] = new Dictionary<int, Callbacker>();
                _dataHandlerMap[functionCode].Add(hSend, callbacker);
            }
        }

        //protected void RemoveDataHandler(FunctionCode funtionCode, Callbacker callbacker)
        //{
        //    if (_dataHandlerMap.ContainsKey(funtionCode))
        //    {
        //        _dataHandlerMap[funtionCode].Dequeue(callbacker);
        //    }
        //}

        protected Callbacker GetDataHandler(FunctionCode functionCode, int hSend)
        {
            Callbacker callbacker = null;

            if (_dataHandlerMap.ContainsKey(functionCode))
            {
                var sendCbMap = _dataHandlerMap[functionCode];
                if (sendCbMap.ContainsKey(hSend))
                {
                    callbacker = sendCbMap[hSend];
                    sendCbMap.Remove(hSend);
                }
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

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)functionCode);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();

            foreach (FieldItem item in functionItem.RequestFields)
            {
                if (item.Name.Equals("entrust_amount"))
                {
                    packer.AddField(item.Name, PackFieldType.FloatType, item.Width, item.Scale);
                }
                else
                {
                    packer.AddField(item.Name, item.Type, item.Width, item.Scale);
                }
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

#if DEBUG
            OutputParam<T>(functionCode, requests);
#endif
            unsafe
            {
                bizMessage.SetContent(packer.GetPackBuf(), packer.GetPackLen());
            }

            ConnectionCode retCode = ConnectionCode.Success;
            int hSend = _t2SDKWrap.SendAsync(bizMessage);
            if (hSend < 0)
            {
                string msg = string.Format("提交UFX请求[{0}]失败, 返回值：[{1}]！", functionCode, hSend);
                logger.Error(msg);
                retCode = ConnectionCode.ErrorConn;
            }
            else
            {
                //注册UFX返回数据后，需要调用的回调
                //此处存在假设，异步提交返回之前，不会触发回调
                AddDataHandler(functionCode, hSend, callbacker);
                retCode = ConnectionCode.Success;
            }

            packer.Dispose();
            bizMessage.Dispose();
            
            return retCode;
        }

        private void OutputParam<T>(FunctionCode functionCode, List<T> requests)
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(functionCode);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=========================================================");
            sb.AppendLine("FunctionCode: " + (int)functionCode);
            foreach (FieldItem item in functionItem.RequestFields)
            {
                if (item.Name.Equals("entrust_amount"))
                {
                    sb.AppendFormat("{0}|{1}|{2}|{3}\n", item.Name, PackFieldType.FloatType, item.Width, item.Scale);
                }
                else
                {
                    sb.AppendFormat("{0}|{1}|{2}|{3}\n", item.Name, item.Type, item.Width, item.Scale);
                }
            }

            var dataFieldMap = UFXDataBindingHelper.GetProperty<T>();
            foreach (var request in requests)
            {
                foreach (FieldItem item in functionItem.RequestFields)
                {
                    if (dataFieldMap.ContainsKey(item.Name))
                    {
                        var dataField = dataFieldMap[item.Name];
                        Type type = request.GetType();

                        object obj = type.GetProperty(dataField.Name).GetValue(request);
                        sb.AppendFormat("{0}:{1}|", item.Name, obj);
                    }
                    else
                    {
                        switch (item.Name)
                        {
                            case "user_token":
                                {
                                    sb.AppendFormat("{0}:{1}|", item.Name, LoginManager.Instance.LoginUser.Token);
                                }
                                break;
                            default:
                                if (item.Type == PackFieldType.IntType)
                                {
                                    sb.AppendFormat("{0}:{1}|", item.Name, -1);
                                }
                                else if (item.Type == PackFieldType.StringType || item.Type == PackFieldType.CharType)
                                {
                                    sb.AppendFormat("{0}:{1}|", item.Name, item.Name);
                                }
                                else
                                {
                                    sb.AppendFormat("{0}:{1}|", item.Name, item.Name);
                                }
                                break;
                        }
                    }
                }
                sb.Append("\n");
            }

            sb.AppendLine("=========================================================");
            logger.Info(sb.ToString());
        }

        #endregion

        #region private

        private void SetRequestField<T>(ref CT2Packer packer, T request, FieldItem fieldItem, Dictionary<string, UFXDataField> dataFieldMap)
        {
            var dataField = dataFieldMap[fieldItem.Name];
            Type type = request.GetType();
            object obj = type.GetProperty(dataField.Name).GetValue(request);
            if (fieldItem.Name.Equals("entrust_amount"))
            {
                //TODO: 委托数量字段跟文档定义不一样，文档定义是N12(整型)，但测试时传入整型出错。
                if (obj != null)
                {
                    packer.AddDouble((double)(int)obj);
                    //packer.AddInt((int)obj);
                }
                else
                {
                    packer.AddDouble(0f);
                    //packer.AddInt(0);
                }
            }
            else if (fieldItem.Type == PackFieldType.IntType)
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
