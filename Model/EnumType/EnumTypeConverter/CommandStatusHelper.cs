using Model.Converter;

namespace Model.EnumType.EnumTypeConverter
{
    public class CommandStatusHelper
    {
        public static string GetCommandStatusName(CommandStatus status)
        {
            return EnumAttributeHelper.GetEnumDescription<CommandStatus>(status);
        }
    }
}
