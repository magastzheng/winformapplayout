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

        public List<RoleFeaturePermission> GetRoleFeaturePermission(Role role)
        {
            var roleFeaturePerms = _roleFeaturePermBLL.GetAll();
            var currentRoleFeaturePerms = roleFeaturePerms.Where(p => p.RoleId == role.Id).ToList();

            return currentRoleFeaturePerms;
        }

        public List<RoleFeaturePermission> GetRoleFeaturePermission(List<Role> roles)
        {
            var roleFeaturePerms = _roleFeaturePermBLL.GetAll();
            var currentRoleFeaturePerms = new List<RoleFeaturePermission>();
            foreach (var role in roles)
            {
                var tempRoleFeaturePerms = roleFeaturePerms.Where(p => p.RoleId == role.Id).ToList();
                currentRoleFeaturePerms.AddRange(tempRoleFeaturePerms);
            }

            return currentRoleFeaturePerms.Distinct().ToList();
        }

        public List<RoleFeaturePermission> GetRoleFeaturePermission(User user)
        {
            var roles = GetRoles(user);

            return GetRoleFeaturePermission(roles);
        }

        #endregion

        #region check/grant/revoke rights of permission

        public bool HasPermission(User user, Feature feature, PermissionMask mask)
        {
            var roleFeaturePerms = GetRoleFeaturePermission(user);
            var currentFeaturePerms = roleFeaturePerms.Where(p => p.FeatureId == feature.Id).ToList();
            var perms = currentFeaturePerms.Select(p => p.Permission).ToList();

            return _permCalculator.HasPermission(perms, mask);
        }

        public int GrantPermission(Role role, Feature feature, PermissionMask mask)
        {
            var roleFeaturePerm = _roleFeaturePermBLL.Get(role.Id, feature.Id);
            roleFeaturePerm.Permission = _permCalculator.GrantPermission(roleFeaturePerm.Permission, mask);

            return _roleFeaturePermBLL.Update(roleFeaturePerm);
        }

        public int RevokePermission(Role role, Feature feature, PermissionMask mask)
        {
            var roleFeaturePerm = _roleFeaturePermBLL.Get(role.Id, feature.Id);
            roleFeaturePerm.Permission = _permCalculator.RevokePermission(roleFeaturePerm.Permission, mask);

            return _roleFeaturePermBLL.Update(roleFeaturePerm);
        }

        #endregion

        #region user resource permission

        public List<UserResourcePermission> GetUserResourcePermission(User user)
        {
            return _userResourcePermBLL.Get(user.Id);
        }

        #endregion

        #region check/grant/revoke rights of resource

        public bool HasPermission(User user, Resource resource, PermissionMask mask)
        {
            var userResourcePerm = _userResourcePermBLL.Get(user.Id, resource.Id);

            return _permCalculator.HasPermission(userResourcePerm.Permission, mask);
        }

        public int GrantPermission(User user, Resource resource, PermissionMask mask)
        {
            var userResourcePerm = _userResourcePermBLL.Get(user.Id, resource.Id);

            return _permCalculator.GrantPermission(userResourcePerm.Permission, mask);
        }

        public int RevokePermission(User user, Resource resource, PermissionMask mask)
        {
            var userResourcePerm = _userResourcePermBLL.Get(user.Id, resource.Id);

            return _permCalculator.RevokePermission(userResourcePerm.Permission, mask);
        }
        #endregion
    }
}
