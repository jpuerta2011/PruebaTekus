using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Data.Entities
{
    public class Client : Base.TypeIdentifiable<long>
    {
        public virtual string Name { get; set; }
        public virtual string NIT { get; set; }
        public virtual string Email { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual IList<ClientService> Services { get; set; }
    }
}
