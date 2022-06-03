using System.Runtime.Serialization;

namespace Application.Exceptions
{
    [Serializable]
    public class InvalidSourceUrlException : Exception
    {
        public InvalidSourceUrlException()
        {
        }

        public InvalidSourceUrlException(string? message) : base(message)
        {
        }

        public InvalidSourceUrlException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidSourceUrlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}