using Model.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Permission
{
    public class PermissionManager
    {
        private RoleBLL _roleBLL = new RoleBLL();
        private UserRoleBLL _userRoleBLL = new UserRoleBLL();
        private RoleFeaturePermissionBLL _roleFeaturePermBLL = new RoleFeaturePermissionBLL();
        private UserResourcePermissionBLL _userResourcePermBLL = new UserResourcePermissionBLL();
        private PermissionCalculator _permCalculator = new PermissionCalculator();

        #region user role permission

        public List<Role> GetRoles(User user)
        {
            var roles = _roleBLL.GetAll();
            var userRoles = _userRoleBLL.GetAll();
            var valideUserRoles = userRoles.Where(p => p.UserId == user.Id).ToList();
            List<Role> currentRoles = new List<Role>();
            foreach (var userRole in userRoles)
            {
                var role = roles.Find(p => p.Id == userRole.RoleId);
                if (role != null)
                {
                    currentRoles.Add(role);
                }
            }

            return currentRoles;
        }

        public List<UserResourcePermission> GetRolePermission(Role role)
        {
            return _userResourcePermBLL.GetByToken(role.Id, TokenType.Role);
        }

        public List<UserResourcePermission> GetUserRolePermission(User user)
        {
            var roles = GetRoles(user);
            var currentPerms = new List<UserResourcePermission>();
            foreach (var role in roles)
            {
                var rolePerm = GetRolePermission(role);
            }

            return currentPerms;
        }

        #endregion

        #region user resource permission

        public List<UserResourcePermission> GetUserResourcePermission(User user)
        {
            return _userResourcePermBLL.GetByToken(user.Id, TokenType.User);
        }

        #endregion

        #region check/grant/revoke rights of resource

        public bool HasPermission(int userId, int resourceId, ResourceType resourceType, PermissionMask mask)
        {
            var userResourcePerm = _userResourcePermBLL.Get(userId, TokenType.User, resourceId, resourceType);

            return _permCalculator.HasPermission(userResourcePerm.Permission, mask);
        }

        public int GrantPermission(int userId, int resourceId, ResourceType resourceType, PermissionMask mask)
        {
            var userResourcePerm = _userResourcePermBLL.Get(userId, TokenType.User, resourceId, resourceType);

            int perm = _permCalculator.GrantPermission(userResourcePerm.Permission, mask);
            if (userResourcePerm.Id == userId)
            {
                return UpdatePermission(userId, TokenType.User, resourceId, resourceType, perm);
            }
            else
            {
                return CreatePermission(userId, TokenType.User, resourceId, resourceType, perm);
            }
        }

        public int RevokePermission(int userId, int resourceId, ResourceType resourceType, PermissionMask mask)
        {
            var userResourcePerm = _userResourcePermBLL.Get(userId, TokenType.User, resourceId, resourceType);

            int perm = _permCalculator.RevokePermission(userResourcePerm.Permission, mask);
            if (userResourcePerm.Id == userId)
            {
                return UpdatePermission(userId, TokenType.User, resourceId, resourceType, perm);
            }
            else
            {
                return CreatePermission(userId, TokenType.User, resourceId, resourceType, perm);
            }
        }
        #endregion

        #region private method

        private int CreatePermission(int token, TokenType tokenType, int resourceId, ResourceType resourceType, int permission)
        {
            UserResourcePermission urPerm = new UserResourcePermission
            {
                Token = token,
                TokenType = tokenType,
                ResourceId = resourceId,
                ResourceType = resourceType,
                Permission = permission,
            };

            return _userResourcePermBLL.Create(urPerm);
        }

        private int UpdatePermission(int token, TokenType tokenType, int resourceId, ResourceType resourceType, int permission)
        {
            UserResourcePermission urPerm = new UserResourcePermission
            {
                Token = token,
                TokenType = tokenType,
                ResourceId = resourceId,
                ResourceType = resourceType,
                Permission = permission,
            };

            return _userResourcePermBLL.Update(urPerm);
        }

        #endregion
    }
}
