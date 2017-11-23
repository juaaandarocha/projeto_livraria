using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using System.Data.Common;

namespace Data.Repositories.Impl
{
    public class DefaultBookRepository : IBookRepository
    {
        protected DbConnection Connection { get; private set; }

        protected DbTransaction Transaction { get; private set; }

        public DefaultBookRepository(DbConnection connection, DbTransaction transaction = null)
        {
            this.Connection = connection;
            this.Transaction = transaction;
        }

        public void Insert(Book book)
        {
            string query = @"
INSERT INTO livro
    (cdg_lvr, nom_lvr, nom_aut, dta_pub, nom_edi, nro_pag)
VALUES
    (@ID, @Name, @Author, @PublishDate, @PublishingCompany, @Pages)";

            this.Transaction.Connection.Execute(query, book, this.Transaction);
        }

        public void Update(Book book)
        {
            string query = @"
UPDATE livro
   SET nom_lvr = @Name,
       nom_aut = @Author,
       dta_pub = @PublishDate,
       nom_edi = @PublishingCompany,
       nro_pag = @Pages
 WHERE cdg_lvr = @ID";

            this.Transaction.Connection.Execute(query, book, this.Transaction);
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM livro WHERE cdg_lvr = @id";

            this.Transaction.Connection.Execute(query, new { id = id }, this.Transaction);
        }

        public Book Select(int id)
        {
            string query = @"
SELECT cdg_lvr AS ID,
       nom_lvr AS Name,
       nom_aut AS Author,
       dta_pub AS PublishDate,
       nom_edi AS PublishingCompany,
       nro_pag AS Pages
  FROM livro
 WHERE cdg_lvr = @id";

            Book book = this.Connection.Query<Book>(query, new { id = id }).FirstOrDefault();
            return book;
        }

        public IList<Book> SelectAll()
        {
            string query = @"
SELECT cdg_lvr AS ID,
       nom_lvr AS Name,
       nom_aut AS Author,
       dta_pub AS PublishDate,
       nom_edi AS PublishingCompany,
       nro_pag AS Pages
  FROM livro";

            List<Book> bookList = this.Connection.Query<Book>(query).ToList();
            return bookList;
        }
    }
}
