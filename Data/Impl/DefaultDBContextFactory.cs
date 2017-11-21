using Data.Helper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Impl
{
    public class DefaultDBContextFactory : IDBContextFactory
    {
        protected string ConnectionString { get; private set; }

        public DefaultDBContextFactory(string connectionString) {
            this.ConnectionString = connectionString;
        }

        IDBContext IDBContextFactory.CreateNewContext()
        {
            DbConnection conn = DBHelper.CreateConnection(this.ConnectionString);
            return new DefaultDBContext(conn, null);
        }

        IDBContext IDBContextFactory.CreateNewTransactionContext()
        {
            DbTransaction tran = DBHelper.CreateTransaction(this.ConnectionString);
            return new DefaultDBContext(tran.Connection, tran);
        }
    }
}
