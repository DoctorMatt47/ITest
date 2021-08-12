using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Data.Entities.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Choices
{
    public class GetChoicesByQuestionIdQueryHandler : BaseHandler,
        IRequestHandler<GetChoicesByQuestionIdQuery, IEnumerable<Choice>>
    {
        public GetChoicesByQuestionIdQueryHandler(DatabaseContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Choice>> Handle(GetChoicesByQuestionIdQuery query,
            CancellationToken cancellationToken) => await _db.Choices
            .Where(choice => choice.QuestionId == query.QuestionId)
            .ToListAsync(cancellationToken);
    }
}