using BLL.UFX;
using Config;
using hundsun.t2sdk;
using log4net;
using Model;
using Model.config;
using Model.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    //套利模块
    public class ArbitrageBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private T2SDKWrap _t2SDKWrap;
        private ReceivedBizMsg _receivedBizMsg;

        public ArbitrageBLL(T2SDKWrap t2SDKWrap)
        {
            _t2SDKWrap = t2SDKWrap;
            _receivedBizMsg = OnReceivedBizMsg;

            _t2SDKWrap.Register(FunctionCode.QueryTradingInstance1, _receivedBizMsg);
            _t2SDKWrap.Register(FunctionCode.QueryMonitorItem, _receivedBizMsg);
            //_t2SDKWrap.Register(FunctionCode.EntrustInstanceBasket, _receivedBizMsg);
            //_t2SDKWrap.Register(FunctionCode.QueryEntrustInstance, _receivedBizMsg);
        }


        public ConnectionCode QueryInstance(AccountItem account)
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QueryTradingInstance1);
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
            bizMessage.SetFunction((int)FunctionCode.QueryTradingInstance1);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            //var accounts = LoginManager.Instance.Accounts;
            //foreach (var account in accounts)
            //{
                Console.WriteLine("Account: " + account.AccountCode + " " + account.AccountName);
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
                                packer.AddStr(account.AccountCode);
                            }
                            break;
                        case "combi_no":
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
                        case "template_no":
                            {
                                packer.AddInt(0);
                            }
                            break;
                        case "long_market_no":
                            {
                                packer.AddStr("");
                            }
                            break;
                        case "long_report_code":
                            {
                                packer.AddStr("");
                            }
                            break;
                        case "short_market_no":
                            {
                                packer.AddStr("");
                            }
                            break;
                        case "short_report_code":
                            {
                                packer.AddStr("");
                            }
                            break;
                        case "invest_plan_no":
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

        public ConnectionCode QueryMonitorItem()
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QueryMonitorItem);
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
            bizMessage.SetFunction((int)FunctionCode.QueryMonitorItem);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            //var accounts = LoginManager.Instance.Accounts;
            //foreach (var account in accounts)
            //{
            //Console.WriteLine("Account: " + account.AccountCode + " " + account.AccountName);
            foreach (FieldItem item in functionItem.RequestFields)
            {
                switch (item.Name)
                {
                    case "user_token":
                        {
                            packer.AddStr(userToken);
                        }
                        break;
                    case "instance_type":
                        {
                            packer.AddStr("");
                        }
                        break;
                    case "monitoritem_no":
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
                    case FunctionCode.QueryTradingInstance1:
                        {

                        }
                        break;
                    case FunctionCode.QueryMonitorItem:
                        { 
                            
                        }
                        break;
                    //case FunctionCode.EntrustInstanceBasket:
                    //    {

                    //    }
                    //    break;
                    //case FunctionCode.QueryEntrustInstance:
                    //    {

                    //    }
                    //    break;
                    //case FunctionCode.QueryDealInstance:
                    //    {

                    //    }
                    //    break;
                    //case FunctionCode.WithdrawBasket:
                    //    {

                    //    }
                    //    break;
                    //case FunctionCode.QuerySpotTemplate:
                    //    {

                    //    }
                    //    break;
                    //case FunctionCode.QuerySpotTemplateStock:
                    //    {

                    //    }
                    //    break;
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
