using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class DataCellRange
    {
        public string Name { get; set; }
        public int FirstRow { get; set; }
        public int LastRow { get; set; }
        public int FirstColumn { get; set; }
        public int LastColumn { get; set; }
    }

    public class DataHeader
    {
        public string Name { get; set; }
        public List<DataColumnHeader> Children { get; set; }
    }

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
