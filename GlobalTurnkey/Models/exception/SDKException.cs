using System;
using System.Runtime.Serialization;

namespace GlobalTurnkey.Models
{
    [Serializable]
    internal class SDKException : Exception
    {
        public SDKException()
        {
        }

        public SDKException(string message) : base(message)
        {
        }

        public SDKException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SDKException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}