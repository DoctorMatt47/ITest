using System;
using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Exceptions.Tests;
using MediatR;

namespace ITest.Cqrs.Tests
{
    public class DeleteTestCommand : IRequest
    {
        public Guid TestId { get; set; }
        public Guid AccountId { get; set; }
    }
    
    public class DeleteTestCommandHandler : BaseHandler, IRequestHandler<DeleteTestCommand>
    {
        private readonly IMediator _mediator;

        public DeleteTestCommandHandler(DatabaseContext db, IMediator mediator) : base(db)
        {
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteTestCommand command, CancellationToken cancellationToken)
        {
            var testToDelete = 
                await _mediator.Send(new GetTestByIdQuery(command.TestId), cancellationToken);
            if (testToDelete is null)
            {
                throw new TestNotFoundException("Test with passed id does not exist");
            }
            if (testToDelete.AccountId != command.AccountId)
            {
                throw new TestForbiddenException("Test with passed id does not belong to your account");
            }

            _db.Remove(testToDelete);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}