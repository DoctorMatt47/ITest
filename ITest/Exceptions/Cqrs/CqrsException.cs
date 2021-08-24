using System;

namespace ITest.Exceptions.Cqrs
{
    public class CqrsException : Exception
    {
        public CqrsException()
        {
        }

        public CqrsException(string message)
            : base(message)
        {
        }

        public CqrsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}