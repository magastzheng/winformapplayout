using Config;
using hundsun.t2sdk;
using log4net;
using Model;
using Model.config;
using System;
using System.Collections.Generic;

namespace BLL.UFX.impl
{
    public class LoginBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private T2SDKWrap _t2SDKWrap;
        //private ReceivedBizMsg _receivedBizMsg;
        private Dictionary<FunctionCode, DataHandlerCallback> _dataHandlerMap = new Dictionary<FunctionCode, DataHandlerCallback>();

        public LoginBLL(T2SDKWrap t2SDKWrap)
        {
            _t2SDKWrap = t2SDKWrap;
            //_receivedBizMsg = HandleReceivedBizMsg;
            _t2SDKWrap.Register(FunctionCode.Login, new DataHandlerCallback(HandlData));
            _t2SDKWrap.Register(FunctionCode.Logout, new DataHandlerCallback(HandlData));
            _t2SDKWrap.Register(FunctionCode.HeartBeat, new DataHandlerCallback(HandlData));
            _t2SDKWrap.Register(FunctionCode.QuerymemoryData, new DataHandlerCallback(HandlData));
            _t2SDKWrap.Register(FunctionCode.QueryAccount, new DataHandlerCallback(HandlData));
            _t2SDKWrap.Register(FunctionCode.QueryAssetUnit, new DataHandlerCallback(HandlData));
            _t2SDKWrap.Register(FunctionCode.QueryPortfolio, new DataHandlerCallback(HandlData));
            _t2SDKWrap.Register(FunctionCode.QueryTradingInstance, new DataHandlerCallback(HandlData));
            _t2SDKWrap.Register(FunctionCode.QueryHolder, new DataHandlerCallback(HandlData));
        }

