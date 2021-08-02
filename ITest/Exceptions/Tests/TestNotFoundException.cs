namespace ITest.Exceptions.Tests
{
    public class TestNotFoundException : TestException
    {
        public TestNotFoundException(string msg) : base(msg)
        {
        }
    }
}