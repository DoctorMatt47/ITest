using System;
using System.Collections.Generic;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.TestAnswers
{
    public class GetTestAnswersByAccountIdQuery : IRequest<IEnumerable<TestAnswer>>
    {
        public GetTestAnswersByAccountIdQuery(Guid accountId) => AccountId = accountId;

        public Guid AccountId { get; set; }
    }
}