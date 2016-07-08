
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
