using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Configuration.Mappings
{
    public class CountryMap : ClassMap<Data.Entities.Country>
    {
        public CountryMap()
        {
            Table("Country");
            Id(x => x.Id, "CountryId").GeneratedBy.Identity();
            Map(x => x.Name, "CountryName");
        }
    }
}
