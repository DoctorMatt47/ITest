using System;
using MediatR;

namespace ITest.Cqrs.Tests
{
    public class DeleteTestCommand : IRequest<Unit>
    {
        public DeleteTestCommand(Guid testId, Guid accountId)
        {
            TestId = testId;
            AccountId = accountId;
        }

        public Guid TestId { get; }
        public Guid AccountId { get; }
    }
}