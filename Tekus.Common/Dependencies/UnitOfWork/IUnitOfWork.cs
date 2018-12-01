using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Common.Dependencies.UnitOfWork
{
    /// <summary>
    /// Interface for UnitOfWork
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit the changes to the database and rollback if anything goes wrong.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollback the changes.
        /// </summary>
        void Rollback();
    }
}
