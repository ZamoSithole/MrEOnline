using MrE.Models;
using MrE.Services.Abstractions;
using System;

namespace MrE.Services.Validations
{
    public class ValidationExceptionService : IValidationExceptionService {
        /// <summary>
        /// ValidationException global property of type ValidationException
        /// </summary>
        public ValidationException Validation { get; set; }
 
        public ValidationExceptionService(ValidationException exception) {
            Validation = exception;
        }

        /// <summary>
        /// TODO: get clarity on what this functions are doing
        /// </summary>
        /// <param name="message"></param>
        protected virtual void Add(string message) {
            var key = Guid.NewGuid();
            Validation.Data.Add(key, message);
        }

        public virtual void Add(string key, string message) {
            if (string.IsNullOrEmpty(key) || Validation.Data.Contains(key)) {
                Add(message);
            } else {
                Validation.Data.Add(key, message);
            }
        } 

        public virtual void ThrowException() {
            if (Validation.Data.Count > 0) {
                throw Validation;
            }
        }

        public void Clear() {
            if (Validation.Data.Count > 0)
                Validation.Data.Clear();
        }
    }
}
