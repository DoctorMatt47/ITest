using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ITest.Models.Tests
{
    public class AnswerChoice : BaseEntity
    {
        [NotNull]
        [StringLength(100, MinimumLength=1)]
        public string AnswerString { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public List<UserQuestionAnswer> QuestionAnswers { get; set; } = new List<UserQuestionAnswer>();
    }
}
