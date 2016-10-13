using Config;
using log4net;
using Model.Quote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TDFAPI;

namespace Quote.TDF
{
    public class TDFQuote
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private TDFImp _dataSource = null;
        private IQuote _quote = null;
        private EventWaitHandle _waitHandle = new AutoResetEvent(false);

        //private Thread _startThread = null;
        //private Thread _stopThread = null;
        //private Thread _subscriptionThread = null;

        public IQuote Quote
        {
            get { return _quote; }
            //set { _quote = value; }
        }

        public TDFQuote()
        {
            _quote = new Quote();
        }

        public TDFQuote(IQuote quote)
        {
            _quote = quote;
        }

        #region

        public void Start()
        {
            InitDataSource();
        }

        public void Stop()
        {
            _waitHandle.Set();
        }

        public void Subscription(TDFSubscriptionType type, List<string> windCodes)
        {
            SetSubscription(type, windCodes);
        }

        #endregion
        
        private void Close()
        {
            _dataSource.Close();
            _dataSource.Dispose();
        }

        private bool InitDataSource()
        {
            var setting = GetSetting();
            _dataSource = new TDFImp(setting);
            _dataSource.SysMsgDeal = OnRecvSysMsg;
            _dataSource.DataMsgDeal = OnRecvDataMsg;

            _dataSource.SetEnv(EnvironSetting.TDF_ENVIRON_OUT_LOG, 1);

            //TODO:订阅多支股票和股指期货
            //增加订阅列表中的股票
            //dataSource.SetSubscription("", SubscriptionType.SUBSCRIPTION_ADD);
            //删除订阅列表中的订阅
            //dataSource.SetSubscription("", SubscriptionType.SUBSCRIPTION_DEL);
            //设置为订阅列表中的股票
            //dataSource.SetSubscription("", SubscriptionType.SUBSCRIPTION_SET);

            TDFERRNO openRet = _dataSource.Open();
            if (openRet != TDFERRNO.TDF_ERR_SUCCESS)
            {
                var errorMessage = openRet.ToString();
                logger.Error("宏汇行情初始化失败: " + errorMessage);

                Close();

                return false;
            }
            else
            {
                logger.Info("宏汇行情初始化成功!");

                _waitHandle.WaitOne();

                Close();

                return true;
            }
        }

        private void SetSubscription(TDFSubscriptionType type, List<string> windCodes)
        {
            switch (type)
            {
                case TDFSubscriptionType.Add:
                    {
                        string str = string.Join(";", windCodes);
                        _dataSource.SetSubscription(str, SubscriptionType.SUBSCRIPTION_ADD);
                    }
                    break;
                case TDFSubscriptionType.Delete:
                    {
                        string str = string.Join(";", windCodes);
                        _dataSource.SetSubscription(str, SubscriptionType.SUBSCRIPTION_DEL);
                    }
                    break;
                case TDFSubscriptionType.Set:
                    {
                        string str = string.Join(";", windCodes);
                        _dataSource.SetSubscription(str, SubscriptionType.SUBSCRIPTION_SET);
                    }
                    break;
                case TDFSubscriptionType.Full:
                    {
                        _dataSource.SetSubscription("", SubscriptionType.SUBSCRIPTION_ADD);
                    }
                    break;
                default:
                    break;
            }
        }

        private TDFOpenSetting GetSetting()
        {
            var setting = ConfigManager.Instance.GetTDFAPIConfig().GetSetting();
            var tdfSetting = new TDFOpenSetting
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

            tdfSetting.TypeFlags = (uint)(DataTypeFlag.DATA_TYPE_INDEX | DataTypeFlag.DATA_TYPE_ORDERQUEUE | DataTypeFlag.DATA_TYPE_FUTURE_CX);

            return tdfSetting;
        }

