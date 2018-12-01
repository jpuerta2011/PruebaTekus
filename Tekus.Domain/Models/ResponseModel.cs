using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Domain.Models
{
    /// <summary>
    /// Generic class for the service's response 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public List<string> Messages { get; set; }
        public T Data { get; set; }

        public ResponseModel()
        {
            Messages = new List<string>();
            Success = false;
        }
    }
}
