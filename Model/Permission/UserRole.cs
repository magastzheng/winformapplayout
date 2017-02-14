using System;

namespace Model.Permission
{
    public class UserRole
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public RoleType RoleId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifieDate { get; set; }
    }
}
