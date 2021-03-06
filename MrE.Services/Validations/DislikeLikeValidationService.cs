﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MrE.Services.Abstractions;
using MrE.Models.Entities;

namespace MrE.Services.Validations {
    public class DislikeLikeValidationService : ValidationService<DislikeLike> {
        public DislikeLikeValidationService(IValidationExceptionService validationExceptionService) 
            : base(validationExceptionService) {
        }

        public override void ValidateInsert(DislikeLike targetObject, IEnumerable<DislikeLike> items) {
            base.ValidateInsert(targetObject);

            //if (items != null && items.Any(elem => string.Compare(elem.UserId, targetObject.UserId) == 0))
            //    ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Unable to insert Genre, there's already a Genre with the same name.");
            //ValidationExceptionService.ThrowException();

            ValidateInsert(targetObject);
            //var Like = items.Any(elem => elem.IsLike);
            //var Dislike = items.Any(elem => elem.IsDislike);
            //if (Like == true) if (items != null && items.Any(elem => elem.videoId != targetObject.videoId && string.Compare(elem.UserId, targetObject.UserId) == 0))
            //        ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Video already Liked by user.");
            //ValidationExceptionService.ThrowException();

            //if (Dislike == true) if (items != null && items.Any(elem => elem.videoId != targetObject.videoId && string.Compare(elem.UserId, targetObject.UserId) == 0))
            //        ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Video already Liked by user.");
            //ValidationExceptionService.ThrowException();
            //ValidateInsert(targetObject);
        }

        public override void ValidateUpdate(DislikeLike targetObject, IEnumerable<DislikeLike> items) {
            base.ValidateUpdate(targetObject);

            if (items != null && items.Any(elem => elem.Id != targetObject.Id && string.Compare(elem.IsLike.ToString(), targetObject.IsLike.ToString()) == 0))
                ValidationExceptionService.Add(Guid.NewGuid().ToString(), "Unable to update Genre, there's already a Genre with the same name.");
            ValidationExceptionService.ThrowException();
        }
    }
}
