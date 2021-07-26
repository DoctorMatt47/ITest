using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ITest.Models.Tests;

namespace ITest.Data.Entities.Tests
{
    public class Choice : BaseEntity
    {
        [MaxLength(255)]
        public string ChoiceString { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        
        public List<TestAnswer> TestAnswers { get; set; } = new List<TestAnswer>();
    }
}
