using System;
using System.Collections.Generic;

namespace ITest.Models.Tests
{
    public class UserQuestionAnswer : BaseEntity
    {
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public List<AnswerChoice> AnswerChoices { get; set; } = new List<AnswerChoice>();
    }
}