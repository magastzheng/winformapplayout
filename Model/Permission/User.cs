using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Permission
{
    public enum UserStatus 
    {
        Inactive = 0,
        Active = 1,
    }

    public class User
    {
        public int Id { get; set; }

        public string Operator { get; set; }

        public string Name { get; set; }

        public UserStatus Status { get; set; }
    }
}
