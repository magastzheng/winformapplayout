using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Binding
{
    public class IntBoolConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value == null || value == DBNull.Value)
                return 0;

            if (value is bool)
            {
                bool temp = (bool)value;
                if (temp)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value == null || value == DBNull.Value)
                return false;

            if(destinationType == typeof(bool))
            {
                if(value is int)
                {
                    int temp = (int) value;
                    if(temp > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }


            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
