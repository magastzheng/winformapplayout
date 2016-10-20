using DBAccess.Permission;
using Model.Permission;
using System.Collections.Generic;

namespace BLL.Permission
{
    public class RoleBLL
    {
        private RoleDAO _roledao = new RoleDAO();

        public RoleBLL()
        { 
        }

        public int Create(Role role)
        {
            return _roledao.Create(role);
        }

        public int Update(Role role)
        {
            return _roledao.Update(role);
        }

        public int Delete(RoleType roleId)
        {
            return _roledao.Delete(roleId);
        }

        public Role Get(RoleType roleId)
        {
            return _roledao.Get(roleId);
        }

        public List<Role> GetAll()
        {
            return _roledao.Get();
        }
    }
}
