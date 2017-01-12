using Model.UFX;
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
            else if (Type == DataValueType.Float)
            {
                return Convert.ToInt32(Value);
            }
            else if (Type == DataValueType.String)
            {
                int temp;
                bool ret = int.TryParse(Value.ToString(), out temp);
                if(ret)
                {
                    return temp;
                }
                else
                {
                    double dTemp;
                    ret = double.TryParse(Value.ToString(), out dTemp);
                    if (ret)
                    {
                        return (int)dTemp;
                    }
                }

                return 0;
            }
            else
            {
                return 0;
            }
        }
        public double GetDouble() 
        {
            if (Type == DataValueType.Float)
            {
                return (double)Value;
            }
            else if (Type == DataValueType.Int)
            {
                return (double)Value;
            }
            else if (Type == DataValueType.String)
            {
                double temp = 0.0f;
                if (double.TryParse(Value.ToString(), out temp))
                {
                    return temp;
                }

                return 0.0f;
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
        public UFXFunctionCode FunctionCode { get; set; }
        public List<RawDataRow> Rows { get; set; }
    }
}
