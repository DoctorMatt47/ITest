using System;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.Tests
{
    public class GetTestByIdQuery : IRequest<Test>
    {
        public GetTestByIdQuery(Guid testId) => TestId = testId;
        
        public Guid TestId { get; }
    }
}