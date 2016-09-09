using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class DateUtil
    {
        public static int GetIntDate(DateTime dt)
        {
            return dt.Year * 10000 + dt.Month * 100 + dt.Day;
        }

        public static int GetIntTime(DateTime dt)
        {
            return dt.Hour * 10000 + dt.Minute * 100 + dt.Second;
        }

        public static bool IsValidDate(string str, string format, out DateTime dt)
        {
            bool isValid = DateTime.TryParseExact(str, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

            return isValid;
        }
    }
}
