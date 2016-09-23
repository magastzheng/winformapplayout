using Model.Constant;
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

        /// <summary>
        /// Parse the integer value like '19990220' to a DateTime value. The integer value MUST have 'yyyyMMdd' format.
        /// It will return a DateTime.MinValue if it fail to parse.
        /// </summary>
        /// <param name="ymd">An integer value with the format 'yyyyMMdd'.</param>
        /// <returns>An DateTime value represent the given year, month, day. Or the default DateTime.MinValue if it fails
        /// to parse the given input value.
        /// </returns>
        public static DateTime GetDateFromInt(int ymd)
        {
            string str = string.Format("{0}", ymd);
            DateTime dt;
            DateTime temp;
            if (IsValidDate(str, ConstVariable.DateFormat1, out temp))
            {
                dt = temp;
            }
            else
            {
                dt = DateTime.MinValue;
            }

            return dt;
        }

        /// <summary>
        /// Parse the given integer YearMonthDay and HourMinuteSecond into a DateTime value. The YearMonthDay MUST have the format
        /// like 'yyyyMMdd' and the HourMinuteSecond MUST have the format as 'HHmmss'.
        /// </summary>
        /// <param name="ymd">The integer value represent the YearMonthDay.</param>
        /// <param name="hms">The integer value represent the HourMinuteSecond.</param>
        /// <returns>A DateTime represent the given year, month, day, hour, minute, second. Or the default DateTime.MinValue
        /// if it fails to parse the given input value.
        /// </returns>
        public static DateTime GetDateTimeFromInt(int ymd, int hms)
        {
            string dateTimeFormat = string.Format("{0} {1}", ConstVariable.DateFormat1, ConstVariable.TimeFormat1);
            string str = string.Format("{0} {1}", ymd, hms);
            DateTime dt;
            DateTime temp;
            if (IsValidDate(str, dateTimeFormat, out temp))
            {
                dt = temp;
            }
            else
            {
                dt = DateTime.MinValue;
            }

            return dt;
        }

        /// <summary>
        /// Validate whether the given string is a valid DateTime with the given format. It will return true if the given string 
        /// can be converted to DateTime with the given format. Otherwise it will return false. The result will store the DateTime
        /// value if it is successful or a default DateTime.MinValue if it is failure.
        /// </summary>
        /// <param name="str">A given string represent the DateTime.</param>
        /// <param name="format">A given date time format.</param>
        /// <param name="result">An converted DateTime value.</param>
        /// <returns>True if it succeed or false if it fails.</returns>
        public static bool IsValidDate(string str, string format, out DateTime result)
        {
            bool isValid = DateTime.TryParseExact(str, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

            return isValid;
        }
    }
}
