using System;
using System.Text;

namespace Model.Converter
{
    public static class StringEnumConverter
    {
        /// <summary>
        /// Get the enum type value from the given integer string(a byte). It will throw exception while the input
        /// value has more than one byte.
        /// </summary>
        /// <typeparam name="T">An enum type.</typeparam>
        /// <param name="typeCode">The given byte converted into string.</param>
        /// <returns>An enum type value defining the specified thing.</returns>
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

        /// <summary>
        /// Get the enum type value from the given integer string. It will throw exception if it fails to converted
        /// the input string into integer value.
        /// </summary>
        /// <typeparam name="T">An enum type.</typeparam>
        /// <param name="intStr">The given integer string.</param>
        /// <returns>An enum type value defining the specified thing.</returns>
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

        /// <summary>
        /// Get a specified enum type value from a given integer value. It will throw exception if there is no
        /// the specified enum type value.
        /// </summary>
        /// <typeparam name="T">An enum type.</typeparam>
        /// <param name="intType">The given integer value.</param>
        /// <returns>An enum type value defining the specified thing.</returns>
        public static T GetType<T>(int intType)
        {
            if (!Enum.IsDefined(typeof(T), intType))
            {
                string msg = string.Format("The {0} [{1}] is not supported!", typeof(T), intType);
                throw new NotSupportedException(msg);
            }

            T enumType = (T)Enum.ToObject(typeof(T), intType);

            return enumType;
        }
    }
}
