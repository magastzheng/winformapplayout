
using Model.Binding;
using Model.UFX;
using System;
using System.ComponentModel;
using System.Reflection;
namespace Model.Converter
{
    public static class UFXTypeConverter
    {
        public static UFXMarketCode GetMarketCode(string code)
        {
            return StringEnumConverter.GetIntType<UFXMarketCode>(code);
        }

        public static UFXEntrustDirection GetEntrustDirection(string direction)
        {
            return StringEnumConverter.GetIntType<UFXEntrustDirection>(direction);
        }

        public static UFXEntrustState GetEntrustState(string status) 
        {
            return StringEnumConverter.GetCharType<UFXEntrustState>(status);
        }

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

        public static string GetMarketName(UFXMarketCode marketCode)
        {
            return GetEnumDescription<UFXMarketCode>(marketCode);
        }

        public static string GetMarketCode(UFXMarketCode marketCode)
        {
            return GetStandardCode<UFXMarketCode>(marketCode);
        }

        public static string GetEntrustDirection(UFXEntrustDirection direction)
        {
            return GetEnumDescription<UFXEntrustDirection>(direction);
        }

        public static string GetEntrustState(UFXEntrustState state)
        {
            return GetEnumDescription<UFXEntrustState>(state);
        }
    }
}
