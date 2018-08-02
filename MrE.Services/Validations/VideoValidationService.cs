using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Services.Validations
{
    public class VideoValidationService : ValidationService<Video>
    {
        public VideoValidationService(IValidationExceptionService validationExceptionService) : base(validationExceptionService)
        {
        }
        public override void ValidateInsert(Video targetObject, IEnumerable<Video> items)
        {
            base.ValidateInsert(targetObject);

            if (items != null && items.Any(elem => string.Compare(elem.Title, targetObject.Title) == 0))
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Unable to insert Video, there's already a Video with the same name.");
            ValidationExceptionService.ThrowException();
        }

        public override void ValidateUpdate(Video targetObject, IEnumerable<Video> items)
        {
            base.ValidateUpdate(targetObject);

            if (items != null && items.Any(elem => elem.Id != targetObject.Id && string.Compare(elem.Title, targetObject.Title) == 0))
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Unable to update Video, there's already a Video with the same name.");
            ValidationExceptionService.ThrowException();
        }

    }
}
