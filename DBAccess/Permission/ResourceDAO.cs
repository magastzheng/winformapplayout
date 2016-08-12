using log4net;
using Model.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Permission
{
    public class ResourceDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Create = "procResourcesInsert";
        private const string SP_Modify = "procResourcesUpdate";
        private const string SP_Delete = "procResourcesDelete";
        private const string SP_Select = "procResourcesSelect";

        public ResourceDAO()
            : base()
        { 
        }

        public ResourceDAO(DbHelper dbHelper)
            : base(dbHelper)
        {
        }

        public int Create(Resource resource)
        { 
         //TODO:
            return -1;
        }
    }
}
