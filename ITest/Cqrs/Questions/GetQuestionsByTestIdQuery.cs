using System;
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
    public class GetQuestionsByTestIdQuery : IRequest<IEnumerable<Question>>
    {
        public GetQuestionsByTestIdQuery(Guid testId) => TestId = testId;
        
        public Guid TestId { get; set; }
    }
    
    public class GetQuestionsByTestIdQueryHandler : BaseHandler,
        IRequestHandler<GetQuestionsByTestIdQuery, IEnumerable<Question>>
    {
        public GetQuestionsByTestIdQueryHandler(DatabaseContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Question>> Handle(GetQuestionsByTestIdQuery query,
            CancellationToken cancellationToken) => await _db.Questions
            .Where(question => question.TestId == query.TestId).ToListAsync(cancellationToken);
    }
}