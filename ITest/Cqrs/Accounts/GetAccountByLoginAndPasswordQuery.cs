using ITest.Data.Entities.Accounts;
using MediatR;

namespace ITest.Cqrs.Accounts
{
    public class GetAccountByLoginAndPasswordQuery : IRequest<Account>
    {
        public GetAccountByLoginAndPasswordQuery(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; set; }
        public string Password { get; set; }
    }
}