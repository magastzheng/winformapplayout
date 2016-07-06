using BLL.UFX;
using Config;
using hundsun.t2sdk;
using log4net;
using Model;
using Model.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StrategyBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private T2SDKWrap _t2SDKWrap;
        private ReceivedBizMsg _receivedBizMsg;

        public StrategyBLL(T2SDKWrap t2SDKWrap)
        {
            _t2SDKWrap = t2SDKWrap;
            _receivedBizMsg = OnReceivedBizMsg;

            _t2SDKWrap.Register(FunctionCode.QueryTradingInstance, _receivedBizMsg);
            _t2SDKWrap.Register(FunctionCode.EntrustInstanceBasket, _receivedBizMsg);
            _t2SDKWrap.Register(FunctionCode.QueryEntrustInstance, _receivedBizMsg);
            _t2SDKWrap.Register(FunctionCode.QueryDealInstance, _receivedBizMsg);
            _t2SDKWrap.Register(FunctionCode.WithdrawBasket, _receivedBizMsg);
            _t2SDKWrap.Register(FunctionCode.QuerySpotTemplate, _receivedBizMsg);
            _t2SDKWrap.Register(FunctionCode.QuerySpotTemplateStock, _receivedBizMsg);
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
                        //if (item.Type == PackFieldType.IntType)
                        //{
                        //    packer.AddInt(-1);
                        //}
                        //else if (item.Type == PackFieldType.StringType || item.Type == PackFieldType.CharType)
                        //{
                        //    packer.AddStr(item.Name);
                        //}
                        //else
                        //{
                        //    packer.AddStr(item.Name);
                        //}
                        break;
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
                logger.Error("查询交易实例失败!");
                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode EntrustBasket()
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.EntrustInstanceBasket);
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
            bizMessage.SetFunction((int)FunctionCode.EntrustInstanceBasket);
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
                        {
                            packer.AddStr(userToken);
                        }
                        break;
                    case "batch_no":
                        {
                            packer.AddInt(0);
                        }
                        break;
                    case "account_group_code":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "instance_no":
                        {
                            packer.AddInt(0);
                        }
                        break;
                    case "market_no":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "stock_code":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "entrust_direction":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "futures_direction":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "convered_flag":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "price_type":
                        {
                            packer.AddDouble(0.0f);
                        }
                        break;
                    case "entrust_price":
                        {
                            packer.AddDouble(0.0f);
                        }
                        break;
                    case "entrust_amount":
                        {
                            packer.AddInt(0);
                        }
                        break;
                    case "limit_entrust_ratio":
                        {
                            packer.AddDouble(0.0f);
                        }
                        break;
                    case "extsystem_id":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "third_reff":
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

        public ConnectionCode QueryEntrust()
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QueryEntrustInstance);
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
            bizMessage.SetFunction((int)FunctionCode.QueryEntrustInstance);
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
                            packer.AddInt(0);
                        }
                        break;
                    case "ext_invest_plan_no_list":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "batch_no":
                        {
                            packer.AddInt(0);
                        }
                        break;
                    case "entrust_no":
                        {
                            packer.AddInt(0);
                        }
                        break;
                    case "market_no":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "stock_code":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "entrust_direction":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "futures_direction":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "entrust_state_list":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "extsystem_id":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "third_reff":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "position_str":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "request_num":
                        {
                            packer.AddInt(0);
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

            int retCode = _t2SDKWrap.SendAsync(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (retCode < 0)
            {
                logger.Error("委托实例失败!");
                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode QueryDeal()
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QueryDealInstance);
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
            bizMessage.SetFunction((int)FunctionCode.QueryDealInstance);
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
                            packer.AddInt(0);
                        }
                        break;
                    case "ext_invest_plan_no_list":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "batch_no":
                        {
                            packer.AddInt(0);
                        }
                        break;
                    case "entrust_no":
                        {
                            packer.AddInt(0);
                        }
                        break;
                    case "market_no":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "stock_code":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "entrust_direction":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "futures_direction":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "extsystem_id":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "third_reff":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "position_str":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "request_num":
                        {
                            packer.AddInt(0);
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

            int retCode = _t2SDKWrap.SendAsync(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (retCode < 0)
            {
                logger.Error("查询成交实例失败!");
                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode WithdrawBasket()
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.WithdrawBasket);
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
            bizMessage.SetFunction((int)FunctionCode.WithdrawBasket);
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
                        {
                            packer.AddStr(userToken);
                        }
                        break;
                    case "batch_no":
                        {
                            packer.AddInt(0);
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

            int retCode = _t2SDKWrap.SendAsync(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (retCode < 0)
            {
                logger.Error("撤销委托实例失败!");
                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode QueryTemplate()
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QuerySpotTemplate);
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
            bizMessage.SetFunction((int)FunctionCode.QuerySpotTemplate);
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
                        {
                            packer.AddStr(userToken);
                        }
                        break;
                    case "template_no":
                        {
                            packer.AddInt(-1);
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

            int retCode = _t2SDKWrap.SendAsync(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (retCode < 0)
            {
                logger.Error("查询现货模板信息失败!");
                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode QueryTemplateStock(int templateNo)
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QuerySpotTemplateStock);
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
            bizMessage.SetFunction((int)FunctionCode.QuerySpotTemplateStock);
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
                        {
                            packer.AddStr(userToken);
                        }
                        break;
                    case "template_no":
                        {
                            packer.AddInt(templateNo);
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

            int retCode = _t2SDKWrap.SendAsync(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (retCode < 0)
            {
                logger.Error("查询现货模板信息失败!");
                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }
        #region

        public int OnReceivedBizMsg(CT2BizMessage bizMessage)
        {
            int iRetCode = bizMessage.GetReturnCode();
            int iErrorCode = bizMessage.GetErrorNo();
            int iFunction = bizMessage.GetFunction();
            if (iRetCode != 0)
            {
                string msg = string.Format("异步接收数据出错： {0}, {1}", iErrorCode, bizMessage.GetErrorInfo());

                return iRetCode;
            }

            CT2UnPacker unpacker = null;
            unsafe
            {
                int iLen = 0;
                void* lpdata = bizMessage.GetContent(&iLen);
                unpacker = new CT2UnPacker(lpdata, (uint)iLen);
            }

            if (unpacker != null)
            {
                Console.WriteLine("功能号：" + iFunction);
                _t2SDKWrap.PrintUnPack(unpacker);
                switch ((FunctionCode)iFunction)
                {
                    case FunctionCode.QueryTradingInstance:
                        {
                            
                        }
                        break;
                    case FunctionCode.EntrustInstanceBasket:
                        { 
                        
                        }
                        break;
                    case FunctionCode.QueryEntrustInstance:
                        { 
                            
                        }
                        break;
                    case FunctionCode.QueryDealInstance:
                        { 
                        
                        }
                        break;
                    case FunctionCode.WithdrawBasket:
                        { 
                            
                        }
                        break;
                    case FunctionCode.QuerySpotTemplate:
                        {

                        }
                        break;
                    case FunctionCode.QuerySpotTemplateStock:
                        {

                        }
                        break;
                    default:
                        break;
                }

                unpacker.Dispose();
            }
            //bizMessage.Dispose();

            return (int)ConnectionCode.Success;
        }

        #endregion
    }
}
