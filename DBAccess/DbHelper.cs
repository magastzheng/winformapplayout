using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public delegate void DbReaderCallback(DbDataReader reader);

    public class DbHelper
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string dbProviderName = ConfigurationManager.AppSettings["DbHelperProvider"];
        private static string dbConnectionString = ConfigurationManager.AppSettings["DbHelperConnectionString"];
        private DbConnection connection;

        public DbHelper()
        {
            this.connection = CreateConnection(dbConnectionString);
        }

        public DbHelper(string connectionString)
        {
            this.connection = CreateConnection(connectionString);
        }

        public static DbConnection CreateConnection(string dbConnectionString)
        {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
            DbConnection dbConn = dbFactory.CreateConnection();
            dbConn.ConnectionString = dbConnectionString;
            return dbConn;
        }

        public DbCommand GetStoredProcCommand(string storedProcedure)
        {
            DbCommand dbCommand = this.connection.CreateCommand();
            dbCommand.CommandText = storedProcedure;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            
            return dbCommand;
        }

        public DbCommand GetSqlStringCommand(string sqlQuery)
        {
            DbCommand dbCommand = this.connection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.CommandType = System.Data.CommandType.Text;

            return dbCommand;
        }

        #region 增加参数

        public void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection)
        {
            foreach (DbParameter dbParameter in dbParameterCollection)
            {
                cmd.Parameters.Add(dbParameter);
            }
        }

        public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = size;
            dbParameter.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(dbParameter);
        }

        public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object dbValue)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = dbValue;
            dbParameter.Direction = ParameterDirection.Input;

            cmd.Parameters.Add(dbParameter);
        }

        public void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(dbParameter);
        }

        public DbParameter GetParameter(DbCommand cmd, string parameterName)
        {
            return cmd.Parameters[parameterName];
        }

        #endregion

        #region 执行

        public DataTable ExecuteDataTable(DbCommand cmd)
        {
            DbProviderFactory dbFactor = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
            DbDataAdapter dbDataAdapter = dbFactor.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable);

            return dataTable;
        }


        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            Open(cmd);
            DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public int ExecuteNonQuery(DbCommand cmd)
        {
            Open(cmd);
            int ret = cmd.ExecuteNonQuery();
            Close(cmd.Connection);

            return ret;
        }

        #endregion

        #region 打开和关闭

        public void Open(DbConnection conn)
        {
            if (conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch
                {
                    logger.Error("Cannot open database connection " + conn.ConnectionString);
                    throw;
                }
            }
        }

        public void Open(DbCommand cmd)
        {
            if (cmd.Connection.State != ConnectionState.Open)
            {
                try
                {
                    cmd.Connection.Open();
                }
                catch
                {
                    logger.Error("Cannot open database connection " + cmd.Connection.ConnectionString);
                    throw;
                }
            }
        }

        public void Close(DbConnection conn)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                try
                {
                    conn.Close();
                }
                catch
                {
                    logger.Error("Fail to close the database connection: " + conn.ConnectionString);
                    throw;
                }
            }
        }   
        #endregion
    }
}
