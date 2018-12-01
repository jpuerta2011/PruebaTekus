using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Data.Entities;

namespace Tekus.Data.Repositories.Data
{
    public interface IClientServiceRepository : Base.IBaseRepository<long, ClientService>
    {
        Task<List<ClientService>> GetAllClientServiceByClientId(long clientId);
        Task<List<ClientService>> GetAllClientServiceByServiceId(long serviceId);
        Task<bool> ValidateIfClientServiceIsUnique(long clientId, long serviceId);
    }
    public class ClientServiceRepository : Base.BaseRepository<long, ClientService>, IClientServiceRepository
    {
        public ClientServiceRepository(ISession session) : base(session) { }

        public async Task<List<ClientService>> GetAllClientServiceByClientId(long clientId)
        {
            var clientServices = await Session.QueryOver<ClientService>()
                .Where(x => x.Client.Id == clientId)
                .ListAsync();
            return clientServices.ToList();
        }

        public async Task<List<ClientService>> GetAllClientServiceByServiceId(long serviceId)
        {
            var clientServices = await Session.QueryOver<ClientService>()
                .Where(x => x.Service.Id == serviceId)
                .ListAsync();
            return clientServices.ToList();
        }

        public async Task<bool> ValidateIfClientServiceIsUnique(long clientId, long serviceId)
        {
            var clientServices = await Session.QueryOver<ClientService>()
                .Where(x => x.Service.Id == serviceId && x.Client.Id == clientId)
                .ListAsync();
            return clientServices.Count <= 0;
        }
    }
}
