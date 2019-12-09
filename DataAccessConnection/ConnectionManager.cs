using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessConnection
{
    public class ConnectionManager
    {
        private const string connectionString = @"Data Source=MARIA-PC\SQLEXPRESS;Initial Catalog=Books;Integrated Security=True";

        private static SqlConnection connection = null;
        private ConnectionManager() { }

        public static SqlConnection GetSqlConnection()
        {
            if (connection == null)
            {
                //string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
                connection = new SqlConnection(connectionString);

                connection.Open();
            }


            return connection;
        }
    }
}
