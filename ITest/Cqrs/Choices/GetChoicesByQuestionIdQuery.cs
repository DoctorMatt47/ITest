using System;
using System.Collections.Generic;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.Choices
{
    public class GetChoicesByQuestionIdQuery : IRequest<ICollection<Choice>>
    {
        public GetChoicesByQuestionIdQuery(Guid questionId) => QuestionId = questionId;

        public Guid QuestionId { get; set; }
    }
}