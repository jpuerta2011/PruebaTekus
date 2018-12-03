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

        public static ServiceModel MakeOne(Data.Entities.Service entity)
        {
            if (entity == null)
            {
                return new ServiceModel();
            }

            var model = new ServiceModel
            {
                Id = entity.Id,
                Name = entity.Name,
                IsEnable = entity.IsEnabled
            };

            return model;
        }

        public static List<ServiceModel> MakeMany(List<Data.Entities.Service> sourceList)
        {
            return sourceList.Select(x => MakeOne(x)).ToList();
        }

        public void FillUp(Data.Entities.Service entity)
        {
            entity.Id = Id;
            entity.Name = Name;
            entity.IsEnabled = IsEnable;
        }
    }
}
