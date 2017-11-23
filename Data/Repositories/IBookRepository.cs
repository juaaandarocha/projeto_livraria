using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IBookRepository
    {
        void Insert(Book book);

        void Update(Book book);

        void Delete(int id);

        Book Select(int id);

        IList<Book> SelectAll();
    }
}
