using System;
using TDFAPI;

namespace Quote.TDF
{
    /// <summary>
    /// TDFMSG
    ///    public TDFAPPHead AppHead; 应用头
    ///    public int ConnectionID; 连接ID，在TDF_Open设置
    ///    public object Data;      数据
    ///    public int DataLen;      数据长度，不包括TDFAPPHead的长度
    ///    public short Flag;       标识符，升级到TDF_VERSION_NX_START_6001
    ///    public TDFMSGID MsgID;   
    ///    public int Order;        流水号
    ///    public int ServerTime;   TDF服务器时间戳（精确到毫秒HHMMSSmmm），对于系统消息为0
    /// </summary>
    class TDFImp : TDFDataSource
    {
        public Action<TDFMSG> SysMsgDeal;
        public Action<TDFMSG> DataMsgDeal;

        public TDFImp(TDFOpenSetting openSetting)
            : base(openSetting)
        { 
        }

        /// <summary>
        /// 行情数据回调
        /// MSG_DATA_MARKET: 行情快照数据 
        /// MSG_DATA_INDEX: 指数行情快照数据
        /// MSG_DATA_FUTURE: 期货和期权行情快照数据
        /// MSG_DATA_TRANSACTION: 逐笔成交
        /// MSG_DATA_ORDERQUEUE: 委托队列
        /// MSG_DATA_ORDER: 逐笔委托
        /// </summary>
        /// <param name="msg"></param>
        public override void OnRecvDataMsg(TDFMSG msg)
        {
            DataMsgDeal(msg);
        }

        /// <summary>
        /// 系统消息回调
        /// MSG_SYS_DISCONNECT_NETWORK: 网络断开事件
        /// MSG_SYS_CONNECT_RESULT: 主动发起连接的结果
        /// MSG_SYS_LOGIN_RESULT: 登陆应答
        /// MSG_SYS_CODETABLE_RESULT: 获取代码表结果
        /// MSG_SYS_QUOTATIONDATE_CHANGE: 行情日期变更通知
        /// MSG_SYS_MARKET_CLOSE: 闭市
        /// MSG_SYS_HEART_BEAT: 服务器心跳消息
        /// </summary>
        /// <param name="msg"></param>
        public override void OnRecvSysMsg(TDFMSG msg)
        {
            SysMsgDeal(msg);
        }
    }
}
