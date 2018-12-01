using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Data.Entities
{
    public class Country : Base.TypeIdentifiable<int>
    {
        public virtual string Name { get; set; }
    }
}
