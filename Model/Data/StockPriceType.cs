using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public enum StockPriceType
    {
        //不限价
        NoLimit = 0,
    
        //最新价
        LastPrice = 1,

        //盘1价
        Price1 = 2,

    }

    public enum FuturesPriceType
    {
        //不限价
        NoLimit = 0,

        //最新价
        LastPrice = 1,

        //盘1价
        Price1 = 2,
    }
}
