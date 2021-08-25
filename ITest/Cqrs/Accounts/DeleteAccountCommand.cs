using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ITest.Configs;
using ITest.Data;
using ITest.Exceptions;
using MediatR;

namespace ITest.Cqrs.Accounts
{
    public class DeleteAccountCommand : IRequest
    {
        public Guid AccountId { get; set; }
        public string Password { get; set; }
    }

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
    
    public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
    {
        public DeleteAccountCommandValidator()
        {
            RuleFor(c => c.Password).NotNull()
                .Length(5, 100).Matches(RegularExpression.Password);
        }
    }
}