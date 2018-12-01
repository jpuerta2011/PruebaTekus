using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Data.Repositories.Data;

namespace Tekus.Data.Repositories
{
    public class Configuration
    {
        // Register the hook between the interface and the repository implementation
        public static void RegisterRepositories(Container container)
        {
            container.Register<IClientRepository, ClientRepository>(Lifestyle.Scoped);
            container.Register<IClientServiceCountryRepository, ClientServiceCountryRepository>(Lifestyle.Scoped);
            container.Register<IClientServiceRepository, ClientServiceRepository>(Lifestyle.Scoped);
            container.Register<ICountryRepository, CountryRepository>(Lifestyle.Scoped);
            container.Register<IServiceRepository, ServiceRepository>(Lifestyle.Scoped);
        }
    }
}
