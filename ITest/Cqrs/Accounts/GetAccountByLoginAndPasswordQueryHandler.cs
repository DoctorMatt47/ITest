using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Data.Entities.Accounts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Accounts
{
    public class GetAccountByLoginAndPasswordQueryHandler : BaseHandler,
        IRequestHandler<GetAccountByLoginAndPasswordQuery, Account>
    {
        private readonly IMediator _mediator;

        public GetAccountByLoginAndPasswordQueryHandler(DatabaseContext db, IMediator mediator) : base(db)
        {
            _mediator = mediator;
        }

        public async Task<Account> Handle(GetAccountByLoginAndPasswordQuery query,
            CancellationToken cancellationToken)
        {
            var userAccount = await _mediator.Send(new GetAccountByLoginQuery(query.Login), cancellationToken);
            return userAccount?.Password == query.Password ? userAccount : null;
        }
    }
}