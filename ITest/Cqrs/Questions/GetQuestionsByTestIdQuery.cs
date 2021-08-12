using System;
using System.Collections.Generic;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.Questions
{
    public class GetQuestionsByTestIdQuery : IRequest<IEnumerable<Question>>
    {
        public GetQuestionsByTestIdQuery(Guid testId) => TestId = testId;
        
        public Guid TestId { get; set; }
    }
}