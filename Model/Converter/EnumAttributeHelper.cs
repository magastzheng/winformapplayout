using Model.Binding;
using System.ComponentModel;
using System.Reflection;

namespace Model.Converter
{
    public static class EnumAttributeHelper
    {
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
