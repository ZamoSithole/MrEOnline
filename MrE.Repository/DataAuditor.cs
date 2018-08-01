using MrE.Models;
using MrE.Models.Entities;
using MrE.Repository;
using MrE.Repository.Abstractions;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace Mr.Services {
    public class DataAuditor : IDataAuditor  {
        protected IUserProvider UserProvider { get; set; }

        public DataAuditor(IUserProvider userContext) {
            UserProvider = userContext;
        }

        public virtual void Audit(DataStoreContext context) {
            var changedEntities = context.ChangeTracker.Entries()
                .Where(
                        e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted
                       );

            foreach (var targetEntity in changedEntities.ToList()) {
                if (targetEntity.GetType() != typeof(AuditEntry)) {
                    switch (targetEntity.State) {
                        case EntityState.Added:
                            AuditInsert(context, targetEntity, DataOperation.Insert);
                            break;
                        case EntityState.Deleted:
                            AuditDelete(context, targetEntity, DataOperation.Delete);
                            break;
                        case EntityState.Modified:
                            AuditUpdate(context, targetEntity);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        protected virtual void AuditUpdate(DataStoreContext context, DbEntityEntry targetEntity) {
            var transactionId = Guid.NewGuid();
            foreach (var propertyName in targetEntity.OriginalValues.PropertyNames) {
                var property = targetEntity.Property(propertyName);
                var oldData = property.OriginalValue != null ? property.OriginalValue.ToString() : "null";
                var newData = property.CurrentValue != null ? property.CurrentValue.ToString() : "null";

                if (property.IsModified && !oldData.Equals(newData)) {
                    var auditTrail = InitEntity(targetEntity, DataOperation.Update, property, propertyName, transactionId);
                    context.Entry(auditTrail).State = EntityState.Added;
                }
            }
        }

        protected virtual void AuditInsert(DataStoreContext context, DbEntityEntry targetEntity, DataOperation operation) {
            var transactionId = Guid.NewGuid();
            foreach (var propertyName in targetEntity.CurrentValues.PropertyNames) {
                var property = targetEntity.Property(propertyName);
                var auditTrail = InitEntity(targetEntity, operation, property, propertyName, transactionId);
                context.Entry(auditTrail).State = EntityState.Added;
            }
        }

        protected virtual void AuditDelete(DataStoreContext context, DbEntityEntry targetEntity, DataOperation operation) {
            var transactionId = Guid.NewGuid();
            foreach (var prop in targetEntity.OriginalValues.PropertyNames) {
                var property = targetEntity.Property(prop);
                var auditTrail = InitEntity(targetEntity, operation, property, prop, transactionId);
                context.Entry(auditTrail).State = EntityState.Added;
            }
        }

        protected virtual AuditEntry InitEntity(DbEntityEntry targetEntity, DataOperation operation, DbPropertyEntry propertyEntry, string propertyName, Guid transactionId) {
            var auditTrail = new AuditEntry {
                UserName = UserProvider.GetUserName(),
                TransactionId = transactionId,
                TableName = GetTableName(targetEntity),
                Operation = operation.GetDescription(),
                NewData = propertyEntry.CurrentValue != null ? propertyEntry.CurrentValue.ToString() : "null",
                Column = propertyName,
                DateCreated = DateTime.Now
            };
            if (operation == DataOperation.Update)
                auditTrail.OldData = propertyEntry.OriginalValue != null ? propertyEntry.OriginalValue.ToString() : "null";
            return auditTrail;
        }

        protected virtual string GetTableName(DbEntityEntry dbEntry) {
            var tableAttr = dbEntry.Entity.GetType().GetTypeInfo().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
            var tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;
            return tableName;
        }
    }
}
