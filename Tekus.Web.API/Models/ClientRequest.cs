﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tekus.Web.API.Models
{
    public class ClientRequest
    {
        public long ClientId { get; set; }
        public string Name { get; set; }
        public string NIT { get; set; }
        public string Email { get; set; }
        public bool IsEnabled { get; set; }
    }
}