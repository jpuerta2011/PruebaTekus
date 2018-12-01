using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Configuration.Mappings
{
    public class ClientServiceCountryMap : ClassMap<Data.Entities.ClientServiceCountry>
    {
        public ClientServiceCountryMap()
        {
            Table("ClientServiceCountry");
            Id(x => x.Id, "ClientServiceCountryId").GeneratedBy.Identity();
            References(x => x.ClientService, "ClientServiceId");
            References(x => x.Country, "CountryId");
        }
    }
}
