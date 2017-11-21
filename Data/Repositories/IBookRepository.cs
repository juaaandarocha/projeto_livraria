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
        bool Insert(Book book);

        bool Update(Book book);

        bool Delete(Book book);

        Book Select(int id);

        List<Book> SelectAll();
    }
}
