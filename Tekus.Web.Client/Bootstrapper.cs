﻿using NHibernate;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Tekus.Common.Dependencies.UnitOfWork;
using Tekus.Configuration;

namespace Tekus.Web.Client
{
    public class Bootstrapper
    {
        public static Container Build()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<ISessionFactory>(() => WebSessionManager.GetSessionFactory("TekusDB"), Lifestyle.Scoped);
            container.Register<ISession>(() => container.GetInstance<ISessionFactory>().OpenSession(), Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);

            // Register the services
            Domain.Configuration.RegisterServices(container);
            // Register the repostories
            Data.Repositories.Configuration.RegisterRepositories(container);
            // Call the extension method from the MVC integration package
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            return container;
        }
    }
}