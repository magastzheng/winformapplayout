using Config;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TDFAPI;

namespace Quote.TDF
{
    //TDF 状态清单
    //0 首日上市
    //1 增发新股
    //2 上网定价发行
    //3 上网竞价发行
    //A 交易节休市
    //B 整天停牌 
    //C 全天收市
    //D 暂停交易							
    //E START - 启动交易盘
    //F PRETR - 盘前处理
    //H HOLIDAY - 放假
    //I OCALL - 开市集合竞价
    //J ICALL - 盘中集合竞价
    //K OPOBB - 开市订单簿平衡前期
    //L IPOBB - 盘中订单簿平衡前期
    //M OOBB  - 开市订单簿平衡
    //N IOBB  - 盘中订单簿平衡
    //O TRADE - 连续撮合
    //P BREAK - 休市
    //Q VOLA  - 波动性中断
    //R BETW  - 交易间
    //S NOTRD - 非交易服务支持
    //T FCALL - 固定价格集合竞价
    //U POSTR - 盘后处理
    //V ENDTR - 结束交易
    //W HALT  - 暂停
    //X SUSP  - 停牌
    //Y ADD   - 新增产品
    //Z DEL   - 可删除的产品

    class TDFService
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TDFService()
        { 
        
        }

        private TDFOpenSetting _setting = null;
        private TDFImp _tdfImp = null;
        private EventWaitHandle _waitHandle = new AutoResetEvent(false);
        private bool _isOpening = false;
        private Thread _serviceThread = null;

        public void Open()
        {
            if (_isOpening)
            {
                return;
            }

            _serviceThread = new Thread(() => {
                InitServiceThread(1000);
            });

            _serviceThread.Start();
        }

        public void Close()
        {
            _waitHandle.Set();
        }

        private void InitServiceThread(int sleepTime)
        {
            _isOpening = true;
            Thread.Sleep(sleepTime);

            SetSetting();
            //set the security code
            //_setting.Subscriptions = "";

            using (var dataSource = new TDFImp(_setting))
            { 
                dataSource.SysMsgDeal = OnRecvSysMsg;
                dataSource.DataMsgDeal = OnRecvDataMsg;

                dataSource.SetEnv(EnvironSetting.TDF_ENVIRON_OUT_LOG, 1);

                TDFERRNO openRet = dataSource.Open();
                if (openRet != TDFERRNO.TDF_ERR_SUCCESS)
                {
                    _isOpening = false;

                    var errorMessage = openRet.ToString();
                    logger.Error("宏汇行情初始化失败: " + errorMessage);

                    return;
                }

                this._tdfImp = dataSource;
                logger.Info("宏汇行情初始化成功!");

                _waitHandle.WaitOne();
            }

            _isOpening = false;
        }

        private void SetSetting()
        {
            var setting = ConfigManager.Instance.GetTDFAPIConfig().GetSetting();
            _setting = new TDFOpenSetting
            {
                Ip = setting.Ip,
                Port = string.Format("{0}", setting.Port),
                Username = setting.User,
                Password = setting.Password,
                ReconnectCount = (uint)setting.ReconnectCount,
                ReconnectGap = (uint)setting.ReconnectGap,
                ConnectionID = (uint)setting.ConnectionId,
                Date = (uint)setting.Date,
                Time = (uint)setting.Time,
                Markets = setting.Markets,
            };

            _setting.TypeFlags = (uint)(DataTypeFlag.DATA_TYPE_INDEX | DataTypeFlag.DATA_TYPE_ORDERQUEUE | DataTypeFlag.DATA_TYPE_FUTURE_CX);
        }

