using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Data.Entities;

namespace Tekus.Data.Repositories.Data
{
    public interface IClientRepository : Base.IBaseRepository<long, Client>
    {
        Task<List<Client>> GetEnabledClients();
        Task<bool> ValidateIfClientNitIsUnique(string clientNsit, long clientId);
        Task<bool> ValidateIfClientNameIsUnique(string clientName, long clientId);
    }
    public class ClientRepository : Base.BaseRepository<long, Client>, IClientRepository
    {
        public ClientRepository(ISession session) : base(session) { }

        public async Task<bool> ValidateIfClientNameIsUnique(string clientName, long clientId)
        {
            var clients = await Session.QueryOver<Client>()
                .Where(x => x.Name == clientName && x.Id != clientId)
                .ListAsync();

            return clients.Count <= 0;
        }

        public async Task<bool> ValidateIfClientNitIsUnique(string clientNit, long clientId)
        {
            var clients = await Session.QueryOver<Client>()
                .Where(x => x.NIT == clientNit && x.Id != clientId)
                .ListAsync();

            return clients.Count <= 0;
        }

        public async Task<List<Client>> GetEnabledClients()
        {
            var clients = await Session.QueryOver<Client>()
                .Where(x => x.IsEnabled)
                .ListAsync();
            return clients.ToList();
        }
    }
}
