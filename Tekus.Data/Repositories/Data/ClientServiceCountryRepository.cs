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
        Task<List<ClientServiceCountry>> GetClientServiceCountriesByClientServiceId(long clientServiceId);
        Task<List<ClientServiceCountry>> GetClientServiceCountriesByCountryId(long countryId);
    }
    public class ClientServiceCountryRepository : Base.BaseRepository<long, ClientServiceCountry>, IClientServiceCountryRepository
    {
        public ClientServiceCountryRepository(ISession session) : base(session) { }

        public async Task<List<ClientServiceCountry>> GetClientServiceCountriesByClientServiceId(long clientServiceId)
        {
            var countries = await Session.QueryOver<ClientServiceCountry>()
                .Where(x => x.ClientService.Id == clientServiceId)
                .ListAsync();
            return countries.ToList();
        }

        public async Task<List<ClientServiceCountry>> GetClientServiceCountriesByCountryId(long countryId)
        {
            var countries = await Session.QueryOver<ClientServiceCountry>()
                .Where(x => x.Country.Id == countryId)
                .ListAsync();
            return countries.ToList();
        }
    }
}
