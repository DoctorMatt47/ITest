using System;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.Questions
{
    public class AddQuestionCommand : IRequest<Question>
    {
        public string QuestionString { get; set; }
        public QuestionType QuestionType { get; set; }
        public Test Test { get; set; }
    }
}