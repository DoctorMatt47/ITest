using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ITest.Attributes;

namespace ITest.Models.Tests
{
    public class Question : BaseEntity
    {
        [NotNull]
        public string QuestionString { get; set; } = string.Empty;
        public QuestionType QuestionType { get; set; }

        public Guid TestId { get; set; }
        public Test Test { get; set; } = default!;

        [CollectionCount(0, 50)]
        public List<Choice> Choices { get; set; } = new List<Choice>();
        public List<TestAnswer> TestAnswers { get; set; } = new List<TestAnswer>();
    }
}
