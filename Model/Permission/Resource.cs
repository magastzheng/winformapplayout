using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Permission
{
    public class Resource
    {
        public int Id { get; set; }

        public int RefId { get; set; }

        public ResourceType Type { get; set; }

        public string Name { get; set; }
    }
}
