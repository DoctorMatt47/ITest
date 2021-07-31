using ITest.Data.Entities.Accounts;
using MediatR;

namespace ITest.Cqrs.Accounts
{
    public class AddAccountCommand : IRequest<Account>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string City { get; set; }
    }
}