using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Services.Validations {
    public class RentalValidationService : ValidationService<Rental> {
        public RentalValidationService(IValidationExceptionService validationExceptionService) : base(validationExceptionService) {
        }

        public override void ValidateInsert(Rental targetObject, IEnumerable<Rental> items) {
            ValidateInsert(targetObject);
        }

        public override void ValidateUpdate(Rental targetObject, IEnumerable<Rental> items) {
            ValidateUpdate(targetObject);
        }
    }
}
