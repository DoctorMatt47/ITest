using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.Internal;
using ITest.Cqrs.Choices;
using ITest.Cqrs.Questions;
using ITest.Data;
using ITest.Data.Entities.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Tests
{
    public class GetTestQuestionsChoicesByTestIdQueryHandler : BaseHandler,
        IRequestHandler<GetTestQuestionsChoicesByTestIdQuery, Test>
    {
        private readonly IMediator _mediator;

        public GetTestQuestionsChoicesByTestIdQueryHandler(DatabaseContext db, IMediator mediator) : base(db)
        {
            _mediator = mediator;
        }

        public async Task<Test> Handle(GetTestQuestionsChoicesByTestIdQuery query,
            CancellationToken cancellationToken)
        {
            var testToGet = 
                await _mediator.Send(new GetTestByIdQuery(query.TestId), cancellationToken);

            if (testToGet == null) return null;

            var questionsToGet =
                await _mediator.Send(new GetQuestionsByTestIdQuery(query.TestId), cancellationToken);
            
            var choicesToGetNestedTasks = questionsToGet.Select(async q =>
                await _mediator.Send(new GetChoicesByQuestionIdQuery(q.Id), cancellationToken));
            
            var choicesToGet = await Task.WhenAll(choicesToGetNestedTasks);

            return testToGet;
        }
    }
}