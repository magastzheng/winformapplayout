using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Binding.BindingUtil
{
    public class UFXDataField
    {
        public string Name { get; set; }

        public DataValueType ValueType { get; set; }
    }

    public static class UFXDataBindingHelper
    {
        /// <summary>
        /// Get the data field databinding mapping [name, field]
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, UFXDataField> GetProperty(Type type)
        {
            Dictionary<string, UFXDataField> bindMap = new Dictionary<string, UFXDataField>();

            PropertyInfo[] infos = type.GetProperties();
            foreach (PropertyInfo info in infos)
            {
                UFXDataAttribute attribute = (UFXDataAttribute)info.GetCustomAttribute(typeof(UFXDataAttribute), false);
                if (attribute != null)
                {
                    string name = attribute.Name;
                    var valueType = attribute.ValueType;
                    //info.MemberType
                    //info.PropertyType
                    UFXDataField dataField = new UFXDataField 
                    {
                        Name = info.Name,
                        ValueType = valueType,
                    };

                    if (!bindMap.ContainsKey(name))
                    {
                        bindMap.Add(name, dataField);
                    }
                }

            }

            return bindMap;
        }

        public static Dictionary<string, UFXDataField> GetProperty<T>()
        {
            //T t = default(T);
            //Type type = t.GetType();
            Type type = typeof(T);
            return GetProperty(type);
        }
    }
}
