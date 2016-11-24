using Model.Binding.BindingUtil;
using Model.Data;
using System;
using System.Collections.Generic;

namespace BLL.UFX
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
    }
}
