using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Data.Entities;

namespace Tekus.Data.Repositories.Data
{
    public interface ICountryRepository : Base.IBaseRepository<int, Country>
    {
    }
    public class CountryRepository : Base.BaseRepository<int, Country>, ICountryRepository
    {
        public CountryRepository(ISession session) : base(session) { }
    }
}
