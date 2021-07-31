using System;

namespace ITest.Exceptions
{
    public class AccountException : Exception
    {
        public AccountException(string msg) : base(msg)
        {
        }
    }
}