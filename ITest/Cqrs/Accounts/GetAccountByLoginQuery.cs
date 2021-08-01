using ITest.Data.Entities.Accounts;
using MediatR;

namespace ITest.Cqrs.Accounts
{
    public class GetAccountByLoginQuery : IRequest<Account>
    {
        public string Login { get; set; }
    }
}