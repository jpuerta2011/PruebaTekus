using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Data.Entities;

namespace Tekus.Data.Repositories.Data
{
    public interface IServiceRepository : Base.IBaseRepository<long, Service>
    {
        Task<List<Service>> GetEnabledServices();
        Task<bool> ValidateIfServiceNameIsUnique(string name);
    }

    public class ServiceRepository : Base.BaseRepository<long, Service>, IServiceRepository
    {
        public ServiceRepository(ISession session) : base(session) { }

        public async Task<List<Service>> GetEnabledServices()
        {
            var services = await Session.QueryOver<Service>()
                .Where(x => x.IsEnabled)
                .ListAsync();

            return services.ToList();
        }

        public async Task<bool> ValidateIfServiceNameIsUnique(string name)
        {
            var services = await Session.QueryOver<Service>()
                .Where(x => x.Name == name)
                .ListAsync();

            return services.Count <= 0;
        }
    }
}
