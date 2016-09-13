using DBAccess.Permission;
using Model.Permission;
using System.Collections.Generic;

namespace BLL.Permission
{
    public class RoleFeaturePermissionBLL
    {
        private RoleFeaturePermissionDAO _rfpdao = new RoleFeaturePermissionDAO();
        public RoleFeaturePermissionBLL()
        { 
        }

        public int Create(RoleFeaturePermission rfPermission)
        {
            return _rfpdao.Create(rfPermission);
        }

        public int Update(RoleFeaturePermission rfPermission)
        {
            return _rfpdao.Update(rfPermission);
        }

        public int Delete(int roleId, int featureId)
        {
            return _rfpdao.Delete(roleId, featureId);
        }

        public RoleFeaturePermission Get(int roleId, int featureId)
        {
            return _rfpdao.Get(roleId, featureId);
        }

        public List<RoleFeaturePermission> GetAll()
        {
            return _rfpdao.GetAll();
        }
    }
}
