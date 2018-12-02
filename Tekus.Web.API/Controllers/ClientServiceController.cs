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
    public class ClientServiceController : Base.BaseController
    {
        public ClientServiceController(IServiceFactory serviceFactory) :
            base(serviceFactory)
        { }

        // GET: api/ClientService?clientId=1
        public async Task<List<ClientServiceResponse>> Get([FromUri]long clientId)
        {
            var clientServices = await Services.ClientServiceService.GetAllClientServiceByClientIdWithCountries(clientId);
            return clientServices.Select(x => new ClientServiceResponse
            {
                ClientId = x.ClientId,
                ClientName = x.ClientName,
                Countries = x.Countries.Select(c => new CountryResponse
                {
                    CountryId = c.Id,
                    Name = c.Name
                }).ToList(),
                HourValue = x.HourValue,
                ServiceId = x.ServiceId,
                ServiceName = x.ServiceName
            }).ToList();
        }

        //// GET: api/ClientService/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/ClientService
        public async Task<IHttpActionResult> Post(ClientServiceRequest request)
        {
            var response = await Services.ClientServiceService.Save(new Domain.Models.ClientServiceModel
            {
                ClientId = request.ClientId,
                HourValue = request.HourValue,
                ServiceId = request.ServiceId,
                Countries = request.Countries.Select(x => new Domain.Models.CountryModel
                {
                    Id = x.CountryId,
                    Name = x.Name
                }).ToList()
            });

            return Ok(new
            {
                response.Success,
                response.Data,
                Message = response.Messages.Count > 0 ? response.Messages[0] : string.Empty
            });
        }

        // DELETE: api/ClientService/5
        public async Task<IHttpActionResult> Delete(long id)
        {
            var response = await Services.ClientServiceService.Delete(id);
            return Ok(new
            {
                response.Success,
                response.Data,
                Message = response.Messages.Count > 0 ? response.Messages[0] : string.Empty
            });
        }
    }
}
