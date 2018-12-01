using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Data.Entities.Base;

namespace Tekus.Data.Repositories.Base
{
    /// <summary>
    /// Base BaseRepository interface
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IBaseRepository<TKey, TEntity>
    {
        /// <summary>
        /// Gets an entity by its id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns>An entity</returns>
        Task<TEntity> GetBy(TKey id);

        /// <summary>
        /// Loads the specified entity by its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>An entity</returns>
        Task<TEntity> Load(TKey id);

        /// <summary>
        /// Saves the current state of the specified entity.
        /// </summary>
        /// <param name="entity">The entity to persist.</param>
        /// <returns>An entity</returns>
        Task<TEntity> Save(TEntity entity);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Gets all instances of the entity type.
        /// </summary>
        /// <returns>An Enumarable with the list of entities.</returns>
        Task<IEnumerable<TEntity>> GetAll();
    }

    /// <summary>
    /// Base BaseRepository class with common methods for TypeIdentifiable
    /// </summary>
    /// <typeparam name="TIdType">The entity's Id type, e.g., int, long</typeparam>
    /// <typeparam name="T">The entity root for the repository.</typeparam>
    public abstract class BaseRepository<TIdType, T> : IBaseRepository<TIdType, T>
        where T : TypeIdentifiable<TIdType>
    {
        /// <summary>
        /// The session
        /// </summary>
        private readonly ISession session;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository&lt;TIdType, T&gt;"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        protected BaseRepository(ISession session)
        {
            this.session = session;
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        protected ISession Session
        {
            get { return this.session; }
        }

        /// <summary>
        /// Loads the specified entity by its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>An entity</returns>
        public virtual async Task<T> Load(TIdType id)
        {
            T entity;
            if (this.Session.Transaction != null && this.Session.Transaction.IsActive)
            {
                entity = await this.Session.LoadAsync<T>(id);
            }
            else
            {
                using (var transaction = this.Session.BeginTransaction())
                {
                    entity = await this.Session.LoadAsync<T>(id);
                    await transaction.CommitAsync();
                }
            }

            return entity;
        }

        /// <summary>
        /// Gets an entity by its id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns>An entity</returns>
        public virtual async Task<T> GetBy(TIdType id)
        {
            T entity;
            if (this.Session.Transaction != null && this.Session.Transaction.IsActive)
            {
                entity = await this.Session.GetAsync<T>(id);
            }
            else
            {
                using (var transaction = this.Session.BeginTransaction())
                {
                    entity = await this.Session.GetAsync<T>(id);
                    await transaction.CommitAsync();
                }
            }

            return entity;
        }

        /// <summary>
        /// Saves the current state of the specified entity.
        /// </summary>
        /// <param name="entity">The entity to persist.</param>
        /// <returns>An entity</returns>
        public virtual async Task<T> Save(T entity)
        {
            if (this.Session.Transaction != null && this.Session.Transaction.IsActive)
            {
                await this.Session.SaveAsync(entity);
            }
            else
            {
                using (var transaction = this.Session.BeginTransaction())
                {
                    try
                    {
                        await this.Session.SaveAsync(entity);
                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }

            return entity;
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public virtual async void Remove(T entity)
        {
            if (this.Session.Transaction != null && this.Session.Transaction.IsActive)
            {
                // TODO: Code IActivable
                //if (entity is IActivable)
                //{
                //    ((IActivable)entity).IsActive = false;
                //    this.Session.Save(entity);
                //}
                //else
                //{
                await this.Session.DeleteAsync(entity);
                //}
            }
            else
            {
                using (var transaction = this.Session.BeginTransaction())
                {
                    try
                    {
                        // TODO: Code IActivable
                        /*if (entity is IActivable)
                        {
                            ((IActivable)entity).IsActive = false;
                            this.Session.Save(entity);
                        }
                        else
                        {*/
                        await this.Session.DeleteAsync(entity);
                        //}

                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Gets all instances of the entity type.
        /// </summary>
        /// <returns>An Enumerable with the list of entities.</returns>
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            IEnumerable<T> results;
            if (this.Session.Transaction != null && this.Session.Transaction.IsActive)
            {
                var query = this.Session.QueryOver<T>();

                // TODO: Code IActivable
                /*if (typeof(T).GetInterface(typeof(IActivable).Name) != null)
                {
                    query.Where(x => ((IActivable)x).IsActive);
                }
                // TODO: Code ICacheable
                if (typeof(T).GetInterface(typeof(ICacheable).Name) != null)
                {
                    query.Cacheable();
                }
                */
                results = await query.ListAsync();
            }
            else
            {
                using (var transaction = this.Session.BeginTransaction())
                {
                    var query = this.Session.QueryOver<T>();

                    // TODO: Code IActivable
                    /*if (typeof(T).GetInterface(typeof(IActivable).Name) != null)
                    {
                        query.Where(x => ((IActivable)x).IsActive);
                    }
                    // TODO: Code ICacheable
                    if (typeof(T).GetInterface(typeof(ICacheable).Name) != null)
                    {
                        query.Cacheable();
                    }
                    */

                    results = await query.ListAsync();
                    await transaction.CommitAsync();
                }
            }

            return results;
        }

        /// <summary>
        /// Gets the IQueryOver for the .
        /// </summary>
        /// <returns>An IQueryOver for the repository's entity.</returns>
        protected virtual IQueryOver<T, T> GetQueryOver()
        {
            var query = this.Session.QueryOver<T>();

            // TODO: Code IActivable
            /*if (typeof(T).GetInterface(typeof(IActivable).Name) != null)
            {
                query.Where(x => ((IActivable)x).IsActive);
            }*/

            return query;
        }
    }
}
