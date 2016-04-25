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
    public class StrategyEventArgs : EventArgs
    {
        public FunctionCode FunctionCode = FunctionCode.Entrust; 
    }

    public class StrategyBLL:T2SDKBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void CommandTradingUpdate(object sender, StrategyEventArgs e);
        private CommandTradingUpdate _OnCommandTradingUpdate;

        public event CommandTradingUpdate OnCommandTradingUpdate
        {
            add
            {
                _OnCommandTradingUpdate += value;
            }
            remove
            {
                _OnCommandTradingUpdate -= value;
            }
        }

        //使用委托和事件处理UI更新
        public StrategyBLL(CT2Configinterface config)
            :base(config)
        {

        }

        public ConnectionCode QueryTradingInstance(string userToken)
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QueryTradingInstance);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
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
                        packer.AddStr(userToken);
                        break;
                    case "account_group_code":
                        packer.AddStr("");
                        break;
                    case "instance_no":
                        packer.AddInt(0);
                        break;
                    case "instance_type":
                        packer.AddStr("");
                        break;
                    case "ext_invest_plan_no_list":
                        packer.AddStr("");
                        break;
                    default:
                        break;
                }
            }
            unsafe
            {
                bizMessage.SetContent(packer.GetPackBuf(), packer.GetPackLen());
            }

            int retCode = SendAsync(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (retCode < 0)
            {
                logger.Error("查询交易实例失败:" + _conn.GetErrorMsg(retCode));
                return ConnectionCode.ErrorConn;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode Entrust()
        {
            return ConnectionCode.Success;
        }

        public ConnectionCode Withdraw()
        {
            return ConnectionCode.Success;
        }

        public override void OnReceivedBizMsg(CT2Connection lpConnection, int hSend, CT2BizMessage lpMsg)
        {
            logger.Info("OnReceivedBizMsg: 接收业务数据！");

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
                CT2UnPacker unpacker = null;
                unsafe
                {
                    int iLen = 0;
                    void* lpdata = lpMsg.GetContent(&iLen);
                    unpacker = new CT2UnPacker(lpdata, (uint)iLen);
                }

                switch (iFunction)
                {
                    case (int)FunctionCode.QueryTradingInstance:
                        {
                            
                        }
                        break;
                    case (int)FunctionCode.EntrustInstanceBasket:
                        break;

                    case (int)FunctionCode.QueryEntrustInstance:
                        break;

                    case (int)FunctionCode.QueryDealInstance:
                        break;
                    default:
                        break;
                }

                unpacker.Dispose();
            }

            lpMsg.Dispose();
        }
    }
}
