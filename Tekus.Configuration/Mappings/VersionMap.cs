using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Configuration.Mappings
{
    public class VersionMap : ClassMap<Data.Entities.Version>
    {
        public VersionMap()
        {
            Table("\"Version\"");
            Id(x => x.Id, "\"idVersion\"").GeneratedBy.Identity();
            Map(x => x.Numero, "\"numero\"");
        }
    }
}
