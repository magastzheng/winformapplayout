using Config;
using hundsun.t2sdk;
using log4net;
using Model;
using Model.config;
using Model.strategy;
using System.Collections.Generic;

namespace BLL.UFX.impl
{
    public class SecurityBLL:UFXBLLBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SecurityBLL(T2SDKWrap t2SDKWrap)
            : base(t2SDKWrap)
        {
            RegisterUFX(FunctionCode.Entrust);
            RegisterUFX(FunctionCode.Withdraw);
            RegisterUFX(FunctionCode.EntrustBasket);
            RegisterUFX(FunctionCode.WithdrawBasket);
            RegisterUFX(FunctionCode.QuerySecurityEntrust);
        }

        public ConnectionCode Entrust(List<UFXEntrustRequest> entrustRequests, Callbacker callbacker)
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.Entrust);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorNoFunctionCode;
            }

            string userToken = LoginManager.Instance.LoginUser.Token;
            if (string.IsNullOrEmpty(userToken))
            {
                return ConnectionCode.ErrorLogin;
            }

            AddDataHandler(FunctionCode.Entrust, callbacker);

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.Entrust);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            foreach (var entrustRequest in entrustRequests)
            {
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
                                packer.AddInt(entrustRequest.BatchNo);
                            }
                            break;
                        case "account_code":
                            {
                                packer.AddStr(entrustRequest.AccountCode);
                            }
                            break;
                        case "asset_no":
                            {
                                packer.AddStr(entrustRequest.AssetNo);
                            }
                            break;
                        case "combi_no":
                            {
                                packer.AddStr(entrustRequest.CombiNo);
                            }
                            break;
                        case "stockholder_id":
                            {
                                packer.AddStr(entrustRequest.StockHolderId);
                            }
                            break;
                        case "report_seat":
                            {
                                packer.AddStr(entrustRequest.ReportSeat);
                            }
                            break;
                        case "market_no":
                            {
                                packer.AddStr(entrustRequest.MarketNo);
                            }
                            break;
                        case "stock_code":
                            {
                                packer.AddStr(entrustRequest.StockCode);
                            }
                            break;
                        case "entrust_direction":
                            {
                                packer.AddStr(entrustRequest.EntrustDirection);
                            }
                            break;
                        case "price_type":
                            {
                                packer.AddStr(entrustRequest.PriceType);
                            }
                            break;
                        case "entrust_price":
                            {
                                packer.AddDouble(entrustRequest.EntrustPrice);
                            }
                            break;
                        case "entrust_amount":
                            {
                                packer.AddInt(entrustRequest.EntrustAmount);
                            }
                            break;
                        case "extsystem_id":
                            {
                                packer.AddInt(entrustRequest.ExtSystemId);
                            }
                            break;
                        case "third_reff":
                            {
                                packer.AddStr(entrustRequest.ThirdReff);
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
                logger.Error("普通买卖委托失败!");
                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode Withdraw(List<UFXWithdrawRequest> cancelRequests, Callbacker callbacker)
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.Withdraw);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorNoFunctionCode;
            }

            string userToken = LoginManager.Instance.LoginUser.Token;
            if (string.IsNullOrEmpty(userToken))
            {
                return ConnectionCode.ErrorLogin;
            }

            AddDataHandler(FunctionCode.Withdraw, callbacker);

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.Withdraw);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            foreach (var cancelRequest in cancelRequests)
            {
                foreach (FieldItem item in functionItem.RequestFields)
                {
                    switch (item.Name)
                    {
                        case "user_token":
                            {
                                packer.AddStr(userToken);
                            }
                            break;
                        case "entrust_no":
                            {
                                packer.AddInt(cancelRequest.EntrustNo);
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
                logger.Error("委托撤单失败!");
                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode EntrustBasket(List<UFXBasketEntrustRequest> entrustRequests, Callbacker callbacker)
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.EntrustBasket);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorNoFunctionCode;
            }

            string userToken = LoginManager.Instance.LoginUser.Token;
            if (string.IsNullOrEmpty(userToken))
            {
                return ConnectionCode.ErrorLogin;
            }

            AddDataHandler(FunctionCode.EntrustBasket, callbacker);

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.EntrustBasket);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            foreach (var entrustRequest in entrustRequests)
            {
                foreach (FieldItem item in functionItem.RequestFields)
                {
                    switch (item.Name)
                    {
                        case "user_token":
                            {
                                packer.AddStr(userToken);
                            }
                            break;
                        case "account_code":
                            {
                                packer.AddStr(entrustRequest.AccountCode);
                            }
                            break;
                        case "asset_no":
                            {
                                packer.AddStr(entrustRequest.AssetNo);
                            }
                            break;
                        case "combi_no":
                            {
                                packer.AddStr(entrustRequest.CombiNo);
                            }
                            break;
                        case "stockholder_id":
                            {
                                packer.AddStr(entrustRequest.StockHolderId);
                            }
                            break;
                        case "report_seat":
                            {
                                packer.AddStr(entrustRequest.ReportSeat);
                            }
                            break;
                        case "market_no":
                            {
                                packer.AddStr(entrustRequest.MarketNo);
                            }
                            break;
                        case "stock_code":
                            {
                                packer.AddStr(entrustRequest.StockCode);
                            }
                            break;
                        case "entrust_direction":
                            {
                                packer.AddStr(entrustRequest.EntrustDirection);
                            }
                            break;
                        case "futures_direction":
                            {
                                packer.AddStr(entrustRequest.FuturesDirection);
                            }
                            break;
                        case "price_type":
                            {
                                packer.AddStr(entrustRequest.PriceType);
                            }
                            break;
                        case "entrust_price":
                            {
                                packer.AddDouble(entrustRequest.EntrustPrice);
                            }
                            break;
                        case "entrust_amount":
                            {
                                packer.AddInt(entrustRequest.EntrustAmount);
                            }
                            break;
                        case "limit_entrust_ratio":
                            {
                                packer.AddDouble(entrustRequest.LimitEntrustRatio);
                            }
                            break;
                        case "ftr_limit_entrust_ratio":
                            {
                                packer.AddDouble(entrustRequest.FutuLimitEntrustRatio);
                            }
                            break;
                        case "opt_limit_entrust_ratio":
                            {
                                packer.AddDouble(entrustRequest.OptLimitEntrustRatio);
                            }
                            break;
                        case "extsystem_id":
                            {
                                packer.AddInt(entrustRequest.ExtSystemId);
                            }
                            break;
                        case "third_reff":
                            {
                                packer.AddStr(entrustRequest.ThirdReff);
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
                logger.Error("证券篮子委托失败!");
                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode WithdrawBasket(List<UFXBasketWithdrawRequest> cancelRequests, Callbacker callbacker)
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

            AddDataHandler(FunctionCode.WithdrawBasket, callbacker);

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

            foreach (var cancelItem in cancelRequests)
            {
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
                                packer.AddInt(cancelItem.BatchNo);
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

        public ConnectionCode QueryEntrust()
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QuerySecurityEntrust);
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
            bizMessage.SetFunction((int)FunctionCode.QuerySecurityEntrust);
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
                            packer.AddInt(2317379);
                        }
                        break;
                    case "entrust_no":
                        {
                            packer.AddInt(-1);
                        }
                        break;
                    case "account_code":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "asset_no":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "combi_no":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "stockholder_id":
                        {
                            packer.AddStr("");
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
                    case "entrust_state_list":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "extsystem_id":
                        {
                            packer.AddInt(-1);
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
                            packer.AddInt(1000);
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

        //#region callback
        //public int OnReceivedBizMsg(CT2BizMessage bizMessage)
        //{
        //    int iRetCode = bizMessage.GetReturnCode();
        //    int iErrorCode = bizMessage.GetErrorNo();
        //    int iFunction = bizMessage.GetFunction();
        //    if (iRetCode != 0)
        //    {
        //        string msg = string.Format("异步接收数据出错： {0}, {1}", iErrorCode, bizMessage.GetErrorInfo());

        //        return iRetCode;
        //    }

        //    CT2UnPacker unpacker = null;
        //    unsafe
        //    {
        //        int iLen = 0;
        //        void* lpdata = bizMessage.GetContent(&iLen);
        //        unpacker = new CT2UnPacker(lpdata, (uint)iLen);
        //    }

        //    if (unpacker != null)
        //    {
        //        Console.WriteLine("功能号：" + iFunction);
        //        _t2SDKWrap.PrintUnPack(unpacker);

        //        switch ((FunctionCode)iFunction)
        //        {
        //            case FunctionCode.Entrust:
        //                { 
                        
        //                }
        //                break;
        //            case FunctionCode.Withdraw:
        //                { 
                            
        //                }
        //                break;
        //            case FunctionCode.EntrustBasket:
        //                {

        //                }
        //                break;
        //            case FunctionCode.WithdrawBasket:
        //                {

        //                }
        //                break;
        //            case FunctionCode.QuerySecurityEntrust:
        //                {

        //                }
        //                break;
        //            default:
        //                break;
        //        }

        //        unpacker.Dispose();
        //    }
        //    //bizMessage.Dispose();

        //    return (int)ConnectionCode.Success;
        //}
        //#endregion
    }
}
