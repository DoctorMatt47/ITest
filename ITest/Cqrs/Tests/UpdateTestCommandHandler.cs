using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ITest.Cqrs.Accounts;
using ITest.Cqrs.Choices;
using ITest.Cqrs.Questions;
using ITest.Data;
using ITest.Data.Dtos.Tests;
using ITest.Data.Entities.Tests;
using ITest.Exceptions.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Tests
{
    public class UpdateTestCommandHandler : BaseHandler,
        IRequestHandler<UpdateTestCommand, Test>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateTestCommandHandler(DatabaseContext db, IMapper mapper, IMediator mediator) : base(db)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Test> Handle(UpdateTestCommand command, CancellationToken cancellationToken)
        {
            var testToUpdate =
                await _mediator.Send(new GetTestByIdQuery(command.TestId), cancellationToken);

            if (testToUpdate is null)
            {
                throw new TestNotFoundException("Test with passed id does not exist");
            }

            if (testToUpdate.AccountId != command.AccountId)
            {
                throw new TestForbiddenException("Test with passed id does not belong to your account");
            }

            var questionsToUpdate =
                await _mediator.Send(new GetQuestionsByTestIdQuery(command.TestId), cancellationToken);

            if (questionsToUpdate.Count != command.TestDto.Questions.Count)
            {
                throw new TestException(
                    "Count of questions in passed object not equals to count of questions in stored object"
                );
            }

            for (var i = 0; i < testToUpdate.Questions.Count; i++)
            {
                var question = testToUpdate.Questions[i];
                var choicesToUpdate =
                    await _mediator.Send(new GetChoicesByQuestionIdQuery(question.Id), cancellationToken);

                if (choicesToUpdate is not null &&
                    choicesToUpdate.Count != command.TestDto.Questions[i].Choices.Count)
                {
                    throw new TestException(
                        "Count of choices in passed object not equals to count of choices in stored object"
                    );
                }
            }
            
            _mapper.Map(command.TestDto, testToUpdate);
            await _db.SaveChangesAsync(cancellationToken);
            return testToUpdate;
        }
    }
}