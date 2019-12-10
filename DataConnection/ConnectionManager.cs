using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataConnection2
{
    public class ConnectionManager
    {
        private static SqlConnection connection = null;
        private ConnectionManager() { }
        public static SqlConnection GetConnection()
        {
            if(connection == null)
            {
                var connectionString = "Data Source = .; Initial Catalog = Tema10Dec; Integrated Security = True;";
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            return connection;
        }
    }
}
