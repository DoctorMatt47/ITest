using System;
using MediatR;

namespace ITest.Cqrs.Accounts
{
    public class DeleteAccountCommand : IRequest
    {
        public Guid AccountId { get; set; }
        public string Password { get; set; }
    }
}