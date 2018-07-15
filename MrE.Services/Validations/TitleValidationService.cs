using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Services.Validations
{
    public class TitleValidationService : ValidationService<Title>
    {
        public TitleValidationService(IValidationExceptionService validationExceptionService) :
            base(validationExceptionService)
        {; }

        public override void ValidateInsert(Title targetObject, IEnumerable<Title> items)
        {
            base.ValidateInsert(targetObject);

            if (items != null && items.Any(elem => string.Compare(elem.Name, targetObject.Name) == 0))
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Unable to insert Title, there's already a Title with the same name.");
            ValidationExceptionService.ThrowException();
        }

        public override void ValidateUpdate(Title targetObject, IEnumerable<Title> items)
        {
            base.ValidateUpdate(targetObject);

            if (items != null && items.Any(elem => elem.Id != targetObject.Id && string.Compare(elem.Name, targetObject.Name) == 0))
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Unable to update Title, there's already a Title with the same name.");
            ValidationExceptionService.ThrowException();
        }
    }
}
