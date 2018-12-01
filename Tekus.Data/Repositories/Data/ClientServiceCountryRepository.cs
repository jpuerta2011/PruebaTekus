using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Data.Entities;

namespace Tekus.Data.Repositories.Data
{
    public interface IClientServiceCountryRepository : Base.IBaseRepository<long, ClientServiceCountry>
    {

    }
    public class ClientServiceCountryRepository : Base.BaseRepository<long, ClientServiceCountry>
    {
        public ClientServiceCountryRepository(ISession session) : base(session) { }
    }
}
