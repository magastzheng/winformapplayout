using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controls.Entity
{
    public class TSNavNodeData
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        //public bool IsRoot { get; set; }
        public bool IsExpansed { get; set; }
        public string Title { get; set; }
        public List<TSNavNodeData> Children { get; set; }
    }
}
