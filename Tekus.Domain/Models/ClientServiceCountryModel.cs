using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Data.Entities;

namespace Tekus.Domain.Models
{
    public class ClientServiceCountryModel : Base.TypeIdentifiableModel<long>
    {
        public long ClientServiceId { get; set; }
        public long CountryId { get; set; }

        public static ClientServiceCountryModel MakeOne(ClientServiceCountry entity)
        {
            var model = new ClientServiceCountryModel
            {
                Id = entity.Id,
                ClientServiceId = entity.ClientService.Id,
                CountryId = entity.Country.Id
            };

            return model;
        }

        public static List<ClientServiceCountryModel> MakeMany(List<ClientServiceCountry> sourceList)
        {
            return sourceList.Select(x => MakeOne(x)).ToList();
        }
    }
}
