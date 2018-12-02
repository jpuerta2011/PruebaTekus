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
    public class ClientController : Base.BaseController
    {

        public ClientController(IServiceFactory serviceFactory) :
            base(serviceFactory)
        { }

        // GET: api/Client
        public async Task<List<ClientResponse>> Get()
        {
            var clients = await Services.ClientService.GetClients(false, false);
            return clients.Select(x => new ClientResponse
            {
                Id = x.Id,
                Email = x.Email,
                IsEnabled = x.IsEnable,
                Name = x.Name,
                NIT = x.NIT
            }).ToList();
        }

        // GET: api/Client/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var client = await Services.ClientService.GetClient(id);

            if (client.Id == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(new ClientResponse
                {
                    Id = client.Id,
                    Email = client.Email,
                    IsEnabled = client.IsEnable,
                    Name = client.Name,
                    NIT = client.NIT
                });
            }
        }

        // POST: api/Client
        public async Task<IHttpActionResult> Post(ClientRequest client)
        {
            var response = await Services.ClientService.Save(new Domain.Models.ClientModel
            {
                Id = client.ClientId,
                Email = client.Email,
                IsEnable = client.IsEnabled,
                Name = client.Name,
                NIT = client.NIT
            });

            return Ok(new
            {
                response.Success,
                response.Data,
                Message = response.Messages.Count > 0 ? response.Messages[0] : string.Empty
            });
        }
    }
}
