using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.config
{
    public class ErrorItem
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }

    public class Label
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }

    public class Message
    {
        public List<ErrorItem> Errors { get; set; }

        public List<Label> Labels { get; set; }
    }
}
