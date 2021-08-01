using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Data.Entities;
using ITest.Data.Entities.Accounts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Accounts
{
    public class GetAccountByLoginQueryHandler : BaseHandler,
        IRequestHandler<GetAccountByLoginQuery, Account>
    {
        public GetAccountByLoginQueryHandler(DatabaseContext db) : base(db)
        {
        }
        
        public Task<Account> Handle(GetAccountByLoginQuery query, CancellationToken cancellationToken)
        {
            var accountToGet = _db.Accounts.FirstOrDefaultAsync(
                acc => acc.Login == query.Login,
                cancellationToken: cancellationToken);
            return accountToGet;
        }
    }
}