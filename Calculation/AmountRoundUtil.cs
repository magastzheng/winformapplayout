using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation
{
    public class AmountRoundUtil
    {
        public static int Round(int amount)
        {
            return (int)Math.Round(amount / 100.0) * 100;
        }

        public static int Ceiling(int amount)
        {
            return (int)Math.Ceiling(amount / 100.0) * 100;
        }

        public static int Floor(int amount)
        {
            return (int)Math.Floor(amount / 100.0) * 100;
        }
    }
}
