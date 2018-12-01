using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Domain
{
    /// <summary>
    /// Handles the registration of the hook between the interfaces and the repositories implementations
    /// </summary>
    public class Configuration
    {
        // Register the hook between the interface and the repository implementation
        public static void RegisterServices(Container container)
        {
        }
    }
}
