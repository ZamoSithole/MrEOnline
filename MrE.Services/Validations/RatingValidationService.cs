using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Services.Validations {
    public class RatingValidationService : ValidationService<Rating> {
        public RatingValidationService(IValidationExceptionService validationExceptionService) : base(validationExceptionService) {
        }

        public override void ValidateInsert(Rating targetObject, IEnumerable<Rating> items) {
            ValidateInsert(targetObject);
        }

        public override void ValidateUpdate(Rating targetObject, IEnumerable<Rating> items) {
            ValidateInsert(targetObject);
        }
    }
}
