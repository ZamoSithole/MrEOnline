using System;
using System.Collections.Generic;
using System.Text;

namespace MrE.Services.Abstractions
{ /// <summary>
  /// Blueprint for all the classes that will provide validation services.
  /// </summary>
  /// <typeparam name="T">The object type to perform the validation against.</typeparam>
    public interface IValidationService<T> where T : class
    {
        IValidationExceptionService ValidationExceptionService { get; set; }

        /// <summary>
        /// Provides general validation services to the target object.
        /// </summary>
        /// <param name="targetObject">Object to validate against.</param>
        void Validate(T targetObject);

        /// <summary>
        /// Provides validation services for deletes against the target object.
        /// </summary>
        /// <param name="targetObject">Object to validate against.</param>
        void ValidateDelete(T targetObject);

        /// <summary>
        /// Provivdes validation services for inserts against the target object.
        /// </summary>
        /// <param name="targetObject">Object to validate against.</param>
        void ValidateInsert(T targetObject);

        /// <summary>
        /// Provides validation services for inserts against a list of items of the same type.
        /// This is ideal for validating against duplicates in the data store e.t.c
        /// </summary>
        /// <param name="targetObject">The target object to validate.</param>
        /// <param name="items">Items from the data store.</param>
        void ValidateInsert(T targetObject, IEnumerable<T> items);

        /// <summary>
        /// Provides validation services for updates against the target object.
        /// </summary>
        /// <param name="targetObject">Object to validate against.</param>
        void ValidateUpdate(T targetObject);

        /// <summary>
        /// Provides validation services for updates against a list of items of the same type.
        /// This is ideal for validating against duplicates in the data store e.t.c
        /// </summary>
        /// <param name="targetObject">The target object to validate.</param>
        /// <param name="items">Items from the data store.</param>
        void ValidateUpdate(T targetObject, IEnumerable<T> items);
    }
}