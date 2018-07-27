using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Services.Abstractions
{
    public interface IService<T, K>: IDisposable where T: class
    {
        /// <summary>
        /// <see cref="Abstractions.IValidationService{T}"/>
        /// </summary>
        IValidationService<T> ValidationService { get; set; }

        /// <summary>
        /// Returns a query for fetching records of type T from the underlying data store.
        /// </summary>
        /// <returns>Query of type T</returns>
        IQueryable<T> Get();
        Task<IEnumerable<T>> GetAsync();

        /// <summary>
        /// Retrieves an item of type T having the supplied key.
        /// </summary>
        /// <param name="key">Unique key to retrieve by.</param>
        /// <returns>Item of type T having unique key.</returns>
        T GetByKey(K key);
        Task<T> GetByKeyAsync(K key);

        /// <summary>
        /// Inserts an item of type T to the underlying data store.
        /// </summary>
        /// <param name="item">Item to be inserted.</param>
        /// <returns>The inserted item.</returns>
        T Insert(T item);

        /// <summary>
        /// Updates the supplied item.
        /// </summary>
        /// <param name="item">Item to updated.</param>
        /// <returns>Updated item.</returns>
        T Update(T item);

        /// <summary>
        /// Deletes the supplied item.
        /// </summary>
        /// <param name="item">Item to be deleted.</param>
        /// <returns>Deleted item.</returns>
        T Delete(T item);

        /// <summary>
        /// Recovers a deleted item.
        /// </summary>
        /// <param name="item">The item to recover</param>
        /// <returns>The recovered item</returns>
        T Recover(T item);
    }
}
