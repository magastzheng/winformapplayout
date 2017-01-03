using DBAccess.Permission;
using Model.Permission;
using System.Collections.Generic;

namespace BLL.Permission
{
    public class TokenResourcePermissionBLL
    {
        private TokenResourcePermissionDAO _urpermissiondao = new TokenResourcePermissionDAO();
        public TokenResourcePermissionBLL()
        { 
        }

        public int Create(TokenResourcePermission urPermission)
        {
            return _urpermissiondao.Create(urPermission);
        }

        public int Update(TokenResourcePermission urPermission)
        {
            return _urpermissiondao.Update(urPermission);
        }

        public int Delete(int resourceId, ResourceType resourceType)
        {
            return _urpermissiondao.Delete(resourceId, resourceType);
        }

        public TokenResourcePermission Get(int token, TokenType tokenType, int resourceId, ResourceType resourceType)
        {
            return _urpermissiondao.GetSingle(token, tokenType, resourceId, resourceType);
        }

        public List<TokenResourcePermission> GetByToken(int token, TokenType tokenType)
        {
            return _urpermissiondao.GetByToken(token, tokenType);
        }

        public List<TokenResourcePermission> GetByResourceType(int token, TokenType tokenType, ResourceType resourceType)
        {
            return _urpermissiondao.GetByResourceType(token, tokenType, resourceType);
        }

        public List<TokenResourcePermission> GetByResource(int resourceId, ResourceType resourceType)
        {
            return _urpermissiondao.GetByResource(resourceId, resourceType);
        }
    }
}
