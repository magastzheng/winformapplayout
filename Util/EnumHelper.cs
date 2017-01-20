using System;

namespace Util
{
    public static class EnumHelper
    {
        public static T ParseEnum<T>(string key, T defaultValue) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            if (string.IsNullOrEmpty(key))
            {
                return defaultValue;
            }

            foreach (T item in Enum.GetValues(typeof(T)))
            {
                if (item.ToString().Equals(key.Trim()))
                {
                    return item;
                }
            }

            return defaultValue;
        }
    }
}
