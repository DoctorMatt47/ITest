using System;

namespace ITest.Exceptions.Tests
{
    public class TestException : Exception
    {
        public TestException(string msg) : base(msg)
        {
        }
    }
}