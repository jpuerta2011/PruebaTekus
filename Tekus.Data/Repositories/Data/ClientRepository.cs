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
    }
    public class ClientRepository : Base.BaseRepository<long, Client>, IClientRepository
    {
        public ClientRepository(ISession session) : base(session) { }

        public async Task<List<Client>> GetEnabledClients()
        {
            var clients = await Session.QueryOver<Client>()
                .Where(x => x.IsEnabled)
                .ListAsync();
            return clients.ToList();
        }
    }
}
