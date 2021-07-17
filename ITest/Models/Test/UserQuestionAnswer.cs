using System;
using System.Collections.Generic;

namespace ITest.Models.Test
{
    public class UserQuestionAnswer
    {
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public List<AnswerChoice> AnswerChoices { get; set; } = new List<AnswerChoice>();
    }
}