using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Configuration.Mappings
{
    public class ServiceMap : ClassMap<Data.Entities.Service>
    {
        public ServiceMap()
        {
            Table("Service");
            Id(x => x.Id, "ServiceId").GeneratedBy.Identity();
            Map(x => x.Name, "ServiceName");
            Map(x => x.IsEnabled, "IsEnabled");
            HasMany(x => x.ClientServices).KeyColumn("ServiceId").LazyLoad();
        }
    }
}
