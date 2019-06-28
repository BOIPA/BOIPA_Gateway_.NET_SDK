﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GlobalTurnkey.Models
{
    [Serializable]
    internal class RequireParamException : Exception
    {
        private List<String> missingFields;

        public RequireParamException()
        {
        }

        public RequireParamException(string message) : base(message)
        {
        }

        public RequireParamException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RequireParamException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RequireParamException(List<String> missingFields)
        {
            this.missingFields = missingFields;
            
        }

    }
}