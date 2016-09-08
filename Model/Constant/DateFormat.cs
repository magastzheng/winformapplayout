using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Constant
{
    public static class DateFormat
    {
        public static string Format(DateTime dt, string format)
        {
            if (dt > DateTime.MinValue && dt < DateTime.MaxValue)
            {
                return dt.ToString(format);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
