using Model.Binding;
using System.ComponentModel;
using System.Reflection;

namespace Model.Converter
{
    public static class EnumAttributeHelper
    {
        /// <summary>
        /// Get the label of Description flag(DescriptionAttribute) in the enum type value. Such as the following:
        /// It can get the string "上交所"
        /// [Description("上交所")]
        /// [StandardCode("SSE")]
        /// ShanghaiSecurityExchange = 1,
        /// </summary>
        /// <typeparam name="T">The given enum type.</typeparam>
        /// <param name="value">The given enum type value.</param>
        /// <returns>The string value in the DescriptionAttribute.</returns>
        public static string GetEnumDescription<T>(T value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        /// <summary>
        /// Get the label of StandardCode flag(StandardCodeAttribute) in the enum type value.
        /// It will get the string "SSE" in the following case.
        /// [Description("上交所")]
        /// [StandardCode("SSE")]
        /// ShanghaiSecurityExchange = 1, 
        /// </summary>
        /// <typeparam name="T">The given enum type.</typeparam>
        /// <param name="value">The given enum type value.</param>
        /// <returns>The string value in the StandardCodeAttribute.</returns>
        public static string GetStandardCode<T>(T value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            StandardCodeAttribute[] attributes = (StandardCodeAttribute[])fi.GetCustomAttributes(typeof(StandardCodeAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Code;
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
