using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Tekus.Domain.Services;
using Tekus.Web.API.Models;

namespace Tekus.Web.API.Controllers
{
    public class CountryController : Base.BaseController
    {

        public CountryController(IServiceFactory serviceFactory) :
            base(serviceFactory)
        { }

        // GET: api/Country
        public async Task<List<CountryResponse>> Get()
        {
            var countries = await Services.CountryService.GetCountries();
            return countries.Select(x => new CountryResponse
            {
                CountryId = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
