using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Permission
{
    public enum PermissionMask
    {
        Create  = 1,
        Delete  = 2,
        Edit    = 4,
        Query   = 8,
        View    = 16,
    }
}
