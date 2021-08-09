using ITest.Data.Entities.Accounts;
using MediatR;

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
}