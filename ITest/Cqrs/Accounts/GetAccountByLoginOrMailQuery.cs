using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Data.Entities.Accounts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Accounts
{
    public class GetAccountByLoginOrMailQuery : IRequest<Account>
    {
        public GetAccountByLoginOrMailQuery(string login, string mail)
        {
            Login = login;
            Mail = mail;
        }

        public string Login { get; set; }

        public string Mail { get; set; }
    }

    public class GetAccountByLoginOrMailQueryHandler : BaseHandler,
        IRequestHandler<GetAccountByLoginOrMailQuery, Account>
    {
        public GetAccountByLoginOrMailQueryHandler(DatabaseContext db) : base(db)
        {
        }

        public async Task<Account> Handle(GetAccountByLoginOrMailQuery query, CancellationToken cancellationToken)
            => await _db.Accounts.FirstOrDefaultAsync(
                acc => acc.Login == query.Login || acc.Email == query.Mail,
                cancellationToken);
    }
}