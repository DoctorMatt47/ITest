using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Data.Entities.Accounts;
using ITest.Exceptions;
using MediatR;

namespace ITest.Cqrs.Accounts
{
    public class AddAccountCommand : IRequest<Account>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string City { get; set; }
    }

    public class AddAccountHandler : BaseHandler, IRequestHandler<AddAccountCommand, Account>
    {
        private readonly IMediator _mediator;

        public AddAccountHandler(DatabaseContext db, IMediator mediator) : base(db)
        {
            _mediator = mediator;
        }

        public async Task<Account> Handle(AddAccountCommand command, CancellationToken cancellationToken)
        {
            var query = new GetAccountByLoginOrMailQuery(command.Login, command.Mail);
            var accWithSameLoginOrMail = await _mediator.Send(query, cancellationToken);

            if (accWithSameLoginOrMail is not null)
            {
                if (accWithSameLoginOrMail.Login == command.Login)
                {
                    throw new AccountException("An account with this login already exists");
                }

                if (accWithSameLoginOrMail.Mail == command.Mail)
                {
                    throw new AccountException("An account with this mail already exists");
                }
            }

            var newAccount = new Account
            {
                Login = command.Login,
                Password = command.Password,
                Mail = command.Mail,
                City = command.City
            };
            await _db.Accounts.AddAsync(newAccount, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return newAccount;
        }
    }
}