using Model.Binding;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Controls.GridView
{
    public static class TSDGVColumnBindingHelper
    {
        /// <summary>
        /// Get the DataGridView column databinding mapping [columnname, datafieldname]
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetPropertyBinding(Type type)
        {
            Dictionary<string, string> bindMap = new Dictionary<string, string>();

            PropertyInfo[] infos = type.GetProperties();
            foreach (PropertyInfo info in infos)
            {
                BindingAttribute attribute = (BindingAttribute)info.GetCustomAttribute(typeof(BindingAttribute), false);
                if (attribute != null)
                {
                    string columnName = attribute.ColumnName;
                    if (!bindMap.ContainsKey(columnName))
                    {
                        bindMap.Add(columnName, info.Name);
                    }
                }

            }

            return bindMap;
        }

        public static Dictionary<string, string> GetPropertyBinding<T>(T obj)
        {
            Type type = obj.GetType();

            return GetPropertyBinding(type);
        }
    }
}
