using System;
using ITest.Data.Entities.Accounts;
using MediatR;

namespace ITest.Cqrs.Accounts
{
    public class GetAccountByIdQuery : IRequest<Account>
    {
        public GetAccountByIdQuery(Guid accountId)
        {
            AccountId = accountId;
        }

        public Guid AccountId { get; set; }
    }
}