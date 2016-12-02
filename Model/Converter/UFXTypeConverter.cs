
using Model.UFX;
namespace Model.Converter
{
    public static class UFXTypeConverter
    {
        #region MarketCode

        /// <summary>
        /// Get the enum type of UFXMarketCode from given string code, which can be converted to integer value.
        /// </summary>
        /// <param name="code">A given string code standing for exchange.</param>
        /// <returns>An enum type value of the exchange.</returns>
        public static UFXMarketCode GetMarketCode(string code)
        {
            return StringEnumConverter.GetIntType<UFXMarketCode>(code);
        }

        /// <summary>
        /// Get the market name(exchange) from the given UFXMarketCode type.
        /// NOTE: it needs to add the Description flag in the enum value.
        /// </summary>
        /// <param name="marketCode">The given UFXMarketCode type value.</param>
        /// <returns>A string value of the market name(exchange).</returns>
        public static string GetMarketName(UFXMarketCode marketCode)
        {
            return EnumAttributeHelper.GetEnumDescription<UFXMarketCode>(marketCode);
        }

        /// <summary>
        /// Get the market code from the given UFXMarketCode enum type.
        /// NOTE: it needs to add the StandardCode flag in the enum value.
        /// </summary>
        /// <param name="marketCode">The given UFXMarketCode type value.</param>
        /// <returns>A string value of the market code(exchange code).</returns>
        public static string GetMarketCode(UFXMarketCode marketCode)
        {
            return EnumAttributeHelper.GetStandardCode<UFXMarketCode>(marketCode);
        }

        #endregion

        #region EntrustState

        /// <summary>
        /// Get the UFXEntrustState type value by a given string status. 
        /// NOTE: the status string should be a char type. It will throw exception while it fails to
        /// convert into UFXEntrustState.
        /// </summary>
        /// <param name="status">The UFX string value of entrust status.</param>
        /// <returns>An enum type value of the UFXEntrustState</returns>
        public static UFXEntrustState GetEntrustState(string status)
        {
            return StringEnumConverter.GetCharType<UFXEntrustState>(status);
        }

        /// <summary>
        /// Get the entrust state name by a given UFXEntrustState value.
        /// NOTE: it needs to add the Description flag in the UFXEntrustState enum value.
        /// </summary>
        /// <param name="state">The enum value of UFXEntrustState type.</param>
        /// <returns>A string of the entrust state name.</returns>
        public static string GetEntrustState(UFXEntrustState state)
        {
            return EnumAttributeHelper.GetEnumDescription<UFXEntrustState>(state);
        }

        #endregion

        #region EntrustDirection

        /// <summary>
        /// Get the UFXEntrustDirection type by a given string.
        /// </summary>
        /// <param name="direction">The UFX string value of direction.</param>
        /// <returns>An enum type value of the UFXEntrustDirection.</returns>
        public static UFXEntrustDirection GetEntrustDirection(string direction)
        {
            return StringEnumConverter.GetIntType<UFXEntrustDirection>(direction);
        }

        /// <summary>
        /// Get the EntrustDirection name from the given UFXEntrustDirection enum type.
        /// NOTE: it needs to add the Description flag in the enum value.
        /// </summary>
        /// <param name="direction">The given UFXEntrustDirection type value.</param>
        /// <returns>A string value of the entrust direction description.</returns>
        public static string GetEntrustDirection(UFXEntrustDirection direction)
        {
            return EnumAttributeHelper.GetEnumDescription<UFXEntrustDirection>(direction);
        }

        #endregion
    }
}
