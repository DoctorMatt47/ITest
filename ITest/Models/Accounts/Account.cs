using System.Collections.Generic;
using ITest.Models.Tests;

namespace ITest.Models.Accounts
{
    public class Account : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string City { get; set; }
        public AccountRole Role { get; set; } = AccountRole.User;
        public bool IsConfirmed { get; set; } = false;

        public List<Test> Tests { get; set; }
        public List<UserTestAnswer> TestAnswers { get; set; }
    }
}
