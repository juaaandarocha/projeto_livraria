using Data;
using Data.Models;
using Data.Repositories;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSRest.Models;

namespace WSRest.Modules
{
    public class DefaultBookModule : NancyModule
    {
        private IDBContextFactory DBContextFactory { get; set; }

        private const string API_PREFIX = "/api";

        public DefaultBookModule(IDBContextFactory dbContextFactory)
        {
            this.DBContextFactory = dbContextFactory;

            Post[$"{API_PREFIX}/insere-livro"] = InsertBook;

            Put[$"{API_PREFIX}/atualiza-livro"] = UpdateBook;

            Delete[$"{API_PREFIX}/deleta-livro/{{Id}}"] = DeleteBook;

            Get[$"{API_PREFIX}/busca-livros"] = SelectAllBooks;

            Get[$"{API_PREFIX}/busca-livros/{{Id}}"] = SelectBook;
        }

        private dynamic SelectBook(dynamic ctx)
        {
            using(IDBContext dbContext = this.DBContextFactory.CreateNewContext())
            {
                try
                {
                    IBookRepository repo = dbContext.CreateBookRepository();

                    Book book = new Book();
                    book = repo.Select(ctx.Id);

                    return this.Negotiate
                               .WithModel(book)
                               .WithStatusCode(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return this.Negotiate
                               .WithModel(this.HandlingError(1, ex.Message))
                               .WithStatusCode(HttpStatusCode.InternalServerError);
                }
            }
        }

        private dynamic SelectAllBooks(dynamic ctx)
        {
            using(IDBContext dbContext = this.DBContextFactory.CreateNewContext())
            {
                try
                {
                    IBookRepository repo = dbContext.CreateBookRepository();

                    IList<Book> list = new List<Book>();
                    list = repo.SelectAll();

                    return this.Negotiate
                               .WithModel(list)
                               .WithStatusCode(HttpStatusCode.OK);
                }
                catch (Exception ex) {
                    return this.Negotiate
                               .WithModel(this.HandlingError(1, ex.Message))
                               .WithStatusCode(HttpStatusCode.InternalServerError);
                }
            }
        }

        private dynamic InsertBook(dynamic ctx)
        {
            Book req;
            try { req = this.Bind<Book>(); }
            catch (Exception ex) {
                return this.Negotiate
                           .WithModel(this.HandlingError(1, ex.Message))
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
                catch (Exception ex) {
                    return this.Negotiate
                           .WithModel(this.HandlingError(2, ex.Message))
                           .WithStatusCode(HttpStatusCode.InternalServerError);
                }
            }
        }

        private dynamic UpdateBook(dynamic ctx)
        {
            Book req;
            try { req = this.Bind<Book>(); }
            catch (Exception ex) {
                return this.Negotiate
                           .WithModel(this.HandlingError(1, ex.Message))
                           .WithStatusCode(HttpStatusCode.InternalServerError);
            }

            using (IDBContext dbContext = this.DBContextFactory.CreateNewTransactionContext())
            {
                try
                {
                    IBookRepository repo = dbContext.CreateBookRepository();
                    repo.Update(req);

                    dbContext.Commit();

                    return this.Negotiate
                               .WithStatusCode(HttpStatusCode.OK);
                }
                catch (Exception ex) {
                    return this.Negotiate
                           .WithModel(this.HandlingError(2, ex.Message))
                           .WithStatusCode(HttpStatusCode.InternalServerError);
                }
            }
        }

        private dynamic DeleteBook(dynamic ctx)
        {
            using (IDBContext dbContext = this.DBContextFactory.CreateNewTransactionContext())
            {
                try
                {
                    IBookRepository repo = dbContext.CreateBookRepository();
                    repo.Delete(ctx.Id);

                    dbContext.Commit();

                    return this.Negotiate
                               .WithStatusCode(HttpStatusCode.OK);
                }
                catch (Exception ex) {
                    return this.Negotiate
                           .WithModel(this.HandlingError(1, ex.Message))
                           .WithStatusCode(HttpStatusCode.InternalServerError);
                }                
            }
        }

        public ErrorModel HandlingError(int errorCode, string errorMessage)
        {
            return new ErrorModel(errorCode, errorMessage);
        }
    }
}