using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Common
{
    public class ApplicationConfiguration
    {

        #region Singleton implementation
        /// <summary>
        /// The instance
        /// </summary>
        private static ApplicationConfiguration instance = new ApplicationConfiguration();

        /// <summary>
        /// Prevents a default instance of the <see cref="ApplicationConfiguration"/> class from being created.
        /// </summary>
        private ApplicationConfiguration() { }

        /// <summary>
        /// Initializes the <see cref="ApplicationConfiguration"/> class.
        /// </summary>
        static ApplicationConfiguration() { }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static ApplicationConfiguration Instance
        {
            get { return instance; }
        }

        #endregion

        #region File-based configuration retrieval methods
        /// <summary>
        /// Gets a value indicating whether the debug flag is activated.
        /// </summary>
        public bool DebugFlagActivated
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["DebugFlagActivated"]); }
        }

        #endregion
    }
}
