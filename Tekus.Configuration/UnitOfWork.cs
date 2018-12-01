using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Common.Dependencies.UnitOfWork;

namespace Tekus.Configuration
{
    /// <summary>
    /// UnitOfWork class that implements the IUnitOfWork interface for NHibernate
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The session
        /// </summary>
        private readonly ISession session;

        /// <summary>
        /// The transaction
        /// </summary>
        private ITransaction transaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public UnitOfWork(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            this.session = session;
            this.transaction = session.BeginTransaction();
        }

        /// <summary>
        /// Commit the changes to the database and rollback if anything goes wrong.
        /// </summary>
        public void Commit()
        {
            try
            {
                this.transaction.Commit();
            }
            catch (Exception)
            {
                this.Rollback();
                throw;
            }
            finally
            {
                this.transaction.Dispose();
                this.transaction = this.session.BeginTransaction();
            }
        }

        /// <summary>
        /// Rollback the changes.
        /// </summary>
        public void Rollback()
        {
            this.transaction.Rollback();
            this.session.Clear();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.transaction.Dispose();
        }
    }
}
