using System.Runtime.Serialization;

namespace Infrastructure
{
    [Serializable]
    public class InvalidJsonException : Exception
    {
        public InvalidJsonException()
        {
        }

        public InvalidJsonException(string? message) : base(message)
        {
        }

        public InvalidJsonException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidJsonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}