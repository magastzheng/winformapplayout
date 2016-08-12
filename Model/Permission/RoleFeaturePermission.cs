using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Permission
{
    public class RoleFeaturePermission
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public int FeatureId { get; set; }

        public int Permission { get; set; }
    }
}
