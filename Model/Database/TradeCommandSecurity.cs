using Model.EnumType;
using Model.SecurityInfo;

namespace Model.Database
{
    /// <summary>
    /// 对应数据库中的表：tradecommandsecurity
    /// </summary>
    public class TradeCommandSecurity
    {
        public TradeCommandSecurity()
        { 
        }

        public TradeCommandSecurity(TradeCommandSecurity security)
        {
            this.SecuCode = security.SecuCode;
            this.SecuName = security.SecuName;
            this.CommandId = security.CommandId;
            this.CommandPrice = security.CommandPrice;
            this.CurrentPrice = security.CurrentPrice;
            this.CommandAmount = security.CommandAmount;
            this.EDirection = security.EDirection;
            this.SecuType = security.SecuType;
            this.EntrustStatus = security.EntrustStatus;
        }

        //证券代码
        public string SecuCode { get; set; }

        //证券名称
        public string SecuName { get; set; }

        //指令序号
        public int CommandId { get; set; }

        //指令价格
        public double CommandPrice { get; set; }

        //最新价格
        public double CurrentPrice { get; set; }

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
