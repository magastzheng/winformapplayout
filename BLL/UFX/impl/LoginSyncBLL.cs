using Config;
using hundsun.t2sdk;
using log4net;
using Model;
using Model.config;

namespace BLL.UFX.impl
{
    public class LoginSyncBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private T2SDKWrap _t2SDKWrap;

        public LoginSyncBLL(T2SDKWrap t2SDKWrap)
        {
            _t2SDKWrap = t2SDKWrap;
        }

        public ConnectionCode Login(LoginUser user)
        {
            FunctionCode functionCode = FunctionCode.Login;
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(functionCode);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorLogin;
            }

            LoginManager.Instance.LoginUser = user;
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

            var parser = _t2SDKWrap.SendSync2(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            var ret = parser.ErrorCode;
            if (ret == ConnectionCode.Success)
            {
                string token = string.Empty;
                string version = string.Empty;
                var response = T2ErrorHandler.Handle(parser);
                if (T2ErrorHandler.Success(response.ErrorCode))
                {
                    if (parser.DataSets[1].Rows[0].Columns.ContainsKey("user_token"))
                    {
                        token = parser.DataSets[1].Rows[0].Columns["user_token"].GetStr();
                    }

                    if (parser.DataSets[1].Rows[0].Columns.ContainsKey("version_no"))
                    {
                        version = parser.DataSets[1].Rows[0].Columns["version_no"].GetStr();
                    }

                    string msg = string.Format("Login success - token: [{0}], version: [{1}]", token, version);
                    UFXLogger.Info(logger, functionCode, msg);
                }
                else
                {
                    UFXLogger.Error(logger, functionCode, response);
                }

                if (!string.IsNullOrEmpty(token))
                {
                    LoginManager.Instance.LoginUser.Token = token;
                    ret = ConnectionCode.Success;
                }
                else
                {
                    ret = ConnectionCode.ErrorLogin;
                }
            }

            return ret;
        }

        public ConnectionCode Logout()
        {
            FunctionCode functionCode = FunctionCode.Logout;
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(functionCode);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
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

            var parser = _t2SDKWrap.SendSync2(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            var ret = parser.ErrorCode;
            if (ret == ConnectionCode.Success)
            {
                var response = T2ErrorHandler.Handle(parser);
                if (!T2ErrorHandler.Success(response.ErrorCode))
                {
                    UFXLogger.Error(logger, functionCode, response);
                    return ConnectionCode.ErrorFailContent;
                }
            }
            else
            {
                UFXLogger.Error(logger, functionCode, "退出登录失败!");
            }

            return ret;
        }

        public ConnectionCode HeartBeat()
        {
            FunctionCode functionCode = FunctionCode.HeartBeat;
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(functionCode);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
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

            var parser = _t2SDKWrap.SendSync2(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (parser.ErrorCode == ConnectionCode.Success)
            {
                var response = T2ErrorHandler.Handle(parser);
                if (!T2ErrorHandler.Success(response.ErrorCode))
                {
                    UFXLogger.Error(logger, functionCode, response);
                    return ConnectionCode.ErrorFailContent;
                }
            }
            else
            {
                UFXLogger.Error(logger, functionCode, "心跳检测失败");
                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }
    }
}
