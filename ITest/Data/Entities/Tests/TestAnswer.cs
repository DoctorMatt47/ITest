using System;
using System.ComponentModel.DataAnnotations;
using ITest.Data.Entities.Accounts;

namespace ITest.Data.Entities.Tests
{
    public class TestAnswer : BaseEntity
    {
        [StringLength(100, MinimumLength=1)]
        public string Answer { get; set; }

        public Guid? ChoiceId { get; set; }
        public Choice Choice { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public Guid TestId { get; set; }
        public Test Test { get; set; }
        
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
    }
}
