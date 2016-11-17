using log4net;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace DBAccess
{
    public delegate void DbReaderCallback(DbDataReader reader);

    public class DbHelper
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string dbProviderName = ConfigurationManager.AppSettings["DbHelperProvider"];
        private static string dbConnectionString = ConfigurationManager.AppSettings["DbHelperConnectionString"];
        private DbConnection connection;

        public DbConnection Connection
        {
            get { return this.connection; }
        }

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

        public DbCommand GetCommand()
        {
            return this.connection.CreateCommand();
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
            DbDataReader reader = cmd.ExecuteReader(); //cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public int ExecuteNonQuery(DbCommand cmd)
        {
            Open(cmd);

            //logger.Info(DbHelper.GetCommandSql(cmd));

            int ret = cmd.ExecuteNonQuery();
            Close(cmd.Connection);

            return ret;
        }

        #endregion


        //#region 事务

        //public int ExecuteTrans(List<DbCommand> dbCommands)
        //{
        //    Open(this.connection);

        //    //SqlTransaction 
        //    DbTransaction trans = this.connection.BeginTransaction();

        //    try
        //    {
        //        foreach (var dbcommand in dbCommands)
        //        {
        //            dbcommand.Transaction = trans;
        //            dbcommand.ExecuteNonQuery();
        //        }

        //        trans.Commit();
        //    }
        //}
    
        //#endregion

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
                finally
                {
                    //logger.Info(DbHelper.GetCommandSql(conn.));
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
                finally
                {
                    logger.Info(DbHelper.GetCommandSql(cmd));
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

        #region 输出sql语句

        public static string GetCommandSql(DbCommand cmd)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("use " + cmd.Connection.Database + ";");
            //sb.Append(cmd.CommandText);
            switch (cmd.CommandType)
            {
                case CommandType.StoredProcedure:
                    {
                        sb.AppendLine("declare @return_value int;");
                        foreach (SqlParameter sp in cmd.Parameters)
                        {
                            if ((sp.Direction == ParameterDirection.InputOutput) || (sp.Direction == ParameterDirection.Output))
                            {
                                sb.AppendLine("declare " + sp.ParameterName + "\t" + sp.SqlDbType.ToString() + "\t= ");
                                //TODO: generate different value type of parameter
                                sb.AppendLine(((sp.Direction == ParameterDirection.Output) ? "null" : sp.Value.ToString()) + ";");
                            }
                        }

                        sb.AppendLine("exec [" + cmd.CommandText + "]");

                        bool haveSp = false;
                        foreach (SqlParameter sp in cmd.Parameters)
                        {
                            if (sp.Direction != ParameterDirection.ReturnValue)
                            {
                                if (!haveSp)
                                {
                                    haveSp = true;
                                }

                                if (sp.Direction == ParameterDirection.Input)
                                {
                                    sb.AppendLine(sp.ParameterName + " = " + sp.Value.ToString());
                                }
                                else
                                {
                                    sb.AppendLine(sp.ParameterName + " = " + sp.ParameterName + " output");
                                }

                                sb.Append(",");
                            }
                        }

                        if (haveSp)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }

                        sb.AppendLine(";");
                        sb.AppendLine("select 'Return Value' = convert(varchar, @return_value);");

                        foreach (SqlParameter sp in cmd.Parameters)
                        {
                            if ((sp.Direction == ParameterDirection.InputOutput) || (sp.Direction == ParameterDirection.Output))
                            {
                                sb.AppendLine("select '" + sp.ParameterName + "' = convert(varchar, " + sp.ParameterName + ");");
                            }
                        }
                    }
                    break;
                case CommandType.Text:
                    sb.AppendLine(cmd.CommandText);
                    break;
            }

            return sb.ToString();
        }

        #endregion
    }
}