        /// <summary>
        /// TDFMarketData
        ///     public int ActionDay;       //业务发生日（自然日） YYMMDD
        ///     public uint[] AskPrice;     //申卖价
        ///     public uint[] AskVol;       //申卖量
        ///     public uint[] BidPrice;     //申买价
        ///     public uint[] BidVol;       //申买量
        ///     public string Code;         //原始代码
        ///     public uint High;           //最高价
        ///     public uint HighLimited;    //涨停价
        ///     public int IOPV;            //IOPV净值估值
        ///     public uint Low;            //最低价
        ///     public uint LowLimited;     //跌停价
        ///     public uint Match;          //最新价
        ///     public uint NumTrades;      //成交笔数
        ///     public uint Open;           //开盘价
        ///     public uint PreClose;       //前收盘价
        ///     public byte[] Prefix;       //证券信息前缀
        ///     public int SD2;             //升跌2（对比上一笔）
        ///     public int Status;          //状态
        ///     public int Syl1;            //市盈率1
        ///     public int Syl2;            //市盈率2
        ///     public int Time;            //时间(HHMMSSmmm)
        ///     public long TotalAskVol;    //委托卖出总量
        ///     public long TotalBidVol;    //委托买入总量
        ///     public int TradingDay;      //
        ///     public long Turnover;       //成交总金额
        ///     public long Volume;         //成交总量
        ///     public uint WeightedAvgAskPrice;    //加权平均委卖价格
        ///     public uint WeightedAvgBidPrice;    //加权平均委买价格
        ///     public string WindCode;             //万得代码,如：600001.SH
        ///     public int YieldToMaturity;         //到期收益率
        ///     
        /// Status状态
        ///     //0 首日上市
        ///     //1 增发新股
        ///     //2 上网定价发行
        ///     //3 上网竞价发行 
        ///     //A 交易节休市 
        ///     //B 整体停牌
        ///     //C 全天收市
        ///     //D 暂停交易
        ///     //E Start - 启动交易盘
        ///     //F PRETR - 盘前处理
        ///     //H Holiday - 放假
        ///     //I OCALL - 开市集合竞价
        ///     //J ICALL - 盘中集合竞价
        ///     //K OPOBB - 开市订单簿平衡前期
        ///     //L IPOBB - 盘中订单簿平衡前期
        ///     //M OOBB - 开市订单簿平衡
        ///     //N IOBB - 盘中订单簿平衡
        ///     //O TRADE - 连续撮合
        ///     //P BREAK - 休市
        ///     //Q VOLA - 波动性中断
        ///     //R BETW - 交易间
        ///     //S NOTRD - 非交易服务支持
        ///     //T FCALL - 固定价格集合竞价
        ///     //U POSTR - 盘后处理
        ///     //V ENDTR - 结束交易
        ///     //W HALT - 暂停
        ///     //X SUSP - 停牌
        ///     //Y ADD - 新增产品
        ///     //Z DEL - 可删除的产品
        ///     //d 集合竞价阶段结束到连续竞价阶段开始之前的时段（如有）
        ///     //G DEL - 不可恢复交易的熔断阶段（上交所的N）
        ///     //Q VOLA - 波动性中断
        ///     //q 可恢复交易的熔断时段（上交所的M）
        ///     
        /// TDFIndexData
        ///     public int ActionDay;   //业务发生日（自然日）(YYMMDD)
        ///     public string Code;     //原始代码
        ///     public int HighIndex;   //最高指数
        ///     public int LastIndex;   //最新指数
        ///     public int LowIndex;    //最低指数
        ///     public int OpenIndex;   //今开盘指数
        ///     public int PreCloseIndex;//前盘指数
        ///     public int Time;        //时间(HHMMSSmmm)
        ///     public long TotalVolume;//参与计算相应指数的交易数量
        ///     public int TradingDay;  //
        ///     public long Turnover;   //参与计算相应指数的成交金额
        ///     public string WindCode; //万得代码
        ///
        /// 
        /// TDFFutureData
        ///     public int ActionDay;       //业务发生日（自然日）(YYMMDD)
        ///     public uint[] AskPrice;     //申卖价
        ///     public uint[] AskVol;       //申卖量
        ///     public uint[] BidPrice;     //申买价
        ///     public uint[] BidVol;       //申买量
        ///     public uint Close;          //今收盘
        ///     public string Code;         //原始代码
        ///     public int CurrDelta;       //今虚实度
        ///     public uint High;           //最高价
        ///     public uint HighLimited;    //涨停价
        ///     public uint Low;            //最低价
        ///     public uint LowLimited;     //跌停价
        ///     public uint Match;          //最新价
        ///     public uint Open;           //开盘价
        ///     public long OpenInterest;   //持仓总量
        ///     public uint PreClose;       //昨收盘价
        ///     public int PreDelta;        //昨虚实度
        ///     public long PreOpenInterest;//昨持仓
        ///     public uint PreSettlePrice; //昨结算
        ///     public uint SettlePrice;    //今结算
        ///     public int Status;          //状态
        ///     public int Time;            //时间(HHMMSSmmm)
        ///     public int TradingDay;      //交易日
        ///     public long Turnover;       //成交总金额
        ///     public long Volume;         //成交总量
        ///     public string WindCode;     //万得代码
        ///     
        /// 
        /// 
        /// TDFOrderQueue
        ///     public int ABItems;         //明细个数
        ///     public int[] ABVolume;      //订单明细
        ///     public int ActionDay;       //自然日(YYMMDD)
        ///     public string Code;         //原始代码
        ///     public int Orders;          //订单数量
        ///     public int Price;           //委托价格
        ///     public int Side;            //买卖方向('B': Bid, 'A': Ask)
        ///     public int Time;            //时间(HHMMSSmmm)
        ///     public string WindCode;     //万得代码
        ///     
        /// 
        /// TDFTransaction
        ///     public int ActionDay;   //成交日期(YYMMDD)
        ///     public int AskOrder;    //叫卖方委托序号
        ///     public int BidOrder;    //叫买方委托序号
        ///     public int BSFlag;      //买卖方向(买：'B', 卖：'A'，不明：' '）
        ///     public string Code;     //原始代码
        ///     public byte FunctionCode;//成交代码
        ///     public int Index;       //成交编号
        ///     public byte OrderKind;  //成交类别
        ///     public int Price;       //成交价格
        ///     public int Time;        //成交时间(HHMMSS)
        ///     public int Turnover;    //成交金额
        ///     public int Volume;      //成交数量
        ///     public string WindCode; //万得代码
        ///     
        /// TDFOrder
        ///     public int ActionDay;       //委托日期(YYMMDD)
        ///     public string Code;         //原始代码
        ///     public byte FunctionCode;   //委托代码
        ///     public int Order;           //委托号
        ///     public byte OrderKind;      //委托类别
        ///     public int Price;           //委托价格
        ///     public int Time;            //委托时间(HHMMSSmmm)
        ///     public int Volume;          //委托数量
        ///     public string WindCode;     //万得代码
        /// </summary>
        /// <param name="msg"></param>
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
                            string windCode = data.WindCode;

