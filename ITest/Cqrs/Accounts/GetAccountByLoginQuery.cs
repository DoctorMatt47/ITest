using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Data.Entities.Accounts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Accounts
{
    public class GetAccountByLoginQuery : IRequest<Account>
    {
        public GetAccountByLoginQuery(string login) => Login = login;

        public string Login { get; set; }
    }

    public class GetAccountByLoginQueryHandler : BaseHandler,
        IRequestHandler<GetAccountByLoginQuery, Account>
    {
        public GetAccountByLoginQueryHandler(DatabaseContext db) : base(db)
        {
        }

        public async Task<Account> Handle(GetAccountByLoginQuery query, CancellationToken cancellationToken) =>
            await _db.Accounts.FirstOrDefaultAsync(acc => acc.Login == query.Login, cancellationToken);
    }
}