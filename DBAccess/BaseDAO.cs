using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public abstract class BaseDAO
    {
        protected DbHelper _dbHelper;

        public BaseDAO()
        { 
            _dbHelper = new DbHelper(); 
        }

        public BaseDAO(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
    }
}
