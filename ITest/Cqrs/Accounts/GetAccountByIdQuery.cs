using System;
using ITest.Data.Entities.Accounts;
using MediatR;

namespace ITest.Cqrs.Accounts
{
    public class GetAccountByIdQuery : IRequest<Account>
    {
        public Guid AccountId { get; set; }
    }
}