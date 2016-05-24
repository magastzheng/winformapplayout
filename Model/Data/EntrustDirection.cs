using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public enum EntrustDirection
    {
        None = -1,
        Buy = 1,
        Sell = 2,
        AdjustedTo = 3,

        BuySpot = 10,
        SellSpot = 11,
        SellOpen = 12,
        BuyClose = 13
    }
}
