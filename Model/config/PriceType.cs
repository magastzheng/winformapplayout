using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.config
{
    public enum PriceType
    {
        //市价
        Market = 0,  
        //指定价
        Assign = 1,
        //最新价
        Last = 2,
        //自动盘口
        Automatic = 3,
        //任意价
        Arbitrary = 4,

        Sell1 = 10,
        Sell2,
        Sell3,
        Sell4,
        Sell5,
        Sell6,
        Sell7,
        Sell8,
        Sell9,
        Sell10,
        Buy1 = 20,
        Buy2,
        Buy3,
        Buy4,
        Buy5,
        Buy6,
        Buy7,
        Buy8,
        Buy9,
        Buy10,
    }

    public enum EntrustPriceType
    { 
        //FixedPrice = char('0'),

    }
}
