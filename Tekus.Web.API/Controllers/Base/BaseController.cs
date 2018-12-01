using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Tekus.Domain.Services;

namespace Tekus.Web.API.Controllers.Base
{
    public class BaseController : ApiController
    {
        private IServiceFactory _serviceFactory;

        public BaseController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        protected IServiceFactory Services
        {
            get
            {
                return _serviceFactory;
            }
        }
    }
}