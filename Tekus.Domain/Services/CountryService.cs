using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Data.Repositories.Data;
using Tekus.Domain.Models;

namespace Tekus.Domain.Services
{
    public interface ICountryService : IService
    {
        Task<List<CountryModel>> GetCountries();
        Task<CountryModel> GetCountry(int countryId);
    }
    public class CountryService : ICountryService
    {
        private ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<List<CountryModel>> GetCountries()
        {
            var countries = await _countryRepository.GetAll();
            return CountryModel.MakeMany(countries.ToList());
        }

        public async Task<CountryModel> GetCountry(int countryId)
        {
            var country = await _countryRepository.Load(countryId);
            return CountryModel.MakeOne(country);
        }
    }
}
