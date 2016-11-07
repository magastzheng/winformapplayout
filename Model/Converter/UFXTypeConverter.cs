
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

        public static string GetMarketName(UFXMarketCode marketCode)
        {
            return EnumAttributeHelper.GetEnumDescription<UFXMarketCode>(marketCode);
        }

        public static string GetMarketCode(UFXMarketCode marketCode)
        {
            return EnumAttributeHelper.GetStandardCode<UFXMarketCode>(marketCode);
        }

        public static string GetEntrustDirection(UFXEntrustDirection direction)
        {
            return EnumAttributeHelper.GetEnumDescription<UFXEntrustDirection>(direction);
        }

        public static string GetEntrustState(UFXEntrustState state)
        {
            return EnumAttributeHelper.GetEnumDescription<UFXEntrustState>(state);
        }
    }
}
