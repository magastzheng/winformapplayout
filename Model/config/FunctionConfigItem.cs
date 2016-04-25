using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class FunctionConfigParamItem
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Require { get; set; }
    }

    public class FunctionConfigResponseItem
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class FunctionConfigItem {
        public int Code { get; set; }
        public string Name_en { get; set; }
        public string Name_zh { get; set; }
        public List<FunctionConfigParamItem> Param { get; set; }
        public List<FunctionConfigResponseItem> Response { get; set; }
    }
}
