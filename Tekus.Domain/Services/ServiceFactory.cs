using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Domain.Services
{
    public interface IServiceFactory
    {
        T GetInstance<T>() where T : IService;

        IClientService ClientService { get; }
        IClientServiceService ClientServiceService { get; }
        ICountryService CountryService { get; }
        IServiceService ServiceService { get; }
    }

    public class ServiceFactory : IServiceFactory
    {
        private readonly Container _container;

        public ServiceFactory(Container container)
        {
            _container = container;
        }

        public IClientService ClientService
        {
            get
            {
                return GetInstance<IClientService>();
            }
        }

        public IClientServiceService ClientServiceService
        {
            get
            {
                return GetInstance<IClientServiceService>();
            }
        }

        public ICountryService CountryService
        {
            get
            {
                return GetInstance<ICountryService>();
            }
        }

        public IServiceService ServiceService
        {
            get
            {
                return GetInstance<IServiceService>();
            }
        }

        public T GetInstance<T>() where T : IService
        {
            return (T)_container.GetInstance(typeof(T));
        }
    }
}
