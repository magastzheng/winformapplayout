using Model.Permission;
using System.Collections.Generic;

namespace BLL.Permission
{
    /// <summary>
    /// The permission value MUST be a positive integer value.
    /// </summary>
    public class PermissionCalculator
    {
        /// <summary>
        /// Check the permission by AND with PermissionMask. 
        /// Compare the bit value of rights place. 
        /// 1 means having rights, 0 means no rights.
        /// </summary>
        /// <param name="perm">The permission value.</param>
        /// <param name="mask">The rights.</param>
        /// <returns>Returns true if it has rights, false if it does not have.</returns>
        public bool HasPermission(int perm, PermissionMask mask)
        {
            int result = CheckPermission(perm, mask);
            return result == (int)mask;
        }

        /// <summary>
        /// Check whether a list has the rights of given mask. Anyone element in the list has the rights, it will means having rights.
        /// </summary>
        /// <param name="perms">A of permission values.</param>
        /// <param name="mask">The rights.</param>
        /// <returns>Returns true if there is any permission value having the rights. False if there is no rights in 
        /// all permissions.
        /// </returns>
        public bool HasPermission(List<int> perms, PermissionMask mask)
        {
            foreach (var perm in perms)
            {
                if (CheckPermission(perm, mask) == (int)mask)
                {
                    break;
                }
            }

            return false;
        }

        /// <summary>
        /// Add the permission: perm & rights. The same rights can be granted repeatedly because only the bit is changed after
        /// the calculation and its value is always 1.
        /// </summary>
        /// <param name="perm">The original permission value.</param>
        /// <param name="mask">The rights.</param>
        /// <returns>A new permission value after adding the rights.</returns>
        public int GrantPermission(int perm, PermissionMask mask)
        {
            return AddPermission(perm, mask);
        }

        /// <summary>
        /// Add some rights for the original permission.
        /// </summary>
        /// <param name="perm">The original permission value.</param>
        /// <param name="masks">The list of some rights.</param>
        /// <returns>A new permission value after adding the rights.</returns>
        public int GrantPermission(int perm, List<PermissionMask> masks)
        {
            int result = perm;

            foreach (var mask in masks)
            {
                result = AddPermission(result, mask);
            }

            return result;
        }

        /// <summary>
        /// remove the permission: perm & ~rights. The same rights can be revoked repeatedly because only the bit is change after
        /// the calculation and its value is always 0.
        /// </summary>
        /// <param name="perm">The origin permission value.</param>
        /// <param name="mask">The rights.</param>
        /// <returns>A new permission value after removing the rights.</returns>
        public int RevokePermission(int perm, PermissionMask mask)
        {
            return RemovePermission(perm, mask);
        }

        /// <summary>
        /// Revoke some rights from the original permission.
        /// </summary>
        /// <param name="perm">The original permission value.</param>
        /// <param name="masks">The list of some rights.</param>
        /// <returns>A new permission value after removing the rights.</returns>
        public int RevokePermission(int perm, List<PermissionMask> masks)
        {
            int result = perm;

            foreach (var mask in masks)
            {
                result = RemovePermission(result, mask);
            }

            return result;
        }

        #region private method

        /// <summary>
        /// Check the permission by AND with PermissionMask. 
        /// Compare the bit value of rights place. 
        /// 1 means having rights, 0 means no rights.
        /// </summary>
        /// <param name="perm">The permission value.</param>
        /// <param name="mask">The rights.</param>
        /// <returns>Returns integer value after calculation. It needs to compare with the rights. It is equal to the right,
        /// it means having rights.
        /// </returns>
        private int CheckPermission(int perm, PermissionMask mask)
        {
            return perm & (int)mask;
        }

        /// <summary>
        /// Add the permission: perm & rights. The same rights can be granted repeatedly because only the bit is changed after
        /// the calculation and its value is always 1.
        /// </summary>
        /// <param name="perm">The original permission value.</param>
        /// <param name="mask">The rights.</param>
        /// <returns>A new permission value after adding the rights.</returns>
        private int AddPermission(int perm, PermissionMask mask)
        {
            return perm | (int)mask;
        }

        /// <summary>
        /// remove the permission: perm & ~rights. The same rights can be revoked repeatedly because only the bit is change after
        /// the calculation and its value is always 0.
        /// </summary>
        /// <param name="perm">The origin permission value.</param>
        /// <param name="mask">The rights.</param>
        /// <returns>A new permission value after removing the rights.</returns>
        private int RemovePermission(int perm, PermissionMask mask)
        {
            return perm & ~((int)mask);
        }

        #endregion
    }
}