        private void OnRecvDataMsg(TDFMSG msg)
        {
            switch (msg.MsgID)
            {
                case TDFMSGID.MSG_DATA_MARKET:
                    { 
                        //行情消息
                        TDFMarketData[] marketOldDataArr = msg.Data as TDFMarketData[];
                        for (int i = 0, length = marketOldDataArr.Length; i < length; i++)
                        {
                            TDFMarketData data = marketOldDataArr[i];
                            string code = data.Code;
                            char status = (char)data.Status;
                            if (status == 'W'
                                || status == 'X'
                                || status == 'B'
                                || status == 'D')
                            {
                                //TODO: 停牌
                            }
                            else
                            { 
                                //正常交易
                            }
                        }
                    }
                    break;
                case TDFMSGID.MSG_DATA_FUTURE:
                    { 
                        //期货行情消息
                        TDFFutureData[] futureDataArr = msg.Data as TDFFutureData[];

                    }
                    break;
                case TDFMSGID.MSG_DATA_INDEX:
                    {
                        //指数消息
                        TDFIndexData[] indexDataArr = msg.Data as TDFIndexData[];
                    }
                    break;
                //case TDFMSGID.MSG_DATA_TRANSACTION:
                //    { 
                    
                //    }
                //    break;
                //case TDFMSGID.MSG_DATA_ORDER:
                //    { 
                    
                //    }
                //    break;
                //case TDFMSGID.MSG_DATA_ORDERQUEUE:
                //    { 
                    
                //    }
                //    break;
                default:
                    break;
            }
        }

        private void OnRecvSysMsg(TDFMSG msg)
        {
            switch (msg.MsgID)
            {
                case TDFMSGID.MSG_SYS_CONNECT_RESULT:
                    { 
                        //连接结果
                        TDFConnectResult connectResult = msg.Data as TDFConnectResult;
                        if (!connectResult.ConnResult)
                        {
                            string strMsg = string.Format("宏汇行情连接服务器失败: IP:{0} Port:{1} User:{2} Password:{3}",
                                connectResult.Ip, connectResult.Port, connectResult.Username, connectResult.Password);
                            logger.Error(strMsg);

                            this.Close();
                        }
                        else
                        {
                            string strMsg = "宏汇行情连接服务器成功！";
                            logger.Info(strMsg);
                        }
                    }
                    break;
                case TDFMSGID.MSG_SYS_LOGIN_RESULT:
                    {
                        //登陆结果
                        TDFLoginResult loginResult = msg.Data as TDFLoginResult;
                        if (!loginResult.LoginResult)
                        {
                            string strMsg = string.Format("宏汇行情登录失败: {0}", loginResult.Info);
                            logger.Error(strMsg);

                            this.Close();
                        }
                        else
                        {
                            string strMsg = "宏汇行情登录成功";
                            logger.Info(strMsg);
                        }
                    }
                    break;
                case TDFMSGID.MSG_SYS_CODETABLE_RESULT:
                    {
                        //接收代码表结果
                        TDFCodeResult codeResult = msg.Data as TDFCodeResult;

                        string strMsg = string.Format("获取到代码表, info:{0}, 市场个数:{1}", codeResult.Info, codeResult.Markets.Length);

                        //获取代码表内容
                        TDFCode[] codeArr;
                        this._tdfImp.GetCodeTable("", out codeArr);

                        //if (codeArr[i].Type >= 0x90 && codeArr[i].Type <= 0x95)
                        //{
                        //    // 期权数据
                        //    TDFOptionCode code = new TDFOptionCode();
                        //    var ret = this._tdfImp.GetOptionCodeInfo(codeArr[i].WindCode, ref code);
                        //}
                    }
                    break;
                case TDFMSGID.MSG_SYS_QUOTATIONDATE_CHANGE:
                    {
                        //行情日期变更。
                        TDFQuotationDateChange quotationChange = msg.Data as TDFQuotationDateChange;

                        string strMsg = string.Format("接收到行情日期变更通知消息，market:{0}, old date:{1}, new date:{2}", quotationChange.Market, quotationChange.OldDate, quotationChange.NewDate);
                        logger.Info(strMsg);
                    }
                    break;
                case TDFMSGID.MSG_SYS_MARKET_CLOSE:
                    {
                        //闭市消息
                        TDFMarketClose marketClose = msg.Data as TDFMarketClose;

                        string strMsg = string.Format("接收到闭市消息, 交易所:{0}, 时间:{1}, 信息:{2}", marketClose.Market, marketClose.Time, marketClose.Info);
                        logger.Info(strMsg);
                    }
                    break;
                case TDFMSGID.MSG_SYS_HEART_BEAT:
                    {
                        //心跳消息
                        logger.Info("接收到心跳消息!");
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
