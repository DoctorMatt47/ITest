using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ITest.Models.Accounts;

namespace ITest.Models.Tests
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
