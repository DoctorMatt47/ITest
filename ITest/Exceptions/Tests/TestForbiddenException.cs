namespace ITest.Exceptions.Tests
{
    public class TestForbiddenException : TestException
    {
        public TestForbiddenException(string msg) : base(msg)
        {
        }
    }
}