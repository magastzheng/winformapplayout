using Model.Permission;
using System;

namespace Model.UsageTracking
{
    public class UserActionTracking
    {
        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public ActionType ActionType { get; set; }

        public ResourceType ResourceType { get; set; }

        public int ResourceId { get; set; }

        public int Num { get; set; }

        public ActionStatus ActionStatus { get; set; }

        public string Details { get; set; }
    }
}
