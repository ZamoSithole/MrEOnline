using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MrE.Models.Entities;
using MrE.Services.Abstractions;

namespace MrE.Services.Validations
{
    public class UserRoleValidationService : ValidationService<UserRoles>
    {
        public UserRoleValidationService(IValidationExceptionService validationExceptionService) : base(validationExceptionService)
        {
        }
        public override void ValidateInsert(UserRoles targetObject, IEnumerable<UserRoles> items)
        {
            base.ValidateInsert(targetObject);
            var workingSet = items.Where(e => !e.IsDeleted);
            if (workingSet.Any(e => e.RoleID == targetObject.RoleID & e.UserID == targetObject.UserID))
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Cannot insert a duplicate Role for the User.");
            ValidationExceptionService.ThrowException();
        }

        public override void ValidateUpdate(UserRoles targetObject, IEnumerable<UserRoles> items)
        {
            base.ValidateUpdate(targetObject);

            var workingSet = items.Where(e => !e.IsDeleted);

            if (workingSet.Any(e => targetObject.UserRoleID != e.UserRoleID & e.RoleID == targetObject.RoleID & e.UserID == targetObject.UserID))
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Cannot insert a duplicate Role for the User.");
            ValidationExceptionService.ThrowException();
        }
    }
}
