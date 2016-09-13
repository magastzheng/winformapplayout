using Model.Permission;
using System.Collections.Generic;

namespace BLL.Permission
{
    public class PermissionCalculator
    {
        /// <summary>
        /// Check the permission by AND with PermissionMask.
        /// </summary>
        /// <param name="perm">The permission value.</param>
        /// <param name="mask">The rights.</param>
        /// <returns>Returns true if it has rights, false if it does not have.</returns>
        public bool HasPermission(int perm, PermissionMask mask)
        {
            int result = perm & (int)mask;
            return result == (int)mask;
        }

        /// <summary>
        /// Check whether a list has the rights of given mask.
        /// </summary>
        /// <param name="perms">A of permission values.</param>
        /// <param name="mask">The rights.</param>
        /// <returns>Returns true if there is any permission value having the rights. False if there is no rights in 
        /// all permissions.
        /// </returns>
        public bool HasPermission(List<int> perms, PermissionMask mask)
        {
            bool hasRights = false;
            foreach (var perm in perms)
            {
                hasRights = (perm & (int)mask) == (int)mask;
                if (hasRights)
                {
                    break;
                }
            }

            return hasRights;
        }

        /// <summary>
        /// Add the permission: perm & rights
        /// </summary>
        /// <param name="perm">The original permission value.</param>
        /// <param name="mask">The rights.</param>
        /// <returns>A new permission value after adding the rights.</returns>
        public int GrantPermission(int perm, PermissionMask mask)
        {
            int result = (perm | (int)mask);

            return result;
        }

        public int GrantPermission(int perm, List<PermissionMask> masks)
        {
            int result = perm;

            foreach (var mask in masks)
            {
                result = (result | (int)mask);
            }

            return result;
        }

        /// <summary>
        /// remove the permission: perm & ~rights
        /// </summary>
        /// <param name="perm">The origin permission value.</param>
        /// <param name="mask">The rights.</param>
        /// <returns>A new permission value after removing the rights.</returns>
        public int RevokePermission(int perm, PermissionMask mask)
        {
            int result = (perm & ~((int)mask));

            return result;
        }

        public int RevokePermission(int perm, List<PermissionMask> masks)
        {
            int result = perm;

            foreach (var mask in masks)
            { 
                result = (result & ~((int)mask));
            }

            return result;
        }
    }
}
