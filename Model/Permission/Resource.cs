using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Permission
{
    public enum ResourceType
    { 
        None = -1,
        Product = 1,
        AssetUnit = 2,
        Portfolio = 3,
        TradeInstance = 4,
        TradeCommand = 5,
        EntrustCommand = 6,
    }

    public class Resource
    {
        public int Id { get; set; }

        public int RefId { get; set; }

        public ResourceType Type { get; set; }

        public string Name { get; set; }
    }
}
