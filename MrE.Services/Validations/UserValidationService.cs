using MrE.Services.Abstractions;
using MrEOnline.Models;
using System;
using System.Collections.Generic;

namespace MrE.Services.Validations {
    public class UserValidationService : ValidationService<User> {
        public UserValidationService(IValidationExceptionService validationExceptionService) : base(validationExceptionService) {
        }

        public override void ValidateInsert(User targetObject, IEnumerable<User> items) {
            ValidateInsert(targetObject);
        }

        public override void ValidateUpdate(User targetObject, IEnumerable<User> items) {
            ValidateUpdate(targetObject);
        }
    }
}
