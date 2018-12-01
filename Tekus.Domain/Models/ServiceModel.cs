using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Data.Entities;

namespace Tekus.Domain.Models
{
    public class ServiceModel : Base.TypeIdentifiableModel<long>
    {
        public string Name { get; set; }
        public bool IsEnable { get; set; }

        public static ServiceModel MakeOne(Service entity, bool withCountries)
        {
            var model = new ServiceModel
            {
                Name = entity.Name,
                IsEnable = entity.IsEnabled
            };

            if (withCountries)
            {
                // TODO: I must add the code to retrive the services countries to the countries for the service
            }

            return model;
        }
    }
}
