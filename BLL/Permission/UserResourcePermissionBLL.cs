using DBAccess.Permission;
using Model.Permission;
using System.Collections.Generic;

namespace BLL.Permission
{
    public class UserResourcePermissionBLL
    {
        private UserResourcePermissionDAO _urpermissiondao = new UserResourcePermissionDAO();
        public UserResourcePermissionBLL()
        { 
        }

        public int Create(UserResourcePermission urPermission)
        {
            return _urpermissiondao.Create(urPermission);
        }

        public int Update(UserResourcePermission urPermission)
        {
            return _urpermissiondao.Update(urPermission);
        }

        public int Delete(int userId, int resouceId)
        {
            return _urpermissiondao.Delete(userId, resouceId);
        }

        public UserResourcePermission Get(int userId, int resourceId)
        {
            return _urpermissiondao.Get(userId, resourceId);
        }

        public List<UserResourcePermission> Get(int userId)
        {
            return _urpermissiondao.Get(userId);
        }

        public List<UserResourcePermission> GetAll()
        {
            return _urpermissiondao.GetAll();
        }
    }
}
