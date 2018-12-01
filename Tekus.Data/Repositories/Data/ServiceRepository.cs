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
        
    }

    public class ServiceRepository : Base.BaseRepository<long, Service>, IServiceRepository
    {
        public ServiceRepository(ISession session) : base(session) { }
    }
}
