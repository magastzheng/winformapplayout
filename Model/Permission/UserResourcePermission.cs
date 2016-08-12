using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Permission
{
    public class UserResourcePermission
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ResourceId { get; set; }

        public int Permission { get; set; }
    }
}
