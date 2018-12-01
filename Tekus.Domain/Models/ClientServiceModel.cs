using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Data.Entities;

namespace Tekus.Domain.Models
{
    public class ClientServiceModel : Base.TypeIdentifiableModel<long>
    {
        public long ClientId { get; set; }
        public long ServiceId { get; set; }
        public double HourValue { get; set; }
        public List<CountryModel> Countries;

        public static ClientServiceModel MakeOne(ClientService entity, bool withCountries)
        {
            var model = new ClientServiceModel
            {
                Id = entity.Id,
                ServiceId = entity.Service.Id,
                ClientId = entity.Client.Id,
                HourValue = entity.HourValue
            };

            if (withCountries)
            {
                model.Countries = CountryModel.MakeMany(entity.Countries.ToList());
            }

            return model;
        }

        public static List<ClientServiceModel> MakeMany(List<ClientService> sourceList, bool withCountries)
        {
            return sourceList.Select(x => MakeOne(x, withCountries)).ToList();
        }

        public void FillUp(ClientService entity)
        {
            entity.Id = Id;
            entity.HourValue = HourValue;
        }
    }
}
