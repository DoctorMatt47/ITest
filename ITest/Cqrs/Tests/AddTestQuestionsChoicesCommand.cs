using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ITest.Data;
using ITest.Data.Dtos.Tests;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.Tests
{
    public class AddTestQuestionsChoicesCommand : IRequest<Test>
    {
        public TestDto TestDto { get; set; }
        public Guid AccountId { get; set; }
    }

    public class AddTestQuestionsChoicesCommandHandler : BaseHandler,
        IRequestHandler<AddTestQuestionsChoicesCommand, Test>
    {
        private readonly IMapper _mapper;

        public AddTestQuestionsChoicesCommandHandler(DatabaseContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public async Task<Test> Handle(AddTestQuestionsChoicesCommand command, CancellationToken cancellationToken)
        {
            var newTest = _mapper.Map<Test>(command.TestDto);
            newTest.AccountId = command.AccountId;

            await _db.Tests.AddAsync(newTest, cancellationToken);
            foreach (var question in newTest.Questions)
            {
                await _db.Questions.AddAsync(question, cancellationToken);
                foreach (var choice in question.Choices)
                {
                    await _db.Choices.AddAsync(choice, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);

            return newTest;
        }
    }
}