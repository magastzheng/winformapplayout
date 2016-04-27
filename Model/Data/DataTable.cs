using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class DataColumnHeader
    {
        public string Name { get; set; }
        public DataValueType Type { get; set; }
    }

    public class DataRow
    {
        public List<DataValue> Columns { get; set; }
    }

    public class DataTable
    {
        public Dictionary<string, int> ColumnIndex { get; set; }
        public List<DataRow> Rows { get; set; }
    }
}
