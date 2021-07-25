using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ITest.Models.Tests
{
    public class Choice : BaseEntity
    {
        [StringLength(100, MinimumLength=1)]
        public string? ChoiceString { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; } = default!;
        
        public List<TestAnswer> TestAnswers { get; set; } = new List<TestAnswer>();
    }
}
