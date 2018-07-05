using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MrE.Services.Validations
{
   public class AddressValidationService : ValidationService<Address>
    {
        public AddressValidationService(IValidationExceptionService validationExceptionService) : base(validationExceptionService)
        {
        }

        public override void ValidateInsert(Address targetObject, IEnumerable<Address> items)
        {
            base.ValidateInsert(targetObject);
        }

        public override void ValidateUpdate(Address targetObject, IEnumerable<Address> items)
        {
            base.ValidateUpdate(targetObject);
        }
    }
}
