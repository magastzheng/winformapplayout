using BLL.UsageTracking;
using Model.Permission;
using Model.UsageTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Permission
{
    /// <summary>
    /// There are two types of user and resource permission. One is that the user is a role and the role has the permission,
    /// then the user has the permission; the other is that the user has the permission directly. The later case is in the
    /// area where user generates the resource.
    /// user --> role --> resource
    /// user --> resource
    /// A user is a role and the role has some permissions.
    /// </summary>
    public class PermissionManager
    {
        private RoleBLL _roleBLL = new RoleBLL();
        private UserRoleBLL _userRoleBLL = new UserRoleBLL();
        private FeatureBLL _featureBLL = new FeatureBLL();
        private TokenResourcePermissionBLL _userResourcePermBLL = new TokenResourcePermissionBLL();
        private PermissionCalculator _permCalculator = new PermissionCalculator();
        private UserActionTrackingBLL _userActionTrackingBLL = new UserActionTrackingBLL();

        #region user role permission

        //public List<TokenResourcePermission> GetRolePermission(Role role)
        //{
        //    return _userResourcePermBLL.GetByToken(role.Id, TokenType.Role);
        //}

        //public List<TokenResourcePermission> GetUserRolePermission(User user)
        //{
        //    var roles = GetRoles(user);
        //    var currentPerms = new List<TokenResourcePermission>();
        //    foreach (var role in roles)
        //    {
        //        var rolePerm = GetRolePermission(role);
        //    }

        //    return currentPerms;
        //}

        public bool HasFeaturePermission(int userId, string featureCode, PermissionMask mask)
        {
            bool hasPerm = false;
            var feature = GetFeature(featureCode);
            if (feature != null)
            {
                hasPerm = ValidatePermission(userId, feature.Id, ResourceType.Feature, mask);
            }

            _userActionTrackingBLL.Create(userId, Model.UsageTracking.ActionType.CheckPermission, ResourceType.Feature, feature.Id, mask.ToString());

            return hasPerm;
        }

        public int GrantFeaturePermission(int roleId, string featureCode, List<PermissionMask> masks)
        {
            var feature = GetFeature(featureCode);
            if (feature != null)
            {
                return GrantByRole(roleId, feature.Id, ResourceType.Feature, masks);
            }
            else
            {
                return -1;
            }
        }

        public int RevokeFeaturePermission(int roleId, string featureCode, List<PermissionMask> masks)
        { 
            var feature = GetFeature(featureCode);
            if (feature != null)
            {
                return RevokeByRole(roleId, feature.Id, ResourceType.Feature, masks);
            }
            else
            {
                return -1;
            }
        }
        #endregion

        #region permission by on role

        public bool HasRolePermission(RoleType roleId, int resourceId, ResourceType resourceType, PermissionMask mask)
        {
            return ValidatePermission(roleId, resourceId, resourceType, mask);
        }

        public int GrantByRole(RoleType roleId, int resourceId, ResourceType resourceType, List<PermissionMask> masks)
        {
            return GrantByRole((int)roleId, resourceId, resourceType, masks);
        }

        public int RevokeByRole(RoleType roleId, int resourceId, ResourceType resourceType, List<PermissionMask> masks)
        {
            return RevokeByRole((int)roleId, resourceId, resourceType, masks);
        }

        #endregion

        #region private method to check/grant/revoke the permission for Role

        private List<Role> GetRoles(User user)
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

        private bool ValidatePermission(int userId, int resourceId, ResourceType resourceType, PermissionMask mask)
        {
            bool hasPerm = false;
            var roles = GetRoles(userId);
            foreach (var role in roles)
            {
                if(ValidatePermission(role.RoleId, resourceId, resourceType, mask))
                {
                    hasPerm = true;
                    break;
                }
            }

            return hasPerm;
        }

        private bool ValidatePermission(RoleType roleId, int resourceId, ResourceType resourceType, PermissionMask mask)
        {
            var roleFeaturePerm = GetPermission((int)roleId, TokenType.Role, resourceId, resourceType);
            if (_permCalculator.HasPermission(roleFeaturePerm.Permission, mask))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Grant the permission to the Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="featureId"></param>
        /// <param name="masks"></param>
        /// <returns></returns>
        private int GrantByRole(int roleId, int resourceId, ResourceType resourceType, List<PermissionMask> masks)
        {
            var userResourcePerm = GetPermission(roleId, TokenType.Role, resourceId, resourceType);
            int perm = userResourcePerm.Permission;
            foreach (var mask in masks)
            {
                perm = _permCalculator.GrantPermission(perm, mask);
            }

            if (userResourcePerm.Id == roleId)
            {
                return UpdatePermission(roleId, TokenType.Role, resourceId, resourceType, perm);
            }
            else
            {
                return CreatePermission(roleId, TokenType.Role, resourceId, resourceType, perm);
            }
        }

        private int RevokeByRole(int roleId, int resourceId, ResourceType resourceType, List<PermissionMask> masks)
        {
            var userResourcePerm = GetPermission(roleId, TokenType.Role, resourceId, resourceType);
            int perm = userResourcePerm.Permission;

            foreach (var mask in masks)
            {
                perm = _permCalculator.RevokePermission(perm, mask);
            }

            if (userResourcePerm.Id == roleId)
            {
                return UpdatePermission(roleId, TokenType.Role, resourceId, resourceType, perm);
            }
            else
            {
                return CreatePermission(roleId, TokenType.Role, resourceId, resourceType, perm);
            }
        }

        #endregion

        #region User --> Role --> Permission

        public bool HasUserRolePermission(int userId, int resourceId, ResourceType resourceType, PermissionMask mask)
        {
            return ValidatePermission(userId, resourceId, resourceType, mask);
        }

        #endregion

        #region user resource permission

        public List<TokenResourcePermission> GetUserResourcePermission(User user)
        {
            return _userResourcePermBLL.GetByToken(user.Id, TokenType.User);
        }

        #endregion

        #region check/grant/revoke rights of resource

        public bool HasPermission(int userId, int resourceId, ResourceType resourceType, PermissionMask mask)
        {
            var userResourcePerm = GetPermission(userId, TokenType.User, resourceId, resourceType);

            Tracking(userId, ActionType.CheckPermission, resourceType, resourceId, (int)mask);

            return _permCalculator.HasPermission(userResourcePerm.Permission, mask);
        }

        public int GrantPermission(int userId, int resourceId, ResourceType resourceType, PermissionMask mask)
        {
            var masks = new List<PermissionMask>(){mask};

            return GrantPermission(userId, resourceId, resourceType, masks);
        }

        public int GrantPermission(int userId, int resourceId, ResourceType resourceType, List<PermissionMask> masks)
        {
            var userResourcePerm = GetPermission(userId, TokenType.User, resourceId, resourceType);
            int perm = userResourcePerm.Permission;
            foreach (var mask in masks)
            {
                perm = _permCalculator.GrantPermission(perm, mask);
            }

            Tracking(userId, ActionType.GrantPermission, resourceType, resourceId, perm);

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
            var masks = new List<PermissionMask>() { mask };

            return RevokePermission(userId, resourceId, resourceType, masks);
        }

        public int RevokePermission(int userId, int resourceId, ResourceType resourceType, List<PermissionMask> masks)
        {
            var userResourcePerm = GetPermission(userId, TokenType.User, resourceId, resourceType);
            int perm = userResourcePerm.Permission;
            
            foreach (var mask in masks)
            {
                perm = _permCalculator.RevokePermission(perm, mask);
            }

            Tracking(userId, ActionType.RevokePermission, resourceType, resourceId, perm);

            if (userResourcePerm.Id == userId)
            {
                return UpdatePermission(userId, TokenType.User, resourceId, resourceType, perm);
            }
            else
            {
                return CreatePermission(userId, TokenType.User, resourceId, resourceType, perm);
            }
        }

        /// <summary>
        /// change the permission value directly if there is old one, otherwise create new one.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="resourceId"></param>
        /// <param name="resourceType"></param>
        /// <param name="perm"></param>
        /// <param name="isUpdated"></param>
        /// <returns></returns>
        public int ChangePermission(int userId, int resourceId, ResourceType resourceType, int perm, bool isUpdated)
        {
            Tracking(userId, ActionType.EditPermission, resourceType, resourceId, perm);

            if (isUpdated)
            {
                return UpdatePermission(userId, TokenType.User, resourceId, resourceType, perm);
            }
            else
            {
                return CreatePermission(userId, TokenType.User, resourceId, resourceType, perm);
            }
        }
        #endregion

        #region permission value

        public List<PermissionMask> GetOwnerPermission()
        {
            return new List<PermissionMask>() { PermissionMask.Edit, PermissionMask.Delete, PermissionMask.Owner, PermissionMask.View};
        }

        public int AddPermission(int perm, PermissionMask mask)
        {
            return _permCalculator.GrantPermission(perm, mask);
        }

        public int AddPermission(int perm, List<PermissionMask> masks)
        {
            return _permCalculator.GrantPermission(perm, masks);
        }

        public int RemovePermission(int perm, PermissionMask mask)
        {
            return _permCalculator.RevokePermission(perm, mask);
        }

        public int RemovePermission(int perm, List<PermissionMask> masks)
        {
            return _permCalculator.RevokePermission(perm, masks);
        }

        #endregion

        #region private method

        private int CreatePermission(int token, TokenType tokenType, int resourceId, ResourceType resourceType, int permission)
        {
            TokenResourcePermission urPerm = new TokenResourcePermission
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
            TokenResourcePermission urPerm = new TokenResourcePermission
            {
                Token = token,
                TokenType = tokenType,
                ResourceId = resourceId,
                ResourceType = resourceType,
                Permission = permission,
            };

            return _userResourcePermBLL.Update(urPerm);
        }

        private TokenResourcePermission GetPermission(int token, TokenType tokenType, int resourceId, ResourceType resourceType)
        {
            return _userResourcePermBLL.Get(token, tokenType, resourceId, resourceType);
        }
        #endregion

        #region private method to get roles

        private List<UserRole> GetRoles(int userId)
        {
            return _userRoleBLL.GetByUser(userId);
        }

        private Feature GetFeature(string featureCode)
        {
            return _featureBLL.GetByCode(featureCode);
        }
        #endregion

        #region usage tracking

        private int Tracking(int userId, ActionType action, ResourceType resourceType, int resourceId, int perm)
        {
            return _userActionTrackingBLL.Create(userId, action, resourceType, resourceId, perm.ToString());
        }

        #endregion
    }
}
