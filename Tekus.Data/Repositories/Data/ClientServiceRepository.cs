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

    }
    public class ClientServiceRepository : Base.BaseRepository<long, ClientService>, IClientServiceRepository
    {
        public ClientServiceRepository(ISession session) : base(session) { }
    }
}
