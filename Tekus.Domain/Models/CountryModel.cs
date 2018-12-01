using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Data.Entities;

namespace Tekus.Domain.Models
{
    public class CountryModel : Base.TypeIdentifiableModel<int>
    {
        public string Name { get; set; }

        /// <summary>
        /// Convert a single country object to the countryModel object
        /// </summary>
        /// <param name="country">Country entity object</param>
        /// <returns></returns>
        public static CountryModel MakeOne(Country entity)
        {
            var model = new CountryModel
            {
                Id = entity.Id,
                Name = entity.Name
            };

            return model;
        }

        /// <summary>
        /// Convert a country list to country model list, using the MakeOne method for each element
        /// </summary>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public static List<CountryModel> MakeMany(List<Country> sourceList)
        {
            return sourceList.Select(x => MakeOne(x)).ToList();
        }
    }
}
