using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MrE.Services.Abstractions;
using MrE.Models.Entities;

namespace MrE.Services.Validations
{
    public class StatusValidationService : ValidationService<Status>
    {
        public StatusValidationService(IValidationExceptionService validationExceptionService) : base(validationExceptionService)
        {
        }
        public override void ValidateInsert(Status targetObject, IEnumerable<Status> items)
        {
            base.ValidateInsert(targetObject);

            if (items != null && items.Any(elem => string.Compare(elem.Name, targetObject.Name) == 0))
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Unable to insert Status, there's already a Status with the same name.");
            ValidationExceptionService.ThrowException();
        }

        public override void ValidateUpdate(Status targetObject, IEnumerable<Status> items)
        {
            base.ValidateUpdate(targetObject);

            if (items != null && items.Any(elem => elem.Id != targetObject.Id && string.Compare(elem.Name, targetObject.Name) == 0))
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Unable to update Status, there's already a Status with the same name.");
            ValidationExceptionService.ThrowException();
        }
    }
}
