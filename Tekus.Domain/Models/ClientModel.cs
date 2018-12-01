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
        //TODO: I need add the model for the services and countries services

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

            // TODO: I must add the processo to convert all services to the services model for the client model
            //

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
