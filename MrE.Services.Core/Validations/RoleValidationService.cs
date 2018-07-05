using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MrE.Services.Validations
{
   public class RoleValidationService : ValidationService<Role>
    {
        public RoleValidationService(IValidationExceptionService validationExceptionService)
           : base(validationExceptionService) {; }
        public override void ValidateInsert(Role targetObject, IEnumerable<Role> items)
        {
            base.ValidateInsert(targetObject);
            if (items != null & items.Any(e => string.Compare(e.Name, targetObject.Name, true) == 0))
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Cannot insert a duplicate Role.");
            ValidationExceptionService.ThrowException();
        }

        public override void ValidateUpdate(Role targetObject, IEnumerable<Role> items)
        {
            base.ValidateUpdate(targetObject);

            if (items != null && items.Any(e => e.RoleID != targetObject.RoleID & string.Compare(e.Name, targetObject.Name, true) == 0))
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Cannot update to a duplicate Role.");
            ValidationExceptionService.ThrowException();
        }
    }
}
