using Model.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LoginUser
    {
        public string Operator { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public User LocalUser { get; set; }
    }
}
