using System;

namespace ITest.Exceptions.Cqrs
{
    public class CqrsValidationException : CqrsException
    {
        public CqrsValidationException()
        {
        }

        public CqrsValidationException(string message)
            : base(message)
        {
        }

        public CqrsValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}