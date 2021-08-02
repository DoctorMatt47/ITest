using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Data.Entities.Accounts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Accounts
{
    public class GetAccountByIdQueryHandler : BaseHandler, IRequestHandler<GetAccountByIdQuery, Account>
    {
        public GetAccountByIdQueryHandler(DatabaseContext db) : base(db)
        {
        }

        public async Task<Account> Handle(GetAccountByIdQuery query, CancellationToken cancellationToken) =>
            await _db.Accounts.FirstOrDefaultAsync(
                acc => acc.Id == query.AccountId,
                cancellationToken);
    }
}