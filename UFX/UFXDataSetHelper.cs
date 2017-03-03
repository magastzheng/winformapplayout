using UFX.impl;
using Model.Binding.BindingUtil;
using Model.Data;
using System;
using System.Collections.Generic;

namespace UFX
{
    public static class UFXDataSetHelper
    {
        public static void SetValue<T>(ref T p, Dictionary<string, DataValue> columns, Dictionary<string, UFXDataField> dataFieldMap)
        {
            foreach (var column in columns)
            {
                if (dataFieldMap.ContainsKey(column.Key))
                {
                    var dataField = dataFieldMap[column.Key];
                    Type type = p.GetType();
                    switch (dataField.ValueType)
                    {
                        case Model.Data.DataValueType.Int:
                            {
                                var val = column.Value.GetInt();
                                type.GetProperty(dataField.Name).SetValue(p, val);
                            }
                            break;
                        case Model.Data.DataValueType.Float:
                            {
                                var val = column.Value.GetDouble();
                                type.GetProperty(dataField.Name).SetValue(p, val);
                            }
                            break;
                        case Model.Data.DataValueType.String:
                            {
                                var val = column.Value.GetStr();
                                type.GetProperty(dataField.Name).SetValue(p, val);
                            }
                            break;
                        default:
                            type.GetProperty(dataField.Name).SetValue(p, column.Value.Value);
                            break;
                    }
                }
            }
        }

        public static List<T> ParseData<T>(DataParser parser) where T : new()
        {
            List<T> responseItems = new List<T>();

            var dataFieldMap = UFXDataBindingHelper.GetProperty<T>();
            for (int i = 1, count = parser.DataSets.Count; i < count; i++)
            {
                var dataSet = parser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    T p = new T();
                    UFXDataSetHelper.SetValue<T>(ref p, dataRow.Columns, dataFieldMap);
                    responseItems.Add(p);
                }
            }

            return responseItems;
        }

        public static List<T> ParseSubscribeData<T>(DataParser parser) where T : new()
        {
            List<T> responseItems = new List<T>();

            var dataFieldMap = UFXDataBindingHelper.GetProperty<T>();
            for (int i = 0, count = parser.DataSets.Count; i < count; i++)
            {
                var dataSet = parser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    T p = new T();
                    UFXDataSetHelper.SetValue<T>(ref p, dataRow.Columns, dataFieldMap);
                    responseItems.Add(p);
                }
            }
  
            return responseItems;
        }
    }
}
