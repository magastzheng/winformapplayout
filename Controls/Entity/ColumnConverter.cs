using Model.Data;
using System.ComponentModel;

namespace Controls.Entity
{
    public class ColumnConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is DataValue)
            {
                DataValue dataValue = value as DataValue;
                switch (dataValue.Type)
                { 
                    case DataValueType.Int:
                        return dataValue.ToString();
                    case DataValueType.Float:
                        return string.Format("{0}", dataValue.GetDouble());
                    case DataValueType.String:
                        return dataValue.GetStr();
                    default:
                        return dataValue.GetStr();
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
