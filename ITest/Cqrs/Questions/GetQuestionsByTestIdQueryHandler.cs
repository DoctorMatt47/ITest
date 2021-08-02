using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Data.Entities.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Questions
{
    public class GetQuestionsByTestIdQueryHandler : BaseHandler,
        IRequestHandler<GetQuestionsByTestIdQuery, ICollection<Question>>
    {
        public GetQuestionsByTestIdQueryHandler(DatabaseContext db) : base(db)
        {
        }

        public async Task<ICollection<Question>> Handle(GetQuestionsByTestIdQuery query,
            CancellationToken cancellationToken) => await _db.Questions
            .Where(question => question.TestId == query.TestId).ToListAsync(cancellationToken);
    }
}