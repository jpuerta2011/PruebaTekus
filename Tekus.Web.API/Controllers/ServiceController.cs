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
    public class ServiceController : Base.BaseController
    {
        public ServiceController(IServiceFactory serviceFactory) :
            base(serviceFactory)
        { }

        // GET: api/Service
        public async Task<List<ServiceResponse>> Get()
        {
            var services = await Services.ServiceService.GetServices();
            return services.Select(x => new ServiceResponse
            {
                Id = x.Id,
                IsEnabled = x.IsEnable,
                Name = x.Name
            }).ToList();
        }

        // GET: api/Service/5
        public async Task<IHttpActionResult> Get(long id)
        {
            var service = await Services.ServiceService.GetService(id);

            if (service.Id == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(new ServiceResponse
                {
                    Id = service.Id,
                    IsEnabled = service.IsEnable,
                    Name = service.Name
                });
            }
        }

        // POST: api/Service
        public async Task<IHttpActionResult> Post(ServiceRequest service)
        {
            var response = await Services.ServiceService.Save(new Domain.Models.ServiceModel
            {
                Id = service.Id,
                IsEnable = true,
                Name = service.Name
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
