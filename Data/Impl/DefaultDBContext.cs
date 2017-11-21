using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace Data.Impl
{
    public class DefaultDBContext : IDBContext
    {
        public bool InTransaction => this.InnerTransaction != null;

        public DbConnection InnerConnection { get; private set; }

        public DbTransaction InnerTransaction { get; private set; }

        public DefaultDBContext(DbConnection connection, DbTransaction transaction)
        {
            this.InnerConnection = connection;
            this.InnerTransaction = transaction;
        }

        public void Commit()
        {
            if (this.InTransaction) {
                this.InnerTransaction.Commit();
                this.InnerTransaction = null;
            }

            if (this.InnerConnection != null) {
                this.InnerConnection.Close();
                this.InnerConnection = null;
            }
        }

        public void Dispose()
        {
            if (this.InTransaction) {
                this.InnerTransaction.Rollback();
                this.InnerTransaction = null;
            }

            if (this.InnerConnection != null) {
                this.InnerConnection.Close();
                this.InnerConnection = null;
            }
        }
    }
}
