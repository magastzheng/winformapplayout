using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote
{
    public class MarketData
    {
        public string InstrumentID;

        public double CurrentPrice;

        public double BuyPrice1;

        public double BuyPrice2;

        public double BuyPrice3;

        public double BuyPrice4;

        public double BuyPrice5;

        public double SellPrice1;

        public double SellPrice2;

        public double SellPrice3;

        public double SellPrice4;

        public double SellPrice5;

        //外盘
        public int BuyAmount;

        //内盘
        public int SellAmount;

        public string UpdateTime;

    }
}
