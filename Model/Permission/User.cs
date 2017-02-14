using System;

namespace Model.Permission
{
    public class User
    {
        public int Id { get; set; }

        public string Operator { get; set; }

        public string Name { get; set; }

        public UserStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifieDate { get; set; }
    }
}