                            MarketData marketData = new MarketData
                            {
                                InstrumentID = windCode
                            };

                            char status = (char)data.Status;
                            if (status == 'W'
                                || status == 'X'
                                || status == 'B'
                                || status == 'D')
                            {
                                //TODO: 停牌
                                marketData.TradingStatus = TradingStatus.Suspend;
                            }
                            else
                            {
                                //正常交易
                                marketData.TradingStatus = TradingStatus.Normal;
                                marketData.SuspendFlag = SuspendFlag.NoSuspension;
                                marketData.CurrentPrice = (double)data.Match / 10000;
                                marketData.PreClose = data.PreClose / 10000;
                                marketData.SellPrice1 = (double)data.AskPrice[0] / 10000;
                                marketData.SellPrice2 = (double)data.AskPrice[1] / 10000;
                                marketData.SellPrice3 = (double)data.AskPrice[2] / 10000;
                                marketData.SellPrice4 = (double)data.AskPrice[3] / 10000;
                                marketData.SellPrice5 = (double)data.AskPrice[4] / 10000;
                                marketData.BuyPrice1 = (double)data.BidPrice[0] / 10000;
                                marketData.BuyPrice2 = (double)data.BidPrice[1] / 10000;
                                marketData.BuyPrice3 = (double)data.BidPrice[2] / 10000;
                                marketData.BuyPrice4 = (double)data.BidPrice[3] / 10000;
                                marketData.BuyPrice5 = (double)data.BidPrice[4] / 10000;
                                marketData.HighLimitPrice = (double)data.HighLimited / 10000;
                                marketData.LowLimitPrice = (double)data.LowLimited / 10000;
                                marketData.BuyAmount = data.TotalBidVol;
                                marketData.SellAmount = data.TotalAskVol;
                            }

