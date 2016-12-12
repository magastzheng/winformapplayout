using Model.EnumType;
using Model.SecurityInfo;

namespace Model.Database
{
    /// <summary>
    /// 对应数据库中的表：tradecommandsecurity
    /// </summary>
    public class TradeCommandSecurity
    {
        //证券代码
        public string SecuCode { get; set; }

        //证券名称
        public string SecuName { get; set; }

        //指令序号
        public int CommandId { get; set; }

        //指令价格
        public double CommandPrice { get; set; }

        //指令数量
        public int CommandAmount { get; set; }

        //委托方向
        public EntrustDirection EDirection { get; set; }

        //public PriceType EPriceType { get; set; }

        //证券类型
        public SecurityType SecuType { get; set; }

        public EntrustStatus EntrustStatus { get; set; }
    }
}
