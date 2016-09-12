using System;
using System.Text;

namespace Model.Converter
{
    public static class StringEnumConverter
    {
        public static T GetCharType<T>(string typeCode)
        {
            if (typeCode == null || string.IsNullOrEmpty(typeCode) || typeCode.Length != 1)
            {
                string msg = string.Format("The {0} [{1}] is not supported!", typeof(T), typeCode);
                throw new NotSupportedException(msg);
            }

            var bytes = Encoding.UTF8.GetBytes(typeCode);
            int charCode = (int)bytes[0];
            return GetType<T>(charCode);
        }

        public static T GetIntType<T>(string intStr)
        {
            bool isExisted = false;
            int temp;
            if (int.TryParse(intStr, out temp))
            {
                isExisted = true;
            }

            if (!isExisted)
            {
                string msg = string.Format("The {0} [{1}] is not supported!", typeof(T), intStr);
                throw new NotSupportedException(msg);
            }

            return GetType<T>(temp);
        }

        public static T GetType<T>(int intType)
        {
            if (!Enum.IsDefined(typeof(T), intType))
            {
                string msg = string.Format("The {0} [{1}] is not supported!", typeof(T), intType);
                throw new NotSupportedException(msg);
            }

            T priceType = (T)Enum.ToObject(typeof(T), intType);

            return priceType;
        }
    }
}
