using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Permission
{
    public enum RoleStatus
    {
        Inactive = 0,
        Normal = 1,
    }

    public enum RoleType
    { 
        Administrator = 1,
        SystemManager = 2,
        FundManager = 5,
        Dealer = 6,
    }

    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public RoleStatus Status { get; set; }

        public RoleType Type { get; set; }
    }
}
