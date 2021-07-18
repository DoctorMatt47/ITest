using System;
using System.Collections.Generic;

namespace ITest.Models.Tests
{
    public class AnswerChoice : BaseEntity
    {
        public string AnswerString { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public List<UserQuestionAnswer> QuestionAnswers { get; set; } = new List<UserQuestionAnswer>();
    }
}
