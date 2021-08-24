using System;
using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Data.Entities.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Tests
{
    public class GetTestByIdQuery : IRequest<Test>
    {
        public GetTestByIdQuery(Guid testId) => TestId = testId;

        public Guid TestId { get; set; }
    }

    public class GetTestByIdQueryHandler : BaseHandler, IRequestHandler<GetTestByIdQuery, Test>
    {
        public GetTestByIdQueryHandler(DatabaseContext db) : base(db)
        {
        }

        public async Task<Test> Handle(GetTestByIdQuery query, CancellationToken cancellationToken) =>
            await _db.Tests.FirstOrDefaultAsync(test => test.Id == query.TestId, cancellationToken);
    }
}