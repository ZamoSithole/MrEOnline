using System;
using System.Collections.Generic;
using MrE.Models.Abstractions;

namespace MrE.Services.Abstractions
{
    public abstract class ValidationService<T> : IValidationService<T>
    where T: class {
        public IValidationExceptionService ValidationExceptionService { get; set; }

        public ValidationService(IValidationExceptionService validationExceptionService)
        {
            ValidationExceptionService = validationExceptionService;
        }

        public virtual void Validate(T targetObject)
        {
            ValidateInsert(targetObject);
            ValidateUpdate(targetObject);
            ValidateDelete(targetObject);
        }

        public virtual void ValidateInsert(T targetObject)
        {
            if (targetObject == null)
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "The target object to be inserted cannot null.");
            ValidationExceptionService.ThrowException();
        }

        public abstract void ValidateInsert(T targetObject, IEnumerable<T> items);

        public virtual void ValidateUpdate(T targetObject)
        {
            if (targetObject == null)
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "The target object to be updated cannot be null.");
            ValidationExceptionService.ThrowException();
        }

        public abstract void ValidateUpdate(T targetObject, IEnumerable<T> items);

        public virtual void ValidateDelete(T targetObject)
        {
            if (targetObject == null)
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "The target object to be deleted cannot null.");
            ValidationExceptionService.ThrowException();

            if (!(targetObject is IDeletable))
                ValidationExceptionService.Add("", "Only soft deletes are allowed, please ensure your entity implements IAuditable interface.");
            ValidationExceptionService.ThrowException();
        }
    }
}
