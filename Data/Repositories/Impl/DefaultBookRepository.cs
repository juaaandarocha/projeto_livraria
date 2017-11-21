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

        public bool Insert(Book book)
        {
            string query = @"
INSERT INTO livro
    (cdg_lvr, nom_lvr, nom_aut, dta_pub, nom_edi, nro_pag)
VALUES
    (:ID, :Name, :Author, :PublishDate, :PublishingCompany, :Pages)";

            int affectedRows = this.Transaction.Connection.Execute(query, book, this.Transaction);
            return affectedRows > 0;
        }

        public bool Update(Book book)
        {
            string query = @"
UPDATE livro
   SET nom_lvr = :Name,
       nom_aut = :Author,
       dta_pub = :PublishDate,
       nom_edi = :PublishingCompany,
       nro_pag = :Pages
 WHERE cdg_lvr = :ID";

            int affectedRows = this.Transaction.Connection.Execute(query, book, this.Transaction);
            return affectedRows > 0;
        }

        public bool Delete(Book book)
        {
            string query = "DELETE FROM livro WHERE cdg_lvr = :ID";

            int affectedRows = this.Transaction.Connection.Execute(query, book, this.Transaction);
            return affectedRows > 0;
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
 WHERE cdg_lvr = :id";

            Book book = this.Connection.Query<Book>(query, new { id = id }).FirstOrDefault();
            return book;
        }

        public List<Book> SelectAll()
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
