using Model.UFX;
using System.Collections.Generic;

namespace Model.config
{
    public class FieldItem
    {
        public string Name { get; set; }
        public sbyte Type { get; set; }
        public int Width { get; set; }
        public int Scale { get; set; }
        public UFXFieldRequireType Require { get; set; }    //0 - No; 1 - Yes
    }

    public class FunctionItem
    {
        public int Code { get; set; }
        public List<FieldItem> RequestFields { get; set; }
        public List<FieldItem> ResponseFields { get; set; }
    }
}
