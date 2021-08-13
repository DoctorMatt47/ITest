using System;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.Tests
{
    public class GetTestQuestionsChoicesByTestIdQuery : IRequest<Test>
    {
        public GetTestQuestionsChoicesByTestIdQuery(Guid testId) => TestId = testId;
        
        public Guid TestId { get; set; }
    }
}