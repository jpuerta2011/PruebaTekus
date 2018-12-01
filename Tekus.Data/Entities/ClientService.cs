using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Data.Entities
{
    public class ClientService : Base.TypeIdentifiable<long>
    {
        public virtual Client Client { get; set; }
        public virtual Service Service { get; set; }
        public virtual double HourValue { get; set; }
        public virtual IList<Country> Countries { get; set; }
    }
}
