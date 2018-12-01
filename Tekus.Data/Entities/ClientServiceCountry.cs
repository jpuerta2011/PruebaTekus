using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Data.Entities
{
    public class ClientServiceCountry : Base.TypeIdentifiable<long>
    {
        public virtual ClientService ClientService { get; set; }
        public virtual Country Country { get; set; }
    }
}
