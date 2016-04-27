using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class ImportSheet
    {
        public string Id { get; set; }
        public List<DataColumnHeader> Columns { get; set; }
    }
}
