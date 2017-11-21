using Nancy.Bootstrappers.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Data.Impl;
using Data;

namespace WSRest
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private const string CONNECTION_STRING_NAME = "CONNECTION_STRING";

        public Bootstrapper() { }

        public static string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            existingContainer.Update(builder =>
            {
                string connectionString = GetConnectionString();

                builder.Register(ctx => new DefaultDBContextFactory(connectionString)).As<IDBContextFactory>();
            });

            base.ConfigureApplicationContainer(existingContainer);
        }
    }
}