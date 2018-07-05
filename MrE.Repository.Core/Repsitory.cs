using System;
using System.Linq;
using System.Data.Entity;
using MrE.Repository.Abstractions;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data;

namespace MrE.Repository
{
    public class Repsitory
    {
        public class Repository<T> : IDisposable, IRepository<T> where T : class
        {
            /// <summary>
            /// <see cref="IDataAuditor{T}"/> 
            /// </summary>
            public IDataAuditor DataAuditService { get; set; }

            /// <summary>
            /// Defaults the context to type Entities.
            /// </summary>
            public DataStoreContext DataStoreContext { get; set; }

            /// <summary>
            /// Constructs a new instance of using the injected parameters.
            /// </summary>
            /// <param name="validationManager">The validation manager to perform validations.</param>
            /// <param name="dataStoreBase">The DbContext to access the underlying storage.</param>
            /// <param name="userName"><see cref="UserName"/></param>
            /// <param name="dataAuditService"><see cref="IDataAuditor{T}"/> </param>
            public Repository(DataStoreContext dataStoreBase)
            {
                DataStoreContext = dataStoreBase;
            }

            /// <summary>
            /// Constructs a new instance of using the injected parameters.
            /// </summary>
            /// <param name="validationManager">The validation manager to perform validations.</param>
            /// <param name="dataStoreBase">The DbContext to access the underlying storage.</param>
            /// <param name="userName"><see cref="UserName"/></param>
            /// <param name="dataAuditService"><see cref="IDataAuditor{T}"/> </param>
            public Repository(DataStoreContext dataStoreBase, IDataAuditor dataAuditService)
                : this(dataStoreBase)
            {
                DataAuditService = dataAuditService;
            }

            /// <summary>
            /// Generic implementation of <see cref="IRepository{T}.Get()"/> 
            /// </summary>
            /// <returns></returns>
            public virtual IQueryable<T> Get()
            {
                return DataStoreContext.Set<T>().OfType<T>();
            }

            public async Task<IQueryable<T>> GetAsync()
            {
                return (await DataStoreContext.Set<T>().ToListAsync()).AsQueryable();
            }

            /// <summary>
            /// Generic implementation of <see cref="IRepository{T}.GetByKey(object)"/> 
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            public virtual T GetByKey(int key)
            {
                return (DataStoreContext.Set<T>().Find(key));
            }

            public virtual async Task<T> GetByKeyAsync(int key)
            {
                return (await DataStoreContext.Set<T>().FindAsync(key));
            }

            /// <summary>
            /// Generic implementation of <see cref="IRepository{T}.Insert(T)"/> 
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public virtual T Insert(T item)
            {
                DataStoreContext.Set<T>().Add(item);
                return (item);
            }

            /// <summary>
            /// Generic implementation of <see cref="IRepository{T}.Update(T)"/> 
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public virtual T Update(T item)
            {
                if (DataStoreContext.Entry<T>(item).State == EntityState.Detached)
                {
                    DataStoreContext.Set<T>().Attach(item);
                    DataStoreContext.Entry<T>(item).State = EntityState.Modified;
                }

                return (item);
            }

            /// <summary>
            /// Generic implementation of <see cref="IRepository{T}.Delete(T)"/> 
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public virtual T Delete(T item)
            {
                if (DataStoreContext.Entry<T>(item).State == EntityState.Detached)
                    DataStoreContext.Set<T>().Attach(item);

                var entry = DataStoreContext.Set<T>().Remove(item);
                return (entry);
            }

            public virtual DbEntityEntry<T> GetEntityEntry(T item)
            {
                return DataStoreContext.Entry(item);
            }

            public DbContextTransaction StartTransaction(IsolationLevel isolationLevel)
            {
                return DataStoreContext.Database.BeginTransaction(isolationLevel);
            }

            public DbContextTransaction StartTransaction()
            {
                return DataStoreContext.Database.BeginTransaction();
            }
            /// <summary>
            /// Implementation of <seealso cref="IRepository{T}.CommitChanges()"/> 
            /// </summary>
            /// <returns></returns>
            public virtual int CommitChanges()
            {
                if (DataAuditService != null) DataAuditService.Audit(DataStoreContext);

                return (DataStoreContext.SaveChanges());
            }

            public async Task<int> CommitChangesAsync()
            {
                return await DataStoreContext.SaveChangesAsync();
            }

            /// <summary>
            /// Implemenation of <seealso cref="IDisposable.Dispose"/> 
            /// </summary>
            public virtual void Dispose()
            {
                if (DataStoreContext != null)
                {
                    DataStoreContext.Dispose();
                    DataStoreContext = null;
                }
            }
        }

    }
}
