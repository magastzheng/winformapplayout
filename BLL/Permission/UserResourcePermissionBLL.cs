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

        public int Delete(UserResourcePermission urPermission)
        {
            return _urpermissiondao.Delete(urPermission.Token, urPermission.TokenType, urPermission.ResourceId, urPermission.ResourceType);
        }

        public UserResourcePermission Get(int token, TokenType tokenType, int resourceId, ResourceType resourceType)
        {
            return _urpermissiondao.GetSingle(token, tokenType, resourceId, resourceType);
        }

        public List<UserResourcePermission> GetByToken(int token, TokenType tokenType)
        {
            return _urpermissiondao.GetByToken(token, tokenType);
        }

        public List<UserResourcePermission> GetByResourceType(int token, TokenType tokenType, ResourceType resourceType)
        {
            return _urpermissiondao.GetByResourceType(token, tokenType, resourceType);
        }
    }
}
