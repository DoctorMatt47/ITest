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
        public GetAccountByLoginAndPasswordQueryHandler(DatabaseContext db) : base(db)
        {
        }

        public async Task<Account> Handle(GetAccountByLoginAndPasswordQuery query,
            CancellationToken cancellationToken) =>
            await _db.Accounts.FirstOrDefaultAsync(
                acc => 
                    (acc.Login == query.Login || acc.Mail == query.Login) &&
                    acc.Password == query.Password,
                cancellationToken);
    }
}