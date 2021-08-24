using System;

namespace ITest.Exceptions.Cqrs
{
    public class CqrsNotFoundException : CqrsException
    {
        public CqrsNotFoundException()
        {
        }

        public CqrsNotFoundException(string message)
            : base(message)
        {
        }

        public CqrsNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}