using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Caches.SysCache;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Dialect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Configuration.Mappings;

namespace Tekus.Configuration
{
    /// <summary>
    /// Create a Session Factory and provide an NHibernate Session
    /// </summary>
    public static class WebSessionManager
    {
        /// <summary>
        /// Session Factory
        /// </summary>
        private static ISessionFactory sessionFactory;

        /// <summary>
        /// Gets the NHibernate session factory.
        /// </summary>
        /// <param name="connectionStringKey">The connection string key for PostgreSQL</param>
        /// <returns>An NHibernate session factory.</returns>
        public static ISessionFactory GetSessionFactory(string connectionStringKey)
        {
            if (sessionFactory == null)
            {
                sessionFactory = Fluently.Configure()
                    .CurrentSessionContext<WebSessionContext>()
                    .Database(MsSqlConfiguration.MsSql2012
                        .ConnectionString(c => c.FromConnectionStringWithKey(connectionStringKey))
                        // TODO: Comment out the next line when we don't want SQL in the Debug output
                        // It is helpful at the beginning but then it can slow down debugging
                        .ShowSql())
                        .Cache(c => c
                            // Enable Query Cache
                            .UseQueryCache()
                            // Enable Second Level Cache
                            .UseSecondLevelCache()
                            // Configure SysCacheProvider as the Second Level Cache
                            .ProviderClass<SysCacheProvider>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<VersionMap>())
                    .ExposeConfiguration(cfg =>
                    {
                        var timeout = TimeSpan.FromMinutes(5).TotalSeconds;
                        cfg.SetProperty(
                            NHibernate.Cfg.Environment.CommandTimeout, timeout.ToString());
                        cfg.SetProperty(
                            NHibernate.Cfg.Environment.Dialect,
                            typeof(MsSql2012Dialect).AssemblyQualifiedName);
                        // It doesn't auto-quote PostgreSQL --- We need to add double quotes as in Version
                        /*cfg.SetProperty(
                            Environment.Hbm2ddlKeyWords,
                            "auto-quote");*/
                        cfg.SetProperty(
                            NHibernate.Cfg.Environment.UseSqlComments,
                            "false");
                        // TODO: Just in case we need to use a Query Hint Interceptor in the future
                        // To add options to queries, we just need to create a subclass of EmptyInterceptor
                        // and name it QueryHintInterceptor.
                        // cfg.SetInterceptor(new QueryHintInterceptor());
                        // TODO: Check whether we will activate the option
                        // cfg.Cache(c => c.UseQueryCache = true);
                        cfg.SessionFactory().Caching.Through<SysCacheProvider>().UsingMinimalPuts();
                        // WARNING - FOR DEBUGGING ONLY!!!
                        // Log queries in console for debugging
                        cfg.DataBaseIntegration(d =>
                        {
                            d.LogFormattedSql = true;
                            d.LogSqlInConsole = true;
                        });
                    }).BuildSessionFactory();
            }

            return sessionFactory;
        }
    }
}
