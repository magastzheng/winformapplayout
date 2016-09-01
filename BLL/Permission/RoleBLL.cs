using DBAccess.Permission;
using Model.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int Delete(int roleId)
        {
            return _roledao.Delete(roleId);
        }

        public Role Get(int roleId)
        {
            return _roledao.Get(roleId);
        }

        public List<Role> GetAll()
        {
            return _roledao.Get();
        }
    }
}
