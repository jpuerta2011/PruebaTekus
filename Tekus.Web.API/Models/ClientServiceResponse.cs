﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tekus.Web.API.Models
{
    public class ClientServiceResponse
    {
        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public long ServiceId { get; set; }
        public string ServiceName { get; set; }
        public double HourValue { get; set; }
        public List<CountryResponse> Countries;
    }
}