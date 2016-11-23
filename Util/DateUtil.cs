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
        private static int[] days = new int[13]{0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

        /// <summary>
        /// Convert the specified DateTime value year, month, day parts into an integer value like the format 'yyyyMMdd'.
        /// NOTE: the year should be nonnegative.
        /// </summary>
        /// <param name="dt">An given DateTime value.</param>
        /// <returns>An nonnegative interger value.</returns>
        public static int GetIntDate(DateTime dt)
        {
            return dt.Year * 10000 + dt.Month * 100 + dt.Day;
        }

        /// <summary>
        /// Convert the specified DateTime value hour, minute, second parts into an integer value like the format 'HHmmss'.
        /// </summary>
        /// <param name="dt">An given DateTime value.</param>
        /// <returns>An nonnegative integer value.</returns>
        public static int GetIntTime(DateTime dt)
        {
            return dt.Hour * 10000 + dt.Minute * 100 + dt.Second;
        }

        /// <summary>
        /// Parse the nonnegative integer value like '19990220' to a DateTime value. The nonnegative integer value MUST have 'yyyyMMdd' format.
        /// It will return a DateTime.MinValue if it fail to parse.
        /// </summary>
        /// <param name="ymd">An nonnegative integer value with the format 'yyyyMMdd'.</param>
        /// <returns>An DateTime value represent the given year, month, day. Or the default DateTime.MinValue if it fails
        /// to parse the given input value.
        /// </returns>
        public static DateTime GetDateFromInt(int ymd)
        {
            DateTime dt = DateTime.MinValue;
            if (ymd < 0)
            {
                return dt;
            }

            int year = 0;
            int month = 0;
            int day = 0;

            year = ymd / 10000;
            month = (ymd % 10000) / 100;
            day = ymd % 100;

            if (IsValidDay(year, month, day))
            {
                dt = new DateTime(year, month, day);
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
            DateTime dt = DateTime.MinValue;
            if (ymd < 0 || hms < 0)
            {
                return dt;
            }

            int year = 0;
            int month = 0;
            int day = 0;
            int hour = 0;
            int minute = 0;
            int second = 0;

            if (GetYMD(ymd, out year, out month, out day) 
                && GetHMS(hms, out hour, out minute, out second))
            {
                dt = new DateTime(year, month, day, hour, minute, second);
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

        /// <summary>
        /// Validate whether the given integer value can be converted to a valid date.
        /// </summary>
        /// <param name="ymd">An integer value.</param>
        /// <returns>It will return true if the ymd can be converted to a valid date(year, month, day), otherwise it will return false.</returns>
        public static bool IsValidDate(int ymd)
        {
            int year;
            int month;
            int day;

            return GetYMD(ymd, out year, out month, out day);
        }

        /// <summary>
        /// Validate whether the given integer value can be converted to a valid time.
        /// </summary>
        /// <param name="hms">An integer value.</param>
        /// <returns>It will return true if the hms can be converted to a valid time(hour, minute, second), otherwise it will return false.</returns>
        public static bool IsValidTime(int hms)
        {
            int hour;
            int minute;
            int second;

            return GetHMS(hms, out hour, out minute, out second);
        }

        /// <summary>
        /// Get the year, month, day from an integer value.
        /// </summary>
        /// <param name="ymd">An integer value from datetime YMD = year * 10000 + month * 100 + day.</param>
        /// <param name="year">The output value of the year.</param>
        /// <param name="month">The output value of the month.</param>
        /// <param name="day">The output value of the day.</param>
        /// <returns>It will return true if the ymd can be converted to an valid date(year, month, day), otherwise return false.</returns>
        private static bool GetYMD(int ymd, out int year, out int month, out int day)
        {
            if (ymd < 0)
            {
                year = 0;
                month = 0;
                day = 0;

                return false;
            }
            else
            {
                year = ymd / 10000;
                month = (ymd % 10000) / 100;
                day = ymd % 100;

                return IsValidDay(year, month, day);
            }
        }

        /// <summary>
        /// Get the hour, minute, second from an integer value.
        /// </summary>
        /// <param name="hms">An integer value of HMS = 10000 * hour + 100 * minute + second.</param>
        /// <param name="hour">The output value of the hour.</param>
        /// <param name="minute">The output value of the minute.</param>
        /// <param name="second">The output value of the second.</param>
        /// <returns>It will return true if the hms can be converted to valid time(hour, minute, second), otherwise return false. </returns>
        private static bool GetHMS(int hms, out int hour, out int minute, out int second)
        {
            if (hms < 0)
            {
                hour = 0;
                minute = 0;
                second = 0;

                return false;
            }
            else
            {
                hour = hms / 10000;
                minute = (hms % 10000) / 100;
                second = hms % 100;

                return IsValidTime(hour, minute, second);
            }
        }

        private static bool IsValidDay(int year, int month, int day)
        {
            bool isValid = false;
            if (month >= 1 && month <= 12)
            {
                int lastDay = days[month];
                if (month == 2 && IsLeap(year))
                {
                    lastDay += 1;
                }

                if (day >= 1 && day <= lastDay)
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        private static bool IsValidTime(int hour, int minute, int second)
        {
            bool isValid = false;
            if (hour >= 0 && hour <= 23 
                && minute >= 0 && minute <= 59 
                && second >= 0 && second <= 59)
            {
                isValid = true;
            }

            return isValid;
        }

        private static bool IsLeap(int year)
        {
            return year % 400 == 0 || (year % 4 == 0 && year % 100 != 0);
        }
    }
}
