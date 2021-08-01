using System;
using ITest.Data.Dtos.Tests;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.Tests
{
    public class AddTestQuestionsChoicesCommand : IRequest<Test>
    {
        public TestDto TestDto { get; set; }
        public Guid AccountId { get; set; }
    }
}