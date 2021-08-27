using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using ITest.Cqrs.Tests;
using ITest.Data;
using ITest.Data.Dtos.Requests.TestAnswers;
using ITest.Data.Entities.Tests;
using ITest.Exceptions;
using ITest.Exceptions.Tests;
using MediatR;

namespace ITest.Cqrs.TestAnswers
{
    public class AddTestAnswersByTestIdCommand : IRequest<IEnumerable<TestAnswer>>
    {
        public IEnumerable<AddTestAnswersByTestQuestionsChoicesCommand.TestAnswerDto> TestAnswerDtos { get; set; }

        public Guid AccountId { get; set; }

        public Guid TestId { get; set; }
    }

    public class AddTestAnswersCommandHandler : BaseHandler,
        IRequestHandler<AddTestAnswersByTestIdCommand, IEnumerable<TestAnswer>>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddTestAnswersCommandHandler(DatabaseContext db, IMediator mediator, IMapper mapper) : base(db)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<TestAnswer>> Handle(AddTestAnswersByTestIdCommand command,
            CancellationToken cancellationToken)
        {
            var getTestQuery = new GetTestQuestionsChoicesByTestIdQuery(command.TestId);
            var testToAnswer = await _mediator.Send(getTestQuery, cancellationToken);

            var addByTestQuestionsChoicesCommand
                = new AddTestAnswersByTestQuestionsChoicesCommand
                {
                    TestAnswerDtos = command.TestAnswerDtos,
                    Test = testToAnswer,
                    AccountId = command.AccountId
                };

            return await _mediator.Send(addByTestQuestionsChoicesCommand, cancellationToken);
            
        }
    }

    public class AddTestAnswersByTestIdCommandValidator :
        AbstractValidator<AddTestAnswersByTestIdCommand>
    {
        public AddTestAnswersByTestIdCommandValidator()
        {
            RuleFor(c => c.TestAnswerDtos).NotNull();
        }
    }
}