using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Accounts
{
    public class DeleteAccountCommandHandler : BaseHandler,
        IRequestHandler<DeleteAccountCommand>
    {
        private readonly IMediator _mediator;

        public DeleteAccountCommandHandler(DatabaseContext db, IMediator mediator) : base(db)
        {
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteAccountCommand command,
            CancellationToken cancellationToken)
        {
            var query = new GetAccountByIdQuery(command.AccountId);
            var accountToDelete = await _mediator.Send(query, cancellationToken);

            if (accountToDelete.Password != command.Password)
            {
                throw new AccountException("Incorrect password");
            }

            _db.Accounts.Remove(accountToDelete);
            return Unit.Value;
        }
    }
}