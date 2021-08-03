using ITest.Data.Entities.Accounts;
using MediatR;

namespace ITest.Cqrs.Accounts
{
    public class GetAccountByLoginQuery : IRequest<Account>
    {
        public GetAccountByLoginQuery(string login) => Login = login;
        
        public string Login { get; set; }
    }
}