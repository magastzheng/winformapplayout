using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation
{
    public class FloatUtil
    {
        private static double ZERO = 0.0000000001;
        //private static double MINUS_ZERO = 0 - ZERO;

        public static bool IsZero(double dvalue)
        {
            return Math.Abs(dvalue) <= ZERO;
        }

        public static int Compare(double left, double right)
        {
            int ret = 0;
            double result = left - right;
            if (result > ZERO)
            {
                ret = 1;
            }
            else if (Math.Abs(result) <= ZERO)
            {
                ret = 0;
            }
            else
            {
                ret = -1;
            }

            return ret;
        }
    }
}
