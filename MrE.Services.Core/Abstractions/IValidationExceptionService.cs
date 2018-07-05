using System;
using System.Collections.Generic;
using System.Text;

namespace MrE.Services.Abstractions
{
    public interface IValidationExceptionService
    {
        Models.ValidationException Validation { get; set; }

        void Add(string key, string message);

        void Clear();
        /// <summary>
        /// Throws a ValidationException if there were errors found.
        /// </summary>
        void ThrowException();
    }
}
