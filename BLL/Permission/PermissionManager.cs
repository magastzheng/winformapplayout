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
    }
}