                            _quote.Add(windCode, marketData);
                        }
                    }
                    break;
                case TDFMSGID.MSG_DATA_FUTURE:
                    {
                        //期货行情消息
                        TDFFutureData[] futureDataArr = msg.Data as TDFFutureData[];
                        foreach (TDFFutureData data in futureDataArr)
                        {
                            string windCode = data.WindCode;
                            if (!windCode.EndsWith(".CF"))
                                continue;

                            MarketData marketData = new MarketData
                            {
                                InstrumentID = windCode
                            };

                            marketData.CurrentPrice = (double)data.Match / 10000;
                            marketData.PreClose = (double)data.PreClose / 10000;
                            marketData.SellPrice1 = (double)data.AskPrice[0] / 10000;
                            marketData.SellPrice2 = (double)data.AskPrice[1] / 10000;
                            marketData.SellPrice3 = (double)data.AskPrice[2] / 10000;
                            marketData.SellPrice4 = (double)data.AskPrice[3] / 10000;
                            marketData.SellPrice5 = (double)data.AskPrice[4] / 10000;
                            marketData.BuyPrice1 = (double)data.BidPrice[0] / 10000;
                            marketData.BuyPrice2 = (double)data.BidPrice[1] / 10000;
                            marketData.BuyPrice3 = (double)data.BidPrice[2] / 10000;
                            marketData.BuyPrice4 = (double)data.BidPrice[3] / 10000;
                            marketData.BuyPrice5 = (double)data.BidPrice[4] / 10000;
                            marketData.HighLimitPrice = (double)data.HighLimited / 10000;
                            marketData.LowLimitPrice = (double)data.LowLimited / 10000;
                            //BuyAmount, SellAmount

                            _quote.Add(windCode, marketData);
                        }
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
                //        TDFTransaction[] tranDataArr = msg.Data as TDFTransaction[];
                //    }
                //    break;
                //case TDFMSGID.MSG_DATA_ORDER:
                //    {
                //        TDFOrder[] orderDataArr = msg.Data as TDFOrder[];
                //    }
                //    break;
                //case TDFMSGID.MSG_DATA_ORDERQUEUE:
                //    {
                //        TDFOrderQueue[] orderQueueDataArr = msg.Data as TDFOrderQueue[];
                //    }
                //    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// TDFConnectResult
        ///     public int ConnectID;       //连接ID
        ///     public bool ConnResult;     //为0则表示连接失败，非0则表示连接成功
        ///     public string Ip;           //连接TDFServer主机IP
        ///     public string Password;     //登录密码
        ///     public string Port;         //连接TDFServer主机的端口
        ///     public string Username;     //登录用户名
        /// TDFLoginResult
        ///     public int[] DynDate;       //动态数据日期
        ///     public string Info;         //登录结果文本
        ///     public bool LoginResult;    //true表示登录验证成功，false表示登录验证失败
        ///     public string[] Markets;    //市场代码 SZ, SH, CF, SHF, CZC, DCE
        ///     
        /// TDFCodeResult
        ///     public int[] CodeCount; //代码表项数
        ///     public int[] CodeDate;  //代码表日期
        ///     public string Info;     //代码表文本结果
        ///     public string[] Markets;//市场代码 SZ, SH, CF, SHF, CZC, DCE
        /// --调用GetCodeTable只能获得调用该函数为止的最新代码表，在运行程序中，当在回调函数中接收到新代码时，客户端可以再次调用GetCodeTable获得该新代码信息
        /// 
        /// TDFQuotationDateChange
        ///     public string Market;   //市场代码
        ///     public int NewDate;     //新行情日期
        ///     public int OldDate;     //原行情日期
        /// 
        /// TDFMarketClose
        ///     public string Info;     //闭市信息
        ///     public string Market;   //交易所名称
        ///     public int Time;        //时间(HHMMSSmmm)
        /// 
        /// TDFCode
        ///     public string CNName;   //代码中文名称，如：沪银1302
        ///     public string Code;     //原始代码，如：AG1302
        ///     public string ENName;   //代码英文名称
        ///     public string Market;   //交易所名称
        ///     public string WindCode; //万得代码，如：AG1302.SHF
        ///     public int Type;        //证券类型，详细类型 Type位与 0xFF
        ///                             //0x00  指数
        ///                             //0x10  股票 （0x10 A股， 0x11中小板股，0x12创业板股）
        ///                             //0x20  基金
        ///                             //0x30  债券&可转债
        ///                             //0x40  回购
        ///                             //0x60  权证
        ///                             //0x70  期货(0x70指数期货，0x71商品期货，0x72股票期货)
        ///                             //0x80  外汇
        ///                             //0xd0  银行利率
        ///                             //0xe0  贵金属
        ///                             //0xf0  其他
        /// </summary>
        /// <param name="msg"></param>
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

                            //TODO: notify the user to handle the connect failure
                            Stop();
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

                            //TODO: notify the user to handle the login failure
                            Stop();
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

                        //获取代码表内容,多个市场使用分号分割：SH;SZ;CF
                        TDFCode[] codeArr;
                        this._dataSource.GetCodeTable("", out codeArr);

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
