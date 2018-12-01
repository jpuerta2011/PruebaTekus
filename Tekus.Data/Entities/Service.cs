using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Data.Entities
{
    public class Service : Base.TypeIdentifiable<long>
    {
        public virtual string Name { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual IList<ClientService> ClientServices { get; set; }
    }
}
