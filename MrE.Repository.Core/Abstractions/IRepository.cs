using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace MrE.Repository.Abstractions
{ /// <summary>
  /// Defines a blueprint for all the classes implementing Unit Of Work.
  /// </summary>
  /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IDisposable
        where T : class
    {
        /// <summary>
        /// Retrieves all the data from the underlying data provider.
        /// </summary>
        /// <returns>All entities in the underlying storage.</returns>
        IQueryable<T> Get();

        Task<IQueryable<T>> GetAsync();

        /// <summary>
        /// Retrieve an entity by primary key.
        /// </summary>
        /// <typeparam name="T">The data type to be retrieved</typeparam>
        /// <param name="key">The primary key of the entity to be retrived.</param>
        /// <returns>The entity owning the primary supplied.</returns>
        T GetByKey(int key);

        Task<T> GetByKeyAsync(int key);

        /// <summary>
        /// Marks the supplied item as ready for Insert to the underlying storage.        
        /// The item is only persisted once the <seealso cref="CommitChanges"/> is excecuted.
        /// </summary>
        /// <typeparam name="T">The data type of the entity to insert.</typeparam>
        /// <param name="item">The entity to be inserted.</param>
        /// <returns>The inserted entity.</returns>
        T Insert(T item);

        /// <summary>
        /// Marks the supplied item as modified and ready to be persisted as modified to the underlying store.
        /// The modifications are only persisted once the <seealso cref="CommitChanges"/> is executed.
        /// </summary>
        /// <param name="item">Item to be marked as updated.</param>
        /// <returns>The updated item.</returns>
        T Update(T item);

        /// <summary>
        /// Deletes the supplied item from the underlying storage.
        /// </summary>
        /// <param name="item">The item to be deleted.</param>
        /// <returns>The deleted item.</returns>
        T Delete(T item);

        DbContextTransaction StartTransaction(IsolationLevel isolationLevel);
        DbContextTransaction StartTransaction();

        /// <summary>
        /// Commit all the changes to the underlying store.
        /// </summary>
        /// <returns>Number of affected items.</returns>
        int CommitChanges();

        Task<int> CommitChangesAsync();

        DbEntityEntry<T> GetEntityEntry(T item);
    }
}
