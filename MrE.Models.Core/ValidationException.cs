using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MrE.Models
{
    [Serializable]
    public class ValidationException : Exception 
    {
        private const string DEFAULT_MESSAGE = "There was a problem processing your request.";
        public ValidationException() : base(DEFAULT_MESSAGE) {; }

        public ValidationException(string message) : base(message) {; }

        public ValidationException(string message, Exception innerException) : base(message, innerException) {; }

        public ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context) {; }

        public override string ToString()
        {
            var sBuilder = new StringBuilder();
            if (Data.Count < 1)
            {
                sBuilder.Append(string.Format("{0}", this.Message));
            }
            foreach (string errorMsg in Data.Values)
            {
                sBuilder.Append(string.Format("{0}", errorMsg));

            }
            return sBuilder.ToString();
        }

    }
}
