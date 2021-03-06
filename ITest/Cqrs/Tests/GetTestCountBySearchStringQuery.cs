using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ITest.Configs;
using ITest.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Tests
{
    public class GetTestCountBySearchStringQuery : IRequest<int>
    {
        public GetTestCountBySearchStringQuery(string searchString) => SearchString = searchString;
        
        public string SearchString { get; set; }
    }
    
    public class GetTestCountBySearchStringQueryHandler : BaseHandler, 
        IRequestHandler<GetTestCountBySearchStringQuery, int>
    {
        public GetTestCountBySearchStringQueryHandler(DatabaseContext db) : base(db)
        {
        }

        public async Task<int> Handle(GetTestCountBySearchStringQuery query, CancellationToken cancellationToken)
            => await _db.Tests.Where(test => test.Title.Contains(query.SearchString))
                .Union(_db.Tests.Where(test => test.Description.Contains(query.SearchString)))
                .CountAsync(cancellationToken);
    }
    
    public class GetTestCountBySearchStringQueryValidator : AbstractValidator<GetTestCountBySearchStringQuery>
    {
        public GetTestCountBySearchStringQueryValidator()
        {
            RuleFor(q => q.SearchString).NotNull()
                .Matches(RegularExpression.TestTitle);
        }
    }
}