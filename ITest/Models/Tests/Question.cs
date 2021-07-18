using System;
using System.Collections.Generic;

namespace ITest.Models.Tests
{
    public class Question : BaseEntity
    {
        public string QuestionString { get; set; }
        public QuestionType QuestionType { get; set; }

        public Guid TestId { get; set; }
        public Test Test { get; set; }

        public List<AnswerChoice> AnswerChoices { get; set; } = new List<AnswerChoice>();
        public List<UserQuestionAnswer> QuestionAnswers { get; set; } = new List<UserQuestionAnswer>();
    }
}
