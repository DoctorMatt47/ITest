using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ITest.Attributes;

namespace ITest.Models.Tests
{
    public class Question : BaseEntity
    {
        [NotNull]
        public string QuestionString { get; set; }
        public QuestionType QuestionType { get; set; }

        public Guid TestId { get; set; }
        public Test Test { get; set; }

        [CollectionCount(0, 50)]
        public List<AnswerChoice> AnswerChoices { get; set; } = new List<AnswerChoice>();
        public List<UserQuestionAnswer> QuestionAnswers { get; set; } = new List<UserQuestionAnswer>();
    }
}
