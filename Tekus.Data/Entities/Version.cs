using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Data.Entities.Base;

namespace Tekus.Data.Entities
{
    public class Version : TypeIdentifiable<int>
    {
        public virtual int Numero { get; set; }
    }
}
