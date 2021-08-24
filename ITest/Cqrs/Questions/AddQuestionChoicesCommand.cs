using System.Collections.Generic;
using ITest.Cqrs.Choices;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.Questions
{
    public class AddQuestionChoicesCommand : IRequest<Question>
    {
        public string QuestionString { get; set; }

        public QuestionType QuestionType { get; set; }

        public List<AddChoiceCommand> Choices { get; set; } =
            new List<AddChoiceCommand>();
    }
}