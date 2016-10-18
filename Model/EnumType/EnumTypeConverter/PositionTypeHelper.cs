
namespace Model.EnumType.EnumTypeConverter
{
    public static class PositionTypeHelper
    {
        public static string GetDisplayText(PositionType positionType)
        {
            string text = string.Empty;
            switch (positionType)
            {
                case PositionType.SpotLong:
                    {
                        text = "多头";
                    }
                    break;
                case PositionType.SpotShort:
                    {
                        text = "空头";
                    }
                    break;
                case PositionType.FuturesLong:
                    {
                        text = "多头";
                    }
                    break;
                case PositionType.FuturesShort:
                    {
                        text = "空头";
                    }
                    break;
                default:
                    break;
            }
            return text;
        }
    }
}
