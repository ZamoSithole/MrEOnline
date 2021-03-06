﻿using MrE.Models.Entities;
using MrE.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Services.Validations
{
    public class CastValidationService : ValidationService<Cast>
    {
        public CastValidationService(IValidationExceptionService validationExceptionService) 
            : base(validationExceptionService)
        {
        }
        public override void ValidateInsert(Cast targetObject, IEnumerable<Cast> items)
        {
            ValidateInsert(targetObject);
            //base.ValidateInsert(targetObject);

            //if (items != null && items.Any(elem => string.Compare(elem.FirstName, targetObject.FirstName) == 0))
            //    ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Unable to insert Title, there's already a Title with the same name.");
            //ValidationExceptionService.ThrowException();
        }

        public override void ValidateUpdate(Cast targetObject, IEnumerable<Cast> items)
        {
            base.ValidateUpdate(targetObject);
            if (items.Any(e => e.Id != targetObject.Id & (e.VideoId == targetObject.VideoId & e.FullName.CompareTo(targetObject.FullName) == 0)))
                ValidationExceptionService.Add("", "Unable to update Cast.");
            ValidationExceptionService.ThrowException();
        }
    }
}
