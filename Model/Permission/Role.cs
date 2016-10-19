
namespace Model.Permission
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public RoleStatus Status { get; set; }

        public RoleType Type { get; set; }
    }
}
