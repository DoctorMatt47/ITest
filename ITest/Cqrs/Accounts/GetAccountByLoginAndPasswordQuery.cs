using ITest.Data.Entities.Accounts;
using MediatR;

namespace ITest.Cqrs.Accounts
{
    public class GetAccountByLoginAndPasswordQuery : IRequest<Account>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}