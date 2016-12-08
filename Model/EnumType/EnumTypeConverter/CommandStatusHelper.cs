using Model.Converter;

namespace Model.EnumType.EnumTypeConverter
{
    public class CommandStatusHelper
    {
        public static string GetCommandStatusName(CommandStatus status)
        {
            return EnumAttributeHelper.GetEnumDescription<CommandStatus>(status);
        }

        public static string GetEntrustName(EntrustStatus status)
        {
            return EnumAttributeHelper.GetEnumDescription<EntrustStatus>(status);
        }

        public static string GetDealName(DealStatus status)
        {
            return EnumAttributeHelper.GetEnumDescription<DealStatus>(status);
        }
    }
}
