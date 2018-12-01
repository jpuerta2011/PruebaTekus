using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Configuration.Mappings
{
    public class ClientMap : ClassMap<Data.Entities.Client>
    {
        public ClientMap()
        {
            Table("Client");
            Id(x => x.Id, "ClientId").GeneratedBy.Identity();
            Map(x => x.Name, "ClientName");
            Map(x => x.NIT, "ClientNIT");
            Map(x => x.IsEnabled, "IsEnabled");
            HasMany(x => x.Services).KeyColumn("ClientId").LazyLoad();
        }
    }
}
