using Data;
using Data.Models;
using Data.Repositories;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSRest.Modules
{
    public class DefaultBookModule : NancyModule
    {
        private IDBContextFactory DBContextFactory { get; set; }

        private IBookRepository BookRepository { get; set; }

        public DefaultBookModule(
            IDBContextFactory dbContextFactory,
            IBookRepository bookRepository)
        {
            this.DBContextFactory = dbContextFactory;
            this.BookRepository = bookRepository;

            Post["/api/inserir-livro"] = InsertBook;
        }

        private dynamic InsertBook(dynamic ctx)
        {
            Book req;
            try { req = this.Bind<Book>(); }
            catch {
                return this.Negotiate
                           .WithStatusCode(HttpStatusCode.InternalServerError);
            }

            using(IDBContext dbContext = this.DBContextFactory.CreateNewTransactionContext())
            {
                try
                {
                    this.BookRepository.Insert(req);

                    dbContext.Commit();

                    return this.Negotiate
                               .WithStatusCode(HttpStatusCode.OK);
                }
                catch
                {
                    return this.Negotiate
                           .WithStatusCode(HttpStatusCode.InternalServerError);
                }
            }
        }
    }
}