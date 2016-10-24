using Model.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UsageTracking
{
    public class UserActionTracking
    {
        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public ActionType ActionType { get; set; }

        public ResourceType ResourceType { get; set; }

        public int ResourceId { get; set; }

        public string Details { get; set; }
    }
}
