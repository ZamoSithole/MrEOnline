using MrE.Models.Abstractions;
using MrE.Repository.Abstractions;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MrEOnline.Services.Abstractions
{
    public abstract class Service<T> : IService<T> where T : class {
        public IValidationService<T> ValidationService { get; set; }
        public IRepository<T> Repository { get; private set; }

        public Service(IRepository<T> repository, IValidationService<T> validationService) {
            ValidationService = validationService;
            Repository = repository;
        }

        public IQueryable<T> Get() {
            var dataQuery = Repository.Get();

            TransformQuery(ref dataQuery);

            return dataQuery;
        }

        public virtual async Task<IEnumerable<T>> GetAsync() => (await Repository.Get().ToListAsync());

        public virtual T GetByKey(int key) => Repository.GetByKey(key);

        public virtual async Task<T> GetByKeyAsync(int key) => (await Repository.GetByKeyAsync(key));

        public virtual T Insert(T item) {
            ValidationService.ValidateInsert(item, Get().AsNoTracking());

            if (item is ICreatable) (item as ICreatable).DateCreated = DateTime.Now;
            if (item is IDeletable) (item as IDeletable).IsDeleted = false;

            Repository.Insert(item);
            return Repository.CommitChanges() > 0 ? item : null;
        }

        public virtual T Update(T item) {
            ValidationService.ValidateUpdate(item, Get().AsNoTracking());

            if (item is IUpdatable) (item as IUpdatable).DateUpdated = DateTime.Now;

            item = CopyToCurrentValues(item);
            Repository.Update(item);

            return Repository.CommitChanges() > 0 ? item : null;
        }

        public virtual T Delete(T item) {
            ValidationService.ValidateDelete(item);

            if (item is IDeletable) {
                var auditable = item as IDeletable;
                auditable.IsDeleted = true;
                auditable.DateDeleted = DateTime.Now;

                item = CopyToCurrentValues(item);
                return Update(item);
            }
            return null;
        }

        public virtual T Recover(T item) {
            ValidationService.ValidateUpdate(item);

            if (item is IUpdatable) (item as IUpdatable).DateUpdated = DateTime.Now;

            if (item is IDeletable) {
                (item as IDeletable).IsDeleted = false;

                (item as IDeletable).DateDeleted = null;
            }
            
            return Update(item);
        }

        /// <summary>
        /// Copies property values from the supplied entity to the targeted entity. Used during <see cref="Update(T)"/> when audit tracking is performed. 
        /// </summary>
        /// <param name="entity">The source entity to copied.</param>
        /// <returns>The copied entity.</returns>
        protected T CopyToCurrentValues(T entity)
        {
            var entityEntry = Repository.GetEntityEntry(GetByKey((entity as IBaseEntity<int>).Id));
            entityEntry.CurrentValues.SetValues(entity);

            return entityEntry.Entity;
        }

        public virtual void Dispose() => Repository?.Dispose();

        protected virtual void TransformQuery(ref IQueryable<T> dataQuery) {
        }
    }
}