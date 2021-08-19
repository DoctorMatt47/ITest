using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITest.Data.Entities.Tests
{
    public class Question : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string QuestionString { get; set; }
        public QuestionType QuestionType { get; set; }

        public Guid TestId { get; set; }
        public Test Test { get; set; }
        
        public List<Choice> Choices { get; set; } = new List<Choice>();
        public List<TestAnswer> TestAnswers { get; set; } = new List<TestAnswer>();
    }
}
