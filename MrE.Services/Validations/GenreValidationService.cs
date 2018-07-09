using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Services.Validations
{
    public class GenreValidationService : ValidationService<Genre>
    {
        public GenreValidationService(IValidationExceptionService validationExceptionService) : base(validationExceptionService)
        {
        }
        public override void ValidateInsert(Genre targetObject, IEnumerable<Genre> items)
        {
            base.ValidateInsert(targetObject);

            if (items != null && items.Any(elem => string.Compare(elem.Name, targetObject.Name) == 0))
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Unable to insert Genre, there's already a Genre with the same name.");
            ValidationExceptionService.ThrowException();
        }

        public override void ValidateUpdate(Genre targetObject, IEnumerable<Genre> items)
        {
            base.ValidateUpdate(targetObject);

            if (items != null && items.Any(elem => elem.Id != targetObject.Id && string.Compare(elem.Name, targetObject.Name) == 0))
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Unable to update Genre, there's already a Genre with the same name.");
            ValidationExceptionService.ThrowException();
        }
    }
}
