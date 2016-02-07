using System;
using System.Runtime.Serialization;

namespace EStore.BL.Exceptions
{
    public class CommonException : Exception
    {
        public CommonException()
        {

        }

        public CommonException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public CommonException(string message)
            : base(message)
        {
        }

        protected CommonException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