        public ConnectionCode Login(User user)
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.Login);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorLogin;
            }

            LoginManager.Instance.LoginUser = user;
            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.Login);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            foreach (FieldItem item in functionItem.RequestFields)
            {
                switch (item.Name)
                {
                    case "operator_no":
                        packer.AddStr(user.Operator);
                        break;
                    case "password":
                        packer.AddStr(user.Password);
                        break;
                    case "mac_address":
                        {
                            packer.AddStr(ConfigManager.Instance.GetTerminalConfig().MacAddress);
                        }
                        break;
                    case "op_station":
                        {
                            packer.AddStr("www.htsec.com");
                        }
                        break;
                    case "ip_address":
                        {
                            packer.AddStr(ConfigManager.Instance.GetTerminalConfig().IPAddress);
                        }
                        break;
                    case "hd_volserial":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "authorization_id":
                        {
                            //TODO:
                            packer.AddStr("authorization_id");
                        }
                        break;
                    default:
                        if (item.Type == PackFieldType.IntType)
                        {
                            packer.AddInt(-1);
                        }
                        else if (item.Type == PackFieldType.StringType || item.Type == PackFieldType.CharType)
                        {
                            packer.AddStr(item.Name);
                        }
                        else
                        {
                            packer.AddStr(item.Name);
                        }
                        break;
                }
            }
            packer.EndPack();

            unsafe
            {
                bizMessage.SetContent(packer.GetPackBuf(), packer.GetPackLen());
            }

            int retCode = _t2SDKWrap.SendSync(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (retCode < 0)
            {
                logger.Error("登录失败!");
                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode Logout()
        {
      
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.Logout);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorLogin;
            }

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.Logout);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            foreach (FieldItem item in functionItem.RequestFields)
            {
                switch (item.Name)
                {
                    case "user_token":
                        packer.AddStr(LoginManager.Instance.LoginUser.Token);
                        break;
                    default:
                        break;
                }
            }

            packer.EndPack();

            unsafe
            {
                bizMessage.SetContent(packer.GetPackBuf(), packer.GetPackLen());
            }

            int retCode = _t2SDKWrap.SendSync(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (retCode < 0)
            {
                logger.Error("退出登录失败!");
                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode HeartBeat()
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.HeartBeat);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorLogin;
            }

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.HeartBeat);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            foreach (FieldItem item in functionItem.RequestFields)
            {
                switch (item.Name)
                {
                    case "user_token":
                        packer.AddStr(LoginManager.Instance.LoginUser.Token);
                        break;
                    default:
                        break;
                }
            }

            packer.EndPack();

            unsafe
            {
                bizMessage.SetContent(packer.GetPackBuf(), packer.GetPackLen());
            }

            int retCode = _t2SDKWrap.SendSync(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (retCode < 0)
            {
                logger.Error("心跳检测失败");

                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode QueryMemoryData()
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QuerymemoryData);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorLogin;
            }

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.QuerymemoryData);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            foreach (FieldItem item in functionItem.RequestFields)
            {
                switch (item.Name)
                {
                    case "user_token":
                        packer.AddStr(LoginManager.Instance.LoginUser.Token);
                        break;
                    case "table_name":
                        packer.AddStr("");
                        break;
                    default:
                        break;
                }
            }

            packer.EndPack();

            unsafe
            {
                bizMessage.SetContent(packer.GetPackBuf(), packer.GetPackLen());
            }

            int retCode = _t2SDKWrap.SendSync(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (retCode < 0)
            {
                logger.Error("查询内存数据失败");

                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode QueryAccount(DataHandlerCallback callback)
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QueryAccount);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorLogin;
            }

            AddDataHandler(FunctionCode.QueryAccount, callback);

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.QueryAccount);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            foreach (FieldItem item in functionItem.RequestFields)
            {
                switch (item.Name)
                {
                    case "user_token":
                        packer.AddStr(LoginManager.Instance.LoginUser.Token);
                        break;
                    case "account_code":
                        packer.AddStr("");
                        break;
                    default:
                        break;
                }
            }

            packer.EndPack();

            unsafe
            {
                bizMessage.SetContent(packer.GetPackBuf(), packer.GetPackLen());
            }

            int retCode = _t2SDKWrap.SendSync(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (retCode < 0)
            {
                logger.Error("账户查询失败");

                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode QueryAssetUnit(DataHandlerCallback callback)
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QueryAssetUnit);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorLogin;
            }

            AddDataHandler(FunctionCode.QueryAssetUnit, callback);

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.QueryAssetUnit);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            foreach (FieldItem item in functionItem.RequestFields)
            {
                switch (item.Name)
                {
                    case "user_token":
                        packer.AddStr(LoginManager.Instance.LoginUser.Token);
                        break;
                    case "capital_account":
                        packer.AddStr("");
                        break;
                    case "account_code":
                        packer.AddStr("");
                        break;
                    case "asset_no":
                        packer.AddStr("");
                        break;
                    default:
                        break;
                }
            }

            packer.EndPack();

            unsafe
            {
                bizMessage.SetContent(packer.GetPackBuf(), packer.GetPackLen());
            }

            int retCode = _t2SDKWrap.SendSync(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (retCode < 0)
            {
                logger.Error("资产单元查询失败");

                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode QueryPortfolio(DataHandlerCallback callback)
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QueryPortfolio);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorLogin;
            }

            AddDataHandler(FunctionCode.QueryPortfolio, callback);

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.QueryPortfolio);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            foreach (FieldItem item in functionItem.RequestFields)
            {
                switch (item.Name)
                {
                    case "user_token":
                        packer.AddStr(LoginManager.Instance.LoginUser.Token);
                        break;
                    case "capital_account":
                        packer.AddStr("");
                        break;
                    case "account_code":
                        packer.AddStr("");
                        break;
                    case "asset_no":
                        packer.AddStr("");
                        break;
                    case "combi_no":
                        packer.AddStr("");
                        break;
                    default:
                        break;
                }
            }

            packer.EndPack();

            unsafe
            {
                bizMessage.SetContent(packer.GetPackBuf(), packer.GetPackLen());
            }

            int retCode = _t2SDKWrap.SendSync(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (retCode < 0)
            {
                logger.Error("组合查询失败失败");

                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode QueryHolder(DataHandlerCallback callback)
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QueryHolder);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorLogin;
            }

            AddDataHandler(FunctionCode.QueryHolder, callback);

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.QueryHolder);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            foreach (FieldItem item in functionItem.RequestFields)
            {
                switch (item.Name)
                {
                    case "user_token":
                        packer.AddStr(LoginManager.Instance.LoginUser.Token);
                        break;
                    case "account_code":
                        packer.AddStr("");
                        break;
                    case "asset_no":
                        packer.AddStr("");
                        break;
                    case "combi_no":
                        packer.AddStr("");
                        break;
                    case "market_no":
                        packer.AddStr("");
                        break;
                    default:
                        break;
                }
            }

            packer.EndPack();

            unsafe
            {
                bizMessage.SetContent(packer.GetPackBuf(), packer.GetPackLen());
            }

            int retCode = _t2SDKWrap.SendSync(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (retCode < 0)
            {
                logger.Error("交易股东查询失败");

                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode QueryTrading()
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QueryTradingInstance);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorNoFunctionCode;
            }

            string userToken = LoginManager.Instance.LoginUser.Token;
            if (string.IsNullOrEmpty(userToken))
            {
                return ConnectionCode.ErrorLogin;
            }

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.QueryTradingInstance);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            //string[] account_code = new string[3] { "850010", "S54638", "SF0007" };

            //foreach (string s in account_code)
            //{
                foreach (FieldItem item in functionItem.RequestFields)
                {
                    switch (item.Name)
                    {
                        case "user_token":
                            {
                                packer.AddStr(userToken);
                            }
                            break;
                        case "account_group_code":
                            {
                                packer.AddStr("");
                            }
                            break;
                        case "instance_no":
                            {
                                packer.AddStr("");
                            }
                            break;
                        case "instance_type":
                            {
                                packer.AddStr("");
                            }
                            break;
                        case "ext_invest_plan_no_list":
                            {
                                packer.AddStr("");
                            }
                            break;
                        default:
                            if (item.Type == PackFieldType.IntType)
                            {
                                packer.AddInt(-1);
                            }
                            else if (item.Type == PackFieldType.StringType || item.Type == PackFieldType.CharType)
                            {
                                packer.AddStr(item.Name);
                            }
                            else
                            {
                                packer.AddStr(item.Name);
                            }
                            break;
                    }
                }
            //}
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
                logger.Error("查询交易实例失败!");
                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public int HandlData(DataParser parser)
        {
            int ret = -1;
            FunctionCode functionCode = (FunctionCode)parser.FunctionCode;
            var errResponse = T2ErrorHandler.Handle(parser);
            if (!T2ErrorHandler.Success(errResponse.ErrorCode))
            {
                string msg = string.Format("Fail to call the UFX - code: [{0}], msg: [{1}], details: [{2}]", errResponse.ErrorCode, errResponse.ErrorMessage, errResponse.MessageDetail);
                logger.Error(msg);
                return ret;
            }

            if (_dataHandlerMap.ContainsKey(functionCode))
            {
                _dataHandlerMap[functionCode](parser);
                _dataHandlerMap.Remove(functionCode);

                ret = 1;
            }
            else
            {
                switch (functionCode)
                {
                    case FunctionCode.Login:
                        {
                            string token = string.Empty;
                            if (parser.DataSets[1].Rows[0].Columns.ContainsKey("user_token"))
                            {
                                token = parser.DataSets[1].Rows[0].Columns["user_token"].GetStr();
                            }

                            if (!string.IsNullOrEmpty(token))
                            {
                                LoginManager.Instance.LoginUser.Token = token;
                                ret = (int)ConnectionCode.Success;
                            }
                            else
                            {
                                return (int)ConnectionCode.ErrorLogin;
                            }
                        }
                        break;
                    case FunctionCode.Logout:
                        break;
                    case FunctionCode.HeartBeat:
                        break;
                    case FunctionCode.QuerymemoryData:
                        break;
                    case FunctionCode.QueryAccount:
                        {
                            if (_dataHandlerMap.ContainsKey(FunctionCode.QueryAccount))
                            {
                                _dataHandlerMap[FunctionCode.QueryAccount](parser);

                                _dataHandlerMap.Remove(FunctionCode.QueryAccount);
                            }
                        }
                        break;
                    case FunctionCode.QueryAssetUnit:
                        break;
                    case FunctionCode.QueryPortfolio:
                        break;
                    case FunctionCode.QueryHolder:
                        break;
                    default:
                        break;
                }
            }

            return ret;
        }

        #region private method

        private void AddDataHandler(FunctionCode functionCode, DataHandlerCallback callback)
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
