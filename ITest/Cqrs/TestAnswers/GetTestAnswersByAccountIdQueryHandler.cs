using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Data.Entities.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.TestAnswers
{
    public class GetTestAnswersByAccountIdQueryHandler : BaseHandler,
        IRequestHandler<GetTestAnswersByAccountIdQuery, IEnumerable<TestAnswer>>
    {
        public GetTestAnswersByAccountIdQueryHandler(DatabaseContext db) : base(db)
        {
        }

        public async Task<IEnumerable<TestAnswer>> Handle(GetTestAnswersByAccountIdQuery query,
            CancellationToken cancellationToken)
            => await _db.TestAnswers.Where(ans => ans.AccountId == query.AccountId)
            .ToListAsync(cancellationToken);
    }
}