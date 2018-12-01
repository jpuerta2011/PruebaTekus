using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Configuration.Mappings
{
    public class ClientServiceMap : ClassMap<Data.Entities.ClientService>
    {
        public ClientServiceMap()
        {
            Table("ClientService");
            Id(x => x.Id, "ClientServiceId").GeneratedBy.Identity();
            Map(x => x.HourValue, "HourValue");
            References(x => x.Client, "ClientId");
            References(x => x.Service, "ServiceId");
            HasManyToMany(x => x.Countries)
                .Table("ClientServiceCountry")
                .ParentKeyColumn("ClientServiceId")
                .ChildKeyColumn("CountryId");
        }
    }
}
