using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Data.Entities;

namespace Tekus.Domain.Models
{
    public class ClientModel : Base.TypeIdentifiableModel<long>
    {
        public string Name { get; set; }
        public string NIT { get; set; }
        public string Email { get; set; }
        public bool IsEnable { get; set; }
        public List<ClientServiceModel> ClientServices { get; set; }

        public static ClientModel MakeOne(Client entity, bool withServices, bool withServicesCountries)
        {
            var model = new ClientModel
            {
                Id = entity.Id,
                Name = entity.Name,
                NIT = entity.NIT,
                Email = entity.Email,
                IsEnable = entity.IsEnabled
            };
            
            if (withServices)
            {
                model.ClientServices = ClientServiceModel.MakeMany(entity.Services.ToList(), withServicesCountries);
            }

            return model;
        }

        public static List<ClientModel> MakeMany(List<Client> sourceList, bool withServices, bool withServicesCountries)
        {
            return sourceList.Select(x => MakeOne(x, withServices, withServicesCountries)).ToList();
        }

        public void FillUp(Client entity)
        {
            entity.Name = Name;
            entity.NIT = NIT;
            entity.Email = Email;
            entity.IsEnabled = IsEnable;
        }
    }
}
