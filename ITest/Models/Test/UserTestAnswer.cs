using System;
using System.Collections.Generic;

namespace ITest.Models.Test
{
    public class UserTestAnswer : BaseEntity
    {
        public User User { get; set; }

        public List<UserQuestionAnswer> QuestionAnswers { get; set; } = new List<UserQuestionAnswer>();

        public Guid TestId { get; set; }
        public Test Test { get; set; }
    }
}
