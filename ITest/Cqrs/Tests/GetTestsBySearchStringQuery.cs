using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ITest.Configs;
using ITest.Data;
using ITest.Data.Entities.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Tests
{
    public class GetTestsBySearchStringQuery : IRequest<IEnumerable<Test>>
    {
        public string SearchString { get; set; }
        public int PagesToSkip { get; set; }
    }
    
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
    
    public class GetTestsBySearchStringQueryValidator : AbstractValidator<GetTestsBySearchStringQuery>
    {
        public GetTestsBySearchStringQueryValidator()
        {
            RuleFor(q => q.SearchString).NotNull()
                .Matches(RegularExpression.TestTitle);

            RuleFor(q => q.PagesToSkip).NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}