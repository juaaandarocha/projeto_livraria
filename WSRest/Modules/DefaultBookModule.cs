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

        public DefaultBookModule(IDBContextFactory dbContextFactory)
        {
            this.DBContextFactory = dbContextFactory;

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
                    IBookRepository repo = dbContext.CreateBookRepository();
                    repo.Insert(req);

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