
using Model.Database;
namespace Model.Archive
{
    public class ArchiveTradeCommandSecurity : TradeCommandSecurity
    {
        public ArchiveTradeCommandSecurity()
        { 
        }

        public ArchiveTradeCommandSecurity(TradeCommandSecurity tradeCommandSecurity)
            : base(tradeCommandSecurity)
        { 
        }

        public int ArchiveId { get; set; }
    }
}
