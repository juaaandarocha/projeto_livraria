using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Helper
{
    public static class DBHelper
    {
        public const string DB_PROVIDER = "MySql.Data.MySqlClient";

        public static DbTransaction CreateTransaction(string connectionString)
        {
            var conn = DbProviderFactories.GetFactory(DB_PROVIDER).CreateConnection();
            conn.ConnectionString = connectionString;
            conn.Open();

            return conn.BeginTransaction();
        }

        public static DbConnection CreateConnection(string connectionString)
        {
            var conn = DbProviderFactories.GetFactory(DB_PROVIDER).CreateConnection();
            conn.ConnectionString = connectionString;
            conn.Open();

            return conn;
        }
    }
}
