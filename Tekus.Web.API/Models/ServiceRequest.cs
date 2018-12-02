using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tekus.Web.API.Models
{
    public class ServiceRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
    }
}