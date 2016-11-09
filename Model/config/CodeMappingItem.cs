using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.config
{
    public class CodeMappingItem
    {
        public string SecuCode { get; set; }

        public string WindCode { get; set; }
    }

    public class CodeMapping
    {
        public string Name { get; set; }

        public List<CodeMappingItem> Mapping { get; set; }
    }
}
