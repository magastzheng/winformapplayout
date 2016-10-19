using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Permission
{
    public class TokenResourcePermission
    {
        public int Id { get; set; }

        public int Token { get; set; }

        public TokenType TokenType { get; set; }

        public int ResourceId { get; set; }

        public ResourceType ResourceType { get; set; }

        public int Permission { get; set; }
    }
}
