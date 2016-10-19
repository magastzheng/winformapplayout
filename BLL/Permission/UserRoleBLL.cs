using DBAccess.Permission;
using Model.Permission;
using System.Collections.Generic;

namespace BLL.Permission
{
    public class UserRoleBLL
    {
        private UserRoleDAO _userroledao = new UserRoleDAO();
        public UserRoleBLL()
        { 
        }

        public int Create(UserRole userRole)
        {
            return _userroledao.Create(userRole);
        }

        public int Delete(int userId, int roleId)
        {
            return _userroledao.Delete(userId, roleId);
        }

        public UserRole Get(int userId, int roleId)
        {
            return _userroledao.Get(userId, roleId);
        }

        public List<UserRole> GetByUser(int userId)
        {
            return _userroledao.GetByUser(userId);
        }

        public List<UserRole> GetAll()
        {
            return _userroledao.GetAll();
        }
    }
}
