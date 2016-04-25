using DBAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccessTest
{
    [TestClass]
    public class DbHelperTest
    {
        [TestMethod]
        public void TestExecuteNonQuery()
        {
            DbHelper dbHelper = new DbHelper();
            string sqlInsert = @"insert into account values(1, 'Magast Zheng', 'magastzheng@163.com', 1, 'Test123')";
            var cmd = dbHelper.GetSqlStringCommand(sqlInsert);
            int ret = dbHelper.ExecuteNonQuery(cmd);

            sqlInsert = @"select * from account";
            cmd = dbHelper.GetSqlStringCommand(sqlInsert);
            var table = dbHelper.ExecuteDataTable(cmd);
            foreach(DataRow row in table.Rows)
            {
                int id = (int)row["Id"];
                string name = (string)row["Name"];
                string email = (string)row["Email"];
                int opTye = (int)row["OpType"];
                string passCode = (string)row["PassCode"];
            }

            var reader = dbHelper.ExecuteReader(cmd);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string info = string.Format("{0}\t{1}\t{2}\t{3}\t{4}", reader[0], reader[1], reader[2], reader[3], reader[4]);
                    Console.WriteLine(info);
                    //int idPos = reader.GetOrdinal("Id");
                    
                    //Console.Write(reader.GetString(idPos));
                    //Console.Write(reader.GetString(reader.GetOrdinal("Name")));
                    //Console.Write(reader.GetString(reader.GetOrdinal("Email")));
                    //Console.Write(reader.GetString(reader.GetOrdinal("OpType")));
                    //Console.Write(reader.GetString(reader.GetOrdinal("PassCode")));
                    //Console.Write("\n");
                }
            }
        }

        [TestMethod]
        public void TestExecuteQueryParameter()
        {
            DbHelper dbHelper = new DbHelper();
            string sqlInsert = @"select * from account where Id=@id";
            var cmd = dbHelper.GetSqlStringCommand(sqlInsert);
            SqlParameter[] sqlParameters = new SqlParameter[] 
            {
                new SqlParameter("@id", 2)
            };
            cmd.Parameters.AddRange(sqlParameters);
           
            var reader = dbHelper.ExecuteReader(cmd);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string info = string.Format("{0}\t{1}\t{2}\t{3}\t{4}", reader[0], reader[1], reader[2], reader[3], reader[4]);
                    Console.WriteLine(info);
                }
            }
        }
    }
}
