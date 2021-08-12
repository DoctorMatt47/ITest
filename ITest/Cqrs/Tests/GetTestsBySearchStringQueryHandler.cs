using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Data.Entities.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Tests
{
    public class GetTestsBySearchStringQueryHandler : BaseHandler,
        IRequestHandler<GetTestsBySearchStringQuery, IEnumerable<Test>>
    {
        private const int MaxElementsOnOnePage = 20;
        
        public GetTestsBySearchStringQueryHandler(DatabaseContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Test>> Handle(GetTestsBySearchStringQuery query,
            CancellationToken cancellationToken)
            => await _db.Tests.Where(test => test.Title.Contains(query.SearchString))
                .Union(_db.Tests.Where(test => test.Description.Contains(query.SearchString)))
                .OrderBy(test => test.VisitorsCount)
                .Skip(MaxElementsOnOnePage * query.PagesToSkip)
                .Take(MaxElementsOnOnePage)
                .ToListAsync(cancellationToken);
    }
}