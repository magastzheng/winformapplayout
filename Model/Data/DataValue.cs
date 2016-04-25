using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class DataValue
    {
        public DataValueType Type { get; set; }
        public object Value { get; set; }
        public string GetStr() 
        {
            if (Type == DataValueType.String)
            {
                return (string)Value;
            }
            else
            {
                return string.Empty;
            }
        }
        public int GetInt() 
        {
            if (Type == DataValueType.Int)
            {
                return (int)Value;
            }
            else
            {
                return -1;
            }
        }
        public double GetDouble() 
        {
            if (Type == DataValueType.Float)
            {
                return (double)Value;
            }
            return 0.0f;
        }
    }

    public class DataRow
    {
        public Dictionary<string, DataValue> Columns { get; set; }
    }

    public class DataSet
    {
        public FunctionCode FunctionCode { get; set; }
        public List<DataRow> Rows { get; set; }
    }
}
