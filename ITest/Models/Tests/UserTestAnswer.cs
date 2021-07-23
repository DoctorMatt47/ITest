using System;
using System.Collections.Generic;
using ITest.Models.Accounts;

namespace ITest.Models.Tests
{
    public class UserTestAnswer : BaseEntity
    {
        public Account User { get; set; }

        public List<UserQuestionAnswer> QuestionAnswers { get; set; } = new List<UserQuestionAnswer>();

        public Guid TestId { get; set; }
        public Test Test { get; set; }
    }
}
