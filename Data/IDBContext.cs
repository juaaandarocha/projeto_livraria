using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDBContext : IDisposable
    {
        bool InTransaction { get; }

        DbConnection InnerConnection { get; }

        DbTransaction InnerTransaction { get; }

        void Commit();
    }
}
