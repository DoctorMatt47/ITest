using System;

namespace ITest.Exceptions
{
    public class TestAnswerException : Exception
    {
        public TestAnswerException(string message) : base(message)
        {
        }
    }
}