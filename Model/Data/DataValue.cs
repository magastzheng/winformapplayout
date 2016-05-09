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
                //int temp = 0;
                //if (int.TryParse((string)Value, out temp))
                //{
                //    return temp;
                //}
                //else
                //{
                //    return 0;
                //}
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
                //double temp = 0.0f;
                //if (double.TryParse((string)Value, out temp))
                //{
                //    return temp;
                //}
                //return 0.0f;
                return (double)Value;
            }
            return 0.0f;
        }
    }

    //The raw data from UFX - t2sdk row
    public class RawDataRow
    {
        public Dictionary<string, DataValue> Columns { get; set; }
    }

    //The raw data from UFX - t2sdk set
    public class RawDataSet
    {
        public FunctionCode FunctionCode { get; set; }
        public List<RawDataRow> Rows { get; set; }
    }
}
